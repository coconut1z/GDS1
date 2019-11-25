using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowCircleEnemyWeapon : WeaponEnemy {

	// Use this for initialization
	protected override void Start () {
		base.Start ();

		if(difficulty == Global.RECRUIT){
			shootDelay = 2f;
		}else if(difficulty == Global.VETEREN){
			shootDelay = 2f;
		}else if(difficulty == Global.BATTLEH){
			shootDelay = 2f;
		}

		shootTime = 0;
	}

	// Update is called once per frame
	protected override void Update () {
		base.Update ();
		//lookAtPlayer ();
	}

	public override void Shoot(){
		if(shootTime > shootDelay){
			float startAngle = Random.Range (0, 361);
			for(int i = 0; i < 10; i++){
				if(difficulty == Global.RECRUIT){
					EnemyProjectile e2 = Instantiate (projectiles [0], shootPos [0].position, 
						Quaternion.Euler (0, 0, i * 36 - 4 + startAngle)).GetComponent<EnemyProjectile>();
					EnemyProjectile e3 = Instantiate (projectiles [0], shootPos [0].position, 
						Quaternion.Euler (0, 0, i * 36 + startAngle)).GetComponent<EnemyProjectile>();
					EnemyProjectile e4 = Instantiate (projectiles [0], shootPos [0].position, 
						Quaternion.Euler (0, 0, i * 36 + 4 + startAngle)).GetComponent<EnemyProjectile>();
					e3.Setup (player, 2f, 1);
					e3.SetRadius (0.1f);
					e2.Setup (player, 1.75f, 1);
					e2.SetRadius (0.1f);
					e4.Setup (player, 1.75f, 1);
					e4.SetRadius (0.1f);
				}else if(difficulty == Global.VETEREN){
					EnemyProjectile e1 = Instantiate (projectiles [0], shootPos [0].position, 
						Quaternion.Euler (0, 0, i * 36 - 8 + startAngle)).GetComponent<EnemyProjectile>();
					EnemyProjectile e2 = Instantiate (projectiles [0], shootPos [0].position, 
						Quaternion.Euler (0, 0, i * 36 - 4 + startAngle)).GetComponent<EnemyProjectile>();
					EnemyProjectile e3 = Instantiate (projectiles [0], shootPos [0].position, 
						Quaternion.Euler (0, 0, i * 36 + startAngle)).GetComponent<EnemyProjectile>();
					EnemyProjectile e4 = Instantiate (projectiles [0], shootPos [0].position, 
						Quaternion.Euler (0, 0, i * 36 + 4 + startAngle)).GetComponent<EnemyProjectile>();
					EnemyProjectile e5 = Instantiate (projectiles [0], shootPos [0].position, 
						Quaternion.Euler (0, 0, i * 36 + 8 + startAngle)).GetComponent<EnemyProjectile>();
					e3.Setup (player, 2f, 1);
					e3.SetRadius (0.1f);
					e1.Setup (player, 1.5f, 1);
					e1.SetRadius (0.1f);
					e2.Setup (player, 1.75f, 1);
					e2.SetRadius (0.1f);
					e4.Setup (player, 1.75f, 1);
					e4.SetRadius (0.1f);
					e5.Setup (player, 1.5f, 1);
					e5.SetRadius (0.1f);
				}else if(difficulty == Global.BATTLEH){
					EnemyProjectile e7 = Instantiate (projectiles [0], shootPos [0].position, 
						Quaternion.Euler (0, 0, i * 36 - 16 + startAngle)).GetComponent<EnemyProjectile>();
					EnemyProjectile e6 = Instantiate (projectiles [0], shootPos [0].position, 
						Quaternion.Euler (0, 0, i * 36 - 12 + startAngle)).GetComponent<EnemyProjectile>();
					EnemyProjectile e1 = Instantiate (projectiles [0], shootPos [0].position, 
						Quaternion.Euler (0, 0, i * 36 - 8 + startAngle)).GetComponent<EnemyProjectile>();
					EnemyProjectile e2 = Instantiate (projectiles [0], shootPos [0].position, 
						Quaternion.Euler (0, 0, i * 36 - 4 + startAngle)).GetComponent<EnemyProjectile>();
					EnemyProjectile e3 = Instantiate (projectiles [0], shootPos [0].position, 
						Quaternion.Euler (0, 0, i * 36 + startAngle)).GetComponent<EnemyProjectile>();
					EnemyProjectile e4 = Instantiate (projectiles [0], shootPos [0].position, 
						Quaternion.Euler (0, 0, i * 36 + 4 + startAngle)).GetComponent<EnemyProjectile>();
					EnemyProjectile e5 = Instantiate (projectiles [0], shootPos [0].position, 
						Quaternion.Euler (0, 0, i * 36 + 8 + startAngle)).GetComponent<EnemyProjectile>();
					EnemyProjectile e8 = Instantiate (projectiles [0], shootPos [0].position, 
						Quaternion.Euler (0, 0, i * 36 + 12 + startAngle)).GetComponent<EnemyProjectile>();
					EnemyProjectile e9 = Instantiate (projectiles [0], shootPos [0].position, 
						Quaternion.Euler (0, 0, i * 36 + 16 + startAngle)).GetComponent<EnemyProjectile>();
					e3.Setup (player, 2f, 1);
					e3.SetRadius (0.1f);
					e1.Setup (player, 1.5f, 1);
					e1.SetRadius (0.1f);
					e2.Setup (player, 1.75f, 1);
					e2.SetRadius (0.1f);
					e4.Setup (player, 1.75f, 1);
					e4.SetRadius (0.1f);
					e5.Setup (player, 1.5f, 1);
					e5.SetRadius (0.1f);
					e6.Setup (player, 1.25f, 1);
					e6.SetRadius (0.1f);
					e7.Setup (player, 1f, 1);
					e7.SetRadius (0.1f);
					e8.Setup (player, 1.25f, 1);
					e8.SetRadius (0.1f);
					e9.Setup (player, 1f, 1);
					e9.SetRadius (0.1f);
				}

			}

			shootTime = 0;
		}
	}
}
