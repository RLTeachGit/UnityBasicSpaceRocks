using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : FakePhysics {


    protected override void Start() {
        base.Start();   //Call FakePhysics Start()
        Destroy(gameObject, 2.0f);
    }

    public  void    FireBullet(Vector3 vPosition, Vector3 vDirection) {
        transform.position = vPosition;
        mVelocity = vDirection.normalized * Speed;
    }

    protected override void DoMove() {
        transform.position += mVelocity * Time.deltaTime;      //Work out new position
    }
}
