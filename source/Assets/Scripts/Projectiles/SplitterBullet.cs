using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitterBullet : Projectile {

    public GameObject flak; // Flak prefab goes in here
    private AudioManager audioManager;

    // Use this for initialization
    protected override void Start() {
        base.Start();
        speed = 10f;
        damage = 2.1f * damageMultiplier;
        audioManager = AudioManager.instance;
        if (audioManager == null) {
            Debug.LogError("No audio manager found in scene, whoops");
        }
        //audioManager.PlaySound("BulletSpam1");
    }

    // Update is called once per frame
    protected override void Update() {
        base.Update();
    }

    protected override void FixedUpdate() {
        base.FixedUpdate();
    }

    public override void ProjectileMovement() {
        rb.velocity = transform.up * speed;
    }

	/*
    protected override void OnBecameInvisible() {
		Destroy (gameObject);
    }
	*/

	void OnDestroy(){
		Vector3 testV = new Vector3(rb.transform.position.x, rb.transform.position.y, rb.transform.position.z);
        Instantiate(flak, testV, Quaternion.Euler(0, 0, transform.eulerAngles.z - 90.0f));
        Instantiate(flak, testV, Quaternion.Euler(0, 0, transform.eulerAngles.z - 45.0f));
        Instantiate(flak, testV, Quaternion.Euler(0, 0, transform.eulerAngles.z));
        Instantiate(flak, testV, Quaternion.Euler(0, 0, transform.eulerAngles.z + 45.0f));
		Instantiate(flak, testV, Quaternion.Euler(0, 0, transform.eulerAngles.z + 90.0f));
	}
}
