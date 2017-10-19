using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jellyfish : MonoBehaviour
{
    public float speed;
    public GameObject explosion;

    void FixedUpdate()
    {
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
