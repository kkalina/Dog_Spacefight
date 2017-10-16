using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelController : MonoBehaviour {

    public int playerCount = 0;

    public GameObject playerPrefab;
    public GameObject playerCamPrefab;

    public GameObject player1;
    public GameObject player2;
    public GameObject player3;
    public GameObject player4;
    public GameObject player1cam;
    public GameObject player2cam;
    public GameObject player3cam;
    public GameObject player4cam;

    public Transform P1Spawn;
    public Transform P2Spawn;
    public Transform P3Spawn;
    public Transform P4Spawn;

    public static levelController instance;

    // Use this for initialization
    void Start () {
        playerCount = playerNumInterface.playerNum;
        instance = this;
        if(playerCount > 0)
        {
            player1 = Instantiate(playerPrefab);
            player1.transform.position = P1Spawn.position;
            player1.GetComponent<flightController>().playerNumber = 1;

            player1cam = Instantiate(playerCamPrefab);
            player1cam.GetComponent<ThirdPersonCamera>().poi = player1.GetComponent<flightController>().POI.transform;

            player1.GetComponent<flightController>().cam = player1cam.GetComponent<ThirdPersonCamera>().childCam;

            player1.GetComponent<flightController>().TPC = player1cam;
        }
        if (playerCount > 1)
        {
            player2 = Instantiate(playerPrefab);
            player2.transform.position = P2Spawn.position;
            player2.GetComponent<flightController>().playerNumber = 2;

            player2cam = Instantiate(playerCamPrefab);
            player2cam.GetComponent<ThirdPersonCamera>().poi = player2.GetComponent<flightController>().POI.transform;

            player2.GetComponent<flightController>().cam = player2cam.GetComponent<ThirdPersonCamera>().childCam;

            player2.GetComponent<flightController>().TPC = player2cam;
        }
        if (playerCount > 2)
        {
            player3 = Instantiate(playerPrefab);
            player3.transform.position = P3Spawn.position;
            player3.GetComponent<flightController>().playerNumber = 3;

            player3cam = Instantiate(playerCamPrefab);
            player3cam.GetComponent<ThirdPersonCamera>().poi = player3.GetComponent<flightController>().POI.transform;

            player3.GetComponent<flightController>().cam = player3cam.GetComponent<ThirdPersonCamera>().childCam;

            player3.GetComponent<flightController>().TPC = player3cam;
        }
        if (playerCount > 3)
        {
            player4 = Instantiate(playerPrefab);
            player4.transform.position = P4Spawn.position;
            player4.GetComponent<flightController>().playerNumber = 4;

            player4cam = Instantiate(playerCamPrefab);
            player4cam.GetComponent<ThirdPersonCamera>().poi = player4.GetComponent<flightController>().POI.transform;

            player4.GetComponent<flightController>().cam = player4cam.GetComponent<ThirdPersonCamera>().childCam;

            player4.GetComponent<flightController>().TPC = player4cam;
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
