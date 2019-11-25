using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour {
	public WeaponEnemy[] weapons;
	public virtual float speed { get; set;}
	public virtual float originalSpeed { get; set;}
    public virtual float speedMultiplier { get; set; }
	public virtual float health {get; set;}
    public virtual float damageReceivedMultiplier { get; set; }
    public virtual float freezeTimer { get; set; }
	public virtual bool canMove { get; set;}
	public virtual bool canShoot { get; set;}
	public virtual bool destroy { get; set;}
	public SpriteRenderer sr;
    public bool frozen;
    private bool goUpdateFrozenColour;
	public Color originalColor;
	protected int difficulty;
	private List<string> wepIDs;
    private float lastClearTime;
    private float specialHitInterval;
    //Ghetto weapon drop scripts
    public GameObject weaponManager;
	public List<GameObject> noHit;
    public GameObject sparks;
    public GameObject ps;
	public bool visible;
    public GameObject deathPs;

	// Use this for initialization
	protected virtual void Start () {
		sparks = (GameObject)(Resources.Load("Sparks"));
        SetUpSparks();
        speed = 0;
		originalSpeed = 0;
		health = 1;
        freezeTimer = 0;
        frozen = false;
        goUpdateFrozenColour = false;
        speedMultiplier = 1;
        damageReceivedMultiplier = 1;
        specialHitInterval = 0.2f; //0.2s between special weapon hits (eg, boomerang, blitzgun)
		wepIDs = new List<string>();
		canMove = true;
		canShoot = false;
		destroy = false;
		visible = false;
		difficulty = Global.Difficulty;

        weaponManager = GameObject.Find("WeaponManager");
		if(GetComponent<SpriteRenderer>() != null){
			sr = GetComponent<SpriteRenderer>();
			originalColor = sr.color;
		}
	}

	// Update is called once per frame
	protected virtual void Update () {
        if (speedMultiplier < 0.4f) {
            speedMultiplier = 0.4f;
        }
        if (damageReceivedMultiplier > 1.45f) {
            damageReceivedMultiplier = 1.45f;
        }
        ps.transform.position = gameObject.transform.position;
        if (health <= 0){
            DropWeapon();
			Death ();
		}
		if(canMove){
			transform.Translate (new Vector2 (0, speed * speedMultiplier * Time.deltaTime));
		}
		if(canShoot){
			Shoot ();
		}
        if (frozen) {
            Freeze();
        }
        if (health <= 0) {
            if (deathPs)
            {
                GameObject psObj = Instantiate(deathPs, this.transform.position, Quaternion.identity);
            }
        }
	}

	// Implement how an enemy shoots
	public abstract void Shoot();

	// Implement death action of enemy
	public abstract void Death ();

	public virtual void OnTriggerEnter2D(Collider2D col){
        if (col.gameObject.name.Contains("RedoxBullet")) {
            ps.SetActive(true);
            health -= col.gameObject.GetComponent<Projectile>().damage * damageReceivedMultiplier;
            damageReceivedMultiplier += 0.15f;
            Destroy(col.gameObject);
            HitFeedback();
            return;
        }
        if (col.gameObject.name.Contains("PlayerIceBullet")) {
            freezeTimer = 1.5f;
            StartFreeze();
            frozen = true;
            health -= col.gameObject.GetComponent<Projectile>().damage * damageReceivedMultiplier;
            Destroy(col.gameObject);
            HitFeedback();
            return;
        }
        if (col.gameObject.tag == "PlayerBullet"){
			// EXPENSIVE
			health -= col.gameObject.GetComponent<Projectile> ().damage * damageReceivedMultiplier;
			Destroy (col.gameObject	); //Tells the bullet's projectile superclass script to off itself.
			HitFeedback();
		}
        if (col.gameObject.tag == "Sword")
        {
            // This fucks it up for some reason so I hardcoded damage here
            //health -= col.GetComponent<Sword>().damage;
            health -= 10.0f * damageReceivedMultiplier;
            HitFeedback();
        }
		if (col.gameObject.tag == "PlayerBulletPenetrate")
		{
			if(noHit.Count != 0){
				for(int i = 0; i < noHit.Count; i++){
					if(noHit[i] == null){
						noHit.RemoveAt (i);
						i--;
					}
				}
                List<GameObject> t = new List<GameObject>();
				foreach(GameObject g in noHit){
					if(g != col.gameObject){
                        t.Add(col.gameObject);
                        HitFeedback();
						//noHit.Add (col.gameObject);
						health -= col.gameObject.GetComponent<Projectile>().damage * damageReceivedMultiplier;
					}
				}
                for (int i = 0; i < t.Count; i++){
                    noHit.Add(t[i]);
                }
			}else{
                //this seems bad! What if bullets need to pass over/through something with a collider?
                //removing for now. -J

				noHit.Add (col.gameObject);
				health -= col.gameObject.GetComponent<Projectile>().damage * damageReceivedMultiplier;
                HitFeedback();
			}
		}

		ShootOnEnter (col);

	}

	protected virtual void ShootOnEnter(Collider2D col){
		// New method for in bounds
		if(col.gameObject.tag == "GameArea"){
			canShoot = true;
		}
	}


    // New method for out of bounds
    public virtual void OnTriggerExit2D(Collider2D col){
		if(col.gameObject.tag == "GameArea" && !destroy){
			destroy = true;
			canShoot = false;
			Invoke ("DestroySelf", 1.0f);
		}
	}

	public virtual void DestroySelf(){
		Destroy (gameObject);
	}

    //Ghetto weapon drop scripts
    public void DropWeapon() {
        //print("Enemy Killed");
        int chance = UnityEngine.Random.Range(1, 10);
        if (chance == 9) {
            weaponManager.GetComponent<WeaponManagerScript>().DropRandomWeapon(this.transform.position);
        }
    }

	public void HitFeedback(){
		sr.color = new Color (255.0f, 0.0f, 0.0f);
		Invoke ("EndFlashSprite", 0.05f);
	}

	public void EndFlashSprite(){
		sr.color = originalColor;
	}

    public void Freeze() {
        freezeTimer -= Time.deltaTime;
        if(freezeTimer <= 0) {
            frozen = false;
            sr.color = originalColor;
            speedMultiplier = 1f;
        }
    }

    private void StartFreeze() {
        speedMultiplier -= 0.1f;
        sr.color = new Color(originalColor.r * speedMultiplier, originalColor.g * speedMultiplier, originalColor.b, 1f);
    }

    public void SetUpSparks() {
        ps = Instantiate(sparks, new Vector3(0, 0, 0), Quaternion.identity);
        ps.transform.SetParent(gameObject.transform);
        ps.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        ps.SetActive(false);
    }

    public void DamageEnemy(float damage, string wepID) {
        if(Time.time > lastClearTime + specialHitInterval) {//if its been long enough...
            wepIDs.Clear();//blank the list!
            lastClearTime = Time.time;//reset last time.
        }
        foreach (string listWepID in wepIDs) { //check each member of the list.
            if(wepID == listWepID) {//if the list already has an ID in it...
                return;//piss off, you've hit me recently!
            }
        }//so if you don't return...
        health = health-damage; //guess it's a fresh hit. deal the damage... //aware -= is a thing. paranoid atm.
        HitFeedback(); //flash the sprite...
        wepIDs.Add(wepID);//and put the weapon on the naughty list.
    }
	// OLD Methods
	/*
	private void OnBecameInvisible(){
		Destroy (gameObject);
	}
	*/

	private void OnBecameVisible(){
		visible = true;
	}

	private void OnDestroy()
	{

        //ParticleSystem ps = psObj.GetComponent<ParticleSystem>();
        //ps.Play();
	}

}
