using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperMKII : WeaponModule {

	// Use this for initialization
	protected override void Start () {
		base.Start ();
		shootDelay = 3f;
		shootTime = shootDelay;
		spread = 0f;
		displacement = new Vector2 (0, 0.35f);
		originalSize = new Vector2 (1, 1);
		weaponId = Global.SNIPER2;
		stage = 1;
	}
	
	// Update is called once per frame
	protected override void Update () {
		base.Update ();
	}
		
	public override void Shoot(){
		if(shootTime > shootDelay){
			// Instantiate (BULLET PREFAB, SHOOT POSITION, Z ROTATION + SPREAD IF THERE IS ANY)
			Instantiate (projectiles [0], shootPos [0].position, 
                         Quaternion.Euler(0, 0, transform.eulerAngles.z + spread - 2 * Random.value * spread));
            Invoke("ShootAgain", 0.25f);
            Invoke("ShootAgain", 0.5f);
			shootTime = 0;
            audioManager.PlaySound("Sniper");

		}
	}

    void ShootAgain() {
        audioManager.PlaySound("Sniper");

        Instantiate(projectiles[0], shootPos[0].position,
                         Quaternion.Euler(0, 0, transform.eulerAngles.z + spread - 2 * Random.value * spread));
    }

	public override int ReturnId(){
		return Global.SNIPER2;
	}
}
