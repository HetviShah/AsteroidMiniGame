using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroids : MonoBehaviour
{
    [SerializeField] private Sprite[] sprites;

    public float size;
    public float minSize;
    public float maxSize;
    public float speed;
    public float maxLifeTime;

    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _riigidBody;

    void SetupSettings()
    {
        //size = Player.playerInstance.gameSettigsInstance.asteroidSize;
        minSize = Player.playerInstance.gameSettigsInstance.asteroidMinSize;
        maxSize = Player.playerInstance.gameSettigsInstance.asteroidMaxSize;
        speed = Player.playerInstance.gameSettigsInstance.asteroidSpeed;
        maxLifeTime = Player.playerInstance.gameSettigsInstance.asteroidMaxLifeTime;
    }

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _riigidBody = GetComponent<Rigidbody2D>();

    }

    private void Start()
    {
        SetupSettings();
        _spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];
        this.transform.eulerAngles = new Vector3(0f, 0f, Random.value * 360.0f);

        this.transform.localScale = Vector3.one * this.size;

        _riigidBody.mass = this.size;
    }

    public void SetTrajectory(Vector2 direction)
    {
        _riigidBody.AddForce(direction * this.speed);
        Destroy(this.gameObject, this.maxLifeTime);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Bullet")
        {
            if((this.size * 0.5f) >= this.minSize)
             {
                CreateSplit();
                CreateSplit();
            }
            FindObjectOfType<GameManager>().AsteroidDestroyed(this);
            Destroy(this.gameObject);
        }
    }

    private void CreateSplit()
    {
        Vector2 position = this.transform.position;
        position += Random.insideUnitCircle * 0.5f;
        Asteroids half = Instantiate(this, position, this.transform.rotation);
        half.size = this.size * 0.5f;
        half.SetSize();
        half.SetTrajectory(Random.insideUnitCircle.normalized);
    }

    public void SetSize()
    {
        this.transform.localScale = Vector3.one * this.size;
    }
}
