using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plasmacoil : WeaponModule {

	// Use this for initialization
	protected override void Start () {
		base.Start ();
		shootDelay = 1.5f;
		shootTime = shootDelay;
		spread = 1f;
        displacement = new Vector2(0, 0.1f);
        originalSize = new Vector2(1, 1);
        weaponId = Global.PLASMACOIL;
        stage = 2;
	}
	
	// Update is called once per frame
	protected override void Update () {
		base.Update ();
	}
		
	public override void Shoot(){
		if(shootTime > shootDelay){
			// Instantiate (BULLET PREFAB, SHOOT POSITION, Z ROTATION + SPREAD IF THERE IS ANY)
            Instantiate(projectiles[0], shootPos[0].position,                   //it tis, each instantiate is a new bullet -john
                Quaternion.Euler(0, 0, transform.eulerAngles.z));
            shootTime = 0;
            audioManager.PlaySound("Plasmacoil");
		}
	}

	public override int ReturnId(){
        return Global.PLASMACOIL;
    }
}
