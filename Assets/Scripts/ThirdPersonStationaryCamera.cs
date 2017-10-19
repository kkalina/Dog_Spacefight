using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonStationaryCamera : MonoBehaviour {

    public Transform poi;
    public float dist = 5;
    public float height = 1;
    public float easing = 0.1f;
    public GameObject childCam;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Vector3 toLoc = poi.position + poi.forward * (-dist) + poi.up * height;
        //Vector3 loc = (1 - easing) * transform.position + easing * toLoc;
        //transform.position = loc;
        //transform.rotation = poi.rotation;
        transform.LookAt(poi);
    }
}
