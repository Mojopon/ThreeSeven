using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using NSubstitute;

public class GridTestFixture : ThreeSevenTestFixsture {

    protected IGrid grid;

    protected IBlockPattern blockPattern;
    protected BlockType[] blockTypeMock;
    protected IGroupPattern groupPattern;
    protected List<IGroupPattern> groupPatternList;
    protected List<Coord[]> locationMock;
    protected IGroup group;

    [SetUp]
    public void Init()
    {
        groupPattern = Substitute.For<IGroupPattern>();
        locationMock = new List<Coord[]>() {
            new Coord[] {
                new Coord(0, 0),
                new Coord(0, -1),
                new Coord(1, 0),
                new Coord(-1, 0),
            },
            new Coord[] {
                new Coord(0, 0),
                new Coord(-1, 0),
                new Coord(0, -1),
                new Coord(0, 1),
            },
            new Coord[] {
                new Coord(0, 0),
                new Coord(0, 1),
                new Coord(-1, 0),
                new Coord(1, 0),
            },
            new Coord[] {
                new Coord(0, 0),
                new Coord(1, 0),
                new Coord(0, 1),
                new Coord(0, -1),
            },
        };
        groupPattern.Patterns.Returns(locationMock);

        groupPatternList = new List<IGroupPattern>();
        groupPatternList.Add(groupPattern);

        groupFactory = new GroupFactory(blockFactory, groupPatternList);

        var gridFactory = new GridFactory(setting, groupFactory);
        grid = gridFactory.Create();
        grid.NewGame();

        blockPattern = Substitute.For<IBlockPattern>();
        blockTypeMock = new BlockType[] 
        {
            BlockType.One,
            BlockType.Six,
            BlockType.Three,
            BlockType.Five,
        };
        blockPattern.Types.Returns(blockTypeMock);

        group = groupFactory.Create(setting, blockPattern, groupPattern);
    }
}
