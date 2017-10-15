using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OwlLaser : MonoBehaviour {


	// Use this for initialization
	void Start () {
        Destroy(this.gameObject, 15f);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        //transform.Translate(this.gameObject.transform.forward);
	}

    void OnCollisionEnter(Collision other) {
        Destroy(this.gameObject);
    }
}
