using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private float respawnTime = 3f;
    [SerializeField] private float respawnInvulnerability = 3f;
    [SerializeField] private int playerLives = 3;
    [SerializeField] private TMP_Text livesText;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private GameObject gameOverPanel;

    public int score = 0;

    public void PlayerDied()
    {
        playerLives--;
        livesText.text = "Lives - " + playerLives;
        if (this.playerLives == 0)
            GameOver();
        else
            Invoke(nameof(Respawn), this.respawnTime);

    }

    public void AsteroidDestroyed(Asteroids asteroid)
    {
        if (asteroid.size < 0.75f)
            this.score += 100;
        //else if (asteroid.size < 1.2f)
        //    this.score += 50;
        //else
        //    this.score += 25;
        scoreText.text = "Score - " + score;
    }

    private void Respawn()
    {
        this.player.gameObject.layer = LayerMask.NameToLayer("Ignore Collision");
        this.player.transform.position = Vector3.zero;
        this.player.gameObject.SetActive(true);

        Invoke(nameof(TurnOnCollision), this.respawnInvulnerability);
    }

    private void TurnOnCollision()
    {
        this.player.gameObject.layer = LayerMask.NameToLayer("Player");
    }

    private void GameOver()
    {
        this.player.gameObject.SetActive(false);
        gameOverPanel.transform.GetChild(0).GetComponent<TMP_Text>().text = "Score - " + score;
        gameOverPanel.SetActive(true);
        //this.playerLives = 0;
        //this.score = 0;
    }

    public void OnResetButton()
    {
        SceneManager.LoadScene(0);
    }
}
