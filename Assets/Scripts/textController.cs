using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class textController : MonoBehaviour {

    GUIStyle style = new GUIStyle();

	// Use this for initialization
	void Start () {
        style.alignment = TextAnchor.MiddleCenter;
        style.fontSize = Screen.height / 10;
        style.normal.textColor = Color.yellow;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnGUI()
    {
       // GUI.Box(new Rect(Screen.width / 6, Screen.height / 5, Screen.width / 3 * 2, Screen.height / 5), "DEATH MATCH", style);
        if (GUI.Button(new Rect(Screen.width / 6, Screen.height / 8, Screen.width / 3 * 2, Screen.height / 4), "DEATH MATCH", style))
        {
            SceneManager.LoadScene("Scene2");
        }
        if (GUI.Button(new Rect(Screen.width / 6, Screen.height / 8 * 3, Screen.width / 3 * 2, Screen.height / 4), "BOSS FIGHT", style))
        {
            SceneManager.LoadScene("Scene1");
        }
        if (GUI.Button(new Rect(Screen.width / 6, Screen.height / 8 * 5, Screen.width / 3 * 2, Screen.height / 4), "MENU", style))
        {
            SceneManager.LoadScene("Menu");
        }
    }
}
