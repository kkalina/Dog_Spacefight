using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuController : MonoBehaviour {

    GUIStyle style = new GUIStyle();
    GUIStyle nameStyle = new GUIStyle();

	// Use this for initialization
	void Start () {
        style.fontSize = 25;
        style.alignment = TextAnchor.MiddleCenter;
        style.normal.textColor = Color.white;
        nameStyle.fontSize = 45;
        nameStyle.alignment = TextAnchor.MiddleCenter;
        nameStyle.normal.textColor = Color.yellow;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.anyKeyDown)
        {
            SceneManager.LoadScene("PreStart");
        }
	}

    void OnGUI()
    {
        GUI.Box(new Rect(Screen.width / 3, Screen.height / 7 * 5, Screen.width / 3, Screen.height / 6), "PRESS ANY KEY TO START", style);
        GUI.Box(new Rect(Screen.width / 5, Screen.height / 7 * 1, Screen.width / 5 * 3, Screen.height / 6), "THIS LINE IS THE NAME OF THE GAME", nameStyle);
    }
}
