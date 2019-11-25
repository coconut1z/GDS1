using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompDroneTempFlak : MonoBehaviour {
    public CompanionDroneI HDI;
    private SpriteRenderer sr;
    public Transform shootPos;
    public GameObject projectile;
    private float shootTime;
    private float shootDelay;
    private float spread;
    private float shotCount;


	// Use this for initialization
	void Start () {
        sr = GetComponent<SpriteRenderer>();
        shootTime = 0.0f;
        shootDelay = 0.75f;
        spread = 15.0f;
        shotCount = 15;
	}
	
	// Update is called once per frame
	void Update () {
        //Debug.Log("Attempting to update");
        LookAtTarget();
        shootTime += Time.deltaTime;
    }

    public void Shoot() {
        if (shootTime > shootDelay) {
            LookAtTarget();
            // Instantiate (BULLET PREFAB, SHOOT POSITION, Z ROTATION + SPREAD IF THERE IS ANY)
            for (int i = 0; i < shotCount; i++) {
                Instantiate(projectile, shootPos.position,
                Quaternion.Euler(0, 0, transform.eulerAngles.z + spread - 2 * UnityEngine.Random.value * spread));
            }
            shootTime = 0;
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
