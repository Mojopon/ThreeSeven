using UnityEngine;
using System.Collections;

public interface IGameTextManager {

    IGameText GetGameText(GameTextType type);
}
