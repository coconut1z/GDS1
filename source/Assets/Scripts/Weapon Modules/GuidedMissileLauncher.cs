using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuidedMissileLauncher : WeaponModule {
    //this script should be safely deleted. Will check later. -J
    protected override void Start() {
        base.Start();
        spooling = false;
        shootDelay = 1.5f; //was 1.0f
        shootTime = shootDelay;
        spread = 0.0f;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    protected override void Update() {
        base.Update();
        
        }

    public override void Shoot() {
        if (shootTime > shootDelay) {
            // Instantiate (BULLET PREFAB, SHOOT POSITION, Z ROTATION + SPREAD IF THERE IS ANY)
            Instantiate(projectiles[0], shootPos[0].position,
                Quaternion.Euler(0, 0, transform.eulerAngles.z + spread - 2 * Random.value * spread));
            anim.SetTrigger("Fire");
            shootTime = 0;

        }
    }

	public override int ReturnId(){
		return -1;
	}
}
