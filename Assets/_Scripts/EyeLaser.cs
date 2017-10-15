using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeLaser : MonoBehaviour {

    public GameObject targetPlayer;
    public GameObject firePoint;
    public GameObject laser;
    public float laserSpeed = 10f;

	// Use this for initialization
	void Start () {

        firePoint = transform.Find("FirePoint").gameObject;

        //Placeholder Target Selection Code: REPLACE THIS
        targetPlayer = GameObject.Find("Dummy_Player");
	}

    void Update() {
        FireLaser();
    }

	// Update is called once per frame
	void FixedUpdate () {
        transform.LookAt(targetPlayer.transform.position);

	}

    void FireLaser() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            GameObject laserclone;
            laserclone = Instantiate(laser, firePoint.transform.position, firePoint.transform.rotation) as GameObject;
            laserclone.GetComponent<Rigidbody>().velocity = laserclone.transform.forward * laserSpeed;
        }
    }
}
