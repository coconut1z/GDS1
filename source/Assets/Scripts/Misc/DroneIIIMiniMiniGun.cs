using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneIIIMiniMiniGun : MonoBehaviour {
    public HunterDroneIII HDI;
    private SpriteRenderer sr;
    public Transform shootPos;
    public GameObject projectile;
    private float shootDelay;
    private float shootTime;
    private float spread;
    private Vector2 displacement;
    private float extrashot;
    private AudioManager audioManager;


    // Use this for initialization
    void Start () {
        //shootDelay = 0.07f;
        //shootDelay = 0.01f;
        shootTime = shootDelay;
        spread = 25f;
        displacement = new Vector2(0, 0.1f);
        shootDelay = 0.035f;
        extrashot = 0;
        audioManager = AudioManager.instance;
        if (audioManager == null) {
            Debug.LogError("No audio manager found in scene, whoops");
        }
    }
	
	// Update is called once per frame
	void Update () {
        LookAtTarget();
        shootTime += Time.deltaTime;
    }


    public void Shoot() {
        if (shootTime > shootDelay) {
            audioManager.PlaySound("BulletSpam2"); //change to missile firing sound.
            LookAtTarget();
            // Instantiate (BULLET PREFAB, SHOOT POSITION, Z ROTATION + SPREAD IF THERE IS ANY)
            Instantiate(projectile, shootPos.position,
                Quaternion.Euler(0, 0, transform.eulerAngles.z + spread - 2 * Random.value * spread));
            Instantiate(projectile, shootPos.position,
                Quaternion.Euler(0, 0, transform.eulerAngles.z + spread - 2 * Random.value * spread));
            shootTime = 0;
            extrashot++;
            if (extrashot == 1) {
                extrashot = 0;
                Instantiate(projectile, shootPos.position,
                    Quaternion.Euler(0, 0, transform.eulerAngles.z + spread - 2 * Random.value * spread));
            }
        }
    }

    private void LookAtTarget() {
        if (HDI.targetGameObject == null) {
            return;
        }
        float angleRadToTarget = Mathf.Atan2(
            HDI.targetGameObject.transform.position.y - transform.position.y,
            HDI.targetGameObject.transform.position.x - transform.position.x
        );
        float angleToDeg = (180 / Mathf.PI) * angleRadToTarget - 90;
        transform.rotation = Quaternion.Euler(0, 0, angleToDeg);
    }
}
