using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiralEnemyWeapon : WeaponEnemy {

	private float degStep;
	private float turnTime, turnDelay;

	// Use this for initialization
	protected override void Start () {
		base.Start ();
		if(difficulty == Global.RECRUIT){
			shootDelay = 0.1f;
		}else if(difficulty == Global.VETEREN){
			shootDelay = 0.066f;
		}else if(difficulty == Global.BATTLEH){
			shootDelay = 0.033f;

		}
		degStep = 5;
		shootTime = shootDelay;
		turnTime = 0;
		turnDelay = 0.01f;
	}

	// Update is called once per frame
	protected override void Update () {
		base.Update ();
		degStep += Time.deltaTime;
		transform.Rotate (new Vector3 (0, 0, degStep));
		//lookAtPlayer ();
	}

	public override void Shoot(){
		if(shootTime > shootDelay){
			EnemyProjectile e = Instantiate (projectiles [0], shootPos [0].position, 
				Quaternion.Euler (0, 0, transform.eulerAngles.z + spread - 2 * Random.value * spread))
				.GetComponent<EnemyProjectile>();
			e.Setup (player, 2.18f, 1);
			e.SetRadius (0.1f);
			shootTime = 0;
		}
	}
}
