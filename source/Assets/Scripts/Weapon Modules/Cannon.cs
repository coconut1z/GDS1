﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : WeaponModule {
    // Use this for initialization
    protected override void Start () {
		base.Start ();
		shootDelay = 0.8f;
		shootTime = shootDelay;
        displacement = new Vector2(0, 0.1f);
        originalSize = new Vector2(1, 1);
		stage = 1;
		weaponId = Global.CANNON;
        //damage = loads
        audioManager = AudioManager.instance;
        if (audioManager == null) {
            Debug.LogError("No audio manager found in scene, whoops");
        }
    }
	
	// Update is called once per frame
	protected override void Update () {
		base.Update ();
	}
		
	public override void Shoot(){
		if(shootTime > shootDelay){
			// Instantiate (BULLET PREFAB, SHOOT POSITION, Z ROTATION + SPREAD IF THERE IS ANY)
			Instantiate (projectiles [0], shootPos [0].position, 
				Quaternion.Euler (0, 0, transform.eulerAngles.z));
			shootTime = 0;
            audioManager.PlaySound("Cannon");
        }
	}

	public override int ReturnId(){
		return Global.CANNON;
	}

}
