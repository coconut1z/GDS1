using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserMk1 : WeaponModule {

	public GameObject laserObj;
    private float audioTimer;
    private float timerRate;

	// Use this for initialization
	protected override void Start () {
		base.Start ();
		shootDelay = 0.8f;
		shootTime = shootDelay;
        displacement = new Vector2(0, 0.1f);
        originalSize = new Vector2(1, 1);
		stage = 3;
		weaponId = Global.LAS1;
        timerRate = 0.5f;
        audioTimer = timerRate;
        //damage = loads
	}
	
	// Update is called once per frame
	protected override void Update () {
		base.Update ();
        if (!InputKeys.isDown(InputKeys.SHOOT)) {
            if (audioTimer > 0.0f) {
                audioManager.StopSound("Laserbeam");
                audioTimer = -1.0f;
            }
            laserObj.SetActive(false);
        }
		
	}
		
	public override void Shoot(){
        if(audioTimer < -0.9f) {
            audioTimer = timerRate;
        }
        laserObj.SetActive(InputKeys.isDown(InputKeys.SHOOT) && equipped);
        if(InputKeys.isDown(InputKeys.SHOOT) && equipped) {
            //laserObj.SetActive(true); //should be able to migrate this here, but being cautious.
            audioTimer += Time.deltaTime;
            if(audioTimer >= timerRate) {//prevents audio manager spamming
                audioManager.PlayLoopedSound("Laserbeam");
                audioTimer -= timerRate;
            }
            
        }
        else {//should be unused now, but sleepy and 'aint broke' mentality.
            if(audioTimer > 0.0f) {
                audioManager.StopSound("Laserbeam");
                audioTimer = 0.0f;
            }
        }
	}

	public override int ReturnId(){
		return Global.LAS1;
	}

}
