using UnityEngine;
using System.Collections;

public interface ISimulatedGroup : IGroupModel
{
    void Simulate(IGroup groupToSimulate);
    void AddBlock(ISimulatedBlock block);
    ISimulatedBlock[] Children { get; }
}
