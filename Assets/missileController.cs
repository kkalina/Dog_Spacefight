using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class missileController : MonoBehaviour {

    public float accel;
    public GameObject detector;
    public float maxRotSpeed = 1;
    public float slerpSpeed = 1;
    public GameObject owner;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        this.GetComponent<Rigidbody>().AddForce(this.transform.forward * accel);
        //if (detector.GetComponent<missileDetector>().target > 0) { 
        //    int currTarget = detector.GetComponent<missileDetector>().target;

        //    if (currTarget == 1) {
        //        print("MISSILE TARGETING PLAYER 1");
        //        Vector3 direction = (levelController.instance.player1.transform.position - this.transform.position);
        //        this.transform.rotation = Quaternion.LookRotation(direction, this.transform.up);
        //    }
        //}
        if (detector.GetComponent<missileDetector>().targetGO != null)
        {
            //this.GetComponent<Rigidbody>().AddForce(this.transform.up * accel);

            GameObject target = detector.GetComponent<missileDetector>().targetGO;
            //print("MISSILE TARGETING PLAYER " + target.GetComponent<flightController>().playerNumber);
            Vector3 direction = (target.transform.position - this.transform.position);
            Debug.DrawRay(this.transform.position, direction, Color.blue, 0.1f);
            //Quaternion slerpedDir = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(this.transform.forward, direction), slerpSpeed);
            Quaternion slerpedDir = Quaternion.RotateTowards(this.transform.rotation, Quaternion.LookRotation(direction, this.transform.up), slerpSpeed);
            // this.transform.rotation = Quaternion.LookRotation(this.transform.forward, direction);
            this.transform.rotation = slerpedDir;
            
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if((other.gameObject.tag == "Player")&&(other.gameObject != owner))
        {
            other.gameObject.GetComponent<flightController>().die();
        }
    }
}
