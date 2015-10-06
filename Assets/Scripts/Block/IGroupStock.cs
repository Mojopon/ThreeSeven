using UnityEngine;
using System.Collections;

public interface IGroupStock : IGroupFactory
{
    Vector3[] StockPositions { get; set; }
    void Prepare(ISetting setting);
}
