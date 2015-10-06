using UnityEngine;
using System.Collections;

public class GameTextManager : IGameTextManager {

    GameTextComponent[] _gameTextComponents;

    public GameTextManager(GameTextComponent[] gameTextComponents)
    {
        _gameTextComponents = gameTextComponents;
    }

    public IGameText GetGameText(GameTextType type)
    {
        foreach (GameTextComponent gameTextComponent in _gameTextComponents)
        {
            if (type == gameTextComponent.gameTextType)
            {
                return gameTextComponent.gameText;
            }
        }

        return NullGameText.Instance;
    }
}
