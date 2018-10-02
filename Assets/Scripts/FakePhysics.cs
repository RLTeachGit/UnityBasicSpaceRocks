using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakePhysics : MonoBehaviour {


    private Rigidbody2D mRB2;       //We dont want other classes messing with this, as we control Physics

    private SpriteRenderer mSR;     //May need this later

    private BoxCollider2D mBC2D;

    [SerializeField]
    private float Speed=1.0f;

    [SerializeField]
    private float RotationSpeed = 360.0f;


    // Use this for initialization
    void Start () {

        mSR = gameObject.GetComponent<SpriteRenderer>(); //Grab SR assigned in IDE
        Debug.Assert(mSR != null, "Error:Missing SpriteRenderer");

        mBC2D = gameObject.GetComponent<BoxCollider2D>();
        mBC2D.isTrigger = true;     //Set it to trigger in code

        mRB2 = gameObject.AddComponent<Rigidbody2D>();  //Add RidgidBody2D in Code
        mRB2.isKinematic = true;       //Don't use Physics as we'll do our own
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        float tMoveSpeed = Input.GetAxis("Vertical") * Speed;
        float tRotationSpeed = Input.GetAxis("Horizontal") * RotationSpeed;
        transform.Rotate(0, 0, tRotationSpeed * RotationSpeed * Time.deltaTime);
        Vector3 tMoveVector = Quaternion.Euler(0, 0, transform.rotation.z) * (Vector3.up * tMoveSpeed);
        transform.position += tMoveVector * Time.deltaTime;
        Debug.Log(tMoveVector);
    }
}


