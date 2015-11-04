using UnityEngine;
using System.Collections;

public interface IControllableBlockModel
{
    Coord Location { get; }
    Coord LocationInTheGroup { get; set; }

    void SetLocation(Coord location);
}
