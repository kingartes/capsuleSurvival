using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelClearedUI : MonoBehaviour
{
    private GameManager gameManager;
    private void Start()
    {
        gameManager = GameManager.Instance;
        gameManager.OnLevelCleared += GameManager_OnLevelCleared;
        gameObject.SetActive(false);
    }

    private void GameManager_OnLevelCleared(object sender, System.EventArgs e)
    {
        gameObject.SetActive(true);
    }
}
