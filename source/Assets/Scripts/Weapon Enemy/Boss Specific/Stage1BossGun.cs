using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1BossGun : WeaponEnemy {

	public int phase;
	public float degStep;
	private float angle;
	private int lineCount;

	// Use this for initialization
	protected override void Start () {
		base.Start ();
		ChangePhase (1);
		lineCount = 0;
	}
	
	// Update is called once per frame
	protected override void Update () {
		base.Update ();
	}

	public override void Shoot(){
		switch(phase){
		case 1:
			PhaseOne ();
			break;
		case 2:
			PhaseTwo ();
			break;
		case 3:
			PhaseThree ();
			break;
		default:
			break;
		}

	}

	private void PhaseOne(){
		angle += degStep * Time.deltaTime;
		transform.localRotation = Quaternion.Euler (0, 0, angle);

		if(shootTime > shootDelay){
			
			if(difficulty == Global.RECRUIT){
				EnemyProjectile e1 = Instantiate (projectiles [0], shootPos [0].position, 
					Quaternion.Euler (0, 0, shootPos [0].eulerAngles.z)).GetComponent<EnemyProjectile>();
				e1.Setup (player, 4.55f, 1);
				e1.SetRadius (0.1f);
				EnemyProjectile e2 = Instantiate (projectiles [0], shootPos [4].position, 
					Quaternion.Euler (0, 0, shootPos [4].eulerAngles.z)).GetComponent<EnemyProjectile>();
				e2.Setup (player, 4.55f, 1);
				e2.SetRadius (0.1f);
			}else if(difficulty == Global.VETEREN){
				EnemyProjectile e1 = Instantiate (projectiles [0], shootPos [0].position, 
					Quaternion.Euler (0, 0, shootPos [0].eulerAngles.z)).GetComponent<EnemyProjectile>();
				e1.Setup (player, 4.55f, 1);
				e1.SetRadius (0.1f);
				EnemyProjectile e2 = Instantiate (projectiles [0], shootPos [2].position, 
					Quaternion.Euler (0, 0, shootPos [2].eulerAngles.z)).GetComponent<EnemyProjectile>();
				e2.Setup (player, 4.55f, 1);
				e2.SetRadius (0.1f);
				EnemyProjectile e3 = Instantiate (projectiles [0], shootPos [4].position, 
					Quaternion.Euler (0, 0, shootPos [4].eulerAngles.z)).GetComponent<EnemyProjectile>();
				e3.Setup (player, 4.55f, 1);
				e3.SetRadius (0.1f);
				EnemyProjectile e4 = Instantiate (projectiles [0], shootPos [6].position, 
					Quaternion.Euler (0, 0, shootPos [6].eulerAngles.z)).GetComponent<EnemyProjectile>();
				e4.Setup (player, 4.55f, 1);
				e4.SetRadius (0.1f);
			}else if(difficulty == Global.BATTLEH){
				EnemyProjectile e1 = Instantiate (projectiles [0], shootPos [0].position, 
					Quaternion.Euler (0, 0, shootPos [0].eulerAngles.z)).GetComponent<EnemyProjectile>();
				e1.Setup (player, 4.55f, 1);
				e1.SetRadius (0.1f);
				EnemyProjectile e2 = Instantiate (projectiles [0], shootPos [2].position, 
					Quaternion.Euler (0, 0, shootPos [0].eulerAngles.z - 30)).GetComponent<EnemyProjectile>();
				e2.Setup (player, 4.55f, 1);
				e2.SetRadius (0.1f);
				EnemyProjectile e3 = Instantiate (projectiles [0], shootPos [4].position, 
					Quaternion.Euler (0, 0, shootPos [0].eulerAngles.z)).GetComponent<EnemyProjectile>();
				e3.Setup (player, 4.55f, 1);
				e3.SetRadius (0.1f);
				EnemyProjectile e4 = Instantiate (projectiles [0], shootPos [6].position, 
					Quaternion.Euler (0, 0, shootPos [0].eulerAngles.z + 30)).GetComponent<EnemyProjectile>();
				e4.Setup (player, 4.55f, 1);
				e4.SetRadius (0.1f);
				EnemyProjectile e5 = Instantiate (projectiles [0], shootPos [4].position, 
					Quaternion.Euler (0, 0, shootPos [4].eulerAngles.z)).GetComponent<EnemyProjectile>();
				e5.Setup (player, 4.55f, 1);
				e5.SetRadius (0.1f);
				EnemyProjectile e6 = Instantiate (projectiles [0], shootPos [6].position, 
					Quaternion.Euler (0, 0, shootPos [4].eulerAngles.z - 30)).GetComponent<EnemyProjectile>();
				e6.Setup (player, 4.55f, 1);
				e6.SetRadius (0.1f);
				EnemyProjectile e7 = Instantiate (projectiles [0], shootPos [0].position, 
					Quaternion.Euler (0, 0, shootPos [4].eulerAngles.z)).GetComponent<EnemyProjectile>();
				e7.Setup (player, 4.55f, 1);
				e7.SetRadius (0.1f);
				EnemyProjectile e8 = Instantiate (projectiles [0], shootPos [2].position, 
					Quaternion.Euler (0, 0, shootPos [4].eulerAngles.z + 30)).GetComponent<EnemyProjectile>();
				e8.Setup (player, 4.55f, 1);
				e8.SetRadius (0.1f);
			}

			shootTime = 0;
		}
	}

	private void PhaseTwo(){
		angle += degStep * Time.deltaTime;
		transform.localRotation = Quaternion.Euler (0, 0, angle);
		if(shootTime > shootDelay){
			int step = 0;
			if(difficulty == Global.RECRUIT){
				step = 4;
			}else if(difficulty == Global.VETEREN){
				step = 2;
			}else if(difficulty == Global.BATTLEH){
				step = 1;
			}
			for(int i = 0; i < shootPos.Length; i+=step){
				spread = 15;
				EnemyProjectile e = Instantiate (projectiles [0], shootPos [i].position,
					Quaternion.Euler (0, 0, shootPos [i].eulerAngles.z + spread - 2 * Random.value * spread))
					.GetComponent<EnemyProjectile>();
				e.Setup (player, Random.Range (1.36f, 3.2f), 1);
				e.SetRadius (0.1f);
			}
			shootTime = 0;
		}
	}

	private void PhaseThree(){
		angle += degStep * Time.deltaTime;
		transform.localRotation = Quaternion.Euler (0, 0, angle);
		if(shootTime > shootDelay && lineCount < 8){
			if(difficulty == Global.RECRUIT){
				Transform line = Instantiate (projectiles [Random.Range(1,9)], shootPos [lineCount].position, 
					Quaternion.Euler (0, 0, shootPos [lineCount].eulerAngles.z)).transform;
				for(int i = 0; i < line.childCount; i++){
					EnemyProjectile e = line.GetChild (i).GetComponent<EnemyProjectile> ();
					e.Setup (player, 9, 1);
					e.SetRadius (0.1f);
				}
				lineCount+=2;
				line.parent = transform;	
			}else if(difficulty == Global.VETEREN){
				Transform line = Instantiate (projectiles [Random.Range(1,9)], shootPos [lineCount].position, 
					Quaternion.Euler (0, 0, shootPos [lineCount].eulerAngles.z)).transform;
				for(int i = 0; i < line.childCount; i++){
					EnemyProjectile e = line.GetChild (i).GetComponent<EnemyProjectile> ();
					e.Setup (player, 9, 1);
					e.SetRadius (0.1f);
				}
				lineCount++;
				line.parent = transform;	
			}else if(difficulty == Global.BATTLEH){
				Transform line = Instantiate (projectiles [Random.Range(1,9)], shootPos [lineCount].position, 
					Quaternion.Euler (0, 0, shootPos [lineCount].eulerAngles.z)).transform;
				for(int i = 0; i < line.childCount; i++){
					EnemyProjectile e = line.GetChild (i).GetComponent<EnemyProjectile> ();
					e.Setup (player, 9, 1);
					e.SetRadius (0.1f);
				}
				lineCount++;
				line.parent = transform;	
			}

			shootTime = 0;
		}
	}

	public void ChangePhase(int i){
		switch(i){
		case 1:
			phase = 1;
			shootDelay = 0.075f;
			degStep = 100f;
			angle = 0;
			break;
		case 2:
			phase = 2;
			degStep = 400f;
			break;
		case 3:
			phase = 3;
			degStep = 25f;
			if(difficulty == Global.RECRUIT){
				shootDelay = 2f;
			}else if(difficulty == Global.VETEREN){
				shootDelay = 2f;
			}else if(difficulty == Global.BATTLEH){
				shootDelay = 0.75f;
			}

			shootTime = 0f;
			angle = -90f;
			if(difficulty == Global.BATTLEH){
				degStep = 50f;
			}
			break;
		case 99:
			phase = 2;
			shootDelay = 0.3f;
			degStep = 100f;
			angle = 0;
			break;
		default:
			break;
		}
	}
}
