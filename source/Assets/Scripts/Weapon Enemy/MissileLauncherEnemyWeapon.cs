using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileLauncherEnemyWeapon : WeaponEnemy {

	// Use this for initialization
	protected override void Start () {
		base.Start ();
        shootDelay = 3.0f; //default value. Just in case.
		if(difficulty == Global.RECRUIT){
			shootDelay = 3.0f;
		}else if(difficulty == Global.VETEREN){
			shootDelay = 2.0f;
		}else if(difficulty == Global.BATTLEH){
			shootDelay = 1.0f;
		}
		shootTime = shootDelay;
	}

	// Update is called once per frame
	protected override void Update () {
		base.Update ();
		lookAtPlayer ();
	}

    public override void Shoot() {
        if (shootTime > shootDelay) {
            shootTime = 0.0f;
            // Instantiate (BULLET PREFAB, SHOOT POSITION, Z ROTATION + SPREAD IF THERE IS ANY)
            EnemyProjectile e = Instantiate (projectiles [0], shootPos [0].position, 
				Quaternion.Euler (0, 0, transform.eulerAngles.z + spread - 2 * Random.value * spread))
				.GetComponent<EnemyProjectile>();
            /*
			e.Setup (player, 2.18f, 1);
			e.SetRadius (0.1f);
            Debug.Log("shoot time = " + shootTime);
			shootTime = 0.0f;
            Debug.Log("2shoot time = " + shootTime); */
        }
	}
}
