using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class GroupPatternConverter : MonoBehaviour, IGroupPatternConverter {

    public Transform[] groupPatterns;

    public List<IGroupPattern> Convert()
    {
        List<IGroupPattern> groupPatternList = new List<IGroupPattern>();
        foreach (var group in groupPatterns)
        {
            List<Coord[]> patternCoordsList = new List<Coord[]>();
            foreach (var pattern in group.Cast<Transform>().OrderBy(t => t.name))
            {
                patternCoordsList.Add(CreateGroupPattern(pattern));
            }

            IGroupPattern groupPattern = new GroupPattern(patternCoordsList);
            groupPatternList.Add(groupPattern);
        }

        return groupPatternList;
    }

    Coord[] CreateGroupPattern(Transform pattern)
    {
        Coord[] patternCoords = new Coord[pattern.childCount];
        int i = 0;
        foreach (var child in pattern.Cast<Transform>().OrderBy(t => t.name))
        {
            patternCoords[i] = new Coord(Mathf.RoundToInt(child.position.x), Mathf.RoundToInt(child.position.y));
            i++;
        }

        return patternCoords;
    }
}
