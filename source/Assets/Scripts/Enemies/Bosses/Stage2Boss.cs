using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stage2Boss : MonoBehaviour {

    public WeaponEnemy[] weapons;
    public SpriteRenderer sr;
	public GameObject wepDropActual;
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
	public GameObject wepDrop;
	private DropWeaponOnDestroy drop;
    //public GameObject abilityReward;

    public float t;

    private AbilityRewardsDropList dropManager;

    public GameObject spawn1;
    public GameObject spawn2;

    public GameObject death1;
    public GameObject death2;

    bool fading;

    // Use this for initialization
    void Start() {
		health = phase1 = 1050 * 4.5f;
		phase2 = 950 * 4.5f;
		phase3 = 850 * 4.5f;
		phase4 = 750 * 4.5f;
		phase5 = 650 * 4.5f;
		phase6 = 550 * 4.5f;
		phase7 = 450 * 4.5f;
        phase8 = 350 * 4.5f;
        phase8p1 = 50 * 4.5f;
        phase8p2 = 100 *4.5f;
        phase8p3 = 150 * 4.5f;
        phase8p4 = 200 * 4.5f;
        phase8p5 = 250 * 4.5f;
		phase8Move = false;
        canMove = true;
        canShoot = true;
        destroy = false;
        fading = true;
        //t = Time.time;
        phase = 1;
        damageReceivedMultiplier = 1;
        sparks.SetActive(false);
        sr = GetComponent<SpriteRenderer>();
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		drop = wepDrop.GetComponent<DropWeaponOnDestroy> ();

        dropManager = GameObject.Find("AbilityDropManager").GetComponent<AbilityRewardsDropList>();
        dropManager.AddBossDropsToList(4);

        Instantiate(spawn1, transform.position, Quaternion.identity);
        Instantiate(spawn2, transform.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update() {
        t += Time.deltaTime;
        if (t > 1)
        {
            fading = false;
        }
        if (fading)
        {
            sr.color = new Color(1f, 1f, 1f, Mathf.SmoothStep(0.0f, 1.0f, t));
        }
        if (damageReceivedMultiplier > 1.15f) {
            damageReceivedMultiplier = 1.15f;
        }
        //UpdateHealthBar();
        if (invulnTimer > 0){
			invulnTimer -= Time.deltaTime;
		}
		if (health <= phase2 && phase == 1) {
			phase = 2;
			weapons [0].GetComponent<Stage2BossGun1> ().ChangePhase (2);
			noshootTimer = invulnTimer = 1f;
			GameObject[] g = GameObject.FindGameObjectsWithTag ("EnemyBullet");
			for (int i = 0; i < g.Length; i++) {
				Destroy (g [i], i / 400f);
			}
		} else if (health <= phase3 && phase == 2) {
			phase = 3;
			noshootTimer = invulnTimer = 4f;
			GameObject[] g = GameObject.FindGameObjectsWithTag ("EnemyBullet");
			for (int i = 0; i < g.Length; i++) {
				g [i].transform.position = new Vector3 (500, 500, 1);
				Destroy (g [i], i / 400f);
			}
			weapons [1].GetComponent<Stage2BossGun2> ().ChangePhase (1);
			weapons [2].GetComponent<Stage2BossGun2> ().ChangePhase (1);
			weapons [3].GetComponent<Stage2BossGun2> ().ChangePhase (1);
			weapons [0].GetComponent<Stage2BossGun1> ().ChangePhase (3);
		} else if (health <= phase4 && phase == 3) {
			phase = 4;
			noshootTimer = invulnTimer = 1f;
			weapons [0].GetComponent<Stage2BossGun1> ().ChangePhase (4);
			GameObject[] g = GameObject.FindGameObjectsWithTag ("EnemyBullet");
			for (int i = 0; i < g.Length; i++) {
				Destroy (g [i], i / 400f);
			}
		} else if (health <= phase5 && phase == 4) {
			phase = 5;
			noshootTimer = invulnTimer = 1f;
			weapons [0].GetComponent<Stage2BossGun1> ().ChangePhase (5);
			weapons [1].GetComponent<Stage2BossGun2> ().ChangePhase (2);
			weapons [2].GetComponent<Stage2BossGun2> ().ChangePhase (2);
			weapons [3].GetComponent<Stage2BossGun2> ().ChangePhase (2);
			weapons [1].transform.rotation = Quaternion.Euler (0, 0, 0);
			weapons [2].transform.rotation = Quaternion.Euler (0, 0, 240);
			weapons [3].transform.rotation = Quaternion.Euler (0, 0, 120);

			GameObject[] g = GameObject.FindGameObjectsWithTag ("EnemyBullet");
			for (int i = 0; i < g.Length; i++) {
				Destroy (g [i], i / 400f);
			}
		} else if (health <= phase6 && phase == 5) {
			phase = 6;
			weapons [0].GetComponent<Stage2BossGun1> ().ChangePhase (6);
			noshootTimer = invulnTimer = 1f;

			GameObject[] g = GameObject.FindGameObjectsWithTag ("EnemyBullet");
			for (int i = 0; i < g.Length; i++) {
				Destroy (g [i], i / 400f);
			}
		} else if (health <= phase7 && phase == 6) {
			phase = 7;
			weapons [4].GetComponent<Stage2BossGun3> ().ChangePhase (1);
			noshootTimer = invulnTimer = 3f;
			GameObject[] g = GameObject.FindGameObjectsWithTag ("EnemyBullet");
			for (int i = 0; i < g.Length; i++) {
				Destroy (g [i], i / 400f);
			}
		} else if (health <= phase8 && phase == 7) {
			phase = 8;
			phase8Move = true;
			noshootTimer = invulnTimer = 9999f;
			weapons [0].GetComponent<Stage2BossGun1> ().ChangePhase (7);
			weapons [1].GetComponent<Stage2BossGun2> ().ChangePhase (3);
			weapons [2].GetComponent<Stage2BossGun2> ().ChangePhase (3);
			weapons [3].GetComponent<Stage2BossGun2> ().ChangePhase (3);
			weapons [4].GetComponent<Stage2BossGun3> ().ChangePhase (2);
			GameObject[] g = GameObject.FindGameObjectsWithTag ("EnemyBullet");
			for (int i = 0; i < g.Length; i++) {
				Destroy (g [i], i / 400f);
			}

		}
			
		if(phase == 8 && phase8Move){
			transform.rotation = Quaternion.Euler (0, 0, transform.eulerAngles.z - Time.deltaTime * 20);
			player.rotation = Quaternion.Euler (0, 0, transform.eulerAngles.z - Time.deltaTime * 20);
			transform.position = Vector2.MoveTowards (transform.position, new Vector2 (4, 0), Time.deltaTime);
			if(transform.eulerAngles.z < 270){
				transform.rotation = Quaternion.Euler (0, 0, 270);
				player.rotation = Quaternion.Euler (0, 0, 270);
			}
			if(Vector2.Distance(transform.position, new Vector2 (4,0)) < 0.1f){
				transform.position = new Vector2 (4, 0);
			}
			if(transform.position == new Vector3(4,0,0) && transform.eulerAngles.z == 270){
				phase8Move = false;
				noshootTimer = invulnTimer = 0;
			}
		}

        if (health <= 0) {
            Death();
			player.rotation = Quaternion.Euler (0, 0, 0);
			GameObject[] g = GameObject.FindGameObjectsWithTag ("EnemyBullet");
			for (int i = 0; i < g.Length; i++) {
				Destroy (g [i], i / 400f);
			}
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
					Shoot (4);
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
        //SceneManager.LoadSceneAsync("Victory", LoadSceneMode.Additive);

        if (!Global.bossMedley) {
			GameObject g = Instantiate(wepDropActual) as GameObject;
			g.transform.position = new Vector3 (2, 3, transform.position.z);
			g.GetComponent<WepDrop> ().wepID = 500 + Random.Range (1, wepDrop.GetComponent<DropWeaponOnDestroy>().stage5.Length);
        }
		

        Instantiate(death1, transform.position, Quaternion.identity);
        Instantiate(death2, transform.position, Quaternion.identity);

        dropManager.DropAbility();

        DestroySelf();
    }

    public virtual void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.name.Contains("RedoxBullet")) {
            sparks.SetActive(true);
            if (invulnTimer > 0) {
                health -= col.gameObject.GetComponent<Projectile>().damage * damageReceivedMultiplier / 5f;
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

        if (col.gameObject.tag == "PlayerBulletPenetrate"){
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
