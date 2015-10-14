using UnityEngine;
using System.Collections;

public interface ICPUManager : IUpdatable
{
    void ChangeCPUMode(CPUMode mode);
}
