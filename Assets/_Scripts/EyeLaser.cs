using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeLaser : MonoBehaviour {

    public GameObject targetPlayer;
    public GameObject firePoint;
    public GameObject laser;
    public float laserSpeed = 10f;
    public float fireRate = 1f;
    float fireInterval = 0f;
    public GameObject ExplosionDeath;

    public float health = 20f;

    public EyeDetector eyeDetector;

	// Use this for initialization
	void Start () {

        firePoint = transform.Find("FirePoint").gameObject;

        eyeDetector = GameObject.Find("EyeDetectionRange").GetComponent<EyeDetector>();

        //Placeholder Target Selection Code: REPLACE THIS
        //targetPlayer = GameObject.Find("Dummy_Player");
	}

    void Update() {
        if (targetPlayer != null)
        {
            transform.LookAt(targetPlayer.transform.position);
            FireLaser();
        }
        targetPlayer = eyeDetector.targetGO;
    }

	// Update is called once per frame
	void FixedUpdate () {
        

    }

    void FireLaser() {
        if (Time.timeSinceLevelLoad > fireInterval) {
            GameObject laserclone;
            laserclone = Instantiate(laser, firePoint.transform.position, firePoint.transform.rotation) as GameObject;
            laserclone.GetComponent<Rigidbody>().velocity = laserclone.transform.forward * laserSpeed;
            fireInterval = Time.timeSinceLevelLoad + fireRate;
        }
    }

    void OnCollisionEnter(Collision other) {
        if (other.gameObject.tag == "Bullet") {
            health -= 1;

            if (health <= 0) {
                GameObject explosion;
                explosion = Instantiate(ExplosionDeath, this.gameObject.transform.position, Quaternion.identity);
                Destroy(this.gameObject);
            }
        }
    }
}
