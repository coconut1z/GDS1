using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage5BossGun1 : WeaponEnemy {
	
	private float phase;
	private float sineRotation, sineRotator;


	// Use this for initialization
	protected override void Start () {
		base.Start ();

		shootTime = 0;
		shootDelay = 2;
		sineRotation = 0;
	}

	// Update is called once per frame
	protected override void Update () {
		base.Update ();
		if(phase == 1 || phase == 1.5f || phase == 2.5f){
			lookAtPlayer ();
		}else if(phase == 2 || phase == 2.1f){
			sineRotation += Time.deltaTime;
			transform.rotation = Quaternion.Euler (0, 0, 180 +  sineRotator * Mathf.Sin(sineRotation));
		}
	}

	public override void Shoot(){
		if(phase == 1f){
			PhaseOne ();
		}else if(phase == 1.5f){
			PhaseOneP5 ();
		}else if(phase == 2 || phase == 2.1f){
			PhaseTwo ();
		}else if(phase == 2.5f){
			PhaseTwoP5 ();
		}
	}

	private void PhaseOne(){
		if(shootTime >= shootDelay){
			if(difficulty == Global.RECRUIT){
			EnemyProjectile e1 = Instantiate (projectiles [0], shootPos [0].position, 
				Quaternion.Euler (0, 0, transform.eulerAngles.z - 8 )).GetComponent<EnemyProjectile>();
			EnemyProjectile e2 = Instantiate (projectiles [0], shootPos [0].position, 
				Quaternion.Euler (0, 0, transform.eulerAngles.z - 4)).GetComponent<EnemyProjectile>();
			EnemyProjectile e3 = Instantiate (projectiles [0], shootPos [0].position, 
				Quaternion.Euler (0, 0, transform.eulerAngles.z)).GetComponent<EnemyProjectile>();
			EnemyProjectile e4 = Instantiate (projectiles [0], shootPos [0].position, 
				Quaternion.Euler (0, 0, transform.eulerAngles.z + 4 )).GetComponent<EnemyProjectile>();
			EnemyProjectile e5 = Instantiate (projectiles [0], shootPos [0].position, 
				Quaternion.Euler (0, 0, transform.eulerAngles.z + 8 )).GetComponent<EnemyProjectile>();
			e3.Setup (player, 3.5f, 1);
			e3.SetRadius (0.1f);
			e1.Setup (player, 2.5f, 1);
			e1.SetRadius (0.1f);
			e2.Setup (player, 3f, 1);
			e2.SetRadius (0.1f);
			e4.Setup (player, 3f, 1);
			e4.SetRadius (0.1f);
			e5.Setup (player, 2.5f, 1);
			e5.SetRadius (0.1f);

			}else if(difficulty == Global.VETEREN){
				EnemyProjectile e7 = Instantiate (projectiles [0], shootPos [0].position, 
					Quaternion.Euler (0, 0, transform.eulerAngles.z - 16)).GetComponent<EnemyProjectile>();
				EnemyProjectile e6 = Instantiate (projectiles [0], shootPos [0].position, 
					Quaternion.Euler (0, 0, transform.eulerAngles.z - 12)).GetComponent<EnemyProjectile>();
				EnemyProjectile e1 = Instantiate (projectiles [0], shootPos [0].position, 
					Quaternion.Euler (0, 0, transform.eulerAngles.z - 8 )).GetComponent<EnemyProjectile>();
				EnemyProjectile e2 = Instantiate (projectiles [0], shootPos [0].position, 
					Quaternion.Euler (0, 0, transform.eulerAngles.z - 4)).GetComponent<EnemyProjectile>();
				EnemyProjectile e3 = Instantiate (projectiles [0], shootPos [0].position, 
					Quaternion.Euler (0, 0, transform.eulerAngles.z)).GetComponent<EnemyProjectile>();
				EnemyProjectile e4 = Instantiate (projectiles [0], shootPos [0].position, 
					Quaternion.Euler (0, 0, transform.eulerAngles.z + 4 )).GetComponent<EnemyProjectile>();
				EnemyProjectile e5 = Instantiate (projectiles [0], shootPos [0].position, 
					Quaternion.Euler (0, 0, transform.eulerAngles.z + 8 )).GetComponent<EnemyProjectile>();
				EnemyProjectile e8 = Instantiate (projectiles [0], shootPos [0].position, 
					Quaternion.Euler (0, 0, transform.eulerAngles.z + 12 )).GetComponent<EnemyProjectile>();
				EnemyProjectile e9 = Instantiate (projectiles [0], shootPos [0].position, 
					Quaternion.Euler (0, 0, transform.eulerAngles.z + 16 )).GetComponent<EnemyProjectile>();
				e3.Setup (player, 3.5f, 1);
				e3.SetRadius (0.1f);
				e1.Setup (player, 2.5f, 1);
				e1.SetRadius (0.1f);
				e2.Setup (player, 3f, 1);
				e2.SetRadius (0.1f);
				e4.Setup (player, 3f, 1);
				e4.SetRadius (0.1f);
				e5.Setup (player, 2.5f, 1);
				e5.SetRadius (0.1f);
				e6.Setup (player, 2f, 1);
				e6.SetRadius (0.1f);
				e7.Setup (player, 1.5f, 1);
				e7.SetRadius (0.1f);
				e8.Setup (player, 2f, 1);
				e8.SetRadius (0.1f);
				e9.Setup (player, 1.5f, 1);
				e9.SetRadius (0.1f);
			}else if(difficulty == Global.BATTLEH){
				for(int i = -1; i < 2; i++){
					EnemyProjectile e7 = Instantiate (projectiles [0], shootPos [0].position, 
						Quaternion.Euler (0, 0, transform.eulerAngles.z - 16 + i*45)).GetComponent<EnemyProjectile>();
					EnemyProjectile e6 = Instantiate (projectiles [0], shootPos [0].position, 
						Quaternion.Euler (0, 0, transform.eulerAngles.z - 12 + i*45)).GetComponent<EnemyProjectile>();
					EnemyProjectile e1 = Instantiate (projectiles [0], shootPos [0].position, 
						Quaternion.Euler (0, 0, transform.eulerAngles.z - 8  + i*45)).GetComponent<EnemyProjectile>();
					EnemyProjectile e2 = Instantiate (projectiles [0], shootPos [0].position, 
						Quaternion.Euler (0, 0, transform.eulerAngles.z - 4 + i*45)).GetComponent<EnemyProjectile>();
					EnemyProjectile e3 = Instantiate (projectiles [0], shootPos [0].position, 
						Quaternion.Euler (0, 0, transform.eulerAngles.z + i*45)).GetComponent<EnemyProjectile>();
					EnemyProjectile e4 = Instantiate (projectiles [0], shootPos [0].position, 
						Quaternion.Euler (0, 0, transform.eulerAngles.z + 4 + i*45)).GetComponent<EnemyProjectile>();
					EnemyProjectile e5 = Instantiate (projectiles [0], shootPos [0].position, 
						Quaternion.Euler (0, 0, transform.eulerAngles.z + 8 + i*45)).GetComponent<EnemyProjectile>();
					EnemyProjectile e8 = Instantiate (projectiles [0], shootPos [0].position, 
						Quaternion.Euler (0, 0, transform.eulerAngles.z + 12 + i*45)).GetComponent<EnemyProjectile>();
					EnemyProjectile e9 = Instantiate (projectiles [0], shootPos [0].position, 
						Quaternion.Euler (0, 0, transform.eulerAngles.z + 16 + i*45)).GetComponent<EnemyProjectile>();
					e3.Setup (player, 3.5f, 1);
					e3.SetRadius (0.1f);
					e1.Setup (player, 2.5f, 1);
					e1.SetRadius (0.1f);
					e2.Setup (player, 3f, 1);
					e2.SetRadius (0.1f);
					e4.Setup (player, 3f, 1);
					e4.SetRadius (0.1f);
					e5.Setup (player, 2.5f, 1);
					e5.SetRadius (0.1f);
					e6.Setup (player, 2f, 1);
					e6.SetRadius (0.1f);
					e7.Setup (player, 1.5f, 1);
					e7.SetRadius (0.1f);
					e8.Setup (player, 2f, 1);
					e8.SetRadius (0.1f);
					e9.Setup (player, 1.5f, 1);
					e9.SetRadius (0.1f);
				}

			}
			shootTime = 0;
		}
	}

	private void PhaseOneP5(){
		if(shootTime >= shootDelay){
			EnemyProjectile e1 = Instantiate (projectiles [0], shootPos [0].position, 
				Quaternion.Euler (0, 0, transform.eulerAngles.z - 72)).GetComponent<EnemyProjectile>();
			EnemyProjectile e2 = Instantiate (projectiles [0], shootPos [0].position, 
				Quaternion.Euler (0, 0, transform.eulerAngles.z - 60)).GetComponent<EnemyProjectile>();
			EnemyProjectile e3 = Instantiate (projectiles [0], shootPos [0].position, 
				Quaternion.Euler (0, 0, transform.eulerAngles.z - 48)).GetComponent<EnemyProjectile>();
			EnemyProjectile e4 = Instantiate (projectiles [0], shootPos [0].position, 
				Quaternion.Euler (0, 0, transform.eulerAngles.z - 36)).GetComponent<EnemyProjectile>();
			EnemyProjectile e5 = Instantiate (projectiles [0], shootPos [0].position, 
				Quaternion.Euler (0, 0, transform.eulerAngles.z - 24 )).GetComponent<EnemyProjectile>();
			EnemyProjectile e6 = Instantiate (projectiles [0], shootPos [0].position, 
				Quaternion.Euler (0, 0, transform.eulerAngles.z - 12)).GetComponent<EnemyProjectile>();
			EnemyProjectile e7 = Instantiate (projectiles [0], shootPos [0].position, 
				Quaternion.Euler (0, 0, transform.eulerAngles.z)).GetComponent<EnemyProjectile>();
			EnemyProjectile e8 = Instantiate (projectiles [0], shootPos [0].position, 
				Quaternion.Euler (0, 0, transform.eulerAngles.z + 12)).GetComponent<EnemyProjectile>();
			EnemyProjectile e9 = Instantiate (projectiles [0], shootPos [0].position, 
				Quaternion.Euler (0, 0, transform.eulerAngles.z + 24)).GetComponent<EnemyProjectile>();
			EnemyProjectile e10 = Instantiate (projectiles [0], shootPos [0].position, 
				Quaternion.Euler (0, 0, transform.eulerAngles.z + 36)).GetComponent<EnemyProjectile>();
			EnemyProjectile e11 = Instantiate (projectiles [0], shootPos [0].position, 
				Quaternion.Euler (0, 0, transform.eulerAngles.z + 48)).GetComponent<EnemyProjectile>();
			EnemyProjectile e12 = Instantiate (projectiles [0], shootPos [0].position, 
				Quaternion.Euler (0, 0, transform.eulerAngles.z + 60)).GetComponent<EnemyProjectile>();
			EnemyProjectile e13 = Instantiate (projectiles [0], shootPos [0].position, 
				Quaternion.Euler (0, 0, transform.eulerAngles.z + 72)).GetComponent<EnemyProjectile>();

			e1.Setup (player, 1.3f, 0);
			e1.SetRadius (0.11f);
			e2.Setup (player, 1.5f, 0);
			e2.SetRadius (0.11f);
			e3.Setup (player, 1.7f, 0);
			e3.SetRadius (0.11f);
			e4.Setup (player, 1.9f, 0);
			e4.SetRadius (0.11f);
			e5.Setup (player, 2.1f, 0);
			e5.SetRadius (0.11f);
			e6.Setup (player, 2.3f, 0);
			e6.SetRadius (0.11f);
			e7.Setup (player, 2.5f, 0);
			e7.SetRadius (0.11f);
			e13.Setup (player, 1.3f, 0);
			e13.SetRadius (0.11f);
			e12.Setup (player, 1.5f, 0);
			e12.SetRadius (0.11f);
			e11.Setup (player, 1.7f, 0);
			e11.SetRadius (0.11f);
			e10.Setup (player, 1.9f, 0);
			e10.SetRadius (0.11f);
			e9.Setup (player, 2.1f, 0);
			e9.SetRadius (0.11f);
			e8.Setup (player, 2.3f, 0);
			e8.SetRadius (0.11f);
			shootTime = 0;
		}
	}

	private void PhaseTwo(){
		if(shootTime >= shootDelay){
			if(difficulty == Global.RECRUIT){
				EnemyProjectile e2 = Instantiate (projectiles [0], shootPos [0].position, 
					Quaternion.Euler (0, 0, transform.eulerAngles.z - 4)).GetComponent<EnemyProjectile>();
				EnemyProjectile e3 = Instantiate (projectiles [0], shootPos [0].position, 
					Quaternion.Euler (0, 0, transform.eulerAngles.z)).GetComponent<EnemyProjectile>();
				EnemyProjectile e4 = Instantiate (projectiles [0], shootPos [0].position, 
					Quaternion.Euler (0, 0, transform.eulerAngles.z + 4 )).GetComponent<EnemyProjectile>();
				e3.Setup (player, 3.5f, 1);
				e3.SetRadius (0.1f);
				e2.Setup (player, 3f, 1);
				e2.SetRadius (0.1f);
				e4.Setup (player, 3f, 1);
				e4.SetRadius (0.1f);
			}else{
				EnemyProjectile e1 = Instantiate (projectiles [0], shootPos [0].position, 
					Quaternion.Euler (0, 0, transform.eulerAngles.z - 8 )).GetComponent<EnemyProjectile>();
				EnemyProjectile e2 = Instantiate (projectiles [0], shootPos [0].position, 
					Quaternion.Euler (0, 0, transform.eulerAngles.z - 4)).GetComponent<EnemyProjectile>();
				EnemyProjectile e3 = Instantiate (projectiles [0], shootPos [0].position, 
					Quaternion.Euler (0, 0, transform.eulerAngles.z)).GetComponent<EnemyProjectile>();
				EnemyProjectile e4 = Instantiate (projectiles [0], shootPos [0].position, 
					Quaternion.Euler (0, 0, transform.eulerAngles.z + 4 )).GetComponent<EnemyProjectile>();
				EnemyProjectile e5 = Instantiate (projectiles [0], shootPos [0].position, 
					Quaternion.Euler (0, 0, transform.eulerAngles.z + 8 )).GetComponent<EnemyProjectile>();
				e3.Setup (player, 3.5f, 1);
				e3.SetRadius (0.1f);
				e1.Setup (player, 2.5f, 1);
				e1.SetRadius (0.1f);
				e2.Setup (player, 3f, 1);
				e2.SetRadius (0.1f);
				e4.Setup (player, 3f, 1);
				e4.SetRadius (0.1f);
				e5.Setup (player, 2.5f, 1);
				e5.SetRadius (0.1f);
			}
			shootTime = 0;
		}
	}

	private void PhaseTwoP5(){
		if(shootTime >= shootDelay){

			EnemyProjectile e41 = Instantiate (projectiles [0], shootPos [0].position, 
				Quaternion.Euler (0, 0, transform.eulerAngles.z - 72)).GetComponent<EnemyProjectile>();
			EnemyProjectile e42 = Instantiate (projectiles [0], shootPos [0].position, 
				Quaternion.Euler (0, 0, transform.eulerAngles.z - 60)).GetComponent<EnemyProjectile>();
			EnemyProjectile e43 = Instantiate (projectiles [0], shootPos [0].position, 
				Quaternion.Euler (0, 0, transform.eulerAngles.z - 48)).GetComponent<EnemyProjectile>();
			EnemyProjectile e4 = Instantiate (projectiles [0], shootPos [0].position, 
				Quaternion.Euler (0, 0, transform.eulerAngles.z - 36)).GetComponent<EnemyProjectile>();
			EnemyProjectile e45 = Instantiate (projectiles [0], shootPos [0].position, 
				Quaternion.Euler (0, 0, transform.eulerAngles.z - 24)).GetComponent<EnemyProjectile>();
			EnemyProjectile e46 = Instantiate (projectiles [0], shootPos [0].position, 
				Quaternion.Euler (0, 0, transform.eulerAngles.z - 12)).GetComponent<EnemyProjectile>();
			EnemyProjectile e47 = Instantiate (projectiles [0], shootPos [0].position, 
				Quaternion.Euler (0, 0, transform.eulerAngles.z - 0)).GetComponent<EnemyProjectile>();
			
			EnemyProjectile e5 = Instantiate (projectiles [0], shootPos [0].position, 
				Quaternion.Euler (0, 0, transform.eulerAngles.z - 24 )).GetComponent<EnemyProjectile>();
			EnemyProjectile e6 = Instantiate (projectiles [0], shootPos [0].position, 
				Quaternion.Euler (0, 0, transform.eulerAngles.z - 12)).GetComponent<EnemyProjectile>();
			EnemyProjectile e7 = Instantiate (projectiles [0], shootPos [0].position, 
				Quaternion.Euler (0, 0, transform.eulerAngles.z)).GetComponent<EnemyProjectile>();
			EnemyProjectile e8 = Instantiate (projectiles [0], shootPos [0].position, 
				Quaternion.Euler (0, 0, transform.eulerAngles.z + 12)).GetComponent<EnemyProjectile>();
			EnemyProjectile e9 = Instantiate (projectiles [0], shootPos [0].position, 
				Quaternion.Euler (0, 0, transform.eulerAngles.z + 24)).GetComponent<EnemyProjectile>();
			
			EnemyProjectile e101 = Instantiate (projectiles [0], shootPos [0].position, 
				Quaternion.Euler (0, 0, transform.eulerAngles.z + 0)).GetComponent<EnemyProjectile>();
			EnemyProjectile e102 = Instantiate (projectiles [0], shootPos [0].position, 
				Quaternion.Euler (0, 0, transform.eulerAngles.z + 12)).GetComponent<EnemyProjectile>();
			EnemyProjectile e103 = Instantiate (projectiles [0], shootPos [0].position, 
				Quaternion.Euler (0, 0, transform.eulerAngles.z + 24)).GetComponent<EnemyProjectile>();
			EnemyProjectile e10 = Instantiate (projectiles [0], shootPos [0].position, 
				Quaternion.Euler (0, 0, transform.eulerAngles.z + 36)).GetComponent<EnemyProjectile>();
			EnemyProjectile e105 = Instantiate (projectiles [0], shootPos [0].position, 
				Quaternion.Euler (0, 0, transform.eulerAngles.z + 48)).GetComponent<EnemyProjectile>();
			EnemyProjectile e106 = Instantiate (projectiles [0], shootPos [0].position, 
				Quaternion.Euler (0, 0, transform.eulerAngles.z + 60)).GetComponent<EnemyProjectile>();
			EnemyProjectile e107 = Instantiate (projectiles [0], shootPos [0].position, 
				Quaternion.Euler (0, 0, transform.eulerAngles.z + 72)).GetComponent<EnemyProjectile>();

			e41.Setup (player, 1.3f, 0);
			e41.SetRadius (0.11f);
			e42.Setup (player, 1.5f, 0);
			e42.SetRadius (0.11f);
			e43.Setup (player, 1.7f, 0);
			e43.SetRadius (0.11f);
			e4.Setup (player, 1.9f, 0);
			e4.SetRadius (0.11f);
			e45.Setup (player, 1.7f, 0);
			e45.SetRadius (0.11f);
			e46.Setup (player, 1.5f, 0);
			e46.SetRadius (0.11f);
			e47.Setup (player, 1.3f, 0);
			e47.SetRadius (0.11f);

			e5.Setup (player, 2.1f, 0);
			e5.SetRadius (0.11f);
			e6.Setup (player, 2.3f, 0);
			e6.SetRadius (0.11f);
			e7.Setup (player, 2.5f, 0);
			e7.SetRadius (0.11f);
			e8.Setup (player, 2.3f, 0);
			e8.SetRadius (0.11f);
			e9.Setup (player, 2.1f, 0);
			e9.SetRadius (0.11f);

			e101.Setup (player, 1.3f, 0);
			e101.SetRadius (0.11f);
			e102.Setup (player, 1.5f, 0);
			e102.SetRadius (0.11f);
			e103.Setup (player, 1.7f, 0);
			e103.SetRadius (0.11f);
			e10.Setup (player, 1.9f, 0);
			e10.SetRadius (0.11f);
			e105.Setup (player, 1.7f, 0);
			e105.SetRadius (0.11f);
			e106.Setup (player, 1.5f, 0);
			e106.SetRadius (0.11f);
			e107.Setup (player, 1.3f, 0);
			e107.SetRadius (0.11f);



			shootTime = 0;
		}
	}

	public void ChangePhase(float i){
		if (i == 1){
			phase = 1;
			shootDelay = 1.5f;
			shootTime = 0;
		}else if(i == 1.5f){
			phase = 1.5f;
			if(difficulty == Global.RECRUIT){
				shootDelay = 4;
			}else{
				shootDelay = 2;
			}
			shootTime = shootDelay;
		}else if(i == 2){
			phase = 2;
			transform.rotation = Quaternion.Euler (0, 0, 180);
			shootTime = 0;
			if(difficulty == Global.RECRUIT){
				shootDelay = 0.6f;
			}else if(difficulty == Global.VETEREN){
				shootDelay = 0.4f;
			}else if(difficulty == Global.BATTLEH){
				shootDelay = 0.2f;
			}

			sineRotator = 90;
		}else if(i == 2.1f){
			phase = 2;
			transform.rotation = Quaternion.Euler (0, 0, 180);
			shootTime = 0;
			if(difficulty == Global.RECRUIT){
				shootDelay = 0.6f;
			}else if(difficulty == Global.VETEREN){
				shootDelay = 0.4f;
			}else if(difficulty == Global.BATTLEH){
				shootDelay = 0.2f;
			}
			sineRotator = -90;
		}else if(i == 2.5f){
			if(difficulty == Global.RECRUIT){
				shootDelay = 3;
			}else if(difficulty == Global.VETEREN){
				shootDelay = 2;
			}else if(difficulty == Global.BATTLEH){
				shootDelay = 1;
			}
			phase = 2.5f;
			shootTime = 0;
		}else if(i == 0){
			phase = 0;
		}
	}
}
