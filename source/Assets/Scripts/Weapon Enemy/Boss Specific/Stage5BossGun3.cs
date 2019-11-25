using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage5BossGun3 : WeaponEnemy {
	
	private float phase;
	private float sineRotation, sineRotator;


	// Use this for initialization
	protected override void Start () {
		base.Start ();

		shootTime = 0;
		shootDelay = 2;
		sineRotation = 0;
		sineRotator = 60;
		//ChangePhase (1);
	}

	// Update is called once per frame
	protected override void Update () {
		base.Update ();
		if(phase == 1){
			sineRotation += Time.deltaTime * 2;
			transform.rotation = Quaternion.Euler (0, 0, 180 +  sineRotator * Mathf.Sin(sineRotation));
		}
	}

	public override void Shoot(){
		if(phase == 1f){
			PhaseOne ();
		}else if(phase == 2){
			PhaseTwo ();
			lookAtPlayer ();
		}
	}

	private void PhaseOne(){
		if(shootTime >= shootDelay){
			EnemyProjectile e = Instantiate(projectiles[0], shootPos[0].position,
				Quaternion.Euler(0, 0, transform.eulerAngles.z))
				.GetComponent<EnemyProjectile>();
			e.Setup (player,Random.Range(1.5f,3f), 1);
			e.SetRadius (0.65f);
			if(difficulty == Global.BATTLEH){
				EnemyProjectile e1 = Instantiate(projectiles[1], shootPos[0].position,
					Quaternion.Euler(0, 0, transform.eulerAngles.z - 30))
					.GetComponent<EnemyProjectile>();
				e1.Setup (player,Random.Range(1.5f,3f), 1);
				e1.SetRadius (0.25f);
				EnemyProjectile e2 = Instantiate(projectiles[1], shootPos[0].position,
					Quaternion.Euler(0, 0, transform.eulerAngles.z + 30))
					.GetComponent<EnemyProjectile>();
				e2.Setup (player,Random.Range(1.5f,3f), 1);
				e2.SetRadius (0.25f);
			}
			shootTime = 0;
		}
	}

	private void PhaseTwo(){
		if(shootTime >= shootDelay){
			EnemyProjectile e = Instantiate(projectiles[1], shootPos[0].position,
				Quaternion.Euler(0, 0, transform.eulerAngles.z))
				.GetComponent<EnemyProjectile>();
			e.Setup (player,2, 1);
			e.SetRadius (0.25f);
			shootTime = 0;
		}
	}

	public void ChangePhase(float i){
		if (i == 1){
			phase = 1;
			if(difficulty == Global.RECRUIT){
				shootDelay = 2f;
			}else if(difficulty == Global.VETEREN){
				shootDelay = 1f;
			}else if(difficulty == Global.BATTLEH){
				shootDelay = 1f;
			}

			shootTime = 0;
		}else if(i == 0){
			phase = 0;
		}else if(i == 2){
			if(difficulty == Global.RECRUIT){
				shootDelay = 4f;
			}else if(difficulty == Global.VETEREN){
				shootDelay = 2f;
			}else if(difficulty == Global.BATTLEH){
				shootDelay = 1f;
			}
			shootTime = 0;
			phase = 2;
			//transform.localRotation = Quaternion.Euler (0, 0, 90);
		}
	}
}
