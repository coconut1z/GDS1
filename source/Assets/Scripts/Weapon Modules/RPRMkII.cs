using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPRMkII : WeaponModule {

    private bool shootSequence;

	// Use this for initialization
	protected override void Start () {
		base.Start ();
        shootSequence = false;
		//shootDelay = 0.14f;
		shootDelay = 0.12f;
		shootTime = shootDelay;
		spread = 2f;
        displacement = new Vector2(0, 0.1f);
        originalSize = new Vector2(1, 1);
		weaponId = Global.RPR2;
		stage = 3;
	}
	
	// Update is called once per frame
	protected override void Update () {
		base.Update ();
	}
		
	public override void Shoot(){
		if(shootTime > shootDelay){
            // Instantiate (BULLET PREFAB, SHOOT POSITION, Z ROTATION + SPREAD IF THERE IS ANY)  
            if (shootSequence) {
                shootSequence = false;
                Instantiate(projectiles[0], shootPos[0].position,
                    Quaternion.Euler(0, 0, transform.eulerAngles.z + spread - 2 * Random.value * spread));
                shootTime = 0;
                audioManager.PlaySound("Minigun");
            }
            else {
                shootSequence = true;
                Instantiate(projectiles[0], shootPos[1].position,
                    Quaternion.Euler(0, 0, transform.eulerAngles.z + spread - 2 * Random.value * spread));
                shootTime = 0;
                audioManager.PlaySound("Minigun");
            }
		}
	}

	public override int ReturnId(){
		return Global.RPR2;
    }
}
