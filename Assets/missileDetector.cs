using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class missileDetector : MonoBehaviour {

    public int target = 0;
    public GameObject targetGO;
    public GameObject parent;

    void OnTriggerEnter(Collider other)
    {
        if((other.gameObject.tag == "Player")&&(targetGO == null)&&(other.gameObject != parent.GetComponent<missileController>().owner))
        {
            target = other.gameObject.GetComponent<flightController>().playerNumber;
            targetGO = other.gameObject;
            other.gameObject.GetComponent<flightController>().numMissilesFollowing++;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if ((other.gameObject.tag == "Player")&&(targetGO == other.gameObject))
        {
            target = 0;
            targetGO = null;
            other.gameObject.GetComponent<flightController>().numMissilesFollowing--;

        }
    }
}
