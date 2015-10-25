using UnityEngine;
using System.Collections;

public interface IGame : IControllable, ISetting, IUpdatable, IPauseEvent
{
    ICameraManager CameraManager { get; set; }
    IBackgroundFactory BackgroundFactory { get; set; }
    IGridFactory GridFactory { get; set; }
    IGroupFactory GroupFactory { get; set; }

    void InitializeGrid();
    void RegisterTheGridToTheGameServer(IGameServer gameServer);
}
