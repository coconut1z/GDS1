using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirtyHPEnemy : Enemy {

	// Use this for initialization
	protected override void Start () {
		base.Start ();
		health = 30;
		speed = 3f;
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
