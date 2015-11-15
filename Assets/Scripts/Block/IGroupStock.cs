using UnityEngine;
using System.Collections;

public interface IGroupStock : IGroupFactory
{
    StockDisplayConfig[] StockDisplayConfig { get; set; }
    void Prepare(ISetting setting);
}
