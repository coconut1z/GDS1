using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1BossGun2 : WeaponEnemy {

	private float cooldownDelay;
	private int cooldownShots;
	private int cooldownShotsRequired;
	private int phase;


	// Use this for initialization
	protected override void Start () {
		base.Start ();

		if(difficulty == Global.RECRUIT){
			shootDelay = 0.15f;
			cooldownShotsRequired = 5;
		}else if(difficulty == Global.VETEREN){
			shootDelay = 0.1f;
			cooldownShotsRequired = 10;
		}else if(difficulty == Global.BATTLEH){
			shootDelay = 0.1f;
			cooldownShotsRequired = 15;
		}
		shootTime = shootDelay;
		cooldownDelay = 0;
		cooldownShots = 0;
		phase = 1;
	}

	// Update is called once per frame
	protected override void Update () {
		base.Update ();
		if(phase == 1){
			lookAtPlayer ();
		}
		cooldownDelay -= Time.deltaTime;
	}

	public override void Shoot(){
		if(shootTime > shootDelay && cooldownDelay <= 0){
			cooldownShots++;
			if(difficulty == Global.RECRUIT){
				EnemyProjectile e1 = Instantiate (projectiles [0], shootPos [0].position, 
					Quaternion.Euler (0, 0, transform.eulerAngles.z)).GetComponent<EnemyProjectile>();
				EnemyProjectile e2 = Instantiate (projectiles [0], shootPos [0].position, 
					Quaternion.Euler (0, 0, transform.eulerAngles.z + 45f)).GetComponent<EnemyProjectile>();
				EnemyProjectile e3 = Instantiate (projectiles [0], shootPos [0].position, 
					Quaternion.Euler (0, 0, transform.eulerAngles.z - 45f)).GetComponent<EnemyProjectile>();
				e1.Setup (player, 4.55f, 1);
				e1.SetRadius (0.1f);
				e2.Setup (player, 4.55f, 1);
				e2.SetRadius (0.1f);
				e3.Setup (player, 4.55f, 1);
				e3.SetRadius (0.1f);
			}else if(difficulty == Global.VETEREN){
				EnemyProjectile e1 = Instantiate (projectiles [0], shootPos [0].position, 
					Quaternion.Euler (0, 0, transform.eulerAngles.z)).GetComponent<EnemyProjectile>();
				EnemyProjectile e2 = Instantiate (projectiles [0], shootPos [0].position, 
					Quaternion.Euler (0, 0, transform.eulerAngles.z + 30f)).GetComponent<EnemyProjectile>();
				EnemyProjectile e3 = Instantiate (projectiles [0], shootPos [0].position, 
					Quaternion.Euler (0, 0, transform.eulerAngles.z - 30f)).GetComponent<EnemyProjectile>();
				e1.Setup (player, 4.55f, 1);
				e1.SetRadius (0.1f);
				e2.Setup (player, 4.55f, 1);
				e2.SetRadius (0.1f);
				e3.Setup (player, 4.55f, 1);
				e3.SetRadius (0.1f);
			}else if(difficulty == Global.BATTLEH){
				EnemyProjectile e1 = 	Instantiate (projectiles [0], shootPos [0].position, 
					Quaternion.Euler (0, 0, transform.eulerAngles.z)).GetComponent<EnemyProjectile>();
				EnemyProjectile e2 = Instantiate (projectiles [0], shootPos [0].position, 
					Quaternion.Euler (0, 0, transform.eulerAngles.z + 15f)).GetComponent<EnemyProjectile>();
				EnemyProjectile e3 = Instantiate (projectiles [0], shootPos [0].position, 
					Quaternion.Euler (0, 0, transform.eulerAngles.z - 15f)).GetComponent<EnemyProjectile>();
				EnemyProjectile e4 = Instantiate (projectiles [0], shootPos [0].position, 
					Quaternion.Euler (0, 0, transform.eulerAngles.z + 30f)).GetComponent<EnemyProjectile>();
				EnemyProjectile e5 = Instantiate (projectiles [0], shootPos [0].position, 
					Quaternion.Euler (0, 0, transform.eulerAngles.z - 30f)).GetComponent<EnemyProjectile>();
				e1.Setup (player, 4.55f, 1);
				e1.SetRadius (0.1f);
				e2.Setup (player, 4.55f, 1);
				e2.SetRadius (0.1f);
				e3.Setup (player, 4.55f, 1);
				e3.SetRadius (0.1f);
				e4.Setup (player, 4.55f, 1);
				e4.SetRadius (0.1f);
				e5.Setup (player, 4.55f, 1);
				e5.SetRadius (0.1f);
			}

			shootTime = 0;
		}
		if(cooldownShots >= cooldownShotsRequired){
			cooldownDelay = 0.4f;
			cooldownShots = 0;
		}
	}

	public void ChangePhase(int i){
		if(i == 2){
			phase = 2;
			cooldownShots = -9999999;
			cooldownDelay = 0;
			transform.rotation = Quaternion.Euler (0, 0, 0);
		}
	}
}
