using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverMenuController : MonoBehaviour
{
    [SerializeField] GameObject gameOverMenu;
    [SerializeField] TMP_Text scoreText;
    [SerializeField] ScoreSystem scoreSystem;
    private void Start()
    {
        GameManager.Instance.OnGameOver += ShowGameOverMenu;
    }
    private void OnDisable()
    {
        GameManager.Instance.OnGameOver -= ShowGameOverMenu;
    }
    public void ShowGameOverMenu()
    {
        gameOverMenu.SetActive(true);
        scoreText.text = scoreSystem.CurrentScore + "";
    }
}
