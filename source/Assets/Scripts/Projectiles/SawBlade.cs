using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawBlade : MonoBehaviour {
    private float damage;
    private float bossDamage;
    private float maxSpeed;
    private float currentSpeed;
    private bool returning;
    private Rigidbody2D rb;
    private float time;
    private float accelerationTime;
    public Buzzsaw buzzsaw; //set in editor    
    public Transform lTransform; //launcher transform, set in editor
    public Transform shootPosTransform; //set in editor
    private bool fired;
    private string UID;
    private PlayerController player;

	// Use this for initialization
	void Start () {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        damage = 3.8f * player.playerDamageMultiplier;
        if(player.playerDamageMultiplier == 0.0f) {
            damage = 3.8f;
        }
        bossDamage = 5.0f;
        maxSpeed = 20.0f;
        currentSpeed = maxSpeed;
        rb = GetComponent<Rigidbody2D>();
        time = 0;
        accelerationTime = 0.75f;
        returning = false;
        fired = false;
        UID = "SawBlade" + UnityEngine.Random.value.ToString();
        //to target the firePos instead.
        GetComponent<CircleCollider2D>().radius = 4.0f;
    }

    public void Launch() {
        time = 0;
        //could also enable/disable trigger collider. frankly though, if people wanna use it as a melee weapon, let them.
        returning = false;
        transform.parent = null; //free sawblade from launcher gun
        transform.position = shootPosTransform.position;
        transform.localScale = new Vector3(0.1f, 0.1f, 1.0f);
        fired = true;
    }
	
	// Update is called once per frame
	void Update () {
        if (!fired) { return; } //only update if fired.
        SendPhase();
        if (returning) {
            ReturnPhase();
        }
	}

    private void ReturnPhase() {
        float AngleRadToMouse = Mathf.Atan2(
            shootPosTransform.position.y - transform.position.y,
            shootPosTransform.position.x - transform.position.x
        );
        float AngleToDeg = (180 / Mathf.PI) * AngleRadToMouse - 270;
        transform.rotation = Quaternion.Euler(0, 0, AngleToDeg);
        checkReturned();//check if reached player
    }

    private void checkReturned() {
        float distance = Vector2.Distance(transform.position, shootPosTransform.position);
        //Debug.Log("Distance = " + distance);
        if (distance <= 0.2f) {
            buzzsaw.fired = false;
            transform.parent = lTransform; //set transform to the parent gun.
            transform.position = lTransform.position;
            transform.localScale = new Vector3(0.05f, 0.05f, 1.0f);
            transform.rotation = lTransform.rotation;
            fired = false;
        }
    }

    private void SendPhase() {
        currentSpeed = Mathf.Lerp(maxSpeed, -20.0f, time / accelerationTime);
        transform.position -= transform.up * -1.0f * currentSpeed * Time.deltaTime;
        time += Time.deltaTime;
        if (currentSpeed <= 0.0f) {
            returning = true;
        }
    }

    void OnTriggerStay2D(Collider2D other) {
        if (other.CompareTag("Enemy")) {
            Debug.Log("Entered enemy");
            other.GetComponent<Enemy>().DamageEnemy(damage, UID);// better
            Debug.Log("Done. " + damage);
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
