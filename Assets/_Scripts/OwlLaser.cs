using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OwlLaser : MonoBehaviour {

    public float speed = 10f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        //transform.Translate(this.gameObject.transform.forward);
	}

    void OnCollisionEnter(Collision other) {
        Destroy(this.gameObject);
    }
}
