using UnityEngine;
using System.Collections;

public class Background : MonoBehaviour, IBackground {


    public Vector3 Center
    {
        get { return transform.position; }
    }
}
