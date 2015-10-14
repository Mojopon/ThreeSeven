using UnityEngine;
using System.Collections;

public class BackgroundFactory : MonoBehaviour, IBackgroundFactory {

    public Transform backgroundPrefab;
    public GameSetting gameSetting;
    public bool drawBackgroundOnGizmos;

    void OnDrawGizmos()
    {
        //set Game Setting to the scene object to show rectangle where background will going to be created.
        if (gameSetting == null || drawBackgroundOnGizmos == false)
        {
            return;
        }

        Gizmos.color = Color.cyan;
        Gizmos.DrawCube(gameSetting.GetGridCenterPosition() + gameSetting.GridPosition, gameSetting.GetGridScale() + new Vector3(0, 0, -1));
    }

    public IBackground Create(ISetting setting)
    {
        Transform background = Instantiate(backgroundPrefab, Vector3.zero, Quaternion.identity) as Transform;

        if (setting.Parent != null)
        {
            background.SetParent(setting.Parent);
        }

        background.localPosition = setting.GetGridCenterPosition();
        background.localScale = setting.GetGridScale();

        return background.GetComponent<IBackground>();
    }
}
