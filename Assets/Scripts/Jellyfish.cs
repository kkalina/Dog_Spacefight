using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jellyfish : MonoBehaviour
{
    public float speed;
<<<<<<< HEAD
    public float radius;
    public float power;
    public GameObject explosion;
=======
>>>>>>> a62ee3c8218f99bd4b25f72763a3de0b386a5df7

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        this.GetComponent<Rigidbody>().velocity = transform.forward * speed;
    }

    void OnCollisionEnter(Collision other)
    {
<<<<<<< HEAD
        //this.transform.RotateAround(this.transform.up, 180);
        if ((other.gameObject.tag == "Player") || other.gameObject.tag == "Bullet")
        {
            GameObject explosionInstance = Instantiate(explosion);
            explosionInstance.transform.position = this.transform.position;
            Destroy(this.gameObject);
        }
    }

   
      
    }
=======
        this.transform.RotateAround(this.transform.up, 180);
    }
}
>>>>>>> a62ee3c8218f99bd4b25f72763a3de0b386a5df7
