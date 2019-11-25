using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * Explosion that spawns after a delay when firing the satellite cannon
 */
public class SatelliteCannonBlast : Projectile{
    private AudioManager audioManager;

    // Use this for initialization
    protected override void Start () {
		base.Start ();
		speed = 0f;
		damage = 10f * damageMultiplier;
        Invoke("BlastEnd", 1f);
        audioManager = AudioManager.instance;
        if (audioManager == null) {
            Debug.LogError("No audio manager found in scene, whoops");
        }
        audioManager.PlaySound("ExplosionLarge2");
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
		//rb.velocity = transform.up * speed * Time.deltaTime;
	}

    void BlastEnd() 
    {
        Destroy(gameObject);
    }
}
