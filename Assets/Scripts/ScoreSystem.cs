using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public sealed class ScoreSystem : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreText;

    public int CurrentScore { get; private set; }

    private void Awake()
    {
        ResetScore();
    }

    public void AddScore(int amount)
    {
        CurrentScore += amount;
        UpdateUI();
    }

    public void ResetScore()
    {
        CurrentScore = 0;
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (_scoreText != null)
            _scoreText.text = CurrentScore.ToString();
    }
}