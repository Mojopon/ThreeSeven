using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using NSubstitute;

public class GridTestFixture : ThreeSevenTestFixsture {

    protected IGrid grid;
    protected IGridFactory gridFactory;

    protected IBlockPattern blockPattern;
    protected BlockType[] blockTypeMock;
    protected IGroupPattern groupPattern;
    protected List<IGroupPattern> groupPatternList;
    protected List<Coord[]> rotationMock;
    protected IGroup group;

    [SetUp]
    public void InitializeGrid()
    {
        groupPattern = Substitute.For<IGroupPattern>();
        rotationMock = new List<Coord[]>() {
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
        groupPattern.Patterns.Returns(rotationMock);

        groupPatternList = new List<IGroupPattern>();
        groupPatternList.Add(groupPattern);

        groupFactory = new GroupFactory(blockFactory, groupPatternList);

        gridFactory = new GridFactory(setting, groupFactory);

        // create grid
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
