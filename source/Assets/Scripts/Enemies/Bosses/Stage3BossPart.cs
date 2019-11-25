using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage3BossPart : MonoBehaviour {

	public float step;
	public float stepSpeed;
	public bool canOscillate;
	private bool returning;
	private Rigidbody2D rb;
	public float health;
	private SpriteRenderer sr;
	public GameObject sparks;
	private float invulnTimer;
	private float damageReceivedMultiplier;
	public float rotationSpeed;
	private bool speedSet;
    private List<string> wepIDs = new List<string>();
    private float lastClearTime;
    private float specialHitInterval = 0.2f;
	public float hpMult;

    public GameObject death1;
    public GameObject death2;

    // Use this for initialization
    void Start () {
		step = 0;
		canOscillate = false;
		stepSpeed = 0.068f;
		returning = false;
		hpMult = 1.5f;
		health = 35f * hpMult;
		damageReceivedMultiplier = 1;
		sr = GetComponent<SpriteRenderer> ();
		rotationSpeed = 50f;
	}

	// Update is called once per frame
	void Update () {
		transform.RotateAround (transform.parent.position, Vector3.forward, rotationSpeed * Time.deltaTime);
		if(canOscillate){
			Oscillate ();
		}
		if (health <= 0) {
			Death();
		}
	}

	private void Death(){		
		for(int i = 0; i < transform.parent.childCount; i++){
			if(transform.parent.GetChild(i).name != "SpawnPos"){
				transform.parent.GetChild (i).GetComponent<Stage3BossPart> ().rotationSpeed =
					transform.parent.GetChild (i).GetComponent<Stage3BossPart> ().rotationSpeed * 1.5f;
				transform.parent.GetChild (i).GetComponent<Stage3BossPart> ().health += 25f * hpMult;
			}
		}
		if(hpMult == 0.25f){
			if(Global.Difficulty == Global.RECRUIT){
				transform.parent.GetComponent<WeakBoss2> ().spawnDelay -= 0.4f;
			}else if(Global.Difficulty == Global.VETEREN){
				transform.parent.GetComponent<WeakBoss2> ().spawnDelay -= 0.4f;
			}else if(Global.Difficulty == Global.BATTLEH){
				transform.parent.GetComponent<WeakBoss2> ().spawnDelay -= 0.4f;
			}
			transform.parent.GetComponent<WeakBoss2> ().deadParts++;
		}else{
			if(Global.Difficulty == Global.RECRUIT){
				transform.parent.GetComponent<Stage3Boss> ().spawnDelay -= 1.25f;
			}else if(Global.Difficulty == Global.VETEREN){
				transform.parent.GetComponent<Stage3Boss> ().spawnDelay -= 0.75f;
			}else if(Global.Difficulty == Global.BATTLEH){
				transform.parent.GetComponent<Stage3Boss> ().spawnDelay -= 1.1f;
			}
			transform.parent.GetComponent<Stage3Boss> ().deadParts++;
		}

        Instantiate(death1, transform.position, Quaternion.identity);
        Instantiate(death2, transform.position, Quaternion.identity);
			
		Destroy (gameObject);
	}
		
	private void Oscillate(){
		step += stepSpeed * Time.deltaTime * 75f;
		Vector3 directon = transform.position - transform.parent.position;
		//if(Vector2.Distance(Vector2.zero, transform.position) > 0.8f && !returning){
		//	transform.Translate (directon * 1.5f * Time.deltaTime * Mathf.Sin(step), Space.World);
		//}else if(Vector2.Distance(Vector2.zero, transform.position) < 4f && returning){
		//	transform.Translate (directon * 1.5f * Time.deltaTime * Mathf.Sin(step), Space.World);
		//}
		if (returning && Mathf.Sin (step) <= 0) {
			transform.Translate (directon * 1.5f * Time.deltaTime * Mathf.Sin (step), Space.World);
		} else if (!returning && Mathf.Sin (step) >= 0){
			transform.Translate (directon * 1.5f * Time.deltaTime * Mathf.Sin (step), Space.World);
		}
			

		if(Vector2.Distance(transform.parent.position, transform.position) <= 0.8 && returning){
			returning = false;
		}else if(Vector2.Distance(transform.parent.position, transform.position) >= 6 && !returning)
			returning = true;
		if(step >= Mathf.PI){
			
		}
		if(step >= 2 * Mathf.PI){
			//canOscillate = false;
		}
	}

	public virtual void OnTriggerEnter2D(Collider2D col) {
		if(canOscillate){
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

			if (col.gameObject.tag == "PlayerBulletPenetrate")
			if(invulnTimer > 0){
				health -= col.gameObject.GetComponent<Projectile>().damage * damageReceivedMultiplier/5f;
				HitFeedback();
			}else{
				health -= col.gameObject.GetComponent<Projectile>().damage * damageReceivedMultiplier;
				HitFeedback();
			}
		}
	}
		
	public void HitFeedback() {
		sr.color = new Color(255, 0, 0);
		Invoke("EndFlashSprite", 0.05f);
	}

	public void EndFlashSprite() {
		sr.color = new Color(255, 255, 255);
	}

    public void DamageEnemy(float damage, string wepID) {
		if(canOscillate){
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
}
