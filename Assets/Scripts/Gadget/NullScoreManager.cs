using UnityEngine;
using System.Collections.Generic;

public class NullScoreManager : IScoreManager {

    private static NullScoreManager instance;
    public static NullScoreManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new NullScoreManager();
            }

            return instance;
        }
    }

    private NullScoreManager() { }

    public int Score
    {
        get { return -1; }
    }

    public void AttachScoreText(IGameText scoreTextObject)
    {
       
    }

    public void OnDeleteBlocks(List<IBlock> blocks, int chains)
    {
        
    }


    public int GetScore()
    {
        return -1;
    }
}
