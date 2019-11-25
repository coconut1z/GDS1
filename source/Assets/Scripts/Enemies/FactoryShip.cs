using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactoryShip : Enemy {

	// Use this for initialization
	protected override void Start () {
		base.Start ();
		health = 60;
		speed = 1f;
		originalSpeed = speed;
	}

	// Update is called once per frame
	protected override void Update () {
		base.Update ();
        Debug.Log(health);
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
