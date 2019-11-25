using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunBullet : Projectile{

	// Use this for initialization
	protected override void Start () {
		base.Start ();
		speed = 600f;
        damage = 0.35f * damageMultiplier;
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
