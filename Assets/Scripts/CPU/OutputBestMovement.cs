using UnityEngine;
using System.Collections.Generic;

public class OutputBestMovement {

    private IGridSimulator _simulator;
	public OutputBestMovement(IGridSimulator simulator)
    {
        _simulator = simulator;
    }

    public List<Direction> Output()
    {
        _simulator.CreateSimulatedGridOriginal();
        var defaultGroupPosition = _simulator.SimulatedGroup.Location;

        int bestScore = -777;
        int bestLocationX = -1;
        for(int i = 0; i < _simulator.SimulatedGrid.GetLength(0); i++)
        {
            _simulator.SetGroupLocation(new Coord(i, _simulator.SimulatedGroup.Location.Y));
            var simulatedScore = _simulator.GetScoreFromSimulation();
            if(simulatedScore > bestScore)
            {
                bestScore = simulatedScore;
                bestLocationX = i;
            }
        }

        List<Direction> movementsToGetDestination = new List<Direction>();

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
