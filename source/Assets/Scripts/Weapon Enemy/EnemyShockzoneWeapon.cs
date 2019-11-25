using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShockzoneWeapon : WeaponEnemy {

    // Use this for initialization
    protected override void Start() {
        base.Start();
        shootDelay = 0.2f;
        shootTime = shootDelay;
    }

    // Update is called once per frame
    protected override void Update() {
        base.Update();
        //lookAtPlayer(); //not needed for minelayer weapon.
    }

    public override void Shoot() {
        if (shootTime > shootDelay) {
            // Instantiate (BULLET PREFAB, SHOOT POSITION, Z ROTATION + SPREAD IF THERE IS ANY)

            EnemyProjectile e = Instantiate(projectiles[0], shootPos[0].position,
				Quaternion.Euler(0, 0, transform.eulerAngles.z + spread - 2 * Random.value * spread)).GetComponent<EnemyProjectile>();
			e.Setup (player);
            shootTime = 0;
            
        }
    }
}
