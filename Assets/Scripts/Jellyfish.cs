using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jellyfish : MonoBehaviour {
    public float speed;
    public float radius;
    public float power;
    public GameObject explosion;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        this.GetComponent<Rigidbody>().velocity = transform.forward * speed;
    }

    void OnCollisionEnter(Collision other)
    {
        //this.transform.RotateAround(this.transform.up, 180);
        if ((other.gameObject.tag == "Player") || other.gameObject.tag == "Bullet")
        {
            GameObject explosionInstance = Instantiate(explosion);
            explosionInstance.transform.position = this.transform.position;
            Destroy(this.gameObject);
        }
    }

   
      
    }
