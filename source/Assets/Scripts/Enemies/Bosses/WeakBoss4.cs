using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WeakBoss4 : MonoBehaviour {

    public WeaponEnemy[] weapons;
    public SpriteRenderer sr;
    public float health;
    private bool canMove;
    private bool canShoot;
    private bool destroy;
    private int phase;
	public float phase1, phase2, phase3, phase4, phase5, phase6, phase7, phase8;
	public float phase8p1, phase8p2, phase8p3, phase8p4, phase8p5;
	private bool phase8Move;
	private float invulnTimer;
	private float noshootTimer;
	private Transform player;
	private PlayerController playerController;
    private float damageReceivedMultiplier;
    public GameObject sparks;
    private List<string> wepIDs = new List<string>();
    private float lastClearTime;
    private float specialHitInterval = 0.2f;
	public WeakBoss4 wb;

    private AbilityRewardsDropList dropManager;

    public GameObject death1;
    public GameObject death2;

    // Use this for initialization
    void Start() {
		health = phase1 = 350 * 3.5f;
		phase8 = 350 * 3.5f;
		phase8p1 = 50 * 3.5f;
		phase8p2 = 100 * 3.5f;
		phase8p3 = 150 * 3.5f;
		phase8p4 = 200 * 3.5f;
		phase8p5 = 250 * 3.5f;
		phase8Move = false;
        canMove = true;
        canShoot = true;
        destroy = false;
        phase = 7;
        damageReceivedMultiplier = 1;
        sparks.SetActive(false);
        sr = GetComponent<SpriteRenderer>();
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		Invoke ("LateStart", 0.01f);
    }

	private void LateStart(){
		phase = 8;
		phase8Move = true;
		noshootTimer = invulnTimer = 0f;
		weapons [0].GetComponent<Stage2BossGun1> ().ChangePhase (7);
		weapons [1].GetComponent<Stage2BossGun2> ().ChangePhase (3);
		weapons [2].GetComponent<Stage2BossGun2> ().ChangePhase (3);
		weapons [3].GetComponent<Stage2BossGun2> ().ChangePhase (3);
		weapons [4].GetComponent<Stage2BossGun3> ().ChangePhase (2);
	}


    // Update is called once per frame
    void Update() {
		if(transform.position.y > 6){
			transform.Translate (new Vector2 (0, -1) * Time.deltaTime);
		}
        if (damageReceivedMultiplier > 1.15f) {
            damageReceivedMultiplier = 1.15f;
        }
        //UpdateHealthBar();
        if (invulnTimer > 0){
			invulnTimer -= Time.deltaTime;
		}

        if (health <= 0) {
            Death();
        }
        if (canMove) {
            //transform.Translate (new Vector2 (0, speed * Time.deltaTime));
        }
        if (canShoot && noshootTimer <= 0) {
			if (phase == 1 || phase == 2 || phase == 4 || phase == 6) {
                Shoot (0);
			}else if (phase == 3 || phase == 5){
				Shoot (1);
				Shoot (2);
				Shoot (3);
				Shoot (0);
			}else if(phase == 7){
				Shoot (4);
			}else if(phase == 8){
				Shoot (1);
				Shoot (2);
				Shoot (3);
				Shoot (0);
				if(health < phase8 - phase8p5){
					//Shoot (4);
				}
			}

		}else{
			noshootTimer -= Time.deltaTime;
		}
    }

    private void Shoot(int i) {
        weapons[i].Shoot();
    }

    private void Death() {

		if(!Global.stationsSpawned){
			Global.stationsSpawned = true;
			SceneManager.LoadSceneAsync ("F4", LoadSceneMode.Additive);
		}

        Instantiate(death1, this.transform.position, Quaternion.identity);
        Instantiate(death2, this.transform.position, Quaternion.identity);

        DestroySelf();
    }

    public virtual void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.name.Contains("RedoxBullet")) {
            sparks.SetActive(true);
            if (invulnTimer > 0) {
                health -= col.gameObject.GetComponent<Projectile>().damage * damageReceivedMultiplier / 5f;
				wb.health -= col.gameObject.GetComponent<Projectile>().damage * damageReceivedMultiplier / 5f;
                damageReceivedMultiplier += 0.03f;
            }
            else {
                health -= col.gameObject.GetComponent<Projectile>().damage * damageReceivedMultiplier;
				wb.health -= col.gameObject.GetComponent<Projectile>().damage * damageReceivedMultiplier;
                damageReceivedMultiplier += 0.03f;
            }
            Destroy(col.gameObject);
            HitFeedback();
        }
        if (col.gameObject.tag == "PlayerBullet") {
			if(invulnTimer > 0){
				health -= col.gameObject.GetComponent<Projectile>().damage * damageReceivedMultiplier/5f;
				wb.health -= col.gameObject.GetComponent<Projectile>().damage * damageReceivedMultiplier/5f;
			}else{
				health -= col.gameObject.GetComponent<Projectile>().damage * damageReceivedMultiplier;
				wb.health -= col.gameObject.GetComponent<Projectile>().damage * damageReceivedMultiplier;
			}

            Destroy(col.gameObject);
            HitFeedback();
        }

        if (col.gameObject.tag == "Sword")
        {
            // This fucks it up for some reason so I hardcoded damage here
            //health -= col.GetComponent<Sword>().damage;
            health -= 10.0f * damageReceivedMultiplier;
			wb.health -= 10.0f * damageReceivedMultiplier;
            HitFeedback();
        }

        if (col.gameObject.tag == "PlayerBulletPenetrate"){
            if (invulnTimer > 0)
            {
				health -= col.gameObject.GetComponent<Projectile>().damage * damageReceivedMultiplier / 5f;
				wb.health -= col.gameObject.GetComponent<Projectile>().damage * damageReceivedMultiplier / 5f;
            }
            else
            {
                health -= col.gameObject.GetComponent<Projectile>().damage * damageReceivedMultiplier;
				wb.health -= col.gameObject.GetComponent<Projectile>().damage * damageReceivedMultiplier;
            }
            HitFeedback();
        }
		

        if (col.gameObject.tag == "GameArea") {
            canShoot = true;
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
    }*/

    //public virtual void OnTriggerExit2D(Collider2D col) {
    //    if (col.gameObject.tag == "GameArea" && !destroy) {
    //        destroy = true;
    //        canShoot = false;
    //        Invoke("DestroySelf", 1.0f);
   //     }
   // }

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

    //I need to make a separate script for this, as well as a method to calculate stuff so don't read into this too much
    //But basically its (((health - currentMin) * (newMax - newMin) / (currentMax - currentMin) + newMin
    //Where newMax and newMin is the amount of health for the current phase
    /*
    private void UpdateHealthBar()
    {
        float currentHealth = health;
		if (health <= phase1 && health > phase2) {
			currentHealth = (health - phase2) / (phase1 - phase2);
            healthBarStageOne.fillAmount = currentHealth;
        }
        else if (health <= phase2 && health > phase3) {
            healthBarStageOne.fillAmount = 0;
			currentHealth = (health - phase3) / (phase2 - phase3);
            healthBarStageTwo.fillAmount = currentHealth;
        }
        else if (health <= phase3 && health > phase4) {
            healthBarStageTwo.fillAmount = 0;
			currentHealth = (health - phase4) / (phase3 - phase4);
            healthBarStageThree.fillAmount = currentHealth;
        }
        else if (health <= phase4) {
            healthBarStageThree.fillAmount = 0;
			currentHealth = (health )  / (phase4);
            healthBarStageFour.fillAmount = currentHealth;
        }
    }
    */
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
