using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gun : MonoBehaviour {

    public GameObject bullet;
    public float muzzleVelocity;

    public void fire()
    {
        GameObject bulletInst = Instantiate(bullet);
        bulletInst.transform.position = this.transform.position;
        bulletInst.GetComponent<Rigidbody>().AddForce(this.transform.up * muzzleVelocity);
    }
}
