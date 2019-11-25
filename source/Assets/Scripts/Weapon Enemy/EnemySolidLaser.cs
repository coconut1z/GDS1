using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySolidLaser : WeaponEnemy {

    // Use this for initialization
    protected override void Start () {
		base.Start ();
        if (difficulty == Global.RECRUIT) {
            shootDelay = 3.5f;
        }
        else if (difficulty == Global.VETEREN) {
            shootDelay = 2.5f;
        }
        else if (difficulty == Global.BATTLEH) {
            shootDelay = 1.5f;
        }
        shootTime = shootDelay;
    }

	// Update is called once per frame
	protected override void Update () {
		base.Update ();
        lookAtPlayer();
    }

	public override void Shoot(){
        if (shootTime >= shootDelay) {
            EnemyProjectile e = Instantiate(projectiles[0], shootPos[0].position,
                Quaternion.Euler(0, 0, transform.eulerAngles.z))
                .GetComponent<EnemyProjectile>();
            e.Setup(player, 2, 1);
            shootTime = 0;
        }
    }
}
