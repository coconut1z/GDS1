using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossaCoreScript : MonoBehaviour {

	public SpriteRenderer sr;
    public WeaponEnemy[] weapons;
    public bool core;
    public float health; //hp
    public bool dying; //if Bossa is dying
    private SpriteRenderer spriteR;
    private float colourLastTime;
    private float damageReceivedMultiplier;
    public GameObject sparks;
    private List<string> wepIDs;
    private float lastClearTime;
    private float specialHitInterval = 0.2f;

    public GameObject death1;
    public GameObject death2;

    // Use this for initialization
    void Start () {
        wepIDs = new List<string>();
        health = 40.0f;
        damageReceivedMultiplier = 1;
        sparks.SetActive(false);
        spriteR = GetComponent<SpriteRenderer>();
        colourLastTime = 0.0f;
		sr = GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
        if (damageReceivedMultiplier > 1.45f) {
            damageReceivedMultiplier = 1.45f;
        }
        checkOwnHealth(); //check if component should be marked for death.
        shoot();
        dyingFlash(); //flash colours while dying.
	}

    private void shoot() {
        if (dying) {
            return;
        }
        foreach (WeaponEnemy wep in weapons) {
            wep.Shoot();
    }
}

    private void dyingFlash() {
        if (!dying) {//if not dying;
            return;//leave.
        }//otherwise;
        float cv = (Time.time - colourLastTime)/0.6f;//multiplier, do not exceed 1.0f.
        spriteR.color = new Color(1.0f, 1.0f - cv, 1.0f - cv, 0.2f);//sets colour according to time.
        if(Time.time - colourLastTime  > 0.4f) {//interval between flash resets
            colourLastTime = Time.time;
        }
    }

    private void checkOwnHealth() {
        if (health <= 0 && !dying) {
            //Death();
            //begin death sequence

            Instantiate(death1, transform.position, Quaternion.identity);
            //Instantiate(death2, transform.position, Quaternion.identity);

            sparks.SetActive(false);
            GetComponent<CircleCollider2D>().enabled = false; //remove collider so bullets pass through.
            GetComponent<CapsuleCollider2D>().enabled = false; //remove collider so bullets pass through.
            dying = true; //mark component for death.
            spriteR.color = new Color(1.0f, 1.0f, 1.0f, 0.2f); //turns component transparent to show it as non-shootable.
        }
    }

    private void Shoot() {
        foreach (WeaponEnemy wep in weapons) {
            wep.Shoot();
        }
    }

    private void Death() {

        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.name.Contains("RedoxBullet")) {
            sparks.SetActive(true);
            health -= col.gameObject.GetComponent<Projectile>().damage * damageReceivedMultiplier;
            damageReceivedMultiplier += 0.05f;
            HitFeedback();
        }
        if (col.gameObject.tag == "PlayerBullet") {
            // EXPENSIVE
            health -= col.gameObject.GetComponent<Projectile>().damage * damageReceivedMultiplier;
            Destroy(col.gameObject); //Tells the bullet's projectile superclass script to off itself.
			HitFeedback();
        }
        if (col.gameObject.tag == "PlayerBulletPenetrate") {
            // EXPENSIVE
            health -= col.gameObject.GetComponent<Projectile>().damage * damageReceivedMultiplier;
            HitFeedback();
        }

        if (col.gameObject.tag == "Sword")
        {
            // This fucks it up for some reason so I hardcoded damage here
            //health -= col.GetComponent<Sword>().damage;
            health -= 10.0f * damageReceivedMultiplier;
            HitFeedback();
        }
    }
	/*
	private void OnTriggerStay2D(Collider2D collision)
	{
        if (collision.tag == "Flames") {
            health -= collision.GetComponent<FlamethrowerHitbox>().damage * damageReceivedMultiplier;
            HitFeedback();
        }
	}
*/
	public float getHealth() {
        return health;
    }

	public void HitFeedback(){
		sr.color = new Color (255, 0, 0);
		Invoke ("EndFlashSprite", 0.05f);
	}

	public void EndFlashSprite(){
		sr.color = new Color (255, 255, 255);
	}

    public void DamageEnemy(float damage, string wepID) {
        if (Time.time > lastClearTime + specialHitInterval) {//if its been long enough...
            wepIDs.Clear();//blank the list!
            lastClearTime = Time.time;//reset last time.
        }
        foreach (string listWepID in wepIDs) { //check each member of the list.
            if (wepID == listWepID) {//if the list already has an ID in it...
                return;//piss off, you've hit me recently!
            }
        }//so if you don't return...
        health = health - damage; //guess it's a fresh hit. deal the damage... //aware -= is a thing. paranoid atm.
        HitFeedback(); //flash the sprite...
        wepIDs.Add(wepID);//and put the weapon on the naughty list.
    }
}
