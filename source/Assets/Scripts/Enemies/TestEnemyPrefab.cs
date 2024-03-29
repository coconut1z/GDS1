﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemyPrefab : Enemy {

	// Use this for initialization
	protected override void Start () {
		base.Start ();
		if(difficulty == Global.RECRUIT){
			
		}else if(difficulty == Global.VETEREN){
			
		}else if(difficulty == Global.BATTLEH){
			
		}
		speed = 3f;
		health = 3;
		originalSpeed = speed;
	}
	
	// Update is called once per frame
	protected override void Update () {
		base.Update ();
	}

	public override void Shoot(){
		foreach(WeaponEnemy wep in weapons){
			wep.Shoot ();
		}
	}

	public override void Death(){
		Destroy (gameObject);
	}
}
