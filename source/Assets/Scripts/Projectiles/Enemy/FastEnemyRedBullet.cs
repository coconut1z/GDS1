using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastEnemyRedBullet : Projectile {

	// Use this for initialization
	protected override void Start () {
		base.Start ();
		speed = 300f;
		damage = 1;
	}
	
	// Update is called once per frame
	protected override void Update () {
		base.Update ();
	}

	protected override void FixedUpdate(){
		base.FixedUpdate ();
	}

	public override void ProjectileMovement ()
	{
		rb.velocity = transform.up * speed * Time.deltaTime;
	}
}
