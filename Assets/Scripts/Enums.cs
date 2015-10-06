using UnityEngine;
using System.Collections;

public enum SettingMode
{
    Test,
    Production,
}

public enum BlockType
{
    None,
    One = 0,
    Two = 1,
    Three = 2,
    Four = 3,
    Five = 4,
    Six = 5,
    Seven = 6,
    AvailableBlocks = 7,
}

public static class BlockTypeHelper
{
    public static BlockType GetRandom()
    {
        int number = Random.Range(0, (int)BlockType.AvailableBlocks);

        return (BlockType)number;
    }
}

public enum GridStates
{
    Paused = 0,
    OnControlGroup,
    OnFix,
    Dropping,
    Dropped,
    Deleting,
    Deleted,
    ReadyForNextGroup,
    GameOver,
}

public enum GridCommands
{
    AddGroup,
    DropBlocks,
    MoveBlocks,
    StartDeleting,
    DoDeleteEffect,
    DeleteImmediate,
}

public enum Direction
{
    None = -1,
    Up,
    Down,
    Left,
    Right,
}

public enum RotateDirection
{
    Clockwise,
    Counterclockwise,
}

public enum SoundName
{
    BumpOnTheGround,
    Delete,
}

public enum GameTextType
{
    LevelText,
    ScoreText,
    GameMessageCenter,
}