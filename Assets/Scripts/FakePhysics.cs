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

    private Vector3 mVelocity = Vector3.zero;

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
	void Update () {
        Vector3 tNewPostion;        //Storage for new position
        DoMove();
        if(DoWrap(out tNewPostion)) {       //Check if we should wrap
            transform.position = tNewPostion;   //Yes we need to have new position
        }
    }

    void DoMove() {
        float tThrust = Input.GetAxis("Vertical") * Speed;       //Get Thrust
        float tRotate = Input.GetAxis("Horizontal") * RotationSpeed; //Get Rotation
        transform.Rotate(0, 0, tRotate * RotationSpeed * Time.deltaTime);    //Rotate ship
        mVelocity += Quaternion.Euler(0, 0, transform.rotation.z) * transform.up * tThrust * Speed; //Non mass velocity
        transform.position += mVelocity * Time.deltaTime; //Work out new position
    }

    bool    DoWrap(out Vector3 vNewPosition) {
        float   tHeight = Camera.main.orthographicSize;  //Figure out what Camera can see
        float   tWidth = Camera.main.aspect * tHeight;  //Use aspect ratio to work out Width
        bool    tMoved = false;     //Default is no wrap
        vNewPosition = transform.position;
        if (vNewPosition.x > tWidth) {
            vNewPosition.x -= 2.0f * tWidth;     //If out of bounds reset position
            tMoved = true;      //We are wrapping
        } else if (vNewPosition.x < -tWidth) {
            vNewPosition.x += 2.0f * tWidth;
            tMoved = true;
        }
        if (vNewPosition.y > tHeight) {
            vNewPosition.y -= 2.0f * tHeight;
            tMoved = true;
        } else if (vNewPosition.y < -tHeight) {
            vNewPosition.y += 2.0f * tHeight;
            tMoved = true;
        }
        return tMoved;
    }
}
