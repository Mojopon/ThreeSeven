using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour, ICameraManager {

    public bool setFromUnityEditor = false;
    public Transform mainCamera;

    public void SetCameraPosition(Vector2 position)
    {
        if (mainCamera == null)
        {
            Debug.Log("Main camera is not set!");
            return;
        }

        if (setFromUnityEditor == false)
        {
            mainCamera.position = new Vector3(position.x, position.y, mainCamera.position.z);
        }
    }
}
