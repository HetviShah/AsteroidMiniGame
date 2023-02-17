using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    [SerializeField] private Asteroids asteroidPrefab;

    private float trajectoryVariance = 15f;
    private float spawnRate = 2f;
    private float spawnDinstance = 15f;
    private int spawAmount = 1;

    private void Start()
    {
        SetupSettings();
        InvokeRepeating(nameof(SpawnAsteroids), this.spawnRate, this.spawnRate);
    }

    void SetupSettings()
    {
        trajectoryVariance = Player.playerInstance.gameSettigsInstance.trajectoryVariance;
        spawnRate = Player.playerInstance.gameSettigsInstance.spawnRate;
        spawnDinstance = Player.playerInstance.gameSettigsInstance.spawnDinstance;
        spawAmount = Player.playerInstance.gameSettigsInstance.spawAmount;
    }

    void SpawnAsteroids()
    {
        for (int i = 0; i < spawAmount; i++)
        {
            Vector3 spawnDirection = Random.insideUnitCircle.normalized * this.spawnDinstance;
            Vector3 spawnPoint = this.transform.position + spawnDirection;

            float variance = Random.Range(-this.trajectoryVariance, this.trajectoryVariance);
            Quaternion rotation = Quaternion.AngleAxis(variance, Vector3.forward);

            Asteroids asteroid = Instantiate(this.asteroidPrefab, spawnPoint, rotation);
            asteroid.size = Random.Range(asteroid.minSize, asteroid.maxSize);
            asteroid.SetSize();
            asteroid.SetTrajectory(rotation * -spawnDirection);
        }
    }
}

