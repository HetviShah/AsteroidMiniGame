using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float speed = 500.0f;
    private float maxLifeTime = 10.0f;
    private Rigidbody2D bulletRigidbody;

    private void Start()
    {
        SetupSettings();
    }

    void SetupSettings()
    {
        speed = Player.playerInstance.gameSettigsInstance.speed;
        maxLifeTime = Player.playerInstance.gameSettigsInstance.maxLifeTime;
    }

    private void Awake()
    {
        bulletRigidbody = GetComponent<Rigidbody2D>();
    }

    public void Project(Vector2 direction)
    {
        //Applyig force to bullet
        bulletRigidbody.AddForce(direction * this.speed);
        Destroy(this.gameObject, this.maxLifeTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(this.gameObject);
    }
}
