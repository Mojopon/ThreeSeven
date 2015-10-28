using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class GameLevelManager : IGameLevelManager
{
    ISetting _setting;
    IGrid _grid;

    IGameText _levelText;

    public GameLevelManager(ISetting setting, IGrid grid)
    {
        _setting = setting;
        _grid = grid;
        _levelText = _setting.GetGameText(GameTextType.LevelText);

        level = 0;
        UpdateLevelText();

        durationUntilNextDrop = 60 * groupDropGap;
        numberToNextLevel = levelUpRateBase;

        nextDrop = durationUntilNextDrop;
    }

    private float nextDrop;
    private float durationUntilNextDrop;
    private float groupDropGap = 2f;

    public void OnUpdate()
    {
        nextDrop -= 60 * Time.deltaTime;


        if (nextDrop < 0)
        {
            nextDrop = durationUntilNextDrop;
            _grid.OnArrowKeyInput(Direction.Down);
        }
    }

    private int level;
    void LevelUp()
    {
        if (deleteCount > numberToNextLevel)
        {
            level++;
            if (durationUntilNextDrop > 70)
            {
                durationUntilNextDrop -= 10;
            }
            else if (durationUntilNextDrop > 40)
            {
                durationUntilNextDrop -= 5;
            }
            else if (durationUntilNextDrop > 8)
            {
                durationUntilNextDrop -= 2;
            }

            numberToNextLevel += levelUpRateBase + (levelUpRate * level);

            UpdateLevelText();

            LevelUp();
        }
    }

    void UpdateLevelText()
    {
        if (_levelText == null)
        {
            return;
        }

        _levelText.UpdateText("Level\n" + level);
    }

    private int levelUpRate = 2;
    private int levelUpRateBase = 5;
    private int numberToNextLevel;
    private int deleteCount;
    
    public void OnBlockDelete(List<IBlock> blocksToDelete)
    {
        deleteCount += blocksToDelete.Count;

        LevelUp();
    }

    public void OnDeleteEvent(IGrid grid, List<IBlock> blocksToDelete, int chains)
    {
        OnBlockDelete(blocksToDelete);
    }
}
