using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GroupPattern : IGroupPattern {

    public GroupPattern(List<Coord[]> patterns)
    {
        Patterns = patterns;
    }

    public Coord[] ChildrenLocations { get; private set; }

    public List<Coord[]> Patterns { get; set; }
}
