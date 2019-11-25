using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stage6Boss: MonoBehaviour {

    public WeaponEnemy[] weapons;
	//public SpriteRenderer[] sr;
	private SpriteRenderer sr;
	public ReturnToOriginalPosition[] returnParts;
    public float health;
    private bool canMove;
    private bool canShoot;
    private int phase;
	public float phase1, phase2, phase3, phase4, phase5, phase6, phase7, phase8;
	private float invulnTimer;
	private float noshootTimer;
    private float damageReceivedMultiplier;
    public GameObject sparks;
	private Transform player;
    private List<string> wepIDs = new List<string>();
    private float lastClearTime;
    private float specialHitInterval = 0.2f;
	private bool startShift, startFinalShift;
	private bool phase2Shift;
	private Transform canvasT;
	private bool finalShiftUI;
	private bool spikePhase, sUp, sLeft, sDown, sRight;
	private bool rotateLeft;
	private bool rotateLeft2;
	private bool finalPrep;
	private bool finalPrep2;
	private bool finalLaserPrep;
	private bool finalFade;
	private bool finalFadeback;
	private bool trueFinal;
	private float rotation;
	public int stationsLeft;
    bool dead;

    public GameObject spawn1;
    public GameObject spawn2;

    public GameObject death1;
    public GameObject death2;
    public GameObject death3;

    public VictoryChecker vc;

	public Transform UpperWing, LowerWing, LeftConnecter, RightConnector, LeftWing, RightWing, LaserProng;

    private float t;

    public GameObject superShipParticles;
    public GameObject healthParticles;

    bool fading;

    // Use this for initialization
    void Start() {
        fading = true;
		health = phase1 = 1000 * 4f;
		phase2 = 900 * 4f;
		phase3 = 800 * 4f;
		phase4 = 700 * 4f;
		phase5 = 600 * 4f;
		phase6 = 500 * 4f;
		phase7 = 400 * 4f;
		phase8 = 300 * 4f;
        canMove = true;
        canShoot = true;
        phase = 1;
        damageReceivedMultiplier = 1;
		sparks = (GameObject)(Resources.Load("Sparks"));
        sparks.SetActive(false);
		sr = transform.GetChild (0).GetComponent<SpriteRenderer> ();
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		Invoke ("LateStart", 0.05f);
		Invoke ("StartShiftCamera",0.02f);
		startShift = false;
		startFinalShift = false;
		finalShiftUI = false;
		phase2Shift = false;
		finalPrep = false;
		finalLaserPrep = false;
		finalFadeback = false;
		trueFinal = false;
		spikePhase = sUp = sLeft = sDown = sRight = false;
		stationsLeft = 2;
		rotation = 0;

		transform.position = new Vector2 (-0.77f, transform.position.y);
		returnParts = GetComponentsInChildren<ReturnToOriginalPosition> ();
		for(int i = 0; i < returnParts.Length; i++){
			returnParts [i].CustomStart ();
		}

        Instantiate(spawn1, transform.position, Quaternion.identity);
        Instantiate(spawn2, transform.position, Quaternion.identity);
        dead = false;
    }

	private void StartShiftCamera(){
		if(!startShift){
			player.GetComponent<PlayerController> ().SetBounds (2);
			GameObject[] goArray = SceneManager.GetSceneByName ("Main").GetRootGameObjects ();
			for (int i = 0; i < goArray.Length; i++) {
				if(goArray[i] != null){
					if(goArray[i].name.Equals("GameArea")){
						goArray [i].transform.localScale = new Vector3 (1.5f, 1.5f, 1.5f);
						goArray [i].transform.position = new Vector3 (-0.77f, 0, 0);
					}
				}
			}
			//GameObject.Find("GameArea").transform.localScale = new Vector3 (1.5f, 1.5f, 1.5f);
			//GameObject.Find("GameArea").transform.position = new Vector3 (-0.77f, 0, 0);
			//transform.position = new Vector3(transform.position.x - 0.77f, transform.position.y, transform.position.z);
			Global.specialBounds = 2;
			startShift = true;
		}

		Camera.main.orthographicSize += 0.02f;
		if(Camera.main.orthographicSize > 7.5f){
			Camera.main.orthographicSize = 7.5f;
		}else{
			Invoke ("StartShiftCamera", 0.02f);
		}
	}

	private void FinalShiftCamera(){
		Global.finalmusic = true;
		GameObject[] goArray = SceneManager.GetSceneByName ("Main").GetRootGameObjects ();
		for (int i = 0; i < goArray.Length; i++) {
			if(goArray[i] != null){
				if(goArray[i].name.Equals("Canvas")){
					canvasT = goArray [i].transform;
					goArray [i].transform.GetChild (7).gameObject.SetActive (true);
				}
				if(goArray[i].name.Equals("GameArea")){
					goArray [i].transform.localScale = new Vector3 (2.6f, 2.6f, 2.6f);
					goArray [i].transform.position = new Vector3 (0, 0, 0);
				}
			}
		}
		finalShiftUI = true;
		startFinalShift = true;
		if(startFinalShift){
			/*
			player.GetComponent<PlayerController> ().SetBounds (2);
			GameObject[] goArray = SceneManager.GetSceneByName ("Main").GetRootGameObjects ();
			for (int i = 0; i < goArray.Length; i++) {
				if(goArray[i] != null){
					if(goArray[i].name.Equals("GameArea")){
						goArray [i].transform.localScale = new Vector3 (2.5f, 2.5f, 2.5f);
						goArray [i].transform.position = new Vector3 (0, 0, 0);
					}
				}
			}
			*/
			Camera.main.transform.position = new Vector3 (0, 0, -10f);
			//GameObject.Find("GameArea").transform.localScale = new Vector3 (1.5f, 1.5f, 1.5f);
			//GameObject.Find("GameArea").transform.position = new Vector3 (-0.77f, 0, 0);
			transform.position = new Vector3(0, transform.position.y, transform.position.z);
			Global.specialBounds = 3;
			player.GetComponent<PlayerController> ().SetBounds (3);
			startFinalShift = false;
		}

		Camera.main.orthographicSize += 0.02f;
		if(Camera.main.orthographicSize > 10.0f){
			Camera.main.orthographicSize = 10.0f;
            Global.final = true;
		}else{
			Invoke ("FinalShiftCamera", 0.02f);
		}
	}


	private void LateStart(){
		weapons [0].GetComponent<Stage6CoreGun> ().ChangePhase (1);
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
			
		if(invulnTimer > 0){
			invulnTimer -= Time.deltaTime;
		}
        if (health <= 500 && !dead) {
            dead = true;
            Death();
        }
        if (canMove) {
            //transform.Translate (new Vector2 (0, speed * Time.deltaTime));
        }

		if (health <= phase2 && phase == 1) {
			phase = 2;
			noshootTimer = invulnTimer = 1f;
			GameObject[] g = GameObject.FindGameObjectsWithTag ("EnemyBullet");
			for (int i = 0; i < g.Length; i++) {
				Destroy (g [i], i / 800f);
			}
			Invoke ("DestroyAgain", 1f);
			phase2Shift = true;
			UpperWing.gameObject.SetActive (true);
		} else if (health <= phase3 && phase == 2) {
			phase = 3;
			noshootTimer = invulnTimer = 1f;
			GameObject[] g = GameObject.FindGameObjectsWithTag ("EnemyBullet");
			for (int i = 0; i < g.Length; i++) {
				Destroy (g [i], i / 800f);
			}
			UpperWing.GetComponent<Stage6Upper> ().ReturnToDefault ();
			LowerWing.gameObject.SetActive (true);
			Invoke ("DestroyAgain", 1f);
		} else if (health <= phase4 && phase == 3) {
			phase = 4;
			noshootTimer = invulnTimer = 1f;
			LowerWing.GetComponent<Stage6Lower> ().ReturnToDefault ();
			RightConnector.gameObject.SetActive (true);
			LeftConnecter.gameObject.SetActive (true);

			Invoke ("DestroyAgain", 1f);
		} else if (health <= phase5 && phase == 4) {
			phase = 5;
			noshootTimer = invulnTimer = 5f;
			GameObject[] g = GameObject.FindGameObjectsWithTag ("EnemyBullet");
			for (int i = 0; i < g.Length; i++) {
				Destroy (g [i], i / 400f);
			}
			RightConnector.GetComponent<Stage6Connecter> ().ReturnToDefault ();
			LeftConnecter.GetComponent<Stage6Connecter> ().ReturnToDefault ();
		} else if (health <= phase6 && phase == 5) {
			phase = 6;
			noshootTimer = invulnTimer = 5f;
			GameObject[] g = GameObject.FindGameObjectsWithTag ("EnemyBullet");
			for (int i = 0; i < g.Length; i++) {
				Destroy (g [i], i / 400f);
			}
			LeftWing.GetComponent<Stage6LeftWing> ().ReturnToDefault ();
			rotateLeft2 = true;
		} else if (health <= phase7 && phase == 6) {
			phase = 7;
			noshootTimer = invulnTimer = 5f;
			GameObject[] g = GameObject.FindGameObjectsWithTag ("EnemyBullet");
			for (int i = 0; i < g.Length; i++) {
				Destroy (g [i], i / 400f);
			}
			RightWing.GetComponent<Stage6RightWing> ().ReturnToDefault ();
			finalPrep = true;
		} else if(health <= phase8 && phase == 7){
			phase = 8;
			noshootTimer = invulnTimer = 1f;
			GameObject[] g = GameObject.FindGameObjectsWithTag ("EnemyBullet");
			for (int i = 0; i < g.Length; i++) {
				Destroy (g [i], i / 400f);
			}
			Invoke ("Warning", 0.05f);
			finalLaserPrep = true;

        } else if (health <= 0) {
            phase = -1;
        }

        if (canShoot && noshootTimer <= 0) {
			if(phase == 1){
				Shoot (0);
			}else if(phase == 5){
				Shoot (1);
				Shoot (2);
			}else if(phase == 6){
				Shoot (3);
				Shoot (4);
			}else if(phase == 7){
				Shoot (1);
				Shoot (3);
			}
			// Every X seconds shoot big laser
			if(trueFinal && noshootTimer <= 0){
				// Next delay
				noshootTimer = 4;
				LaserProng.GetComponent<Stage6LaserProng> ().Shoot ();
			}
		}else{
			noshootTimer -= Time.deltaTime;
		}

		// Shot for .5 seconds all weapons (4-3.5);
		if(noshootTimer >= 3.5f && trueFinal){
			Shoot (3);
			Shoot (4);
			Shoot (1);
			Shoot (2);
		}

		// Laser turning
		if(trueFinal && noshootTimer <= 2){
			Vector3 direction = player.position - transform.position;
			float angle = Mathf.Atan2 (direction.y, direction.x) * Mathf.Rad2Deg - 270;
			Quaternion lookRotation = Quaternion.AngleAxis (angle, Vector3.forward);
			transform.rotation = Quaternion.Slerp (transform.rotation, lookRotation, Time.deltaTime * 2);
		}

		if(spikePhase){
			if(phase == 5){
				noshootTimer = invulnTimer = 1f;
			}
			if(sUp){
				transform.Translate (Vector3.up * Time.deltaTime * 5);
				if(transform.position.y > 7){
					transform.position = new Vector3 (transform.position.x, 7, transform.position.z);
					sUp = false;
					sLeft = true;
				}
			}else if(sLeft){
				transform.Translate (Vector3.left * Time.deltaTime * 5);
				if(transform.position.x < -11){
					transform.position = new Vector3 (-11, transform.position.y, transform.position.z);
					sLeft = false;
					sDown = true;
				}
				if(phase == 5 && Mathf.Abs(transform.position.x) < 0.3f){
					sLeft = false;
					transform.position = new Vector3 (0, 7, transform.position.z);
					rotateLeft = true;
					spikePhase = false;
				}
			}else if(sDown){
				transform.Translate (Vector3.down * Time.deltaTime * 5);
				if(transform.position.y < -7){
					transform.position = new Vector3 (transform.position.x, -7, transform.position.z);
					sDown = false;
					sRight = true;
				}
			}else if(sRight){
				transform.Translate (Vector3.right * Time.deltaTime * 5);
				if(transform.position.x > 9.7f){
					transform.position = new Vector3 (9.7f, transform.position.y, transform.position.z);
					sRight = false;
					sUp = true;
				}
			}

		}

		if(phase2Shift){
			transform.Translate ((Vector3.zero - transform.position) * Time.deltaTime * 0.4f,Space.World);
			if (Vector2.Distance (transform.position, Vector3.zero) < 0.06f){
				transform.position = Vector3.zero;
				phase2Shift = false;
			}
		}

		if(finalShiftUI){
			for(int i = 0; i < canvasT.childCount - 1; i++){
				canvasT.GetChild (i).transform.Translate (0, -Time.deltaTime * 500, 0);
				if(canvasT.GetChild(i).transform.position.y < -3000){
					finalShiftUI = false;
				}
			}
		}

		if(rotateLeft){
			transform.rotation = Quaternion.Euler (0, 0, transform.eulerAngles.z + 25 * Time.deltaTime);
			noshootTimer = invulnTimer = 1f;
			if(transform.eulerAngles.z > 90){
				transform.rotation = Quaternion.Euler (0, 0, 90);
				LeftWing.gameObject.SetActive (true);
				rotateLeft = false;
			}
		}

		if(rotateLeft2){
			noshootTimer = invulnTimer = 1f;
			transform.rotation = Quaternion.Euler (0, 0, transform.eulerAngles.z + 25 * Time.deltaTime);
			if(transform.eulerAngles.z > 270){
				transform.rotation = Quaternion.Euler (0, 0, 270);
				RightWing.gameObject.SetActive (true);
				rotateLeft2 = false;
			}
		}

		if(finalPrep){
			noshootTimer = invulnTimer = 1f;
			transform.rotation = Quaternion.Euler (0, 0, transform.eulerAngles.z + 25 * Time.deltaTime);
			if(Mathf.Abs(transform.eulerAngles.z) < 3){
				transform.rotation = Quaternion.Euler (0, 0, 0);
				finalPrep = false;
				finalPrep2 = true;
			}
		}
		if(finalPrep2){
			noshootTimer = invulnTimer = 1f;
			transform.Translate (Vector3.down * Time.deltaTime * 3);
			if(transform.position.y < 5){
				transform.position = new Vector3 (transform.position.x, 5, transform.position.z);
				LeftWing.GetComponent<Stage6LeftWing> ().SecondPhase ();
				RightWing.GetComponent<Stage6RightWing> ().SecondPhase ();
				finalPrep2 = false;
				LaserProng.gameObject.SetActive (true);
			}
		}
		if(finalLaserPrep){
			noshootTimer = invulnTimer = 1f;
			transform.Translate (Vector3.up * Time.deltaTime * 3);
			if(transform.position.y > 6.5){
				transform.position = new Vector3 (transform.position.x, 6.5f, transform.position.z);
				finalLaserPrep = false;
			}
		}

		if(finalFade){
			for(int i = 0; i < transform.childCount; i++){
				if(transform.GetChild(transform.childCount-1).GetComponent<SpriteRenderer>().color.r > 0.5f){
					SpriteRenderer s = transform.GetChild (i).GetComponent<SpriteRenderer> ();
					s.color = new Color (Mathf.Clamp01(s.color.r) - Time.deltaTime * 0.3f, 
						Mathf.Clamp01(s.color.g)- Time.deltaTime * 0.3f, 
						Mathf.Clamp01(s.color.b) - Time.deltaTime * 0.3f);
				}else{
					finalFade = false;
				}
			}
		}

		if(stationsLeft <= 0 && !finalFadeback){
			finalFadeback = true;
			LeftWing.GetComponent<Stage6LeftWing> ().StartFinalFadeback ();
			RightWing.GetComponent<Stage6RightWing> ().StartFinalFadeback ();
			transform.GetChild (0).GetComponent<CircleCollider2D> ().enabled = true;
			for(int i = 0; i < transform.childCount; i++){
				transform.GetChild (i).GetComponent<SpriteRenderer> ().sortingOrder += 2;
			}
			stationsLeft = 99;
			Invoke ("StartTrueFinal", 5.0f);
			noshootTimer = invulnTimer = 1f;
			GameObject[] g = GameObject.FindGameObjectsWithTag ("EnemyBullet");
			for (int i = 0; i < g.Length; i++) {
				Destroy (g [i], i / 400f);
			}
		}

		if(finalFadeback){
			for(int i = 0; i < transform.childCount; i++){
				if(transform.GetChild(transform.childCount-1).GetComponent<SpriteRenderer>().color.r < 1){
					SpriteRenderer s = transform.GetChild (i).GetComponent<SpriteRenderer> ();
					s.color = new Color (Mathf.Clamp01(s.color.r) + Time.deltaTime * 0.3f, 
						Mathf.Clamp01(s.color.g) + Time.deltaTime * 0.3f, 
						Mathf.Clamp01(s.color.b) + Time.deltaTime * 0.3f);
				}else{
					finalFadeback = false;
				}
			}
		}

    }

	private void StartTrueFinal(){
		trueFinal = true;
	}

	private void Warning(){
		SceneManager.LoadSceneAsync ("WarningFinal", LoadSceneMode.Additive);
		Invoke ("BreakLaser", 10.4f); //was5
        LaserProng.GetComponent<Stage6LaserProng>().BreakLaser(); //starts deathray particles.
    }

	private void BreakLaser(){
		Invoke ("FinalShiftCamera", 1.6f); //when the screen shatters
		Invoke ("StartFinalFade", 6.0f);

	}

	private void StartFinalFade(){
		finalFade = true;
		transform.GetChild (0).GetComponent<CircleCollider2D> ().enabled = false;
		LeftWing.GetComponent<Stage6LeftWing> ().StartFinalFade ();
		RightWing.GetComponent<Stage6RightWing> ().StartFinalFade ();
		for(int i = 0; i < transform.childCount; i++){
			transform.GetChild (i).GetComponent<SpriteRenderer> ().sortingOrder -= 2;
		}
		Invoke ("Wave1", 3.0f);
		Invoke ("Wave2", 50.0f);
        Instantiate(superShipParticles);
        Instantiate(healthParticles);
	}

    private void Sound1() {
        AudioManager.instance.PlaySound("B6Death1");
    }
    private void Sound2() {
        AudioManager.instance.PlaySound("B6Death2");
    }
    private void Sound3() {
        AudioManager.instance.PlaySound("B6Death3");
    }

	private void Wave1(){
		SceneManager.LoadSceneAsync ("F1", LoadSceneMode.Additive);
	}

	private void Wave2(){
		SceneManager.LoadSceneAsync ("F2", LoadSceneMode.Additive);
	}

    private void Shoot(int i) {
        weapons[i].Shoot();
    }

    private void Death() {
        Sound1();
        Invoke("Sound2", 2.0f);
        Invoke("Sound3", 3.5f);
        Global.asteroidCancel = true;
        
        Instantiate(death1, this.transform.position, Quaternion.identity);
        Instantiate(death2, this.transform.position, Quaternion.identity);
        Instantiate(death3, this.transform.position, Quaternion.identity);

        vc.BossDead();
        Global.final = false;

        DestroySelf();
    }

    private void Explosion() {
        Instantiate(death3, this.transform.position, Quaternion.identity);
    }

    private void Victory() {
        SceneManager.LoadSceneAsync("Victory", LoadSceneMode.Additive);
    }

    public virtual void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.name.Contains("RedoxBullet")) {
            sparks.SetActive(true);
            if (invulnTimer > 0) {
                health -= col.gameObject.GetComponent<Projectile>().damage * damageReceivedMultiplier/10f;
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
				health -= col.gameObject.GetComponent<Projectile>().damage * damageReceivedMultiplier/10f;
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
                health -= col.gameObject.GetComponent<Projectile>().damage * damageReceivedMultiplier / 10f;
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
		//for(int i = 0; i < sr.Length; i++){
		//	sr[i].color = new Color(255, 0, 0);
		//	Invoke("EndFlashSprite", 0.05f);
		//}     

		sr.color = new Color(255, 0, 0);
		Invoke("EndFlashSprite", 0.05f);
    }

    public void EndFlashSprite() {
		//for(int i = 0; i < sr.Length; i++){
		//	sr[i].color = new Color(255, 255, 255);
		//} 
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
        Debug.Log("Enetered damageEnemy");
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
        Debug.Log("FinisheddamageEnemy");
    }

	private void DestroyAgain(){
		GameObject[] g = GameObject.FindGameObjectsWithTag ("EnemyBullet");
		for (int i = 0; i < g.Length; i++) {
			Destroy (g [i], i / 400f);
		}
	}

	public void StartSpikePhase(){
		spikePhase = true;
		sUp = true;
	}
}


