using UnityEngine;
using System.Collections.Generic;

public class ScoreManager : IScoreManager 
{
    public int Score { get; private set; }

    public ScoreManager()
    {
        Score = 0;
    }

    private IGameText _scoreText;

    public void OnDeleteBlocks(List<IBlock> blocks, int chains)
    {
        int scoreBase = 0;
        foreach (IBlock block in blocks)
        {
            scoreBase += block.Number;
        }

        int score = (scoreBase * 10) * chains;

        AddScore(score);
    }

    public void AttachScoreText(IGameText scoreTextObject)
    {
        _scoreText = scoreTextObject;
        UpdateScoreText();
    }

    public int GetScore()
    {
        return Score;
    }

    void AddScore(int score)
    {
        Score += score;
        UpdateScoreText();
    }

    void UpdateScoreText()
    {
        if (_scoreText == null) return;

        _scoreText.UpdateText("Score\n" + Score);
    }
}
