using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySuperRNG : WeaponEnemy {

	// Use this for initialization
	protected override void Start () {
		base.Start ();
		if(difficulty == Global.RECRUIT){
			shootDelay = 4f;
		}else if(difficulty == Global.VETEREN){
			shootDelay = 2.5f;
		}else if(difficulty == Global.BATTLEH){
			shootDelay = 1.0f;
		}
		shootTime = shootDelay;
        spread = 8f;
	}

	// Update is called once per frame
	protected override void Update () {
		base.Update ();
		lookAtPlayer ();
	}

	public override void Shoot(){
		if(shootTime > shootDelay){
			// Instantiate (BULLET PREFAB, SHOOT POSITION, Z ROTATION + SPREAD IF THERE IS ANY)
			EnemyProjectile e = Instantiate (projectiles [0], shootPos [0].position, 
				Quaternion.Euler (0, 0, transform.eulerAngles.z + 30f + spread - 2 * Random.value * spread))
				.GetComponent<EnemyProjectile>();
			e.Setup (player, 2.18f, 1);
			e.SetRadius (0.1f);
            EnemyProjectile e1 = Instantiate(projectiles[0], shootPos[0].position,
                Quaternion.Euler(0, 0, transform.eulerAngles.z + 20f + spread - 2 * Random.value * spread))
                .GetComponent<EnemyProjectile>();
            e1.Setup(player, 2.18f, 1);
            e1.SetRadius(0.1f);
            EnemyProjectile e2 = Instantiate(projectiles[0], shootPos[0].position,
                Quaternion.Euler(0, 0, transform.eulerAngles.z + spread - 2 * Random.value * spread))
                .GetComponent<EnemyProjectile>();
            e2.Setup(player, 2.18f, 1);
            e2.SetRadius(0.1f);
            EnemyProjectile e3 = Instantiate(projectiles[0], shootPos[0].position,
                Quaternion.Euler(0, 0, transform.eulerAngles.z + -20f + spread - 2 * Random.value * spread))
                .GetComponent<EnemyProjectile>();
            e3.Setup(player, 2.18f, 1);
            e3.SetRadius(0.1f);
            EnemyProjectile e4 = Instantiate(projectiles[0], shootPos[0].position,
                Quaternion.Euler(0, 0, transform.eulerAngles.z + -30f + spread - 2 * Random.value * spread))
                .GetComponent<EnemyProjectile>();
            e4.Setup(player, 2.18f, 1);
            e4.SetRadius(0.1f);
            shootTime = 0;
		}
	}
}
