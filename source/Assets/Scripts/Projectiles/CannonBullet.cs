using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBullet : Projectile{
    private AudioManager audioManager;
    // Use this for initialization
    protected override void Start () {
		base.Start ();
		speed = 650f;
		damage = 2.9f * damageMultiplier;
        audioManager = AudioManager.instance;
        if (audioManager == null) {
            Debug.LogError("No audio manager found in scene, whoops");
        }
        audioManager.PlaySound("Cannon1");
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
