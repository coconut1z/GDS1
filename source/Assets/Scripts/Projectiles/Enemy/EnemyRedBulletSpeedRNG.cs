using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRedBulletSpeedRNG : EnemyProjectile {

	// Use this for initialization
	protected override void Start () {
		base.Start ();
		//speed = setSpeed/45f;
		speed = 4;
        //damage = 1;
        transform.Rotate(0, 0, Random.Range(-5, 5));
    }

	// Update is called once per frame
	protected override void Update () {
		base.Update ();

	}

	public override void ProjectileMovement () {
		transform.Translate (transform.up * speed * Time.deltaTime, Space.World);
    }
}
