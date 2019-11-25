using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class WeakBoss1 : MonoBehaviour {

	public bool left;
    public WeaponEnemy[] weapons;
    public SpriteRenderer sr;
	public GameObject wepDrop;
    public Text phaseIndicator;
    private float health;
    private bool canMove;
    private bool canShoot;
    private bool destroy;
    private float damageReceivedMultiplier;
    public GameObject sparks;
	private float invulnTimer;
	private List<string> wepIDs = new List<string>();
	private float lastClearTime;
	private float specialHitInterval;

    private DropWeaponOnDestroy drop;

	private Transform player;

    private AbilityRewardsDropList dropManager;

    public GameObject death1;
    public GameObject death2;

    // Use this for initialization
    void Start() {
		invulnTimer = 0;
        health = 30 * 5f;
		if(left){
			transform.localScale = new Vector3 (-0.606f, 0.606f, 0.606f);
		}
		Invoke ("LateStart", 0.01f);
		sr = GetComponent<SpriteRenderer> ();
		canMove = true;
		damageReceivedMultiplier = 1;
    }

	private void LateStart(){
		weapons [0].GetComponent<Stage1BossGun> ().ChangePhase (99);
	}

    // Update is called once per frame
    void Update() {
        if (damageReceivedMultiplier > 1.15f) {
            damageReceivedMultiplier = 1.15f;
        }
        if (health <= 0) {
            Death();
        }
        if (canMove) {
			if (left) {
				transform.Translate (Vector2.right * Time.deltaTime);
			} else {
				transform.Translate (Vector2.left * Time.deltaTime);
			}
				

        }
		Shoot (0);
    }

    private void Shoot(int i) {
        weapons[i].Shoot();
    }

    private void Death() {
        Instantiate(death1, transform.position, Quaternion.identity);
        Instantiate(death2, transform.position, Quaternion.identity);
        DestroySelf();
    }

    public virtual void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.name.Contains("RedoxBullet")) {
            sparks.SetActive(true);
            if (invulnTimer > 0) {
                health -= col.gameObject.GetComponent<Projectile>().damage * damageReceivedMultiplier/5f;
                damageReceivedMultiplier += 0.03f;
            }
            else {
                health -= col.gameObject.GetComponent<Projectile>().damage * damageReceivedMultiplier;
                damageReceivedMultiplier += 0.03f;
            }
            Destroy(col.gameObject);
            HitFeedback();
        }
        if (col.gameObject.tag == "PlayerBullet") {
			if(invulnTimer > 0){
				health -= col.gameObject.GetComponent<Projectile>().damage * damageReceivedMultiplier/5f;
			}else{
				health -= col.gameObject.GetComponent<Projectile>().damage * damageReceivedMultiplier;
			}
            Destroy(col.gameObject);
            HitFeedback();
        }

        if (col.gameObject.tag == "Sword")
        {
            // This fucks it up for some reason so I hardcoded damage here
            //health -= col.GetComponent<Sword>().damage;
            health -= 10.0f * damageReceivedMultiplier;
            HitFeedback();
        }

        if (col.gameObject.tag == "PlayerBulletPenetrate") {
            if (invulnTimer > 0)
            {
                health -= col.gameObject.GetComponent<Projectile>().damage * damageReceivedMultiplier / 5f;
            }
            else
            {
                health -= col.gameObject.GetComponent<Projectile>().damage * damageReceivedMultiplier;
            }
            HitFeedback();
        }
		

        if (col.gameObject.tag == "GameArea") {
            canShoot = true;
        }
    }

    public virtual void OnTriggerExit2D(Collider2D col) {
        if (col.gameObject.tag == "GameArea" && !destroy) {
            destroy = true;
            canShoot = false;
            Invoke("DestroySelf", 1.0f);
        }
    }
	/*
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Flames")
        {
            health -= collision.GetComponent<FlamethrowerHitbox>().damage * damageReceivedMultiplier;
            HitFeedback();
        }
    }
*/
    public virtual void DestroySelf() {
        Destroy(gameObject);
		//GameObject.Find ("StageManager").GetComponent<StageManager> ().LoadStage2 ();
    }


    public void HitFeedback() {
        sr.color = new Color(255, 0, 0);
        Invoke("EndFlashSprite", 0.05f);
    }

    public void EndFlashSprite() {
        sr.color = new Color(255, 255, 255);
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

}
