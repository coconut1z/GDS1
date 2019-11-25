using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleEnemyWeapon : WeaponEnemy {

	private float deg, degStep;

	// Use this for initialization
	protected override void Start () {
		base.Start ();

		if(difficulty == Global.RECRUIT){
			shootDelay = 2f;
			degStep = 30;
		}else if(difficulty == Global.VETEREN){
			shootDelay = 1.5f;
			degStep = 20;
		}else if(difficulty == Global.BATTLEH){
			shootDelay = 1f;
			degStep = 15;
		}

		shootTime = 0;
		deg = 0;
	}

	// Update is called once per frame
	protected override void Update () {
		base.Update ();
		//lookAtPlayer ();
	}

	public override void Shoot(){
		if(shootTime > shootDelay){

			// Instantiate (BULLET PREFAB, SHOOT POSITION, Z ROTATION + SPREAD IF THERE IS ANY)
			while(deg < 360){
				EnemyProjectile e = Instantiate (projectiles [0], shootPos [0].position, 
					Quaternion.Euler (0, 0, deg )).GetComponent<EnemyProjectile>();
				e.Setup (player, 2.18f, 1);
				e.SetRadius (0.1f);
				deg += degStep;
			}
			deg = 0;
			shootTime = 0;
		}
	}
}
