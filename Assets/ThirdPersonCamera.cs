using UnityEngine;
using System.Collections;

public class ThirdPersonCamera : MonoBehaviour {
	public Transform	poi;
	public float		dist = 5;
	public float		height = 1;
	public float		easing = 0.1f;
    public GameObject childCam;

    public bool stationaryMode = false;
    public float maxStatDist = 50;
    public float statMoveSpeed = 0.001f;
    public Transform statRotation;


	// Use this for initialization
	void Update () {
        if (Input.GetKeyDown(KeyCode.H))
        {
            if (stationaryMode)
            {
                stationaryMode = false;
            }
            else
            {
                stationaryMode = true;
                statRotation = this.transform;
            }
        }
        if (stationaryMode)
        {
            float dist = Vector3.Distance(poi.transform.position, this.transform.position);
            if (dist > maxStatDist)
            {
                //this.transform.position = Vector3.MoveTowards(this.transform.position, poi.transform.position, statMoveSpeed);
                this.transform.position = Vector3.Lerp(this.transform.position, poi.transform.position, dist * statMoveSpeed);
            }
            //this.transform.rotation = statRotation.rotation;
        }
    }

    
	
	// Update is called once per frame
	void FixedUpdate () {
        if (!stationaryMode)
        {
            Vector3 toLoc = poi.position + poi.forward * (-dist) + poi.up * height;
            Vector3 loc = (1 - easing) * transform.position + easing * toLoc;
            transform.position = loc;
            transform.rotation = poi.rotation;
        }
        else
        {
            transform.LookAt(poi.position,statRotation.up);
            //this.transform.rotation = statRotation.rotation;
        }
	}
}
