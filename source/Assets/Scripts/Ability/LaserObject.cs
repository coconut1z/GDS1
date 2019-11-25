using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserObject : MonoBehaviour {
    private float hitboxLifespan;
    private float hitboxSpawntime;
    private float damage;
    private CapsuleCollider2D hitbox;
    private float timeSinceSpawn;
    private bool laserDamaging;
    private string UID;
    private float bossDamage;
	// Use this for initialization
	void Start () {
        hitbox = GetComponent<CapsuleCollider2D>();
        timeSinceSpawn = 0.0f;
        laserDamaging = false;
        hitboxLifespan = 2.8f;
        hitboxSpawntime = 1.5f;
        damage = 20.0f;
        bossDamage = 7.145f;
        UID = "BIG FUCK-OFF LASER" + UnityEngine.Random.value.ToString();
        AudioManager.instance.PlaySound("BFOCharge");
	}
	
	// Update is called once per frame
	void Update () {
        timeSinceSpawn += Time.deltaTime;
        CheckHitboxTime();
        CheckDeathTime();
	}

    private void CheckDeathTime() {
        if(timeSinceSpawn >= 6.0f) {
            //return player movement speed;
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().playerSpeedMultiplier *= 2.0f; //minor abomination, but it's a single call, should be fine..
            Destroy(gameObject);
        }
    }

    private void CheckHitboxTime() {
        if (!laserDamaging) {
            if (timeSinceSpawn >= hitboxSpawntime) {
                laserDamaging = true;
                hitbox.enabled = true;
                AudioManager.instance.PlayLoopedSound("BossBeamLoop");
            }
        }
        if (laserDamaging) {
            if (timeSinceSpawn >= hitboxSpawntime + hitboxLifespan) {
                hitbox.enabled = false;
                AudioManager.instance.StopSound("BossBeamLoop");
            }//I am aware it makes logical sense to flip laserDamaging back over. but this works perfectly
        }//with just 4 if statements and they're nested to take minimal performance. Leave plz :)
    }

    void OnTriggerStay2D(Collider2D other) {
        if (other.CompareTag("Enemy")) {
			other.GetComponent<Enemy>().DamageEnemy(damage, UID);// better
        }
        if (other.CompareTag("Boss")){ //2.8s of HB lifespan = 14 hits. Boss damage simplified to 7.145f. So, adding below multipliers to balance per boss.
            //14*7.145 = 100 damage. Work it from there.
            if (other.GetComponent<Stage1Boss>()){
                other.GetComponent<Stage1Boss>().DamageEnemy(bossDamage*0.6f, UID); //phases are 350/283//216/150, so cannot one-shot any phase.
            }
            else if (other.GetComponent<SnekSegment>()){
                other.GetComponent<SnekSegment>().DamageEnemy(bossDamage*0.9f, UID); //will almost oneshot on BH (120hp atm).
            }
            else if (other.GetComponent<SnekHead>()){
                other.GetComponent<SnekHead>().DamageEnemy(bossDamage*0.9f, UID); //irrelevant tbh, but will similarly almost kill it.
            }
            else if (other.GetComponent<Stage2Boss>()){
                other.GetComponent<Stage2Boss>().DamageEnemy(bossDamage*1.0f, UID); //frankly, this one's hp is a mystery to me. This should complete one of its many phases.
            }
            else if (other.GetComponent<Stage3BossPart>()){
                other.GetComponent<Stage3BossPart>().DamageEnemy(bossDamage*0.3f, UID);//lol @ b3p health = 35*1.5.
            }
            else if (other.GetComponent<Stage5Boss>()){
                other.GetComponent<Stage5Boss>().DamageEnemy(bossDamage*1.1f, UID); //will complete 1 of many phases.
            }
            else if (other.GetComponent<BossaCoreScript>()){
                other.GetComponent<BossaCoreScript>().DamageEnemy(bossDamage * 0.3f, UID); //*.3 = 30
            }
            else if (other.GetComponent<WeakBoss1>()) {
                other.GetComponent<WeakBoss1>().DamageEnemy(bossDamage * 1.5f, UID); //*.3 = 30
            }
            else if (other.GetComponent<WeakBoss4>()) {
                other.GetComponent<WeakBoss4>().DamageEnemy(bossDamage * 1.8f, UID); //*.3 = 30
            }
            else if (other.GetComponent<WeakBoss5>()) {
                other.GetComponent<WeakBoss5>().DamageEnemy(bossDamage * 1.9f, UID); //*.3 = 30
            }
            else if (other.transform.parent != null) {//must be last in list.
                if (other.transform.parent.GetComponent<Stage3Boss>()) {
                    other.transform.parent.GetComponent<Stage3Boss>().DamageEnemy(bossDamage * 0.8f, UID); //80 damage vs 100 per phase.
                }
                if (other.transform.parent.GetComponent<WeakBoss2>()) {
                    other.transform.parent.GetComponent<WeakBoss2>().DamageEnemy(bossDamage * 0.9f, UID); //80 damage vs 100 per phase.
                }
                if (other.transform.parent.GetComponent<Stage6Boss>()) {
                    other.transform.parent.GetComponent<Stage6Boss>().DamageEnemy(bossDamage * 2.5f, UID); //*.3 = 30
                }
            }
            //I think I got em all. add to this with new bosses, but otherwise done for now.
        }
    }
    
}
