using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jellyfish : MonoBehaviour {
    public float speed;
<<<<<<< HEAD
    public float radius;
    public float power;
    public GameObject explosion;
=======
>>>>>>> a9494366bd947c84bc1dbd2111e439caebb704ca

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        this.GetComponent<Rigidbody>().velocity = transform.forward * speed;
    }

    void OnCollisionEnter(Collision other)
    {
        this.transform.RotateAround(this.transform.up, 180);
<<<<<<< HEAD

        GameObject explosionInstance = Instantiate(explosion);
        explosionInstance.transform.position = this.transform.position;
        Destroy(this.gameObject);
    }

   
      
    }
=======
    }

}
>>>>>>> a9494366bd947c84bc1dbd2111e439caebb704ca
