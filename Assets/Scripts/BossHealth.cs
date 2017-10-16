using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour {

    public float bossHealth = 50f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter(Collision other) {
        if (other.gameObject.tag == "Bullet") {
            bossHealth -= 1;
            if (bossHealth <= 0) {
                Destroy(this.gameObject);
                Application.LoadLevel(Application.loadedLevel);
            }
        }
    }
}
