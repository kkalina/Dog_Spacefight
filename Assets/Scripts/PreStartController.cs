using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using XInputDotNetPure;

public static class playerNumInterface
{
    public static int playerNum;
}

public class PreStartController : MonoBehaviour {

    GUIStyle commandStyle = new GUIStyle();
    
    public GameObject button1;
    public GameObject button2;
    public GameObject button3;
    public GameObject button4;

    bool player1IndexSet = false;
    bool player2IndexSet = false;
    bool player3IndexSet = false;
    bool player4IndexSet = false;
    PlayerIndex player1Index;
    PlayerIndex player2Index;
    PlayerIndex player3Index;
    PlayerIndex player4Index;
    GamePadState state1;
    GamePadState state2;
    GamePadState state3;
    GamePadState state4;
    GamePadState prevState1;
    GamePadState prevState2;
    GamePadState prevState3;
    GamePadState prevState4;

    private bool isA = false;
    private bool isB = false;
    private bool isC = false;
    private bool isD = false;

	// Use this for initialization
	void Start () {
		commandStyle.fontSize = Screen.height / 25;
        commandStyle.alignment = TextAnchor.MiddleCenter;
        commandStyle.normal.textColor = Color.yellow;
        playerNumInterface.playerNum = 0;
	}

    void FixedUpdate()
    {
        // SetVibration should be sent in a slower rate.
        // Set vibration according to triggers
        GamePad.SetVibration(player1Index, state1.Triggers.Left, state1.Triggers.Right);
        GamePad.SetVibration(player2Index, state2.Triggers.Left, state2.Triggers.Right);
    }
	
	// Update is called once per frame
	void Update () {
        // Find a PlayerIndex, for a single player game
        // Will find the first controller that is connected ans use it
        if (!player1IndexSet || !prevState1.IsConnected)
        {
            for (int i = 0; i < 1; ++i)
            {
                PlayerIndex testPlayerIndex = (PlayerIndex)i;
                GamePadState testState = GamePad.GetState(testPlayerIndex);
                if (testState.IsConnected)
                {
                    Debug.Log(string.Format("GamePad 1 found {0}", testPlayerIndex));
                    player1Index = testPlayerIndex;
                    player1IndexSet = true;
                }
            }
        }
        if (!player2IndexSet || !prevState2.IsConnected)
        {
            for (int i = 1; i < 2; ++i)
            {
                PlayerIndex testPlayerIndex = (PlayerIndex)i;
                GamePadState testState = GamePad.GetState(testPlayerIndex);
                if (testState.IsConnected)
                {
                    Debug.Log(string.Format("GamePad 2 found {0}", testPlayerIndex));
                    player2Index = testPlayerIndex;
                    player2IndexSet = true;
                }
            }
        }
        if (!player3IndexSet || !prevState3.IsConnected)
        {
            for (int i = 2; i < 3; ++i)
            {
                PlayerIndex testPlayerIndex = (PlayerIndex)i;
                GamePadState testState = GamePad.GetState(testPlayerIndex);
                if (testState.IsConnected)
                {
                    Debug.Log(string.Format("GamePad 3 found {0}", testPlayerIndex));
                    player3Index = testPlayerIndex;
                    player3IndexSet = true;
                }
            }
        }
        if (!player4IndexSet || !prevState4.IsConnected)
        {
            for (int i = 3; i < 4; ++i)
            {
                PlayerIndex testPlayerIndex = (PlayerIndex)i;
                GamePadState testState = GamePad.GetState(testPlayerIndex);
                if (testState.IsConnected)
                {
                    Debug.Log(string.Format("GamePad 4 found {0}", testPlayerIndex));
                    player4Index = testPlayerIndex;
                    player4IndexSet = true;
                }
            }
        }
        if (player1IndexSet)
        {
            prevState1 = state1;
            state1 = GamePad.GetState(player1Index);
            if (prevState1.Buttons.A == ButtonState.Released && state1.Buttons.A == ButtonState.Pressed)
            {
                if (!isA)
                {
                    playerNumInterface.playerNum++;
                    isA = true;
                }
                //button1.GetComponent<Renderer>().material.color = Color.red;
                button1.GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
            }
        }
        if (player2IndexSet)
        {
            prevState2 = state2;
            state2 = GamePad.GetState(player2Index);
            if (prevState2.Buttons.A == ButtonState.Released && state2.Buttons.A == ButtonState.Pressed)
            {
                button2.GetComponent<Renderer>().material.color = Color.red;
                if (!isB)
                {
                    playerNumInterface.playerNum++;
                    isB = true;
                }
            }
        }
        if (player3IndexSet)
        {
            prevState3 = state3;
            state3 = GamePad.GetState(player3Index);
            if (prevState3.Buttons.A == ButtonState.Released && state3.Buttons.A == ButtonState.Pressed)
            {
                button3.GetComponent<Renderer>().material.color = Color.red;
                if (!isC)
                {
                    playerNumInterface.playerNum++;
                    isC = true;
                }
            }
        }
        if (player4IndexSet)
        {
            prevState4 = state4;
            state4 = GamePad.GetState(player4Index);
            if (prevState4.Buttons.A == ButtonState.Released && state4.Buttons.A == ButtonState.Pressed)
            {
                button4.GetComponent<Renderer>().material.color = Color.red;
                if (!isD)
                {
                    playerNumInterface.playerNum++;
                    isD = true;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            SceneManager.LoadScene("ModeSelect");
        }
	}

    void OnGUI()
    {
        if (GUI.Button(new Rect(Screen.width / 5, Screen.height / 9 * 7, Screen.width / 5 * 3, Screen.height / 9), "PRESS 'S' TO START", commandStyle))
        {
            SceneManager.LoadScene("ModeSelect");
        }
    }
}
