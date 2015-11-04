using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface IScoreManager : IOnDeleteEventListener
{
    int Score { get; }

    void AttachScoreText(IGameText scoreTextObject);
    void OnDeleteBlocks(List<IBlock> blocks, int chains);
    int GetScore();
}
