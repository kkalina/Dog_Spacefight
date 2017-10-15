using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

public class flightController : MonoBehaviour {

    PlayerIndex playerIndex;
    GamePadState state;

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

    // Use this for initialization
    void Start () {
        afterburner.GetComponent<ParticleSystem>().enableEmission = false;
        ABGlow.GetComponent<ParticleSystem>().enableEmission = false;
        afterburnerOn = false;
        burnerLight.SetActive(false);
        gun2RefireTime = 0.5f * refireDelay;


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

        roll = state.ThumbSticks.Left.X;
        pitch = state.ThumbSticks.Left.Y;
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


        //if(((Mathf.Abs(state.ThumbSticks.Right.X) > 0.1f) || (Mathf.Abs(state.ThumbSticks.Right.Y) > 0.1f))&&(TPC.GetComponent<ThirdPersonCamera>().stationaryMode == false))
        if ((state.Buttons.B == ButtonState.Pressed) && (TPC.GetComponent<ThirdPersonCamera>().stationaryMode == false))
        {
            TPC.GetComponent<ThirdPersonCamera>().stationaryMode = true;
            TPC.GetComponent<ThirdPersonCamera>().statRotation = TPC.transform;
        }
        //else if (((Mathf.Abs(state.ThumbSticks.Right.X) < 0.1f) && (Mathf.Abs(state.ThumbSticks.Right.Y) < 0.1f)) && (TPC.GetComponent<ThirdPersonCamera>().stationaryMode == true))
        else if ((state.Buttons.B == ButtonState.Released) && (TPC.GetComponent<ThirdPersonCamera>().stationaryMode == true))

        {
            TPC.GetComponent<ThirdPersonCamera>().stationaryMode = false;
        }

        if (state.Buttons.RightShoulder == ButtonState.Pressed)
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

    }

    // Update is called once per frame
    void Update () {

        state = GamePad.GetState(playerIndex);
        /*
        roll = state.ThumbSticks.Left.X;
        pitch = state.ThumbSticks.Left.Y;

        this.GetComponent<Rigidbody>().AddTorque(transform.forward * -1 * roll * rollSpeed * Time.deltaTime);
        this.GetComponent<Rigidbody>().AddTorque(transform.right * pitch * pitchSpeed * Time.deltaTime);
        */

            if (state.Buttons.LeftShoulder == ButtonState.Pressed)
        {
            gunLight.SetActive(true);
            if (gun1RefireTime <= 0)
            {
                gun1.GetComponent<gun>().fire();
                gun1RefireTime = refireDelay;
            }
            else
            {
                gun1RefireTime -= Time.deltaTime;
            }

            if (gun2RefireTime <= 0)
            {
                gun2.GetComponent<gun>().fire();
                gun2RefireTime = refireDelay;
            }
            else
            {
                gun2RefireTime -= Time.deltaTime;
            }
            //gun1RefireTime -= Time.deltaTime;
            //gun2RefireTime -= Time.deltaTime;
        }

        if (state.Buttons.LeftShoulder == ButtonState.Released)
        {
            gunLight.SetActive(false);
            gun1RefireTime = 0;
            gun2RefireTime = 0.5f * refireDelay;
        }

            if (((state.Buttons.RightShoulder == ButtonState.Pressed)||(TPC.GetComponent<ThirdPersonCamera>().stationaryMode)) && (!afterburnerOn))
        {
            afterburner.GetComponent<ParticleSystem>().enableEmission = true;
            ABGlow.GetComponent<ParticleSystem>().enableEmission = true;
            //afterburner.SetActive(true);
            afterburnerOn = true;
            cam.GetComponent<shake>().shakeSources++;
            burnerLight.SetActive(true);
        }
        if (((state.Buttons.RightShoulder == ButtonState.Released)&&(!TPC.GetComponent<ThirdPersonCamera>().stationaryMode)) && (afterburnerOn))
        {
            afterburner.GetComponent<ParticleSystem>().enableEmission = false;
            ABGlow.GetComponent<ParticleSystem>().enableEmission = false;
            //afterburner.SetActive(false);
            afterburnerOn = false;
            cam.GetComponent<shake>().shakeSources--;
            burnerLight.SetActive(false);
            if (cam.GetComponent<shake>().shakeSources < 0)
            {
                cam.GetComponent<shake>().shakeSources = 0;
            }
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

        this.enabled = false;
    }

    public GameObject sparks;

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
}
