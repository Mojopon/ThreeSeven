using UnityEngine;
using System.Collections;

public delegate void OnDeleteEndEventHandler(IGrid grid);
public interface IOnDeleteEnd
{
    event OnDeleteEndEventHandler OnDeleteEndEvent;
}
