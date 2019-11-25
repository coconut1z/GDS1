using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrongCircleStatic : Enemy {

    public bool startShooting;

	// Use this for initialization
	protected override void Start () {
		base.Start ();
		if(difficulty == Global.RECRUIT){

		}else if(difficulty == Global.VETEREN){

		}else if(difficulty == Global.BATTLEH){

		}
		speed = 0f;
		health = 85;
		originalSpeed = speed;
        startShooting = false;
	}

	// Update is called once per frame
	protected override void Update () {
		base.Update ();
	}

	public override void Shoot(){
        if (startShooting) {
            foreach (WeaponEnemy wep in weapons) {
                wep.Shoot();
            }
        }
	}

	public override void Death(){
		Destroy (gameObject);
	}
}
