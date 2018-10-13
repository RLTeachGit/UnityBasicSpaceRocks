using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFO : FakePhysics {


    private float mTimeout = 0.0f;
    // Use this for initialization

    protected override void DoMove() {
        if(mTimeout<=0.0f) {
            mTimeout = Random.Range(1.0f, 10.0f);
            mVelocity = new Vector3(Random.Range(-Speed, Speed)
                                        , Random.Range(-Speed, Speed)
                                        , 0);
        } else {
            mTimeout -= Time.deltaTime;
        }
        base.DoMove();      //Call Base class which will apply Velocity
    }
}
