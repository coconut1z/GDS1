using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlasmaBallBullet : Projectile{
    private float lifespan;


	// Use this for initialization
	protected override void Start () {
		base.Start ();
        speed = 160.0f; //set to ~100 when done testing
		damage = 8.0f * damageMultiplier; //high damage, slow movement.
        //no longer needed; ball has no collider and thus does no damage.
        lifespan = 8.0f;
	}
	
	// Update is called once per frame
	protected override void Update () {
		base.Update ();
        LifespanCheck();
	}

    private void LifespanCheck() {
        lifespan -= Time.deltaTime;//wind down the remaining life.
        if (lifespan <= 0.0f) {
            DestroySelf();
        }
    }

    protected override void FixedUpdate(){
		base.FixedUpdate ();
	}

	public override void ProjectileMovement ()
	{
		rb.velocity = transform.up * speed * Time.deltaTime;
	}
}
