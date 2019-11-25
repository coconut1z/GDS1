using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WeakBoss5: MonoBehaviour {

    public WeaponEnemy[] weapons;
    public SpriteRenderer sr;
    public float health;
    private bool canMove;
    private bool canShoot;
    private int phase;
	public float phase1, phase2, phase3, phase4, phase5, phase6, phase7, phase8, phase9;
	private float invulnTimer;
	private float noshootTimer;
    private float damageReceivedMultiplier;
    public GameObject sparks;
	private float rotateAmount = 0;
	private float rotationStart;
	private Transform player;
    private List<string> wepIDs = new List<string>();
    private float lastClearTime;
    private float specialHitInterval = 0.2f;
	public bool left;
	private bool minised;

    private AbilityRewardsDropList dropManager;

    public GameObject death1;
    public GameObject death2;

    // Use this for initialization
    void Start() {
		health = phase1 = 150 * 3.5f;
		phase9 = 150 * 3.5f;
        canMove = true;
        canShoot = true;
        phase = 8;
		minised = false;
        damageReceivedMultiplier = 1;
		sparks = (GameObject)(Resources.Load("Sparks"));
        sparks.SetActive(false);
        sr = GetComponent<SpriteRenderer>();
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		Invoke ("LateStart", 0.05f);

        dropManager = GameObject.Find("AbilityDropManager").GetComponent<AbilityRewardsDropList>();
        dropManager.AddBossDropsToList(5);
    }

	private void LateStart(){
		phase = 9;
		weapons [10].GetComponent<Stage5BossGun5> ().ChangePhase (5);
		for (int i = 11; i < 25; i++) {
			weapons [i].GetComponent<Stage5BossGun5> ().ChangePhase (5);
		}
		weapons [0].GetComponent<Stage5BossGun1> ().ChangePhase (1.5f);
		weapons [1].GetComponent<Stage5BossGun1> ().ChangePhase (2);
		weapons [2].GetComponent<Stage5BossGun1> ().ChangePhase (2.1f);
		weapons [3].GetComponent<Stage5BossGun2> ().ChangePhase (3);
		weapons [4].GetComponent<Stage5BossGun3> ().ChangePhase (2);
		weapons [5].GetComponent<Stage5BossGun3> ().ChangePhase (2);
		weapons [6].GetComponent<Stage5BossGun3> ().ChangePhase (2);
		weapons [7].GetComponent<Stage5BossGun3> ().ChangePhase (2);
		weapons [8].GetComponent<Stage5BossGun3> ().ChangePhase (2);
		weapons [9].GetComponent<Stage5BossGun4> ().ChangePhase (2);
		noshootTimer = invulnTimer = 1f;
		rotateAmount = 99999999;
	}

    // Update is called once per frame
    void Update() {
        if (damageReceivedMultiplier > 1.15f) {
            damageReceivedMultiplier = 1.15f;
        }

		if(left){
			if(transform.position.x < -17.8f){
				transform.position = new Vector2 (transform.position.x + 2 * Time.deltaTime, transform.position.y);
				canShoot = false;
			}else{
				canShoot = true;
			}
			if(rotateAmount > 0){
				rotateAmount -= Time.deltaTime * 25;
				transform.rotation = Quaternion.Euler (0, 0, transform.eulerAngles.z + Time.deltaTime * 25);
				if(rotateAmount <= 0){
					transform.rotation = Quaternion.Euler (0, 0, rotationStart + 90);
				}
			}
		}else{
			if(transform.position.x > 17.8){
				transform.position = new Vector2 (transform.position.x - 2 * Time.deltaTime, transform.position.y);
				canShoot = false;
			}else{
				canShoot = true;
			}
			if(rotateAmount > 0){
				rotateAmount += Time.deltaTime * 25;
				transform.rotation = Quaternion.Euler (0, 0, transform.eulerAngles.z + Time.deltaTime * 25);
				if(rotateAmount <= 0){
					transform.rotation = Quaternion.Euler (0, 0, rotationStart + 90);
				}
			}
		}


		if(rotateAmount < -100){
			rotateAmount += Time.deltaTime * 25;
			transform.rotation = Quaternion.Euler (0, 0, transform.eulerAngles.z - Time.deltaTime * 25);
		}
		if(invulnTimer > 0){
			invulnTimer -= Time.deltaTime;
		}
        if (health <= 0) {
            Death();
        }
        if (canMove) {
            //transform.Translate (new Vector2 (0, speed * Time.deltaTime));
        }
        if (canShoot && noshootTimer <= 0) {
			if (phase == 1 || phase == 2) {
				Shoot (0);
				Shoot (1);
				Shoot (2);
			}else if(phase == 3 || phase == 4){
				Shoot (3);
				Shoot (4);
				Shoot (5);
				Shoot (6);
				Shoot (7);
				Shoot (8);
			}else if(phase == 5 || phase == 6){
				Shoot (9);
			}else if(phase == 7){
				Shoot (10);
				for(int i = 11; i < 21; i++){
					Shoot (i);
				}
				Shoot (21);
				Shoot (22);
				Shoot (23);
				Shoot (24);

			}else if(phase == 8){
				Shoot (10);
				//for(int i = 11; i < 21; i++){
				//	Shoot (i);
				//}
				//Shoot (21);
				//Shoot (22);
				//Shoot (23);
				//Shoot (24);
			}else if(phase == 9){
				for(int i = 0; i < 25; i++){
					if(weapons[i].transform.position.x > -17f && i != 3 && i != 0 && i != 5 && i != 6 && i != 7){
						Shoot (i);
					}

				}
			}
		}else{
			noshootTimer -= Time.deltaTime;
		}
    }

    private void Shoot(int i) {
		if(canShoot){
			weapons[i].Shoot();
		}

    }

    private void Death() {
		GameObject[] goArray = SceneManager.GetSceneByName ("S6PB").GetRootGameObjects ();
		for (int i = 0; i < goArray.Length; i++) {
			if(goArray[i] != null){
				if(goArray[i].name.Equals("Supercluster")){
					if(!minised){
						goArray [i].GetComponent<Stage6Boss> ().stationsLeft -= 1;
						print ("D");
					}
					minised = true;
					i = 99;
				}
			}
		}

        Instantiate(death1, this.transform.position, Quaternion.identity);
        Instantiate(death2, this.transform.position, Quaternion.identity);

        DestroySelf();
    }

	private void SwitchDirection(){
		rotateAmount = -9999999;
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
    }


    public void HitFeedback() {
        sr.color = new Color(255, 0, 0);
        Invoke("EndFlashSprite", 0.05f);
    }

    public void EndFlashSprite() {
        sr.color = new Color(255, 255, 255);
    }

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
