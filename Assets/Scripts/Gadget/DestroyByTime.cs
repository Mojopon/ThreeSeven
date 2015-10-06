using UnityEngine;
using System.Collections;

public class DestroyByTime : MonoBehaviour {

    public float timeToDestroy = 2f;

	void Start () {
        Destroy(gameObject, timeToDestroy);
	}
}
