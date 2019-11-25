using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneIIShotty : MonoBehaviour {
    public HunterDroneII HDI;
    private SpriteRenderer sr;
    public Transform shootPos;
    public GameObject projectile;
    private float shootDelay;
    private float shootTime;
    private float spread;
    private Vector2 displacement;
    private AudioManager audioManager;


    // Use this for initialization
    void Start () {
        shootDelay = 0.38f / 1.5f;
        shootTime = shootDelay;
        spread = 1f;
        displacement = new Vector2(0, 0.1f);
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
            LookAtTarget();
            // Instantiate (BULLET PREFAB, SHOOT POSITION, Z ROTATION + SPREAD IF THERE IS ANY)
            Instantiate(projectile, shootPos.position,
                Quaternion.Euler(0, 0, transform.eulerAngles.z - 10));
            Instantiate(projectile, shootPos.position,
                Quaternion.Euler(0, 0, transform.eulerAngles.z - 5));           //not sure if this is how we have multiple bullets
            Instantiate(projectile, shootPos.position,                   //it tis, each instantiate is a new bullet -john
                Quaternion.Euler(0, 0, transform.eulerAngles.z + 5));
            Instantiate(projectile, shootPos.position,
                Quaternion.Euler(0, 0, transform.eulerAngles.z + 10));
            Instantiate(projectile, shootPos.position,
                Quaternion.Euler(0, 0, transform.eulerAngles.z - 2.5f));
            Instantiate(projectile, shootPos.position,
                Quaternion.Euler(0, 0, transform.eulerAngles.z + 2.5f));
            Instantiate(projectile, shootPos.position,
                Quaternion.Euler(0, 0, transform.eulerAngles.z - 7.5f));
            Instantiate(projectile, shootPos.position,
                Quaternion.Euler(0, 0, transform.eulerAngles.z + 7.5f));
            shootTime = 0;
            audioManager.PlaySound("Flak1"); //change to missile firing sound.
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
