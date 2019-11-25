using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRedBulletSpeed : EnemyProjectile {

	// Use this for initialization
	protected override void Start () {
		base.Start ();
		//speed = setSpeed/45f;
		//speed = setSpeed;
		//damage = 1;
	}

	// Update is called once per frame
	protected override void Update () {
		base.Update ();

	}

	public override void ProjectileMovement ()
	{
		transform.Translate (transform.up * speed * Time.deltaTime, Space.World);
	}
}
