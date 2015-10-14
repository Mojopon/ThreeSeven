using UnityEngine;
using System.Collections;

public interface IGame : IControllable, ISetting, IUpdatable
{
    ICameraManager CameraManager { get; set; }
    IBackgroundFactory BackgroundFactory { get; set; }
    IGridFactory GridFactory { get; set; }
    IGroupFactory GroupFactory { get; set; }

    void InitializeGrid();
}
