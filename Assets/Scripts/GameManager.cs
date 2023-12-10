using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : SingletonComponent<GameManager>
{
    public event EventHandler OnGameOver;
    public event EventHandler OnLevelCleared;

    [SerializeField] private Player player;

    private Health playerHealth;

    protected override void Awake()
    {
        base.Awake();
        playerHealth = player.GetComponent<Health>();
    }

    private void Start()
    {
        player.OnPlayerReachedEndOfLevel += Player_OnPlayerReachedEndOfLevel;
        playerHealth.OnHealthDropsZero += PlayerHealth_OnHealthDropsZero;
    }

    private void Player_OnPlayerReachedEndOfLevel(object sender, EventArgs e)
    {
        LevelCleared();
    }

    private void PlayerHealth_OnHealthDropsZero(object sender, System.EventArgs e)
    {
        GameOver();
    }

    private void LevelCleared()
    {
        Time.timeScale = 0;
        OnLevelCleared?.Invoke(this, EventArgs.Empty);
    }

    private void GameOver()
    {
        Time.timeScale = 0;
        OnGameOver?.Invoke(this, EventArgs.Empty);
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
