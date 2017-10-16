using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuController : MonoBehaviour {

    GUIStyle style = new GUIStyle();
    GUIStyle nameStyle = new GUIStyle();

	// Use this for initialization
	void Start () {
        style.fontSize = Screen.height / 16;
        style.alignment = TextAnchor.MiddleCenter;
        style.normal.textColor = Color.white;
        nameStyle.fontSize = Screen.height / 9;
        nameStyle.alignment = TextAnchor.MiddleCenter;
        nameStyle.normal.textColor = Color.red;
        nameStyle.fontStyle = FontStyle.BoldAndItalic;
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
        GUI.Box(new Rect(Screen.width / 5, Screen.height / 15 * 1, Screen.width / 5 * 3, Screen.height / 15 * 2), "Hey Buddy you killed my wife, ", nameStyle);
        GUI.Box(new Rect(Screen.width / 5, Screen.height / 5, Screen.width / 5 * 3, Screen.height / 15 * 2), "but now we're in space,", nameStyle);
        GUI.Box(new Rect(Screen.width / 5, Screen.height / 3, Screen.width / 5 * 3, Screen.height / 15 * 2), "I'm gonna kill you: ", nameStyle);
        GUI.Box(new Rect(Screen.width / 5, Screen.height / 15 * 7, Screen.width / 5 * 3, Screen.height / 15 * 2), "The Game", nameStyle);
        Debug.Log(Screen.height);
        Debug.Log(Screen.width);
    }
}
