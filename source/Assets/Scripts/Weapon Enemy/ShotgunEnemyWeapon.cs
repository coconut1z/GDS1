using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunEnemyWeapon : WeaponEnemy {


	// Use this for initialization
	protected override void Start () {
		base.Start ();
		shootDelay = 1f;
		shootTime = shootDelay;
		
	}

	// Update is called once per frame
	protected override void Update () {
		base.Update ();
		lookAtPlayer ();
	}

	public override void Shoot(){
        if (shootTime > shootDelay){
            
			if(difficulty == Global.RECRUIT){
				EnemyProjectile e1 = Instantiate(projectiles[0], shootPos[0].position,
					Quaternion.Euler(0, 0, transform.eulerAngles.z - 5)).GetComponent<EnemyProjectile>();
				EnemyProjectile e2 = Instantiate(projectiles[0], shootPos[0].position,
					Quaternion.Euler(0, 0, transform.eulerAngles.z)).GetComponent<EnemyProjectile>();
				EnemyProjectile e3 = Instantiate(projectiles[0], shootPos[0].position,
					Quaternion.Euler(0, 0, transform.eulerAngles.z + 5)).GetComponent<EnemyProjectile>();
				e1.Setup (player, 2.18f, 1);
				e1.SetRadius (0.1f);
				e2.Setup (player, 2.18f, 1);
				e2.SetRadius (0.1f);
				e3.Setup (player, 2.18f, 1);
				e3.SetRadius (0.1f);
			}else if(difficulty == Global.VETEREN){
				EnemyProjectile e1 = Instantiate (projectiles [0], shootPos [0].position, 
					Quaternion.Euler (0, 0, transform.eulerAngles.z - 9)).GetComponent<EnemyProjectile>();
				EnemyProjectile e2 = Instantiate(projectiles[0], shootPos[0].position,
					Quaternion.Euler(0, 0, transform.eulerAngles.z - 3)).GetComponent<EnemyProjectile>();
				EnemyProjectile e3 = Instantiate(projectiles[0], shootPos[0].position,
					Quaternion.Euler(0, 0, transform.eulerAngles.z + 3)).GetComponent<EnemyProjectile>();
				EnemyProjectile e4 = Instantiate(projectiles[0], shootPos[0].position,
					Quaternion.Euler(0, 0, transform.eulerAngles.z + 9)).GetComponent<EnemyProjectile>();
				e1.Setup (player, 2.18f, 1);
				e1.SetRadius (0.1f);
				e2.Setup (player, 2.18f, 1);
				e2.SetRadius (0.1f);
				e3.Setup (player, 2.18f, 1);
				e3.SetRadius (0.1f);
				e4.Setup (player, 2.18f, 1);
				e4.SetRadius (0.1f);
			}else if(difficulty == Global.BATTLEH){
				EnemyProjectile e1 = Instantiate (projectiles [0], shootPos [0].position, 
					Quaternion.Euler (0, 0, transform.eulerAngles.z - 10)).GetComponent<EnemyProjectile>();
				EnemyProjectile e2 = Instantiate(projectiles[0], shootPos[0].position,
					Quaternion.Euler(0, 0, transform.eulerAngles.z - 5)).GetComponent<EnemyProjectile>();
				EnemyProjectile e3 = Instantiate(projectiles[0], shootPos[0].position,
					Quaternion.Euler(0, 0, transform.eulerAngles.z)).GetComponent<EnemyProjectile>();
				EnemyProjectile e4 = Instantiate(projectiles[0], shootPos[0].position,
					Quaternion.Euler(0, 0, transform.eulerAngles.z + 5)).GetComponent<EnemyProjectile>();
				EnemyProjectile e5 = Instantiate(projectiles[0], shootPos[0].position,
					Quaternion.Euler(0, 0, transform.eulerAngles.z + 10)).GetComponent<EnemyProjectile>();
				e1.Setup (player, 2.18f, 1);
				e1.SetRadius (0.1f);
				e2.Setup (player, 2.18f, 1);
				e2.SetRadius (0.1f);
				e3.Setup (player, 2.18f, 1);
				e3.SetRadius (0.1f);
				e4.Setup (player, 2.18f, 1);
				e4.SetRadius (0.1f);
				e5.Setup (player, 2.18f, 1);
				e5.SetRadius (0.1f);
			}
            shootTime = 0;
		}
	}
}
