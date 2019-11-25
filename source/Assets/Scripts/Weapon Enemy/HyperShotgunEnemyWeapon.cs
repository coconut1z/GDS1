using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HyperShotgunEnemyWeapon : WeaponEnemy {


	// Use this for initialization
	protected override void Start () {
		base.Start ();
		shootDelay = 2f;
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
				for(int i = 0; i < 3; i++){
					EnemyProjectile e1 = Instantiate(projectiles[0], shootPos[1].position,
						Quaternion.Euler(0, 0, transform.eulerAngles.z + 4)).GetComponent<EnemyProjectile>();
					EnemyProjectile e2 = Instantiate(projectiles[0], shootPos[2].position,
						Quaternion.Euler(0, 0, transform.eulerAngles.z)).GetComponent<EnemyProjectile>();
					EnemyProjectile e3 = Instantiate(projectiles[0], shootPos[3].position,
						Quaternion.Euler(0, 0, transform.eulerAngles.z - 4)).GetComponent<EnemyProjectile>();
					e1.Setup (player, 2f - i * 0.5f, 1);
					e1.SetRadius (0.1f);
					e2.Setup (player, 2f - i * 0.5f, 1);
					e2.SetRadius (0.1f);
					e3.Setup (player, 2f - i * 0.5f, 1);
					e3.SetRadius (0.1f);
				}
			}else if(difficulty == Global.VETEREN){
				for(int i = 0; i < 5; i++){
					EnemyProjectile e1 = Instantiate(projectiles[0], shootPos[1].position,
						Quaternion.Euler(0, 0, transform.eulerAngles.z + 4)).GetComponent<EnemyProjectile>();
					EnemyProjectile e2 = Instantiate(projectiles[0], shootPos[2].position,
						Quaternion.Euler(0, 0, transform.eulerAngles.z)).GetComponent<EnemyProjectile>();
					EnemyProjectile e3 = Instantiate(projectiles[0], shootPos[3].position,
						Quaternion.Euler(0, 0, transform.eulerAngles.z - 4)).GetComponent<EnemyProjectile>();
					e1.Setup (player, 3f - i * 0.5f, 1);
					e1.SetRadius (0.1f);
					e2.Setup (player, 3f - i * 0.5f, 1);
					e2.SetRadius (0.1f);
					e3.Setup (player, 3f - i * 0.5f, 1);
					e3.SetRadius (0.1f);
				}
			}else if(difficulty == Global.BATTLEH){
				for(int i = 0; i < 5; i++){
					EnemyProjectile e0 = Instantiate(projectiles[0], shootPos[0].position,
						Quaternion.Euler(0, 0, transform.eulerAngles.z + 8)).GetComponent<EnemyProjectile>();
					EnemyProjectile e1 = Instantiate(projectiles[0], shootPos[1].position,
						Quaternion.Euler(0, 0, transform.eulerAngles.z + 4)).GetComponent<EnemyProjectile>();
					EnemyProjectile e2 = Instantiate(projectiles[0], shootPos[2].position,
						Quaternion.Euler(0, 0, transform.eulerAngles.z)).GetComponent<EnemyProjectile>();
					EnemyProjectile e3 = Instantiate(projectiles[0], shootPos[3].position,
						Quaternion.Euler(0, 0, transform.eulerAngles.z - 4)).GetComponent<EnemyProjectile>();
					EnemyProjectile e4 = Instantiate(projectiles[0], shootPos[4].position,
						Quaternion.Euler(0, 0, transform.eulerAngles.z - 8)).GetComponent<EnemyProjectile>();
					e0.Setup (player, 3f - i * 0.5f, 1);
					e0.SetRadius (0.1f);
					e1.Setup (player, 3f - i * 0.5f, 1);
					e1.SetRadius (0.1f);
					e2.Setup (player, 3f - i * 0.5f, 1);
					e2.SetRadius (0.1f);
					e3.Setup (player, 3f - i * 0.5f, 1);
					e3.SetRadius (0.1f);
					e4.Setup (player, 3f - i * 0.5f, 1);
					e4.SetRadius (0.1f);
				}
			}
            shootTime = 0;
		}
	}
}
