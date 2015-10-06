using UnityEngine;
using System.Collections;

public class SmoothMover : MonoBehaviour, IMovableTransform {

    public float moveSpeed = 0.5f;

	void Start () 
    {
        //_destination = transform.position;
	}
	
	void Update () 
    {
        transform.position = Vector3.Lerp(transform.position, _destination, moveSpeed);
	}

    public Vector3 _destination;
    public void MoveTo(Vector3 destination)
    {
        _destination = destination;
    }
}
