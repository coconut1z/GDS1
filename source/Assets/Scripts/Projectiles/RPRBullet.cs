using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPRBullet : Projectile{

    private float lifespan;
    private float spawnTime;
    private AudioManager audioManager;

    // Use this for initialization
    protected override void Start () {
		base.Start ();
		speed = 650f;
		damage = 2f * damageMultiplier;
        spawnTime = Time.time;
        lifespan = 2.0f;
        audioManager = AudioManager.instance;
        if (audioManager == null) {
            Debug.LogError("No audio manager found in scene, whoops");
        }
    }
	
	// Update is called once per frame
	protected override void Update () {
		base.Update ();
        checkLifespan();
    }

	protected override void FixedUpdate(){
		base.FixedUpdate ();
	}

	public override void ProjectileMovement ()
	{
		rb.velocity = transform.up * speed * Time.deltaTime;
	}

    private void checkLifespan() {
        if (Time.time > spawnTime + lifespan) {
            Destroy(gameObject);
        }
    }
}
