using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage5BossGun5 : WeaponEnemy {
	
	private float phase;
	private float angle;
	private float counter;
	private float startAngle;
	private float direction;

	// Use this for initialization
	protected override void Start () {
		base.Start ();

		shootTime = 0;
		shootDelay = 2;
		angle = 0;
		counter = 0;
		startAngle = 0;
		direction = 1;
		//ChangePhase (1);
	}

	// Update is called once per frame
	protected override void Update () {
		base.Update ();
		if(phase == 1){
			//lookAtPlayer ();
			//sineRotation += Time.deltaTime * 25;
			//shootTimer2 += Time.deltaTime;
		}
	}

	public override void Shoot(){
		if(phase == 1f){
			PhaseOne ();
			//lookAtPlayer ();
		}else if(phase == 2){
			PhaseTwo ();
		}else if(phase == 3){
			lookAtPlayer ();
			PhaseThree ();
		}else if(phase == 4){
			PhaseFour ();
		}else if(phase == 5){
			PhaseFive ();
		}
	}

	private void PhaseOne(){
		if(shootTime >= shootDelay){
			transform.localRotation = Quaternion.Euler (0, 0, Random.Range(180, 361));
			Instantiate (projectiles [0], shootPos [0].position, Quaternion.Euler (0, 0, transform.eulerAngles.z));
			if(difficulty == Global.VETEREN){
				for(int i = 0; i < 4; i++){
					Instantiate (projectiles [0], shootPos [0].position, 
						Quaternion.Euler (0, 0, transform.eulerAngles.z + 90 - Random.Range(0,180)));
				}
			}else if(difficulty == Global.BATTLEH){
				for(int i = 0; i < 8; i++){
					Instantiate (projectiles [0], shootPos [0].position, 
						Quaternion.Euler (0, 0, transform.eulerAngles.z + 90 - Random.Range(0,180)));
				}

			}else if(difficulty == Global.RECRUIT){
				//Instantiate (projectiles [0], shootPos [0].position, 
					//Quaternion.Euler (0, 0, transform.eulerAngles.z + 90 - Random.Range(0,180)));
			}
			shootTime = 0;
		}

	}

	private void PhaseTwo(){
		float randomAngle = Random.Range (0, 361);
		if(shootTime >= shootDelay){
			int step = 0;
			if(difficulty == Global.RECRUIT){
				step = 15;
			}else if(difficulty == Global.VETEREN){
				step = 10;
			}else if(difficulty == Global.BATTLEH){
				step = 6;
			}
			for(int i = 0; i < 360; i+=step){
				Instantiate (projectiles [0], shootPos [0].position, Quaternion.Euler (0, 0,i + randomAngle));
			}
			shootTime = 0;
		}
	}

	private void PhaseThree(){
		if(shootTime >= shootDelay){
			Instantiate (projectiles [0], shootPos [0].position, Quaternion.Euler (0, 0, transform.eulerAngles.z));
			shootTime = 0;
		}

	}

	private void PhaseFour(){
		if(shootTime >= shootDelay){
			shootTime = 0;
			int step = 0;
			if(difficulty == Global.RECRUIT){
				step = 15;
			}else if(difficulty == Global.VETEREN){
				step = 12;
			}else if(difficulty == Global.BATTLEH){
				step = 10;
			}
			for(int i = 0; i < 360; i+=step){
				Instantiate (projectiles [0], shootPos [0].position, Quaternion.Euler (0, 0, (i  + angle + startAngle) * direction));
			}
			angle += 1.5f;
			if(counter < 0){
				startAngle = Random.Range (0, 361);
				angle = 0;
				counter = 15;
				shootTime = -2f;
				if(Random.value < 0.5f){
					direction = -direction;
				}
			}
			counter--;
		}
	}

	private void PhaseFive(){
		if(shootTime >= shootDelay){
			transform.localRotation = Quaternion.Euler (0, 0, Random.Range(180, 361));
			Instantiate (projectiles [0], shootPos [0].position, Quaternion.Euler (0, 0, transform.eulerAngles.z));
			shootTime = 0;
		}

	}

	public void ChangePhase(float i){
		if (i == 1){
			phase = 1;
			shootTime = 0;
			shootDelay = 2;
			if(difficulty == Global.RECRUIT){
				
			}else if(difficulty == Global.VETEREN){
				
			}else if(difficulty == Global.BATTLEH){
				
			}

			shootTime = 0;
		}else if(i == 0){
			phase = 0;
		}else if (i == 2){
			phase = 2;
			shootDelay = 2;
			shootTime = 0;
			transform.localRotation = Quaternion.Euler (0, 0, 270);
		}else if(i == 3){
			phase = 3;
			shootDelay = 2;
			shootTime = 0;
		}else if(i == 4){
			transform.localRotation = Quaternion.Euler (0, 0, 270);
			phase = 4;
			shootDelay = 0.4f;
			shootTime = 0;
			counter = 15;
		}else if(i == 5){
			phase = 5;
			shootDelay = 2f;
			shootTime = 0;
		}
	}
}
