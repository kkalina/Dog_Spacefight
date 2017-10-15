using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boundary : MonoBehaviour {

    public float warnDistance = 50;

	// Use this for initialization
	void Start () {
        Color c = this.GetComponent<Renderer>().material.color;
        c.a = 0.0f;
        this.GetComponent<Renderer>().material.color = c;
    }

    //// Update is called once per frame
    //void FixedUpdate () {
    //       float distance = warnDistance+1;
    //       Color c = this.GetComponent<Renderer>().material.color;
    //       if (levelController.instance.player1 != null)
    //       {
    //           distance = Vector3.Distance(levelController.instance.player1.transform.position, this.transform.position);
    //       }
    //       if ((levelController.instance.player1 != null) && (Vector3.Distance(levelController.instance.player1.transform.position, this.transform.position)
    //       {
    //           distance = Vector3.Distance(levelController.instance.player1.transform.position, this.transform.position);
    //       }
    //       if (distance < warnDistance)
    //       {
    //           c.a = (warnDistance - distance);
    //       }
    //}

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            print("exec");
            Color c = this.GetComponent<Renderer>().material.color;
            float distance = Vector3.Distance(other.transform.position, this.transform.position);
            c.a = (warnDistance - distance) * 0.03f;
            this.GetComponent<Renderer>().material.color = c;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Color c = this.GetComponent<Renderer>().material.color;
            c.a = 0.0f;
            this.GetComponent<Renderer>().material.color = c;
        }
    }
}
