﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoomerangWeapon : WeaponEnemy {

    private GameObject parent;

	// Use this for initialization
	protected override void Start () {
		base.Start ();
		if(difficulty == Global.RECRUIT){
			shootDelay = 3f;
		}else if(difficulty == Global.VETEREN){
			shootDelay = 1.5f;
		}else if(difficulty == Global.BATTLEH){
			shootDelay = 0.8f;
		}
		shootTime = shootDelay;
        spread = 2f;
        parent = transform.parent.gameObject;
	}

	// Update is called once per frame
	protected override void Update () {
		base.Update ();
		lookAtPlayer ();
	}

	public override void Shoot(){
		if(shootTime > shootDelay){
			// Instantiate (BULLET PREFAB, SHOOT POSITION, Z ROTATION + SPREAD IF THERE IS ANY)
			EnemyProjectile e = Instantiate (projectiles [0], shootPos [0].position, 
				Quaternion.Euler (0, 0, transform.eulerAngles.z + spread - 2 * Random.value * spread))
				.GetComponent<EnemyProjectile>();
			e.Setup (player, 2.18f, 1);
			e.SetRadius (0.1f);
            e.transform.SetParent(parent.transform);
			shootTime = 0;
		}
	}
}
