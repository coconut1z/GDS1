using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WeakBoss2 : MonoBehaviour {

	public GameObject[] spawnPrefabs;
    public SpriteRenderer[] sr;
    public float health;
    private bool canMove;
    private bool canShoot;
    private bool destroy;
	private float invulnTimer;
	private float noshootTimer;
    private float damageReceivedMultiplier;
    public GameObject sparks;
	private Transform player;
	private Stage3BossPart[] parts;
	public float spawnTimer, spawnDelay;
	public List<GameObject> noHit;
	public int deadParts;
    private List<string> wepIDs = new List<string>();
    private float lastClearTime;
    private float specialHitInterval = 0.2f;

    private AbilityRewardsDropList dropManager;

    public GameObject death1;
    public GameObject death2;

    public GameObject spawn1;
    public GameObject spawn2;

    // Use this for initialization
    void Start() {	
		health = 0;
        canMove = true;
        canShoot = true;
        destroy = false;
        damageReceivedMultiplier = 1;
        sparks.SetActive(false);
		sr = GetComponentsInChildren<SpriteRenderer> ();
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		parts = GetComponentsInChildren<Stage3BossPart> ();
		spawnTimer = 0;
		spawnDelay = 2f;
		deadParts = 0;

		Invoke ("LateStart", 0.01f);

        Instantiate(spawn1, transform.position, Quaternion.identity);
        Instantiate(spawn2, transform.position, Quaternion.identity);

    }

	private void LateStart(){
		for(int i = 0; i < parts.Length; i++){
			parts [i].canOscillate = true;
			parts [i].hpMult = 0.25f;
			parts [i].health = 20f;
		}
		Destroy (GetComponent<Rigidbody2D>());
		if(Global.Difficulty == Global.RECRUIT){
			spawnDelay = 5f;
		}else if(Global.Difficulty == Global.VETEREN){
			spawnDelay = 5f;
		}else if(Global.Difficulty == Global.BATTLEH){
			spawnDelay = 5;
		}
	}

    // Update is called once per frame
    void Update() {
		if(invulnTimer > 0){
			invulnTimer -= Time.deltaTime;
		}
		if (health <= 0 && deadParts == 8) {
			SceneManager.LoadSceneAsync ("F3", LoadSceneMode.Additive);
            Death();
        }

        if (canMove) {
            //transform.Translate (new Vector2 (0, speed * Time.deltaTime));
        }
		if(spawnTimer >= spawnDelay){
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
		}else{
			spawnTimer += Time.deltaTime;
		}
    }

    private void Shoot(int i) {
        
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
