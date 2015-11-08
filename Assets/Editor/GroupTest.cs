using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using NSubstitute;

[TestFixture]
public class GroupTest : ThreeSevenTestFixsture
{
    IGroupPattern groupPattern;
    List<Coord[]> locationMock;

    IBlockPattern blockPattern;
    BlockType[] blockTypeMock;

    [SetUp]
    public void Init()
    {
        blockPattern = Substitute.For<IBlockPattern>();
        blockTypeMock = new BlockType[] 
        {
            BlockType.Two,
            BlockType.Four,
            BlockType.Seven,
        };
        blockPattern.Types.Returns(blockTypeMock);

        groupPattern = Substitute.For<IGroupPattern>();
        locationMock = new List<Coord[]>() {
            new Coord[] {
                new Coord(0, 0),
                new Coord(0, 1),
                new Coord(1, 0),
            },
            new Coord[] {
                new Coord(0, 0),
                new Coord(1, 0),
                new Coord(0, -1),
            },
            new Coord[] {
                new Coord(0, 0),
                new Coord(0, -1),
                new Coord(-1, 0),
            },
            new Coord[] {
                new Coord(0, 0),
                new Coord(-1, 0),
                new Coord(0, 1),
            },
        };
        groupPattern.Patterns.Returns(locationMock);
    }

    [Test]
    public void ShouldAddBlockToTheGroup()
    {
        IGroup group = new Group(setting);
        Assert.IsFalse(group.ChildrenLocation.ToList().Contains(new Coord(0, 0)));

        IBlock blockOne = Substitute.For<IBlock>();
        blockOne.Location.Returns(new Coord(0, 1));
        IBlock blockTwo = Substitute.For<IBlock>();
        blockTwo.Location.Returns(new Coord(1, 0));
        group.AddBlock(blockOne);
        group.AddBlock(blockTwo);
        List<Coord> coords = group.ChildrenLocation.ToList();
        Assert.IsTrue(coords.Contains(new Coord(0, 1)));
        Assert.IsTrue(coords.Contains(new Coord(1, 0)));
        for (int i = 0; i < group.Children.Length; i++)
        {
            Assert.AreEqual(group.Children[i].Location, group.ChildrenLocation[i]);
        }
    }

    [Test]
    public void ShouldCreateGroupFromGivenSettings()
    {
        IGroup group = groupFactory.Create(setting, blockPattern, groupPattern);

        for (int i = 0; i < group.Children.Length; i++)
        {
            Assert.AreEqual(blockPattern.Types[i], group.Children[i].BlockType);
        }
    }

    [Test]
    public void ShouldCreateGroupFromGroupPattern()
    {
        IGroup group = groupFactory.Create(setting, blockPattern, groupPattern);
        group.SetLocation(new Coord(0, 0));
        foreach (Coord coord in locationMock[0])
        {
            group.ChildrenLocation.Contains(coord);
        }

        int[] numbers = new int[] { 3, 1, 5 };
        IBlockPattern blockPatternTwo = BlockPattern.CreateFromNumbers(numbers);
        IGroup groupTwo = groupFactory.Create(setting, blockPatternTwo, groupPattern);

        for (int i = 0; i < numbers.Length; i++)
        {
            Assert.AreEqual((BlockType)numbers[i]-1, groupTwo.Children[i].BlockType);
        }
    }

    [Test]
    public void ShouldChangeChildrenLocationsWhenMove()
    {
        IGroup group = groupFactory.Create(setting, blockPattern, groupPattern);
        group.SetLocation(new Coord(0, 0));

        group.Move(Direction.Right);
        Assert.AreEqual(group.Location, Direction.Right.ToCoord());
        foreach (Coord coord in locationMock[0])
        {
            Assert.IsTrue(group.ChildrenLocation.Contains(coord + Direction.Right.ToCoord()));
        }
    }

    [Test]
    public void ShouldChangeChildrenOffsets()
    {
        IGroup group = new Group(setting);

        IBlock blockOne = Substitute.For<IBlock>();
        blockOne.Location.Returns(new Coord(0, 1));
        IBlock blockTwo = Substitute.For<IBlock>();
        blockTwo.Location.Returns(new Coord(1, 0));
        group.AddBlock(blockOne);
        group.AddBlock(blockTwo);

        group.Offset = new Vector3(2, 2);
        blockOne.Received().Offset = new Vector3(2, 2);
        blockTwo.Received().Offset = new Vector3(2, 2);
    }

    [Test]
    public void ShouldReturnCurrentRotatePatternNumber()
    {
        IGroup group = groupFactory.Create(setting, blockPattern, groupPattern);
        group.SetLocation(new Coord(0, 0));

        Assert.AreEqual(0, group.CurrentRotatePatternNumber);

        group.Rotate(RotateDirection.Clockwise);
        Assert.AreEqual(1, group.CurrentRotatePatternNumber);

        group.Rotate(RotateDirection.Clockwise);
        group.Rotate(RotateDirection.Clockwise);
        group.Rotate(RotateDirection.Clockwise);
        Assert.AreEqual(0, group.CurrentRotatePatternNumber);
    }

    [Test]
    public void ShouldRotateToNextPattern()
    {
        IGroup group = groupFactory.Create(setting, blockPattern, groupPattern);
        group.SetLocation(new Coord(0, 0));

        for(int i = 0; i < group.ChildrenLocation.Length; i++)
        {
            Assert.AreEqual(locationMock[0][i], group.ChildrenLocation[i]);
        }

        group.Rotate(RotateDirection.Clockwise);

        for (int i = 0; i < group.ChildrenLocation.Length; i++)
        {
            Assert.AreEqual(locationMock[1][i], group.ChildrenLocation[i]);
        }
    }

    [Test]
    public void BlocksShouldReceiveOnFixWhenFixGroup()
    {
        IGroup group = new Group(setting);
        IBlock blockOne = Substitute.For<IBlock>();
        blockOne.Location.Returns(new Coord(0, 1));
        IBlock blockTwo = Substitute.For<IBlock>();
        blockTwo.Location.Returns(new Coord(1, 0));
        group.AddBlock(blockOne);
        group.AddBlock(blockTwo);
        group.Fix();
        blockOne.Received().OnFix();
        blockTwo.Received().OnFix();
    }
}
