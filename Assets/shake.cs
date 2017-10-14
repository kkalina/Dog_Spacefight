using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shake : MonoBehaviour {

    //public bool shaking = false;
    public int shakeSources = 0;
    public GameObject anchor;
    public float shakeMultiplier;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (shakeSources > 0)
        {
            Vector3 newPos = anchor.transform.position;
            newPos.x += Random.value * shakeMultiplier;
            newPos.y += Random.value * shakeMultiplier;
            newPos.z += Random.value * shakeMultiplier;
            this.transform.position = newPos;
        }
        else
        {
            this.transform.position = anchor.transform.position;
        }
	}
}
