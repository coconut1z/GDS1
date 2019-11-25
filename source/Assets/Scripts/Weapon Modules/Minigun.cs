using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minigun : WeaponModule {

    // Use this for initialization
	private float extrashot;

    protected override void Start() {
        base.Start();
        spooling = false;
        spoolDelay = 2.0f;
        spoolTime = 0.0f;
        shootDelay = 0.03f;
        shootTime = shootDelay;
        spread = 20f;
		originalSize = new Vector2 (1, 1);
        anim = GetComponent<Animator>();
		weaponId = Global.MINIGUN;
		stage = 2;
		extrashot = 0;
        audioManager = AudioManager.instance;
        if (audioManager == null) {
            Debug.LogError("No audio manager found in scene, whoops");
        }
    }

    // Update is called once per frame
    protected override void Update() {
        base.Update();
        anim.SetFloat("SpoolSpeed", (spoolTime*1.5f));
        if (spooling) {//if weapon is spinning and
            if (spoolTime > 0.0f) { //not at the minimum spin time and
                if (Input.GetKey(KeyCode.Mouse0) == false) {//if mb1 not held;
                    spoolTime -= Time.deltaTime; //reduce the spool time. multipliy or divide TDT for faster or slower dropoff.
                }
            }
            else {
                spooling = false;//once done, reset spooling boolean.
            }
        }
    }

    public override void Shoot() {
        //spoolTime += Time.deltaTime;
        spooling = true;
        if (spoolTime < spoolDelay) { //if not fulled spooled;
            spoolTime += Time.deltaTime;//continue spooling.
        }
        //Debug.Log("spoolTime = " + spoolTime + " and spoolDelay = " + spoolDelay); //Debug to show relavent variables.
		//print(seed);
		if (shootTime > shootDelay && spoolTime >= spoolDelay) { //if spooled AND waited the time following the last shot;
            audioManager.PlaySound("Megaminigun");
            // Instantiate (BULLET PREFAB, SHOOT POSITION, Z ROTATION + SPREAD IF THERE IS ANY)
            Instantiate(projectiles[0], shootPos[0].position,
                Quaternion.Euler(0, 0, transform.eulerAngles.z + spread - 2 * Random.value * spread));
            shootTime = 0;
			extrashot++;
			if(extrashot == 2){
				extrashot = 0;
                Instantiate(projectiles[0], shootPos[0].position,
                    Quaternion.Euler(0, 0, transform.eulerAngles.z + spread - 2 * Random.value * spread));
			}
        }
    }

	public override int ReturnId(){
		return Global.MINIGUN;
	}
}
