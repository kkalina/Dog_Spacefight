using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

public class flightController : MonoBehaviour {

    PlayerIndex playerIndex;
    GamePadState state;

    private bool isEnd;

    public float pitch;
    public float roll;
    public float pitchSpeed = 1;
    public float rollSpeed = 1;

    public float yaw;
    public float VYaw;
    public float yawSpeed = 1;
    public float VYawSpeed = 1;

    public float accel = 1;

    public GameObject afterburner;
    public GameObject ABGlow;
    public bool afterburnerOn = false;
    public GameObject cam;
    public GameObject burnerLight;
    public GameObject gun1;
    public GameObject gun2;

    public GameObject POI;

    private float gun1RefireTime = 0;
    private float gun2RefireTime = 0;
    public float refireDelay = 0.2f;

    public int playerNumber = 1;

    public int numMissilesFollowing = 0;

    public int playerHealth = 3;

    public GameObject gunLight;

    public GameObject TPC;

    public TrailRenderer LTip;
    public TrailRenderer RTip;

    public GameObject missileLauncherL;
    public GameObject missileLauncherR;

    private GUIStyle style = new GUIStyle();

    // Use this for initialization
    void Start () {
        afterburner.GetComponent<ParticleSystem>().enableEmission = false;
        ABGlow.GetComponent<ParticleSystem>().enableEmission = false;
        afterburnerOn = false;
        burnerLight.SetActive(false);
        gun2RefireTime = 0.5f * refireDelay;

        style.normal.textColor = Color.red;
        style.fontSize = 40;

        if(playerNumber == 1)
        {
            playerIndex = XInputDotNetPure.PlayerIndex.One;
        }else if (playerNumber == 2)
        {
            playerIndex = XInputDotNetPure.PlayerIndex.Two;
        }
        else if (playerNumber == 3)
        {
            playerIndex = XInputDotNetPure.PlayerIndex.Three;
        }
        else if (playerNumber == 4)
        {
            playerIndex = XInputDotNetPure.PlayerIndex.Four;
        }

        if (levelController.instance.playerCount < 2)
        {
            cam.GetComponent<Camera>().rect = new Rect(0.0f, 0.0f, 1.0f, 1.0f);
        }
        else if (levelController.instance.playerCount < 3)
        {
            if (playerNumber == 1)
            {
                cam.GetComponent<Camera>().rect = new Rect(0.0f, 0.0f, 0.5f, 1.0f);
            }
            else if (playerNumber == 2)
            {
                cam.GetComponent<Camera>().rect = new Rect(0.5f, 0.0f, 1.0f, 1.0f);
            }
        }
        else if (levelController.instance.playerCount > 2)
        {
            if (playerNumber == 1)
            {
                cam.GetComponent<Camera>().rect = new Rect(0.0f, 0.5f, 0.5f, 1.0f);
            }
            else if (playerNumber == 2)
            {
                cam.GetComponent<Camera>().rect = new Rect(0.5f, 0.5f, 1.0f, 1.0f);
            }
            else if (playerNumber == 3)
            {
                cam.GetComponent<Camera>().rect = new Rect(0.0f, 0.0f, 0.5f, 0.5f);
            }
            else if (playerNumber == 4)
            {
                cam.GetComponent<Camera>().rect = new Rect(0.5f, 0.0f, 1.0f, 0.5f);
            }
        }
    }

    public float staticRotMultiplier = 2.0f;
    public float staticAccMultiplier = 0.5f;

    public float trailMult = 2.5f;

    public float missileCooldown = 0.5f;
    private float missileTimer;

    private void FixedUpdate()
    {
        state = GamePad.GetState(playerIndex);

        //if (!TPC.GetComponent<ThirdPersonCamera>().stationaryMode)
        //{
        //    roll = state.ThumbSticks.Left.X;
        //    pitch = state.ThumbSticks.Left.Y;

        //}
        //else
        //{
        //    roll = state.ThumbSticks.Right.X;
        //    pitch = state.ThumbSticks.Right.Y;

        //    roll = roll * staticRotMultiplier;
        //    pitch = pitch * staticRotMultiplier;

        //}
        if (!state.IsConnected)
        {
            roll = Input.GetAxis("Horizontal");
            pitch = Input.GetAxis("Vertical");
        }
        else
        {
            roll = state.ThumbSticks.Left.X;
            pitch = state.ThumbSticks.Left.Y;
        }


        if (TPC.GetComponent<ThirdPersonCamera>().stationaryMode)
        {
                roll = roll * staticRotMultiplier;
                pitch = pitch * staticRotMultiplier;
        }


        this.GetComponent<Rigidbody>().AddTorque(transform.forward * -1 * roll * rollSpeed * Time.fixedDeltaTime);
        this.GetComponent<Rigidbody>().AddTorque(transform.right * pitch * pitchSpeed * Time.fixedDeltaTime);

        //Yaw scheme
        yaw = state.ThumbSticks.Right.X;
        VYaw = state.ThumbSticks.Right.Y;

        this.GetComponent<Rigidbody>().AddTorque(transform.up * yaw * yawSpeed * Time.fixedDeltaTime);
        this.GetComponent<Rigidbody>().AddTorque(transform.right * -1.0f * VYaw * VYawSpeed * Time.fixedDeltaTime);

        bool evasiveMode = false;
        if (state.IsConnected)
        {
            if (state.Buttons.B == ButtonState.Pressed)
            {
                evasiveMode = true;
            }
        }
        else
        {
            evasiveMode = Input.GetKey(KeyCode.RightShift);
        }

        //if(((Mathf.Abs(state.ThumbSticks.Right.X) > 0.1f) || (Mathf.Abs(state.ThumbSticks.Right.Y) > 0.1f))&&(TPC.GetComponent<ThirdPersonCamera>().stationaryMode == false))
        if ((evasiveMode) && (TPC.GetComponent<ThirdPersonCamera>().stationaryMode == false))
        {
            TPC.GetComponent<ThirdPersonCamera>().stationaryMode = true;
            TPC.GetComponent<ThirdPersonCamera>().statRotation = TPC.transform;
        }
        //else if (((Mathf.Abs(state.ThumbSticks.Right.X) < 0.1f) && (Mathf.Abs(state.ThumbSticks.Right.Y) < 0.1f)) && (TPC.GetComponent<ThirdPersonCamera>().stationaryMode == true))
        else if ((!evasiveMode) && (TPC.GetComponent<ThirdPersonCamera>().stationaryMode == true))

        {
            TPC.GetComponent<ThirdPersonCamera>().stationaryMode = false;
        }

        bool accelerator = false;
        if (!state.IsConnected)
        {
            accelerator = Input.GetKey(KeyCode.LeftShift);
        }
        else
        {
            if (state.Buttons.RightShoulder == ButtonState.Pressed)
            {
                accelerator = true;
            }
        }


        if (accelerator)
        {
            float acc = 0;
            acc = accel * Time.fixedDeltaTime;
            if (TPC.GetComponent<ThirdPersonCamera>().stationaryMode)
            {
                acc = acc * staticAccMultiplier;
            }
            this.GetComponent<Rigidbody>().AddForce(transform.forward * acc);
        }
        else if (TPC.GetComponent<ThirdPersonCamera>().stationaryMode)
        {
            float acc = 0;
            acc = accel * Time.fixedDeltaTime;
            acc = acc * staticAccMultiplier;
            this.GetComponent<Rigidbody>().AddForce(transform.forward * acc);
        }


        //Trail length based on speed
        float t = this.gameObject.GetComponent<Rigidbody>().velocity.magnitude * trailMult;
        LTip.time = t;
        RTip.time = t;

        if (afterburnerOn)
        {
            this.GetComponent<AudioSource>().pitch = 0.8f+(this.GetComponent<Rigidbody>().velocity.magnitude*0.1f);
        }
    }

    public GameObject missile;
    private bool leftMissile = false;
    private bool leftGun = false;

    // Update is called once per frame
    void Update () {

        state = GamePad.GetState(playerIndex);
        /*
        roll = state.ThumbSticks.Left.X;
        pitch = state.ThumbSticks.Left.Y;

        this.GetComponent<Rigidbody>().AddTorque(transform.forward * -1 * roll * rollSpeed * Time.deltaTime);
        this.GetComponent<Rigidbody>().AddTorque(transform.right * pitch * pitchSpeed * Time.deltaTime);
        */

        bool gunTrigger = false;

        if (state.IsConnected)
        {
            if (state.Buttons.LeftShoulder == ButtonState.Pressed)
            {
                gunTrigger = true;
            }
        }
        else
        {
            gunTrigger = Input.GetKey(KeyCode.Space);
        }

        if (gunTrigger)
        {
            gunLight.SetActive(true);
            if (gun1RefireTime <= 0)
            {
                if (leftGun)
                {
                    gun1.GetComponent<gun>().fire();
                    leftGun = false;
                }
                else
                {
                    gun2.GetComponent<gun>().fire();
                    leftGun = true;
                }
                gun1RefireTime = refireDelay;
            }
            else
            {
                gun1RefireTime -= Time.deltaTime;
            }

            //if (gun2RefireTime <= 0)
            //{
            //    gun2.GetComponent<gun>().fire();
            //    gun2RefireTime = refireDelay;
            //}
            //else
            //{
            //    gun2RefireTime -= Time.deltaTime;
            //}
            //gun1RefireTime -= Time.deltaTime;
            //gun2RefireTime -= Time.deltaTime;
        }
        else
        {
            gunLight.SetActive(false);
            gun1RefireTime = 0;
            //gun2RefireTime = 0.5f * refireDelay;
        }

        bool accelerator = false;

        if (state.IsConnected)
        {
            if(state.Buttons.RightShoulder == ButtonState.Pressed)
            {
                accelerator = true;
            }
        }
        else
        {
            accelerator = Input.GetKey(KeyCode.LeftShift);
        }

        if ((accelerator||(TPC.GetComponent<ThirdPersonCamera>().stationaryMode)) && (!afterburnerOn))
        {
            afterburner.GetComponent<ParticleSystem>().enableEmission = true;
            ABGlow.GetComponent<ParticleSystem>().enableEmission = true;
            //afterburner.SetActive(true);
            afterburnerOn = true;
            this.GetComponent<AudioSource>().Play();
            cam.GetComponent<shake>().shakeSources++;
            burnerLight.SetActive(true);
        }
        if (((!accelerator) && (!TPC.GetComponent<ThirdPersonCamera>().stationaryMode)) && (afterburnerOn))
        {
            afterburner.GetComponent<ParticleSystem>().enableEmission = false;
            ABGlow.GetComponent<ParticleSystem>().enableEmission = false;
            //afterburner.SetActive(false);
            afterburnerOn = false;
            this.GetComponent<AudioSource>().Stop();
            cam.GetComponent<shake>().shakeSources--;
            burnerLight.SetActive(false);
            if (cam.GetComponent<shake>().shakeSources < 0)
            {
                cam.GetComponent<shake>().shakeSources = 0;
            }
        }

        if ((state.Buttons.A == ButtonState.Pressed) && ((missileTimer+missileCooldown) < Time.time))
        {
            GameObject missInst = Instantiate(missile);
            if (leftMissile)
            {
                missInst.transform.position = missileLauncherL.transform.position;
                missInst.transform.rotation = missileLauncherL.transform.rotation;
                leftMissile = false;
            }
            else
            {
                missInst.transform.position = missileLauncherR.transform.position;
                missInst.transform.rotation = missileLauncherR.transform.rotation;
                leftMissile = true;

            }
            missInst.GetComponent<missileController>().owner = this.gameObject;
            missInst.GetComponent<Rigidbody>().velocity = this.GetComponent<Rigidbody>().velocity;
            missileTimer = Time.time;
        }

        //death
        if (Input.GetKey(KeyCode.I))
        {
            die();
        }

    }

    public GameObject shipExplosion;
    public GameObject model;

    public void die()
    {
        GameObject dieExp = Instantiate(shipExplosion);
        dieExp.transform.position = this.transform.position;
        dieExp.GetComponent<Rigidbody>().velocity = this.GetComponent<Rigidbody>().velocity;
        model.SetActive(false);
        this.GetComponent<Rigidbody>().Sleep();
        this.GetComponent<BoxCollider>().enabled = false;
        afterburner.SetActive(false);
        burnerLight.SetActive(false);
        this.GetComponent<AudioSource>().Stop();

        this.enabled = false;

        levelController.instance.playerCount -= 1;
        if (levelController.instance.playerCount <= 1) {
            isEnd = true;
            StartCoroutine(EndTIme());
            Application.LoadLevel("Menu");
        }

        Destroy(this.gameObject);


    }

    public GameObject sparks;
    public IEnumerator EndTIme() {
        yield return new WaitForSeconds(5);
    }
    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Bullet")
        {
            playerHealth--;
            if (playerHealth <= 0)
            {
                die();
            }
            else
            {
                GameObject spark = Instantiate(sparks);
                spark.transform.position = other.transform.position;
            }
        }
    }

    void OnGUI()
    {
        if (isEnd)
        {
            GUI.Box(new Rect(Screen.width / 3, Screen.height / 3, Screen.width / 3, Screen.height / 4), "GAME OVER", style);
            //GUI.Box(new Rect(Screen.width / 3, Screen.height / 2, Screen.width / 3, Screen.height / 3), "Press 'R' to restart");
        }
    }
}
