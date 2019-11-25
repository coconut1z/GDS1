using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncingBulletDamager : MonoBehaviour {
    private BouncingBullet BB;
    private float damage;
    private string UID;
    private CircleCollider2D hitbox;
    public List<string> previousEnemies = new List<string>();
    public GameObject hitParticle;
    private PlayerController player;

    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        BB = GetComponentInParent<BouncingBullet>();
        damage = 2.9f * player.playerDamageMultiplier;
        UID = "BouncingBastard" + UnityEngine.Random.value.ToString();
        hitbox = GetComponent<CircleCollider2D>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}


    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Enemy" || other.tag == "Boss") {
            DamageEnemy(other);
            BB.HitTarget();
            hitbox.enabled = false;
            Invoke("EnableCollider", 0.1f);
        }

    }

    private void EnableCollider() {
        hitbox.enabled = true;
    }

    private void DamageEnemy(Collider2D other) {
        //only hit an enemy once.
        foreach (string str in previousEnemies) { //for each name in the list
            if (str == other.name) { //if list name == current collided name
                return; //skip it.
            }
        }
        previousEnemies.Add(other.name);
        Instantiate(hitParticle, transform.position, Quaternion.identity);
        if (other.tag == "Enemy") {
            Enemy enemyscript = other.GetComponent<Enemy>();
            enemyscript.DamageEnemy(damage, UID);
            //side note, would be nice if enemies (and bosses!!!) had a method I could call rather than directly modifying
            //the variables and invoking the methods.
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
            else if (other.transform.parent != null) {
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
        }
}
}
