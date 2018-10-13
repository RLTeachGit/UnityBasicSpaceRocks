using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FakePhysics : MonoBehaviour {

    protected Rigidbody2D mRB2;       //We only want safe classes messing with this, as we control Physics

    protected SpriteRenderer mSR;     //May need this later


    [SerializeField]
    protected float Speed=1.0f;

    [SerializeField]
    protected float RotationSpeed = 360.0f;

    protected Vector3 mVelocity = Vector3.zero;

    BoxCollider2D mTemp;

    protected Collider2D mC2D; // this works as all 2D colliders are based on this

    // Use this for initialization
    protected virtual void Start () {

        mSR = gameObject.GetComponent<SpriteRenderer>(); //Grab SR assigned in IDE
        Debug.Assert(mSR != null, "Error:Missing SpriteRenderer");

        mC2D = gameObject.GetComponent<Collider2D>();
        Debug.Assert(mC2D != null, "Error:Missing Collider2D");
        mC2D.isTrigger = true;     //Set it to trigger in code

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


    //DefaultMove Does nothing
    protected virtual   void  DoMove() {

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

    //We are using triggers, so this gets called on overlap
   private void OnTriggerEnter2D(Collider2D vCollision) {
        FakePhysics tOtherObject = vCollision.gameObject.GetComponent<FakePhysics>();
        Debug.Assert(tOtherObject != null, "Other Object is not FakePhysics compatible");
        ObjectHit(tOtherObject);
    }


    //virtual functions can be overridded in derived classes
    protected   virtual void    ObjectHit(FakePhysics vOtherObject) {
        Debug.LogFormat("{0} hit by {1}",name, vOtherObject.name);      //Just print it for now
    }
}
