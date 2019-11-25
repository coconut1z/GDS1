using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HellfireRocket: Projectile{
    private AudioManager audioManager;
    public GameObject explosionPrefab;
    // Use this for initialization
    protected override void Start () {
		base.Start ();
		speed = 100.0f;
		damage = 3f * damageMultiplier;
        audioManager = AudioManager.instance;
        if (audioManager == null) {
            Debug.LogError("No audio manager found in scene, whoops");
        }
        audioManager.PlaySound("MissileFire1"); //change to missile firing sound.
    }
	
	// Update is called once per frame
	protected override void Update () {
		base.Update ();
	}

	protected override void FixedUpdate(){
		base.FixedUpdate ();
	}

	public override void ProjectileMovement (){
		rb.velocity = transform.up * speed * Time.deltaTime;
        speed *= (1+(5*Time.deltaTime));
	}
    protected override void OnDestroy() {
        base.OnDestroy(); //spawns a particle on hit if there is one provided.
        Instantiate(explosionPrefab, transform.position, transform.rotation); //instantiates the explosion at the same position, but without making it a child.
        transform.DetachChildren();
    }
}
