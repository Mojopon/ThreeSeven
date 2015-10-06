using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour, ICameraManager {

    public Transform mainCamera;

    public void SetCameraPosition(Vector2 position)
    {
        if (mainCamera == null)
        {
            Debug.Log("Main camera is not set!");
            return;
        }

        mainCamera.position = new Vector3(position.x, position.y, mainCamera.position.z);
    }
}
