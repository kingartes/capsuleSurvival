using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.Instance;
        gameManager.OnGameOver += GameManger_OnGameOver;
        gameObject.SetActive(false);
    }

    private void GameManger_OnGameOver(object sender, System.EventArgs e)
    {
        gameObject.SetActive(true);
    }

}
