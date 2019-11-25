using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunMkII : WeaponModule {

    // Use this for initialization
    protected override void Start () {
		base.Start ();
		shootDelay = 0.5f;
		//shootDelay = 0.01f;
		shootTime = shootDelay;
		spread = 1f;
        displacement = new Vector2(0, 0.1f);
        originalSize = new Vector2(1, 1);
		weaponId = Global.SHOTGUN2;
		stage = 2;
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
				Quaternion.Euler (0, 0, transform.eulerAngles.z - 10));
            Instantiate(projectiles[0], shootPos[0].position,                           
                Quaternion.Euler(0, 0, transform.eulerAngles.z - 5));           //not sure if this is how we have multiple bullets
            Instantiate(projectiles[0], shootPos[0].position,                   //it tis, each instantiate is a new bullet -john
                Quaternion.Euler(0, 0, transform.eulerAngles.z + 5));
            Instantiate(projectiles[0], shootPos[0].position,
                Quaternion.Euler(0, 0, transform.eulerAngles.z + 10));
			Instantiate (projectiles [1], shootPos [0].position, 
				Quaternion.Euler (0, 0, transform.eulerAngles.z));
            Instantiate(projectiles[0], shootPos[0].position,                   //it tis, each instantiate is a new bullet -john
                Quaternion.Euler(0, 0, transform.eulerAngles.z + 2.5f));
            Instantiate(projectiles[0], shootPos[0].position,
                Quaternion.Euler(0, 0, transform.eulerAngles.z - 2.5f));
            shootTime = 0;
            audioManager.PlaySound("Shotgun");
        }
	}

	public override int ReturnId(){
		return Global.SHOTGUN2;
	}
}
