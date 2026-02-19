using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverMenuController : MonoBehaviour
{
    [SerializeField] GameObject gameOverMenu;
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
    }
}
