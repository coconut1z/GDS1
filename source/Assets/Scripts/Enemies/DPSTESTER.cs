using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DPSTESTER : MonoBehaviour {

	private float damage;
    private float damageReceivedMultiplier;
    public bool visible;
    private List<GameObject> noHit;
	private List<string> wepIDs = new List<string>();
	private float lastClearTime;
	private float specialHitInterval = 0.2f;

	// Use this for initialization
	void Start () {
        damageReceivedMultiplier = 1;
        noHit = new List<GameObject>();
		damage = 0;
		InvokeRepeating ("calculateDPS", 0, 10.0f);
        //specialHitInterval = 0.2f;
        //test

	}
	
	// Update is called once per frame
	void Update () {
        if (damageReceivedMultiplier > 1.45f) {
            damageReceivedMultiplier = 1.45f;
        }
		specialHitInterval = 0.2f;
		wepIDs = new List<string>();
	}

	public virtual void OnTriggerEnter2D(Collider2D col){
        if (col.gameObject.name.Contains("RedoxBullet")) {
            damage += col.gameObject.GetComponent<Projectile>().damage * damageReceivedMultiplier;
            damageReceivedMultiplier += 0.15f;
            Destroy(col.gameObject);
            return;
        }
        if (col.gameObject.tag == "PlayerBullet"){
			damage += col.gameObject.GetComponent<Projectile> ().damage * damageReceivedMultiplier;
			Destroy (col.gameObject);
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
				foreach(GameObject g in noHit){
					if(g != col.gameObject){
						noHit.Add (col.gameObject);
						damage += col.gameObject.GetComponent<Projectile>().damage * damageReceivedMultiplier;
					}
				}
			}else{
				noHit.Add (col.gameObject);
				damage += col.gameObject.GetComponent<Projectile>().damage * damageReceivedMultiplier;
			}
		}
	}

	private void calculateDPS(){
        print("Damage multiplier: " + damageReceivedMultiplier);
		print ("DPS: " + damage/10);
		damage = 0;
	}

	public void DamageEnemy(float damage, string wepID) {
		if(Time.time > lastClearTime + specialHitInterval) {
			wepIDs.Clear();
			lastClearTime = Time.time;
		}
		foreach (string listWepID in wepIDs) { 
			if(wepID == listWepID) {
				return;
			}
		}
		this.damage+= damage;
		wepIDs.Add(wepID);
	}


}
