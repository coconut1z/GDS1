using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage5BossGun4 : WeaponEnemy {
	
	private float phase;
	private float sineRotation, sineRotator;
	private float shootTimer2;


	// Use this for initialization
	protected override void Start () {
		base.Start ();

		shootTime = 0;
		shootDelay = 2;
		sineRotation = 0;
		sineRotator = 60;
		shootTimer2 = 0;
		//ChangePhase (1);
	}

	// Update is called once per frame
	protected override void Update () {
		base.Update ();
		if(phase == 2){
			transform.rotation = Quaternion.Euler (0, 0, transform.eulerAngles.z + Time.deltaTime * 50);
		}else if(phase == 1){
			sineRotation += Time.deltaTime * 25;
			shootTimer2 += Time.deltaTime;
		}
	}

	public override void Shoot(){
		if(phase == 1f){
			PhaseOne ();
			if(shootTimer2 > 0.05f){
				EnemyProjectile e1 = Instantiate (projectiles [1], shootPos [0].position, 
					Quaternion.Euler (0, 0, 250 + 45 *Mathf.Sin(sineRotation))).GetComponent<EnemyProjectile>();
				e1.Setup (player, 5, 1);
				e1.SetRadius (0.2f);
				EnemyProjectile e2 = Instantiate (projectiles [1], shootPos [0].position, 
					Quaternion.Euler (0, 0, 110 + 45* Mathf.Sin(sineRotation))).GetComponent<EnemyProjectile>();
				e2.Setup (player, 5, 1);
				e2.SetRadius (0.2f);
				shootTimer2 = 0;
			}
		}else if(phase == 2){
			PhaseTwo ();
		}
	}

	private void PhaseOne(){
		float startAngle = Random.Range (0, 360);
		if(shootTime >= shootDelay){
			int step = 0;
			int gapCount = 8;
			int gapCounter = 0;
			int gapSize = 0;
			int step2 = 0;
			if(difficulty == Global.RECRUIT){
				step2 = 20;
				step = 15;
			}else if(difficulty == Global.VETEREN){
				step2 = 10;
				step = 12;
			}else if(difficulty == Global.BATTLEH){
				step2 = 8;
				step = 10;
			}
			gapSize = 20;

			/*
			 * step = 5;
			for(int i = 0; i < 360; i+=step){
				if(gapCounter >= gapCount){
					gapCounter = 0;
					i += gapSize;
				}
				EnemyProjectile e = Instantiate (projectiles [0], shootPos [0].position, 
					Quaternion.Euler (0, 0, startAngle + i)).GetComponent<EnemyProjectile>();
				e.Setup (player, 1f, 1);
				e.SetRadius (0.4f);
				gapCounter++;

			}
			*/

			for(int i = 0; i < 360; i += step){
				EnemyProjectile e = Instantiate (projectiles [0], shootPos [0].position, 
					Quaternion.Euler (0, 0, startAngle + i)).GetComponent<EnemyProjectile>();
				e.Setup (player, 1.5f, 1);
				e.SetRadius (0.4f);
			}

			startAngle = Random.Range (0, 360);
			for(int i = 0; i < 360; i += step2){
				EnemyProjectile e = Instantiate (projectiles [1], shootPos [0].position, 
					Quaternion.Euler (0, 0, startAngle + i)).GetComponent<EnemyProjectile>();
				e.Setup (player, 3f, 1);
				e.SetRadius (0.2f);
			}
			shootTime = 0;
		}
	}

	private void PhaseTwo(){
		if (shootTime >= shootDelay) {
			EnemyProjectile e = Instantiate (projectiles [2], shootPos [0].position, 
				                   Quaternion.Euler (0, 0, transform.eulerAngles.z)).GetComponent<EnemyProjectile> ();
			e.Setup (player);
			e.SetRadius (0.4f);
			shootTime = 0;
		}
	}

	public void ChangePhase(float i){
		if (i == 1){
			phase = 1;
			if(difficulty == Global.RECRUIT){
				shootDelay = 3f;
			}else if(difficulty == Global.VETEREN){
				shootDelay = 3f;
			}else if(difficulty == Global.BATTLEH){
				shootDelay = 3f;
			}

			shootTime = 0;
		}else if(i == 0){
			phase = 0;
		}else if(i == 2){
			if(difficulty == Global.RECRUIT){
				shootDelay = 1f;
			}else if(difficulty == Global.VETEREN){
				shootDelay = 1f;
			}else if(difficulty == Global.BATTLEH){
				shootDelay = 0.5f;
			}
			shootTime = 0;
			phase = 2;
		}
	}
}
