using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedoxBullet : Projectile{

    private float frequency = 20f;
    private float magnitude = 0.5f;
    private float lifeTime;
    private Vector3 direction;
    private Vector3 pos;
    private AudioManager audioManager;

    // Use this for initialization
    protected override void Start () {
		base.Start ();
		speed = 8f;
		damage = 0.4f * damageMultiplier;
        direction = transform.right;
        pos = transform.position;
        audioManager = AudioManager.instance;
        if (audioManager == null) {
            Debug.LogError("No audio manager found in scene, whoops");
        }
        //audioManager.PlaySound("Cannon1");
    }
	
	// Update is called once per frame
	protected override void Update () {
		base.Update ();
	}

	protected override void FixedUpdate(){
		base.FixedUpdate ();
	}

	public override void ProjectileMovement () {
        //rb.velocity = transform.up * speed * Time.deltaTime;
        lifeTime += Time.deltaTime;
        pos += transform.up * speed * Time.deltaTime;
        transform.position = pos + direction * Mathf.Sin(lifeTime * frequency) * magnitude;
    }
}
