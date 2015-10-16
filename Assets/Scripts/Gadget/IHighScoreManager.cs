using UnityEngine;
using System.Collections;

public interface IHighScoreManager  
{
    void SetHighScore(int highScore);
    int GetHighScore();
}
