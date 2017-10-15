using UnityEngine;
using System.Collections;
using DG.Tweening;

public class ThirdPersonController : MonoBehaviour {
	public float 	inH, inV;
	public float	rSpeed = 90; // Degrees per second
	public float	speed = 10; // Meters per second
	public Vector3	vel, angles;

	Rigidbody rigid;

    public GameObject Hammer;

	// Use this for initialization
	void Start () {
		rigid = GetComponent<Rigidbody>();
        Hammer = GameObject.Find("Hammer");
	}

    void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            HammerSwing();
        }
    }

	// Update is called once per frame
	void FixedUpdate () {
		inH = Input.GetAxis("Horizontal");
		inV = Input.GetAxis("Vertical");

		angles = transform.rotation.eulerAngles;
		angles.y += inH * rSpeed * Time.fixedDeltaTime;
		transform.rotation = Quaternion.Euler(angles);

//		vel = rigid.velocity;
		vel = Vector3.zero;
		vel += transform.forward * inV * speed;
		vel.y = rigid.velocity.y;

		rigid.velocity = vel;


	}

    void HammerSwing() {
        Hammer.transform.DOLocalJump(this.transform.forward, 3f, 3, 2f, false);
    }
}
