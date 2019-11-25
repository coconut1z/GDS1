using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser1 : MonoBehaviour {

	private float damage;
	private string UID;


	// Use this for initialization
	void Start () {
		UID = Random.value.ToString ();
		damage = 2f;
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnTriggerStay2D(Collider2D other) {
        if (other.CompareTag("Enemy")) {
			if (other.GetComponent<Enemy>().visible) {
                other.GetComponent<Enemy>().DamageEnemy(damage, UID);
            } 
        }
		if (other.CompareTag("Boss")) {
            if (other.GetComponent<Stage1Boss>()) {
                other.GetComponent<Stage1Boss>().DamageEnemy(damage, UID);
            }
            else if (other.GetComponent<SnekSegment>()) {
                other.GetComponent<SnekSegment>().DamageEnemy(damage, UID);
            }
            else if (other.GetComponent<SnekHead>()) {
                other.GetComponent<SnekHead>().DamageEnemy(damage, UID);
            }
            else if (other.GetComponent<Stage2Boss>()) {
                other.GetComponent<Stage2Boss>().DamageEnemy(damage, UID);
            }
            else if (other.GetComponent<Stage3BossPart>()) {
                other.GetComponent<Stage3BossPart>().DamageEnemy(damage, UID);
            }
            else if (other.GetComponent<Stage5Boss>()) {
                other.GetComponent<Stage5Boss>().DamageEnemy(damage, UID);
            }
            else if (other.GetComponent<BossaCoreScript>()) {
                other.GetComponent<BossaCoreScript>().DamageEnemy(damage, UID);
            }
            else if (other.GetComponent<WeakBoss1>()) {
                other.GetComponent<WeakBoss1>().DamageEnemy(damage, UID);
            }
            else if (other.GetComponent<WeakBoss4>()) {
                other.GetComponent<WeakBoss4>().DamageEnemy(damage, UID);
            }
            else if (other.GetComponent<WeakBoss5>()) {
                other.GetComponent<WeakBoss5>().DamageEnemy(damage, UID);
            }
            else if (other.transform.parent != null) {//must be last in list.
                if (other.transform.parent.GetComponent<Stage3Boss>()) {
                    other.transform.parent.GetComponent<Stage3Boss>().DamageEnemy(damage, UID);
                }
                if (other.transform.parent.GetComponent<WeakBoss2>()) {
                    other.transform.parent.GetComponent<WeakBoss2>().DamageEnemy(damage, UID);
                }
                if (other.transform.parent.GetComponent<Stage6Boss>()) {
                    other.transform.parent.GetComponent<Stage6Boss>().DamageEnemy(damage, UID);
                }
            }
            //I think I got em all. add to this with new bosses, but otherwise done for now.
        }
	}
}
