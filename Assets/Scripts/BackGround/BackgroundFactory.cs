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
        Gizmos.DrawCube(GetGridPosition(gameSetting) + gameSetting.PlayerGridPosition, GetGridScale(gameSetting) + new Vector3(0, 0, -1));
    }

    public IBackground Create(ISetting setting)
    {
        Transform background = Instantiate(backgroundPrefab, Vector3.zero, Quaternion.identity) as Transform;

        if (setting.Parent != null)
        {
            background.SetParent(setting.Parent);
        }

        background.localPosition = GetGridPosition(setting);
        background.localScale = GetGridScale(setting);

        return background.GetComponent<IBackground>();
    }

    Vector3 GetGridPosition(ISetting setting)
    {
        Vector3 gridPosition = new Vector3(0, 0, 1);
        //Vector3 gridPosition = setting.PlayerGridPosition;
        float adjustedX = (float)setting.GridWidth / 2;
        adjustedX -= 0.5f;

        float adjustedY = (float)setting.GridHeight / 2;
        adjustedY -= 0.5f;

        gridPosition = new Vector3(gridPosition.x + adjustedX, gridPosition.y + adjustedY, gridPosition.z);

        return gridPosition;
    }

    Vector3 GetGridScale(ISetting setting)
    {
        Vector3 gridScale = new Vector3(setting.GridWidth, setting.GridHeight, 1f);
        return gridScale;
    }
}
