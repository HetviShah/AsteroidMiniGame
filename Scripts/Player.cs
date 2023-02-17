using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D objRigidbody;
    public Bullet bulletPrefab;
    public Bullet moonBulletPrefab;
    [SerializeField] private GameObject barrierObj;
    [SerializeField] private GameManager gameManager;
    private float thrustSpeed;
    private float turnSpeed;
    private bool isThrusting;
    private float turnDirection;

    public bool isBlasterOn = false;
    public bool isBarrierOn = false;

    public static Player playerInstance;
    public GameSettings gameSettigsInstance;

    private void Start()
    {
        SetupSettings();
    }

    void SetupSettings()
    {
        thrustSpeed = gameSettigsInstance.thrustSpeed;
        turnSpeed = gameSettigsInstance.turnSpeed;
    }

    private void Awake()
    {
        objRigidbody = GetComponent<Rigidbody2D>();
        if (playerInstance == null)
        {
            playerInstance = this;
        }
        else
            Destroy(this);
    }

    private void Update()
    {
        isThrusting = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            turnDirection = 1.0f;
        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.RightArrow))
            turnDirection = -1.0f;
        else
            turnDirection = 0;

        if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    private void FixedUpdate()
    {
        if(isThrusting)
        {
            objRigidbody.AddForce(-this.transform.up * this.thrustSpeed);
        }
        if(turnDirection != 0.0f)
        {
            objRigidbody.AddTorque(turnDirection * this.turnSpeed);
        }
    }

    private void Shoot()
    {
        Bullet bullet;
        //To spawn bullet
        if(isBlasterOn)
        {
            bullet = Instantiate(this.moonBulletPrefab, this.transform.position, this.transform.rotation);
        } 
        else
        {
            bullet = Instantiate(this.bulletPrefab, this.transform.position, this.transform.rotation);
        }
        bullet.Project(-this.transform.up);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Asteroid")
        {
            objRigidbody.velocity = Vector2.zero;
            objRigidbody.angularVelocity = 0.0f;

            this.gameObject.SetActive(false);

            gameManager.PlayerDied();
        }
    }

    public void BlasterActivate(float duration)
    {
        StartCoroutine(BlasterCoroutine(duration));
    }

    IEnumerator BlasterCoroutine(float duration)
    {
        isBlasterOn = true;
        yield return new WaitForSeconds(duration);
        isBlasterOn = false;
    }

    public void BarrierActivate(float duration)
    {
        StartCoroutine(BarrierCoroutine(duration));
    }

    IEnumerator BarrierCoroutine(float duration)
    {
        isBarrierOn = true;
        barrierObj.SetActive(true);
        this.gameObject.layer = LayerMask.NameToLayer("Ignore Collision");
        yield return new WaitForSeconds(duration);
        barrierObj.SetActive(false);
        this.gameObject.layer = LayerMask.NameToLayer("Player");
        isBarrierOn = false;

    }


}
