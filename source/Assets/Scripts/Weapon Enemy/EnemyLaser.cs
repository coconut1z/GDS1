using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLaser : WeaponEnemy {

    private GameObject parent;

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
        spread = 2f;
        parent = transform.parent.gameObject;
    }

	// Update is called once per frame
	protected override void Update () {
		base.Update ();
        if (shootTime > shootDelay / 1.2f) {
            lookAtPlayer();
        } 
	}

	public override void Shoot(){
        if (shootTime >= shootDelay) {
            LaserSwitcher e = Instantiate(projectiles[0], shootPos[0].position,
                Quaternion.Euler(0, 0, transform.eulerAngles.z + spread - 2 * Random.value * spread))
                .GetComponent<LaserSwitcher>();
            e.transform.SetParent(parent.transform);
            /*EnemyProjectile e = Instantiate(projectiles[0], shootPos[0].position,
                Quaternion.Euler(0, 0, transform.eulerAngles.z + spread - 2 * Random.value * spread))
                .GetComponent<EnemyProjectile>();
            e.Setup(player, 2, 1);*/
            shootTime = 0;
        }
    }
}
