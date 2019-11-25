using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericExplosionDamage : MonoBehaviour {
    //currently using on the railgun projectile's explosion.
    //add to a gameobject with a circle collider.
    //it'll deactivate the collider after .1s (1 damage tick).
    //alternately, modify public lifespan for more.
    //if you need other colliders, please dupe this script or modify for any attached colliders.

    private PlayerController player;

    public float lifespan = 0.1f; //hitbox lifespan
    public float objectLifespan = 1.3f; //object lifespan (should not be needed. Use particle Stop Action (destroy)).
    private CircleCollider2D hitbox;
    public float damage;
    private string UID; //used to prevent repeat damage.
    // Use this for initialization
    void Start() {
        hitbox = GetComponent<CircleCollider2D>();
        UID = "Generic Explosion" + UnityEngine.Random.value.ToString();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        damage *= player.playerDamageMultiplier;
    }

    // Update is called once per frame
    void Update() {
        lifespan -= Time.deltaTime;
        if (lifespan <= 0.0f) {
            hitbox.enabled = false;
        }

        objectLifespan -= Time.deltaTime;
        if (objectLifespan <= 0.0f) {
            Destroy(gameObject);
        }
    }

    void OnTriggerStay2D(Collider2D other) {
        if (other.CompareTag("Enemy")) {
            other.GetComponent<Enemy>().DamageEnemy(damage, UID);// better
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
        }
}
}
