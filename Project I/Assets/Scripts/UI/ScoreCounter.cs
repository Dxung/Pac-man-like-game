using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class ScoreCounter : MonoBehaviour
{
    [SerializeField] private long _score = 0;
    [SerializeField] private long _highScore = 0;
    [SerializeField] private TextMeshProUGUI _scoreText;

    [SerializeField] private int _ghostKilled=0;


    private void Start()
    {
        UpdatePoint();
    }

    public void AddPointFromSmallPellet()
    {
        _score += 10;
        UpdatePoint();
    }

    public void AddPointFromPowerPellet()
    {
        ResetGhostKilledCounter();
        _score += 50;
        UpdatePoint();
    }

    public void AddPointFromGhost()
    {
        _score += 200 * Convert.ToInt64(Mathf.Pow(2, _ghostKilled));
        _ghostKilled += 1;
        UpdatePoint();
    }

    private void UpdatePoint()
    {
        if(_score > _highScore)
        {
            _highScore = _score;
        }
        _scoreText.text = "Score: " + _score.ToString();
    }

    private void ResetGhostKilledCounter()
    {
        _ghostKilled = 0;
    }

    public long GetHighScore()
    {
        return _highScore;
    }
}
