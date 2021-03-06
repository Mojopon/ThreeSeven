﻿using UnityEngine;
using System.Collections;

public class RandomMovementBehaviour : ICPUBehaviour 
{
    IGrid _grid;
    public RandomMovementBehaviour(IGrid grid)
    {
        _grid = grid;
    }

    private const float timeBetweenAction = 0.5f;
    private float nextAction = timeBetweenAction;

    public void DoAction()
    {
        nextAction += Time.deltaTime;
        if (nextAction >= timeBetweenAction)
        {

            int randomNum = Random.Range(0, 5);

            if (randomNum == 0)
            {
                _grid.OnArrowKeyInput(Direction.Right);
            }
            else if (randomNum == 1)
            {
                _grid.OnArrowKeyInput(Direction.Left);
            }
            else if (randomNum == 2)
            {
                _grid.OnArrowKeyInput(Direction.Up);
            }
            else if (randomNum == 3)
            {
                _grid.OnArrowKeyInput(Direction.Down);
            }
            else
            {
                _grid.OnArrowKeyInput(Direction.None);
            }

            nextAction = 0;
        }
    }
}
