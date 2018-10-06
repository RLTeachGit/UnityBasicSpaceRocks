﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShip : FakePhysics {

    //The rock just moves by simply adding the velocity & rotation
    protected override void DoMove() {
        float tThrust = Input.GetAxis("Vertical") * Speed;       //Get Thrust
        float tRotate = Input.GetAxis("Horizontal") * RotationSpeed; //Get Rotation
        transform.Rotate(0, 0, tRotate  * Time.deltaTime);    //Rotate ship
        mVelocity += Quaternion.Euler(0, 0, transform.rotation.z) * transform.up * tThrust * Speed; //Non mass velocity
        transform.position += mVelocity * Time.deltaTime; //Work out new position
    }
}
