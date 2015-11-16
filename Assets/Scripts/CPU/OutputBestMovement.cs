using UnityEngine;
using System.Collections.Generic;

public class OutputBestMovement {

    private IGridSimulator _simulator;
	public OutputBestMovement(IGridSimulator simulator)
    {
        _simulator = simulator;
    }

    private Direction directionToPut = Direction.Left;
    public List<Direction> Output()
    {
        _simulator.CreateSimulatedGridOriginal();
        var defaultGroupPosition = _simulator.SimulatedGroup.Location;

        int bestScore = -777;
        int bestLocationX = -1;
        int bestRotation = 0;
        for (int j = 0; j < _simulator.SimulatedGroup.RotationPatternNumber; j++)
        {
            for (int i = 0; i < _simulator.SimulatedGrid.GetLength(0); i++)
            {
                _simulator.SetGroupLocation(new Coord(i, _simulator.SimulatedGroup.Location.Y));
                var simulatedScore = _simulator.GetScoreFromSimulation();
                if (simulatedScore > bestScore)
                {
                    bestScore = simulatedScore;
                    bestLocationX = i;
                    bestRotation = _simulator.SimulatedGroup.CurrentRotatePatternNumber;
                }
            }
            _simulator.RotateGroup();
        }

        if(bestScore == 0)
        {
            if(directionToPut == Direction.Left)
            {
                bestLocationX = 0;
                directionToPut = Direction.Right;
            }
            else if(directionToPut == Direction.Right)
            {
                bestLocationX = _simulator.SimulatedGrid.GetLength(0) - 1;
                directionToPut = Direction.Left;
            }
        }

        List<Direction> movementsToGetDestination = new List<Direction>();
        
        while(bestRotation != 0)
        {
            movementsToGetDestination.Add(Direction.Up);
            bestRotation--;
        }

        while(defaultGroupPosition.X != bestLocationX)
        {
            if(bestLocationX > defaultGroupPosition.X)
            {
                movementsToGetDestination.Add(Direction.Right);
                defaultGroupPosition += Direction.Right.ToCoord();
            }
            else if (bestLocationX < defaultGroupPosition.X)
            {
                movementsToGetDestination.Add(Direction.Left);
                defaultGroupPosition += Direction.Left.ToCoord();
            }
        }
        return movementsToGetDestination;
    }

}
