using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stage3Boss : MonoBehaviour {

    public WeaponEnemy[] weapons;
	public GameObject[] spawnPrefabs;
	public GameObject[] bullets;
	public GameObject wepDropActual;
    public SpriteRenderer[] sr;
    public float health;
    private bool canMove;
    private bool canShoot;
    private bool destroy;
    private int phase;
	public float phase1, phase2, phase3, phase4, phase5, phase6;
	private float invulnTimer;
	private float noshootTimer;
    private float damageReceivedMultiplier;
    public GameObject sparks;
	private Transform player;
	private Stage3BossPart[] parts;
	private Transform spawnPos;
	public float spawnTimer, spawnDelay;
	private float bulletAngle;
	private float addAngle;
	private float randomSpin;
	private bool shiftCenter;
	private bool shiftCamera;
	public List<GameObject> noHit;
	private bool startNormalization, normalized;
	public int deadParts;
    private List<string> wepIDs = new List<string>();
    private float lastClearTime;
    private float specialHitInterval = 0.2f;
	public GameObject wepDrop;
	private DropWeaponOnDestroy drop;
    //public GameObject abilityReward;
    //public GameObject abilityReward2;

    public GameObject spawn1;
    public GameObject spawn2;

    private float t;

    bool fading;

    private AbilityRewardsDropList dropManager;

    // Use this for initialization
    void Start() {	
        health = phase1 = 500 * 1.5f;
		phase2 = 400 * 1.5f;
		phase3 = 300 * 1.5f;
		phase4 = 200 * 1.5f;
		phase5 = 100 * 1.5f;
		phase6 = 0;
        canMove = true;
        canShoot = true;
        destroy = false;
        phase = 1;
        damageReceivedMultiplier = 1;
        sparks.SetActive(false);
		sr = GetComponentsInChildren<SpriteRenderer> ();
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		parts = GetComponentsInChildren<Stage3BossPart> ();
		spawnTimer = 0;
		spawnDelay = 2f;
		spawnPos = transform.GetChild (4);
		bulletAngle = 0;
		addAngle = -2.5f;
		shiftCenter = false;
		shiftCamera = false;
		startNormalization = false;
		normalized = false;
		deadParts = 0;
		drop = wepDrop.GetComponent<DropWeaponOnDestroy> ();
        fading = true;

        dropManager = GameObject.Find("AbilityDropManager").GetComponent<AbilityRewardsDropList>();
        dropManager.AddBossDropsToList(2);

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
            foreach (SpriteRenderer sprite in sr)
            {
                t += Time.deltaTime;
                sprite.color = new Color(1f, 1f, 1f, Mathf.SmoothStep(0.0f, 1.0f, t));
            }
        }

		float rad = Mathf.Atan2 (
			transform.position.y - player.position.y,
			transform.position.x - player.position.x
		);
		float deg = (180 / Mathf.PI) * rad - 90;
		player.rotation = Quaternion.Euler (0, 0, deg);
        if (damageReceivedMultiplier > 1.15f) {
            damageReceivedMultiplier = 1.15f;
        }
		if (health <= phase2 && phase == 1) {
			phase = 2;
			noshootTimer = invulnTimer = 1f;
			GameObject[] g = GameObject.FindGameObjectsWithTag ("EnemyBullet");
			for (int i = 0; i < g.Length; i++) {
				//g [i].SetActive (false);
				Destroy (g [i], i / 400f);
			}

			GameObject[] e = GameObject.FindGameObjectsWithTag ("Enemy");
			for (int i = 0; i < e.Length; i++) {
				Destroy (e [i]);
			}
		}else if (health <= phase3 && phase == 2) {
			phase = 3;
			noshootTimer = invulnTimer = 1f;
			GameObject[] g = GameObject.FindGameObjectsWithTag ("EnemyBullet");
			for (int i = 0; i < g.Length; i++) {
				//g [i].SetActive (false);
				Destroy (g [i], i / 400f);
			}
			GameObject[] e = GameObject.FindGameObjectsWithTag ("Enemy");
			for (int i = 0; i < e.Length; i++) {
				//g [i].SetActive (false);
				Destroy (e [i]);
			}
			if(Global.Difficulty == Global.RECRUIT){
				spawnDelay = 3f;
			}else if(Global.Difficulty == Global.VETEREN){
				spawnDelay = 1.5f;
			}else if(Global.Difficulty == Global.BATTLEH){
				spawnDelay = 0.8f;
			}
		}else if(health <= phase4 && phase == 3){
			phase = 4;
			noshootTimer = invulnTimer = 1f;
			GameObject[] g = GameObject.FindGameObjectsWithTag ("EnemyBullet");
			for (int i = 0; i < g.Length; i++) {
				//g [i].SetActive (false);
				Destroy (g [i], i / 400f);
			}
			GameObject[] e = GameObject.FindGameObjectsWithTag ("Enemy");
			for (int i = 0; i < e.Length; i++) {
				//g [i].SetActive (false);
				Destroy (e [i]);
			}
			spawnTimer = 3f;
			if(Global.Difficulty == Global.RECRUIT){
				spawnDelay = 5f;
			}else if(Global.Difficulty == Global.VETEREN){
				spawnDelay = 4f;
			}else if(Global.Difficulty == Global.BATTLEH){
				spawnDelay = 3f;
			}
		}else if(health <= phase5 && phase == 4){
			phase = 5;
			noshootTimer = invulnTimer = 1f;
			GameObject[] g = GameObject.FindGameObjectsWithTag ("EnemyBullet");
			for (int i = 0; i < g.Length; i++) {
				//g [i].SetActive (false);
				Destroy (g [i], i / 400f);
			}
			GameObject[] e = GameObject.FindGameObjectsWithTag ("Enemy");
			for (int i = 0; i < e.Length; i++) {
				//g [i].SetActive (false);
				Destroy (e [i]);
			}
			spawnDelay = 2f;
		}else if(health <= phase6 && phase == 5){
			phase = 6;
			noshootTimer = invulnTimer = 5f;
			GameObject[] g = GameObject.FindGameObjectsWithTag ("EnemyBullet");
			for (int i = 0; i < g.Length; i++) {
				//g [i].SetActive (false);
				Destroy (g [i], i / 400f);
			}
			GameObject[] e = GameObject.FindGameObjectsWithTag ("Enemy");
			for (int i = 0; i < e.Length; i++) {
				//g [i].SetActive (false);
				Destroy (e [i]);
			}

			for(int i = 0; i < parts.Length; i++){
				parts [i].canOscillate = true;
			}

			Destroy (GetComponent<Rigidbody2D>());
			if(Global.Difficulty == Global.RECRUIT){
				spawnDelay = 5f;
			}else if(Global.Difficulty == Global.VETEREN){
				spawnDelay = 4f;
			}else if(Global.Difficulty == Global.BATTLEH){
				spawnDelay = 4f;
			}
			player.GetComponent<PlayerController> ().SetBounds (2);
			shiftCenter = true;
			shiftCamera = true;
			GameObject[] goArray = SceneManager.GetSceneByName ("Main").GetRootGameObjects ();
			for (int i = 0; i < goArray.Length; i++) {
				if(goArray[i] != null){
					if(goArray[i].name.Equals("GameArea")){
						goArray [i].transform.localScale = new Vector3 (1.5f, 1.5f, 1.5f);
						goArray [i].transform.position = new Vector3 (-0.77f, 0, 0);
					}
				}
			}
			Global.specialBounds = 2;
		}
		if(shiftCenter){
			transform.Translate ((transform.position - new Vector3 (0.77f, 0, 0)) * Time.deltaTime * 0.4f);
			if (Vector2.Distance (transform.position, new Vector2 (-0.77f, 0)) < 0.06f){
				transform.position = new Vector2 (-0.77f, 0);
				shiftCenter = false;
			}

		}

		if(shiftCamera){
			Camera.main.orthographicSize += Time.deltaTime;
			if(Camera.main.orthographicSize > 7.5f){
				Camera.main.orthographicSize = 7.5f;
				shiftCamera = false;
			}
		}

        //UpdateHealthBar();
		if(invulnTimer > 0){
			invulnTimer -= Time.deltaTime;
		}
		if (health <= 0 && deadParts == 4) {
            //Death();
			startNormalization = true;
        }
		if(startNormalization){
			Camera.main.orthographicSize -= Time.deltaTime;
			if(Camera.main.orthographicSize < 5f){
				Camera.main.orthographicSize = 5f;
				normalized = true;
			}
			GameObject[] goArray = SceneManager.GetSceneByName ("Main").GetRootGameObjects ();
			for (int i = 0; i < goArray.Length; i++) {
				if(goArray[i] != null){
					if(goArray[i].name.Equals("GameArea")){
						goArray [i].transform.localScale = Vector3.one;
						goArray [i].transform.position = Vector3.zero;
					}
				}
			}
			player.rotation = Quaternion.Euler (0, 0, 0);
			player.GetComponent<PlayerController> ().SetBounds (1);
			if(Vector2.Distance(player.position, Vector2.zero) > 4f){
				Vector3 directon = Vector3.zero - player.position;
				player.Translate (directon * 1.5f * Time.deltaTime);
			}
			Global.specialBounds = 0;
			GameObject[] g = GameObject.FindGameObjectsWithTag ("EnemyBullet");
			for (int i = 0; i < g.Length; i++) {
				//g [i].SetActive (false);
				Destroy (g [i], i / 400f);
			}
			GameObject[] e = GameObject.FindGameObjectsWithTag ("Enemy");
			for (int i = 0; i < e.Length; i++) {
				//g [i].SetActive (false);
				Destroy (e [i]);
			}

		}
		if(normalized){
			Death ();
		}
        if (canMove) {
            //transform.Translate (new Vector2 (0, speed * Time.deltaTime));
        }
		if(phase == 5){
			randomSpin -= Time.deltaTime;
			if(randomSpin <= 0){
				randomSpin = Random.value * 6;
				addAngle = -addAngle;
			}
		}
        if (canShoot && noshootTimer <= 0) {
			if(phase == 5){
				int amount = 0;
				if(Global.Difficulty == Global.RECRUIT){
					amount = 4;
				}else if(Global.Difficulty == Global.VETEREN){
					amount = 6;
				}else if(Global.Difficulty == Global.BATTLEH){
					amount = 8;
				}
				bulletAngle += addAngle;
				for(int i = 0; i < amount; i++){
					EnemyProjectile e = Instantiate (bullets [0], Vector2.zero, 
						Quaternion.Euler (0, 0, 360/amount * i + bulletAngle)).GetComponent<EnemyProjectile> ();
					e.Setup (player, 3f, 1);
					e.SetRadius (0.2f);
				}
				noshootTimer = 0.2f;
			}
		}else{
			noshootTimer -= Time.deltaTime;
		}
		if(spawnTimer >= spawnDelay){
			if(phase == 1){
				int spawn = 0;
				if(Global.Difficulty == Global.RECRUIT){
					spawn = 5;
				}else if(Global.Difficulty == Global.VETEREN){
					spawn = 6;
				}else if(Global.Difficulty == Global.BATTLEH){
					spawn = 7;
				}
				float startAngle = Random.Range (0, 361);
				for(int i = 0; i < spawn; i++){
					GameObject e = Instantiate (spawnPrefabs [0]) as GameObject;
					e.transform.rotation = Quaternion.Euler (0, 0, startAngle + (i+1) * (360f/spawn));
					e.transform.position = transform.position;
					spawnTimer = 0;
				}
			}else if(phase == 2){
				int spawn = 0;
				if(Global.Difficulty == Global.RECRUIT){
					spawn = 3;
				}else if(Global.Difficulty == Global.VETEREN){
					spawn = 4;
				}else if(Global.Difficulty == Global.BATTLEH){
					spawn = 5;
				}
				float startAngle = Random.Range (0, 361);
				for(int i = 0; i < spawn; i++){
					GameObject e = Instantiate (spawnPrefabs [1]) as GameObject;
					e.transform.rotation = Quaternion.Euler (0, 0, startAngle + (i+1) * (360f/spawn));
					e.transform.position = transform.position;
					spawnTimer = 0;
				}
			}else if(phase == 3){
				for(int i = 0; i < parts.Length; i++){
					GameObject e = Instantiate (spawnPrefabs [2]) as GameObject;
					e.transform.rotation = parts [i].transform.rotation;
					e.transform.position = parts [i].transform.position;
					spawnTimer = 0;
				}
			}else if(phase == 4){
				GameObject e = Instantiate (spawnPrefabs [3]) as GameObject;
				e.transform.rotation = Quaternion.Euler (0, 0, Random.Range(0,361));
				e.transform.position = transform.position;
				spawnTimer = 0;
			}else if(phase == 5){
				for(int i = 0; i < parts.Length; i++){
					GameObject e = Instantiate (spawnPrefabs [4]) as GameObject;
					e.transform.rotation = Quaternion.Euler (0, 0, Random.Range (0, 361));
					//Vector3 directon = transform.position - Vector3.zero;
					e.transform.position = parts [i].transform.position;
					spawnTimer = 0;
				}
			}else if(phase == 6){
				for(int i = 0; i < parts.Length; i++){
					if (parts [i] != null){
						int spawn = Random.Range (1, 10);
						int amount = 1;
						if(spawn == 1){
							if(Global.Difficulty != Global.RECRUIT){
								spawn = 0;
							}else{
								spawn = 0;
							}
						}else if(spawn == 2 || spawn == 3){
							spawn = 4;
						}else if(spawn == 4 || spawn == 5){
							spawn = 2;
							if(Global.Difficulty == Global.RECRUIT){
								amount = 2;
							}else if(Global.Difficulty == Global.VETEREN){
								amount = 2;
							}else if(Global.Difficulty == Global.BATTLEH){
								amount = 3;
							}
						}else if(spawn == 6 || spawn == 7){
							spawn = 1;
						}else if(spawn == 8 || spawn == 9){
							spawn = 0;
						}
						for(int j = 0; j < amount; j++){
							GameObject e = Instantiate (spawnPrefabs [spawn]) as GameObject;
							e.transform.rotation = Quaternion.Euler (0, 0, Random.Range (0, 361));
							e.transform.position = parts [i].transform.position;
							spawnTimer = 0;
						}

					}
				}
			}

		}else{
			spawnTimer += Time.deltaTime;
		}
    }

    private void Shoot(int i) {
        weapons[i].Shoot();
    }

    private void Death() {
		
        if (!Global.bossMedley)
        {
			GameObject g = Instantiate(wepDropActual) as GameObject;
			g.transform.position = new Vector3 (2, 3, transform.position.z);
			g.GetComponent<WepDrop> ().wepID = 300 + Random.Range (1, wepDrop.GetComponent<DropWeaponOnDestroy>().stage3.Length);
        }


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
			/*
			if(invulnTimer > 0){
				health -= col.gameObject.GetComponent<Projectile>().damage * damageReceivedMultiplier/5f;
			}else{
				health -= col.gameObject.GetComponent<Projectile>().damage * damageReceivedMultiplier;
			}

            Destroy(col.gameObject);
            HitFeedback();
            */
			if(noHit.Count != 0){
				for(int i = 0; i < noHit.Count; i++){
					if(noHit[i] == null){
						noHit.RemoveAt (i);
						i--;
					}else{

					}
				}
				bool isIn = false;
				foreach(GameObject g in noHit){
					if(g == col.gameObject){
						isIn = true;
					}
				}
				if(!isIn){
					noHit.Add (col.gameObject);
					health -= col.gameObject.GetComponent<Projectile>().damage * damageReceivedMultiplier;
				}
			}else{
				noHit.Add (col.gameObject);
				health -= col.gameObject.GetComponent<Projectile>().damage * damageReceivedMultiplier;
			}
			Destroy (col.gameObject);
			HitFeedback ();
        }

        if (col.gameObject.tag == "Sword")
        {
            // This fucks it up for some reason so I hardcoded damage here
            //health -= col.GetComponent<Sword>().damage;
            health -= 10.0f * damageReceivedMultiplier;
            HitFeedback();
        }

		if (col.gameObject.tag == "PlayerBulletPenetrate"){
			if(noHit.Count != 0){
				for(int i = 0; i < noHit.Count; i++){
					if(noHit[i] == null){
						noHit.RemoveAt (i);
						i--;
					}
				}
				foreach(GameObject g in noHit){
					if(g != col.gameObject){
						noHit.Add (col.gameObject);
						health -= col.gameObject.GetComponent<Projectile>().damage * damageReceivedMultiplier;
					}
				}
			}else{
				//this seems bad! What if bullets need to pass over/through something with a collider?
				//removing for now. -J

				noHit.Add (col.gameObject);
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

	/*
    public virtual void OnTriggerExit2D(Collider2D col) {
        if (col.gameObject.tag == "GameArea" && !destroy) {
            destroy = true;
            canShoot = false;
            Invoke("DestroySelf", 1.0f);
        }
    }
	*/

    public virtual void DestroySelf() {
        Destroy(gameObject);
		//GameObject.Find ("StageManager").GetComponent<StageManager> ().LoadStage2 ();
    }


    public void HitFeedback() {
		for(int i = 0; i < sr.Length; i++){
			sr[i].color = new Color(255, 0, 0);
		}
        Invoke("EndFlashSprite", 0.05f);
    }

    public void EndFlashSprite() {
		for(int i = 0; i < sr.Length; i++){
			sr[i].color = new Color(255, 255, 255);
		}
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
