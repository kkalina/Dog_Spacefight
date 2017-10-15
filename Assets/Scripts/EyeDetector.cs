using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeDetector : MonoBehaviour {

    public int target = 0;
    public GameObject targetGO;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (targetGO != null) {

        }
	}

    void OnTriggerEnter(Collider other)
    {
        if ((other.gameObject.tag == "Player") && (targetGO == null))
        {
            //target = other.gameObject.GetComponent<flightController>().playerNumber;
            targetGO = other.gameObject;
            //other.gameObject.GetComponent<flightController>().numMissilesFollowing++;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if ((other.gameObject.tag == "Player") && (targetGO == other.gameObject))
        {
            //target = 0;
            targetGO = null;
            //other.gameObject.GetComponent<flightController>().numMissilesFollowing--;

        }
    }
}
