using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeTextController : MonoBehaviour {

    public float appearTime;

    private float disappearTime;
    private bool isSet;
    private GUIStyle style = new GUIStyle();

	// Use this for initialization
	void Start () {
        style.normal.textColor = Color.green;
        style.fontSize = 25;
        Destroy(gameObject, appearTime);
	}
	
	// Update is called once per frame
	void Update () {
        style.fontSize += 3;
	}

    void OnGUI()
    {
        GUI.Box(new Rect(Screen.width / 2, Screen.height / 5 * 2, Screen.width / 5, Screen.height / 7), "GAIN RIGHT GAIN LIFE", style);
    }
}
