using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIceBullet : Projectile{
    private AudioManager audioManager;

    // Use this for initialization
    protected override void Start () {
		base.Start ();
		speed = 500f;
		damage = 0.6f * damageMultiplier;
        audioManager = AudioManager.instance;
        if (audioManager == null) {
            Debug.LogError("No audio manager found in scene, whoops");
        }
        audioManager.PlaySound("Ice1");
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
