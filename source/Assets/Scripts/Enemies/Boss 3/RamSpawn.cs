using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RamSpawn : Enemy {
    float enterDelay;
    bool canCharge;
    bool charging;
    bool ramming;
    bool charged;
    bool shake;
    PlayerController player;
    Transform pTransform;
    float startTime;
    float chargeDuration;
    float damage;
    SpriteRenderer flame;
    Animator anim;

	private float alpha;


    // Use this for initialization
    protected override void Start() {
        base.Start();
        speed = 1.5f;
        health = 15;
        damage = 1.0f;
        enterDelay = 0f; //delay before charging on entering visible area.
        originalSpeed = speed;
        charging = false;
        ramming = false;
        charged = false;
        shake = false;
        canCharge = false;
        player = null; //will only fetch if it hits player :)
		pTransform = GameObject.FindGameObjectWithTag("Player").transform;
        chargeDuration = 1.0f;
        flame = GetComponentsInChildren<SpriteRenderer>()[1];
        //anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    protected override void Update() {
        base.Update();
        FacePlayer();
        CheckRange();
        ChargeTimer();
        SpeedIncrement();
		alpha += Time.deltaTime;
		if(alpha >= 1){
			alpha = 1;
		}
		sr.color = new Color (sr.color.r, sr.color.g, sr.color.b, alpha);
    }

    private void SpeedIncrement() {
        if (!ramming) {
            return;
        }
        if (Time.time > startTime + 0.1f) { //every .1s increment speed.
            speed = speed + 1.0f;
            startTime = Time.time;
        }
    }

    private void ChargeTimer() { 
        if (!charging || charged) {
            return;
        }

        if (shake) {//temp hacky brute-force frame-dependent shake animation just while charging.
            transform.position = new Vector2(transform.position.x + 0.02f, transform.position.y + 0.02f);
            shake = false;
        }
        else {
            transform.position = new Vector2(transform.position.x - 0.02f, transform.position.y - 0.02f);
            shake = true;
        }

        if (Time.time > startTime + chargeDuration) {
            ramming = true;
            charged = true;
            flame.enabled = true;
            startTime = Time.time;
        }
    }

    private void CheckRange() {
        /*float range = Vector3.Distance(transform.position, pTransform.position);
        if(range <= 6 && !charging) {
            charging = true; //flag to begin charging.
            startTime = Time.time; //set startTime to current time.
            //anim.SetBool("isCharging", true);
            speed = 0.0f;
        } //no longer needed, was used to attack on entering player range.
        */
        if (canCharge && !charging) {
            enterDelay -= Time.deltaTime;
            if (enterDelay <= 0.0f) {
                charging = true; //flag to begin charging.
                startTime = Time.time; //set startTime to current time.
                speed = 0.0f;
            }
        }
    }

    private void FacePlayer() {
        if (ramming) {
            return;
        }
       /* float AngleRadToMouse = Mathf.Atan2(
            pTransform.position.y - transform.position.y,
            pTransform.position.x - transform.position.x
        );
        float AngleToDeg = (180 / Mathf.PI) * AngleRadToMouse - 90;
        transform.rotation = Quaternion.Euler(0, 0, AngleToDeg);
	*/
		Vector3 direction = pTransform.position - transform.position;
		float angle = Mathf.Atan2 (direction.y, direction.x) * Mathf.Rad2Deg - 90;
		Quaternion lookRotation = Quaternion.AngleAxis (angle, Vector3.forward);
		transform.rotation = Quaternion.Slerp (transform.rotation, lookRotation, Time.deltaTime * 1.3f);
    }

    public override void Shoot() {
        foreach (WeaponEnemy wep in weapons) {
            wep.Shoot();
        }
    }

    public override void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.tag == "PlayerBullet") {
            // EXPENSIVE
            health -= col.gameObject.GetComponent<Projectile>().damage;
            Destroy(col.gameObject); //Tells the bullet's projectile superclass script to off itself.
        }
        if (col.gameObject.tag == "PlayerBulletPenetrate") {
            // EXPENSIVE
            health -= col.gameObject.GetComponent<Projectile>().damage;
        }
        if (col.gameObject.tag == "Player") {
            player = col.GetComponent<PlayerController>();
            player.DamagePlayer(damage);
            Death();
            
        }
        if (col.gameObject.tag == "GameArea") {
            canCharge = true;
        }
    }

    public override void Death() {
        //Debug.Log("Ramship deleted with " + health + "hp");
        Destroy(gameObject);
    }

    protected virtual void OnBecameInvisible() {
        if (ramming) {
            Destroy(gameObject);
            //Debug.Log("Ramship deleted while offscreen");
        }
    }
}
