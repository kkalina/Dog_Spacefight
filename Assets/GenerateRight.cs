using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateRight : MonoBehaviour {

    public float generateInterval;
    public int generateNum;
    public GameObject right;

    private float nextGenerate;

	// Use this for initialization
	void Start () {
        nextGenerate = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
        if (Time.time > nextGenerate)
        {
            nextGenerate += generateInterval;
            for (int i = 0; i < generateNum; i++)
            {
                Instantiate(right);
            }   
        }
	}
}
