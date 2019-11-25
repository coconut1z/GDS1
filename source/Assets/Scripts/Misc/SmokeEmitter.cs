using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeEmitter : MonoBehaviour {
    private float lastEmitTime;
    public float emitFreq = 0.1f;
    public GameObject smoke = null;

	// Use this for initialization
	void Start () {
        lastEmitTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
        checkTime();
	}

    private void checkTime() {
        if(lastEmitTime + emitFreq < Time.time) {
            Instantiate(smoke, transform.position, Quaternion.Euler(0, 0, transform.eulerAngles.z));
            lastEmitTime = Time.time;
        }
    }
}
