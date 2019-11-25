using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Stage1Boss : MonoBehaviour {

    public WeaponEnemy[] weapons;
    public SpriteRenderer sr;
	public GameObject wepDrop;
	public GameObject wepDropActual;
    public Text phaseIndicator;
    [SerializeField] private Image healthBarStageOne, healthBarStageTwo, healthBarStageThree, healthBarStageFour;
    private float health;
    private bool canMove;
    private bool canShoot;
    private bool destroy;
    private int phase;
	private float phase1, phase2, phase3, phase4;
	private float invulnTimer;
	private float noshootTimer;
    private float damageReceivedMultiplier;
    public GameObject sparks;
    //public GameObject abilityReward;
	private List<string> wepIDs = new List<string>();
	private float lastClearTime;
	private float specialHitInterval;

    public GameObject spawn1;
    public GameObject spawn2;

    public GameObject death1;
    public GameObject death2;

    private DropWeaponOnDestroy drop;

	private Transform player;

    private AbilityRewardsDropList dropManager;

    private float t;
    bool fading;

    // Use this for initialization
    void Start() {
        health = phase1 = 350;
		phase2 = 283;
		phase3 = 216;
		phase4 = 150;
        canMove = true;
        canShoot = true;
        destroy = false;
        phase = 1;
        phaseIndicator.text = "4";
        damageReceivedMultiplier = 1;
        sparks.SetActive(false);
        sr = GetComponent<SpriteRenderer>();
		player = GameObject.FindGameObjectWithTag ("Player").transform;
        dropManager = GameObject.Find("AbilityDropManager").GetComponent<AbilityRewardsDropList>();
		drop = wepDrop.GetComponent<DropWeaponOnDestroy> ();
		specialHitInterval = 0.2f;
		wepIDs = new List<string>();
        dropManager.AddBossDropsToList(1);
        fading = true;

        Instantiate(spawn1, transform.position, Quaternion.identity);
        Instantiate(spawn2, transform.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update() {
        t += Time.deltaTime;
        if (t > 1) {
            fading = false;
        }
        if (fading) {
            sr.color = new Color(1f, 1f, 1f, Mathf.SmoothStep(0.0f, 1.0f, t));
        }
        if (damageReceivedMultiplier > 1.15f) {
            damageReceivedMultiplier = 1.15f;
        }
        UpdateHealthBar();
		if(invulnTimer > 0){
			invulnTimer -= Time.deltaTime;
		}
		if (health <= phase2 && phase == 1) {
            phase = 2;
            weapons[0].GetComponent<Stage1BossGun>().degStep = 0;
			noshootTimer = invulnTimer = 1f;
			GameObject[] g = GameObject.FindGameObjectsWithTag ("EnemyBullet");
			for (int i = 0; i < g.Length; i++) {
				Destroy (g [i], i / 400f);
			}
		} else if (health <= phase3 && phase == 2) {
            phase = 3;
            // Yes it is supposed to change this specific weapon to phase 2 when the boss is in phase 3
            weapons[0].GetComponent<Stage1BossGun>().ChangePhase(2);
			noshootTimer = invulnTimer = 1f;
			GameObject[] g = GameObject.FindGameObjectsWithTag ("EnemyBullet");
			for (int i = 0; i < g.Length; i++) {
				Destroy (g [i], i / 400f);
			}
		} else if (health <= phase4 && phase == 3) {
            phase = 4;
            weapons[0].GetComponent<Stage1BossGun>().ChangePhase(3);
            weapons[1].GetComponent<Stage1BossGun2>().ChangePhase(2);
            weapons[2].GetComponent<Stage1BossGun2>().ChangePhase(2);
			noshootTimer = 1f;
			invulnTimer = 8f;
			GameObject[] g = GameObject.FindGameObjectsWithTag ("EnemyBullet");
			for (int i = 0; i < g.Length; i++) {
				Destroy (g [i], i / 400f);
			}
        }
        if (health <= 0) {
            Death();
        }
        if (canMove) {
            //transform.Translate (new Vector2 (0, speed * Time.deltaTime));
        }
        if (canShoot && noshootTimer <= 0) {
            if (phase == 1 || phase == 3) {
                Shoot(0);
            } else if (phase == 2) {
                Shoot(1);
                Shoot(2);
            } else if (phase == 4) {
                Shoot(0);
                Shoot(1);
                Shoot(2);
            }

		}else{
			noshootTimer -= Time.deltaTime;
		}
    }

    private void Shoot(int i) {
        weapons[i].Shoot();
    }

    private void Death() {

        if (!Global.bossMedley) {
            // GameObject g = Instantiate(drop.stage2[Random.Range(0, drop.stage2.Length)]) as GameObject;
            //g.transform.position = new Vector3(2, 3, transform.position.z);
			GameObject g = Instantiate(wepDropActual) as GameObject;
			g.transform.position = new Vector3 (2, 3, transform.position.z);
			g.GetComponent<WepDrop> ().wepID = 200 + Random.Range (1, wepDrop.GetComponent<DropWeaponOnDestroy>().stage2.Length);
        }
		

        Instantiate(death1, transform.position, Quaternion.identity);
        Instantiate(death2, transform.position, Quaternion.identity);

        //Instantiate(abilityReward, this.transform.position, Quaternion.identity);
        dropManager.DropAbility();

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

    //I need to make a separate script for this, as well as a method to calculate stuff so don't read into this too much
    //But basically its (((health - currentMin) * (newMax - newMin) / (currentMax - currentMin) + newMin
    //Where newMax and newMin is the amount of health for the current phase
    private void UpdateHealthBar()
    {
        float currentHealth = health;
        phaseIndicator.text = "|||";
        if (health <= phase1 && health > phase2) {
			currentHealth = (health - phase2) / (phase1 - phase2);
            healthBarStageOne.fillAmount = currentHealth;
        }
        else if (health <= phase2 && health > phase3) {
            phaseIndicator.text = "||";
            healthBarStageOne.fillAmount = 0;
			currentHealth = (health - phase3) / (phase2 - phase3);
            healthBarStageTwo.fillAmount = currentHealth;
        }
        else if (health <= phase3 && health > phase4) {
            phaseIndicator.text = "|";
            healthBarStageTwo.fillAmount = 0;
			currentHealth = (health - phase4) / (phase3 - phase4);
            healthBarStageThree.fillAmount = currentHealth;
        }
        else if (health <= phase4) {
            phaseIndicator.text = "";
            healthBarStageThree.fillAmount = 0;
			currentHealth = (health )  / (phase4);
            healthBarStageFour.fillAmount = currentHealth;
        }
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
