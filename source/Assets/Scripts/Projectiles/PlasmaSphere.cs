using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//carries the visual effect for the plasma bullet proj. No gameplay effect; purely visual. Rotates the 3d Sphere here.

public class PlasmaSphere : MonoBehaviour {
    int frameCountdown;
    int pauseFrames;
	// Use this for initialization
	void Start () {
        pauseFrames = 5;
        frameCountdown = pauseFrames;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void FixedUpdate() {
        frameCountdown--;
        if (frameCountdown <= 0) { //frame based, since it's just a visual effect. No gameplay.
            transform.rotation = Random.rotation; //this is a function?! why not, lets test.
            frameCountdown = pauseFrames;
        }
    }
}
