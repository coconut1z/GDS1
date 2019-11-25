using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//The arc emitter; creates the sparks that reach out from the projectile and damage enemies.


public class PlasmaArcEmitter : MonoBehaviour {
    private float damageInterval;
    public float damageCountdown;
    private LineRenderer lr;
    public bool ready;
    private float arcDamage;
    private float damage;
    private string UID;
    private AudioManager audioManager;
    private PlayerController player;

    // Use this for initialization
    void Start() {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        lr = GetComponent<LineRenderer>();
        damageInterval = 0.1f;
        damageCountdown = damageInterval;
        arcDamage = 1.45f;
        damage = arcDamage * player.playerDamageMultiplier; //multiplier here.
        UID = "Plasmacoil" + UnityEngine.Random.value.ToString();
        audioManager = AudioManager.instance;
        if (audioManager == null) {
            Debug.LogError("No audio manager found in scene, whoops");
        }
        //audioManager.PlaySound ("Zap1");
    }

    // Update is called once per frame
    void Update() {
        ReadyCheck();
    }

    private void ReadyCheck() {
        if (ready) { return; }
        damageCountdown -= Time.deltaTime;
        if(damageCountdown <= 0.0f) {
            ready = true;
            lr.enabled = false;
        }
    }

    void OnTriggerStay2D(Collider2D other) {
		if (!ready) {
			return;
		} //check if ready to arc first. Saves a bit of cpu.

		if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "Boss") {
			lr.enabled = true;
			lr.positionCount = 2;
			Vector3 surfaceTarget = other.gameObject.GetComponent<Collider2D> ().bounds.ClosestPoint (transform.position); //hopefully targets the outer part of the collider.
			lr.SetPosition (0, transform.position);//from inside of ball to
			lr.SetPosition (1, surfaceTarget);//enemy's collider edge.
			audioManager.PlaySound ("Zap1");
            damageCountdown = damageInterval;//we're about to hit an enemy. this will keep us from hitting another enemy for 0.1s
            ready = false;

			if (other.gameObject.tag == "Enemy") {//if it's a regular enemy
				Enemy targetScript = other.GetComponent<Enemy> ();//get the enemy script
				targetScript.health -= arcDamage;//damage it. hopefully.
				targetScript.HitFeedback ();
			}
			if (other.CompareTag ("Boss")) {
				if (other.GetComponent<Stage1Boss> ()) {
					other.GetComponent<Stage1Boss> ().DamageEnemy (damage, UID);
				} else if (other.GetComponent<SnekSegment> ()) {
					other.GetComponent<SnekSegment> ().DamageEnemy (damage, UID);
				} else if (other.GetComponent<SnekHead> ()) {
					other.GetComponent<SnekHead> ().DamageEnemy (damage, UID);
				} else if (other.GetComponent<Stage2Boss> ()) {
					other.GetComponent<Stage2Boss> ().DamageEnemy (damage, UID);
				}  else if (other.GetComponent<Stage3BossPart> ()) {
					other.GetComponent<Stage3BossPart> ().DamageEnemy (damage, UID);
				} else if (other.GetComponent<Stage5Boss> ()) {
					other.GetComponent<Stage5Boss> ().DamageEnemy (damage, UID);
				} else if (other.GetComponent<BossaCoreScript> ()) {
					other.GetComponent<BossaCoreScript> ().DamageEnemy (damage, UID);
				} else if (other.GetComponent<WeakBoss1> ()) {
					other.GetComponent<WeakBoss1> ().DamageEnemy (damage, UID);
				} else if (other.GetComponent<WeakBoss4> ()) {
					other.GetComponent<WeakBoss4> ().DamageEnemy (damage, UID);
				} else if (other.GetComponent<WeakBoss5> ()) {
					other.GetComponent<WeakBoss5> ().DamageEnemy (damage, UID);
				} else if (other.transform.parent != null) {
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

	}}
