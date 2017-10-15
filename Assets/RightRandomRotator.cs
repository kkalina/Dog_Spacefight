using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightRandomRotator : MonoBehaviour {

    public float speed;
    public float xmin;
    public float xmax;
    public float ymin;
    public float ymax;
    public float zmin;
    public float zmax;
    public GameObject text;

	// Use this for initialization
	void Start () {
		transform.Translate(new Vector3(Random.Range(xmin, xmax), Random.Range(ymin, ymax), Random.Range(zmin, zmax)));
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(Random.insideUnitSphere * Time.deltaTime * speed);
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<flightController>().playerHealth += 2;
            Instantiate(text);
            Destroy(gameObject);
        }       
    }
}
