using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameSettings : ScriptableObject
{
    [Header("Player Control Fields")]
    public  float thrustSpeed = 1.0f;
    public  float turnSpeed = 1.0f;

    [Header("Bullet Control Fields")]
    public float speed = 500.0f;
    public float maxLifeTime = 10.0f;

    [Header("Asteroid Spawner Control Fields")]
    public float trajectoryVariance = 15f;
    public float spawnRate = 2f;
    public float spawnDinstance = 15f;
    public int spawAmount = 1;

    [Header("Asteroids Control Fields")]
    public float asteroidMinSize = 0.5f;
    public float asteroidMaxSize = 1.5f;
    public float asteroidSpeed = 50f;
    public float asteroidMaxLifeTime = 30f;

}
