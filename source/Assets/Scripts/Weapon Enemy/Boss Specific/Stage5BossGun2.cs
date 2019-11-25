using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage5BossGun2 : WeaponEnemy {
	
	private float phase;
	private float sineRotation, sineRotator;


	// Use this for initialization
	protected override void Start () {
		base.Start ();

		shootTime = 0;
		shootDelay = 2;
		sineRotation = 0;
		sineRotator = 90;
		//ChangePhase (1);
	}

	// Update is called once per frame
	protected override void Update () {
		base.Update ();
		if(phase == 1){
			
			//sineRotation += Time.deltaTime * 2;
			//transform.rotation = Quaternion.Euler (0, 0, 180 +  sineRotator * Mathf.Sin(sineRotation));
		}
	}

	public override void Shoot(){
		if(phase == 1f){
			PhaseOne ();
		}else if(phase == 2f){
			PhaseTwo ();
		}
	}

	private void PhaseOne(){
		if(shootTime >= shootDelay){

			for(int i = 0; i < 10; i++){
				EnemyProjectile e = Instantiate(projectiles[0], shootPos[0].position,
					Quaternion.Euler(0, 0, transform.eulerAngles.z - 90 + i * 18))
					.GetComponent<EnemyProjectile>();
				e.Setup (player);
			}



			shootTime = 0;
		}
	}

	private void PhaseTwo(){
		if (shootTime >= shootDelay) {
			int shootAmount = 0;
			if(difficulty == Global.RECRUIT){
				shootAmount = 3;
			}else if(difficulty == Global.VETEREN){
				shootAmount = 6;
			}else if(difficulty == Global.BATTLEH){
				shootAmount = 10;
			}
			for (int i = 0; i < shootAmount; i++) {
				EnemyProjectile e = Instantiate (projectiles [1], shootPos [0].position,
					Quaternion.Euler (0, 0, transform.eulerAngles.z - 45 + Random.Range(0,91)))
				.GetComponent<EnemyProjectile> ();
				e.Setup (player, 2);
			}
			shootTime = 0;
		}
	}

	public void ChangePhase(float i){
		if (i == 1){
			phase = 1;
			shootDelay = 1f;
			shootTime = 0;
			transform.localRotation = Quaternion.Euler (0, 0, 90);
		}else if(i == 0){
			phase = 0;
		}else if(i == 2){
			phase = 2;
			transform.localRotation = Quaternion.Euler (0, 0, 270);
			shootDelay = 0.3f;
		}else if(i == 3){
			shootDelay = 0.6f;
			phase = 2;
			transform.localRotation = Quaternion.Euler (0, 0, 270);
		}
	}
}
