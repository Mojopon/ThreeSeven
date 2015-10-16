using UnityEngine;
using System.Collections;

public class HighScoreManager : IHighScoreManager {

    private string key = "HIGH SCORE";
    private int _highScore = 0;

    public HighScoreManager()
    {
        _highScore = PlayerPrefs.GetInt(key, 0);
    }

    public void SetHighScore(int newHighScore)
    {
        if (newHighScore > GetHighScore())
        {
            _highScore = newHighScore;
            PlayerPrefs.SetInt(key, _highScore);
        }
    }

    public int GetHighScore()
    {
        return _highScore;
    }
}
