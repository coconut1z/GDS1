using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage2BossGun1 : WeaponEnemy {

	private int phase;
	public GameObject[] RLines, VLines, BLines;
	private float p6Angle, p6Step, turnAmount, turnCount, p6StartingAngle;
	private bool p7First, p7Second, p7Third;
	private Stage2Boss boss;
	private WeakBoss4 bossw;
	private float switchDirection;

	// Use this for initialization
	protected override void Start () {
		base.Start ();
		ChangePhase (1);
		if(transform.parent.parent.GetComponent<Stage2Boss> ()){
			boss = transform.parent.parent.GetComponent<Stage2Boss> ();
		}else{
			bossw = transform.parent.parent.GetComponent<WeakBoss4> ();
		}

	}

	// Update is called once per frame
	protected override void Update () {
		base.Update ();

		if(boss){
			if(phase == 7 && boss.health > boss.phase8 - boss.phase8p1){
				shootTime = 0;
			}
		}else{
			if(phase == 7 && bossw.health > bossw.phase8 - bossw.phase8p1){
				shootTime = 0;
			}
		}

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
		case 4:
			PhaseFour ();
			break;
		case 5:
			PhaseFive ();
			break;
		case 6:
			PhaseSix ();
			break;
		case 7:
			PhaseSeven ();
			break;
		default:
			break;
		}

	}

	private void PhaseOne(){
		float startAngle = Random.Range (0, 360);
		int step = 0;
		int gapCount = 8;
		int gapCounter = 0;
		int gapSize = 0;
		if(shootTime > shootDelay){
			if (difficulty == Global.RECRUIT) {
				step = 5;
				gapSize = 20;
			} else if (difficulty == Global.VETEREN) {
				step = 5;
				gapSize = 10;
			} else if (difficulty == Global.BATTLEH) {
				step = 3;
				gapSize = 6;
			}

			for(int i = 0; i < 360; i+=step){
				if(gapCounter >= gapCount){
					gapCounter = 0;
					i += gapSize;
				}
				EnemyProjectile e = Instantiate (projectiles [0], shootPos [0].position, 
					Quaternion.Euler (0, 0, startAngle + i)).GetComponent<EnemyProjectile>();
				e.Setup (player, 3.88f, 1);
				e.SetRadius (0.1f);
				gapCounter++;

			}
			shootTime = 0;
			if(shootDelay > 0.65f){
				shootDelay = shootDelay / 1.05f;
			}

		}
	}

	private void PhaseTwo(){
		if (shootTime > shootDelay) {
			float Rad = Mathf.Atan2 (
				player.position.y - transform.position.y,
				player.position.x - transform.position.x
			);
			float Deg = (180 / Mathf.PI) * Rad - 90;


			EnemyProjectile e = Instantiate (projectiles [1], shootPos [0].position, 
				Quaternion.Euler (0, 0, Deg)).GetComponent<EnemyProjectile>();
			e.Setup (player);
			e.SetRadius (0.45f);
			shootTime = 0;
		}
	}
		
	private void PhaseThree(){
		if (shootTime > shootDelay) {
			if (difficulty == Global.RECRUIT) {
				for(int i = 0; i < 5; i++){
					float angle = Random.Range (0, 361);
					EnemyProjectile e = Instantiate (projectiles [0], shootPos [0].position, 
						Quaternion.Euler (0, 0, angle)).GetComponent<EnemyProjectile>();
					e.Setup (player, Random.Range (1.11f, 4.44f), 1);
					e.SetRadius (0.1f);
				}
			} else if (difficulty == Global.VETEREN) {
				for(int i = 0; i < 10; i++){
					float angle = Random.Range (0, 361);
					EnemyProjectile e1 = Instantiate (projectiles [0], shootPos [0].position, 
						Quaternion.Euler (0, 0, angle)).GetComponent<EnemyProjectile>();
					e1.Setup (player, Random.Range (1.11f, 4.44f), 1);
					e1.SetRadius (0.1f);
					if(i % 2 == 0){
						angle = Random.Range (0, 361);
						EnemyProjectile e2 = Instantiate (projectiles [2], shootPos [0].position, 
							Quaternion.Euler (0, 0, angle)).GetComponent<EnemyProjectile>();
						e2.Setup (player, Random.Range (1.11f, 4.44f), 1);
						e2.SetRadius (0.15f);
					}
				}
			} else if (difficulty == Global.BATTLEH) {
				for(int i = 0; i < 12; i++){
					float angle = Random.Range (0, 361);
					EnemyProjectile e1 = Instantiate (projectiles [0], shootPos [0].position, 
						Quaternion.Euler (0, 0, angle)).GetComponent<EnemyProjectile>();
					e1.Setup (player, Random.Range (1.11f, 4.44f), 1);
					e1.SetRadius (0.1f);
					if(i % 2 == 0){
						angle = Random.Range (0, 361);
						EnemyProjectile e2 = Instantiate (projectiles [2], shootPos [0].position, 
							Quaternion.Euler (0, 0, angle)).GetComponent<EnemyProjectile>();
						e2.Setup (player, Random.Range (1.11f, 4.44f), 1);
						e2.SetRadius (0.15f);
					}
					if(i % 3 == 0){
						angle = Random.Range (0, 361);
						EnemyProjectile e3 = Instantiate (projectiles [3], shootPos [0].position, 
							Quaternion.Euler (0, 0, angle)).GetComponent<EnemyProjectile>();
						e3.Setup (player, Random.Range (1.11f, 4.44f), 1);
						e3.SetRadius (0.2f);
					}
				}
			}
			shootTime = 0;
		}
	}

	private void PhaseFour(){
		float startAngle = Random.Range (0, 361);
		int step = 0;
		if(difficulty == Global.RECRUIT){
			step = 20;
		}else if(difficulty == Global.VETEREN){
			step = 10;
		}else if(difficulty == Global.BATTLEH){
			step = 6;
		}
		if (shootTime > shootDelay) {
			//bulletReferenceCurve.setCurveSpeed = -bulletReferenceCurve.setCurveSpeed;
			for(int i = 0; i < 360; i+=step){
				EnemyRedBulletCurve e = Instantiate (projectiles [4], shootPos [0].position,
					Quaternion.Euler (0, 0, i + startAngle)).GetComponent<EnemyRedBulletCurve>();
				if(difficulty == Global.BATTLEH){
					e.Setup (player, 1.8f, 1);
					e.setCurveSpeed = 8 * switchDirection;
				}else{
					e.Setup (player, 3.5f, 1);
					e.setCurveSpeed = 15 * switchDirection;
				}

			}
			switchDirection = -switchDirection;
			if(shootDelay > 0.5f){
				shootDelay = shootDelay / 1.05f;
			}
			shootTime = 0;
		}
	}

	private void PhaseFive(){
		if (shootTime > shootDelay) {
			if(difficulty == Global.RECRUIT){
				Transform line = Instantiate (RLines [Random.Range (0, 3)], shootPos [0].position, 
					Quaternion.Euler (0, 0, 0)).transform;
				for(int i = 0; i < line.childCount; i++){
					line.GetChild (i).GetComponent<DynamicTimeDelete> ().Setup (player, 4.44f, 1);
				}
			}else if(difficulty == Global.VETEREN){
				Transform line = Instantiate (VLines[Random.Range(0,5)], shootPos [0].position, 
					Quaternion.Euler (0, 0, 0)).transform;
				for(int i = 0; i < line.childCount; i++){
					line.GetChild (i).GetComponent<DynamicTimeDelete> ().Setup (player, 4.44f, 1);
				}
			}else if(difficulty == Global.BATTLEH){
				Transform line = Instantiate (BLines[Random.Range(0,8)], shootPos [0].position, 
					Quaternion.Euler (0, 0, 0)).transform;
				for(int i = 0; i < line.childCount; i++){
					line.GetChild (i).GetComponent<DynamicTimeDelete> ().Setup (player, 4.44f, 1);
				}
			}
			shootTime = 0;
		}
	}

	private void PhaseSix(){
		int step = 60;
		if (shootTime > shootDelay) {
			shootTime = 0;
			p6Angle += p6Step;
			if(difficulty != Global.BATTLEH){
				for(int i = (int)p6StartingAngle; i < p6StartingAngle + 360f; i+=step){
					EnemyProjectile e = Instantiate (projectiles [0], shootPos [0].position, 
						Quaternion.Euler (0, 0, p6Angle + i)).GetComponent<EnemyProjectile>();
					if(difficulty == Global.RECRUIT){
						e.Setup (player, 3.33f, 1);
						e.SetRadius (0.1f);
					}else if(difficulty == Global.VETEREN){
						e.Setup (player, 3.75f, 1);
						e.SetRadius (0.1f);
					}
				}

				for(int i = (int)p6StartingAngle + 10; i < p6StartingAngle + 370; i+=step){
					EnemyProjectile e = Instantiate (projectiles [0], shootPos [0].position, 
						Quaternion.Euler (0, 0, -p6Angle + i)).GetComponent<EnemyProjectile>();
					if(difficulty == Global.RECRUIT){
						e.Setup (player, 3.33f, 1);
						e.SetRadius (0.1f);
					}else if(difficulty == Global.VETEREN){
						e.Setup (player, 3.75f, 1);
						e.SetRadius (0.1f);
					}
				}
				if(turnCount > turnAmount){
					turnCount = 0;
					p6Step = -p6Step;
					if(p6Step > 0){
						if(difficulty == Global.RECRUIT){
							shootTime = -0.4f;
						}else if(difficulty == Global.VETEREN){
							shootTime = 0;
						}
						p6StartingAngle = Random.Range (0, 361);
						if(boss){
							if(boss.health < boss.phase8 - boss.phase8p4){
								ChangePhase (7);
							}
						}else{
							if(bossw.health < bossw.phase8 - bossw.phase8p4){
								ChangePhase (7);
							}
						}

					}
				}
			}else{
				for(int i = (int)p6StartingAngle; i < p6StartingAngle + 360f; i+=step){
					EnemyProjectile e = Instantiate (projectiles [0], shootPos [0].position, 
						Quaternion.Euler (0, 0, p6Angle + i)).GetComponent<EnemyProjectile>();
					e.Setup (player, 3f, 1);
					e.SetRadius (0.1f);
				}

				for(int i = (int)p6StartingAngle ; i < p6StartingAngle + 360f; i+=step){
					EnemyProjectile e = Instantiate (projectiles [0], shootPos [0].position, 
						Quaternion.Euler (0, 0, -p6Angle + i)).GetComponent<EnemyProjectile>();
					e.Setup (player, 3f, 1);
					e.SetRadius (0.1f);
				}
				if(turnCount > turnAmount){
					turnCount = 0;
					p6Step = -p6Step;
					if(p6Step > 0){
						shootTime = 0;
						p6StartingAngle += Random.Range(0,21);
						if(boss){
							if(boss.health < boss.phase8 - boss.phase8p4){
								ChangePhase (7);
							}
						}else{
							if(bossw.health < bossw.phase8 - bossw.phase8p4){
								ChangePhase (7);
							}
						}
					}
				}
			}
			turnCount++;
		}
	}

	private void PhaseSeven(){
		float startAngle = Random.Range (0, 360);
		int step = 0;
		int gapCount = 8;
		int gapCounter = 0;
		int gapSize = 0;
		if(boss){
			if(boss.health > boss.phase8 - boss.phase8p1){
				shootTime = 0;
			}
			if(shootTime > 1.5 && !p7First && boss.health <= boss.phase8 - boss.phase8p1){
				p7First = true;
				if (difficulty == Global.RECRUIT) {
					step = 5;
					gapSize = 20;
				} else if (difficulty == Global.VETEREN) {
					step = 5;
					gapSize = 10;
				} else if (difficulty == Global.BATTLEH) {
					step = 3;
					gapSize = 6;
				}

				for(int i = 0; i < 360; i+=step){
					if(gapCounter >= gapCount){
						gapCounter = 0;
						i += gapSize;
					}
					EnemyProjectile e = Instantiate (projectiles [0], shootPos [0].position, 
						Quaternion.Euler (0, 0, startAngle + i)).GetComponent<EnemyProjectile>();
					e.Setup (player, 3.88f, 1);
					e.SetRadius (0.1f);
					gapCounter++;

				}
			}
			if(shootTime > 3 && !p7Second && boss.health <= boss.phase8 - boss.phase8p2){
				p7Second = true;
				if(difficulty == Global.RECRUIT){
					step = 20;
				}else if(difficulty == Global.VETEREN){
					step = 10;
				}else if(difficulty == Global.BATTLEH){
					step = 6;
				}
				startAngle = Random.Range (0, 361);
				for(int i = 0; i < 360; i+=step){
					EnemyRedBulletCurve e = Instantiate (projectiles [4], shootPos [0].position,
						Quaternion.Euler (0, 0, i + startAngle)).GetComponent<EnemyRedBulletCurve>();
					if(difficulty == Global.BATTLEH){
						e.Setup (player, 3.1f, 1);
						e.setCurveSpeed = 8 * switchDirection;
					}else{
						e.Setup (player, 3.1f, 1);
						e.setCurveSpeed = 15 * switchDirection;
					}

				}
				switchDirection = -switchDirection;
			}
			if(shootTime > 4.5 && !p7Third && boss.health <= boss.phase8 - boss.phase8p3){
				p7Third = true;
				float Rad = Mathf.Atan2 (
					player.position.y - transform.position.y,
					player.position.x - transform.position.x
				);
				float Deg = (180 / Mathf.PI) * Rad - 90;

				EnemyProjectile e = Instantiate (projectiles [1], shootPos [0].position, 
					Quaternion.Euler (0, 0, Deg)).GetComponent<EnemyProjectile>();
				e.Setup (player);
				e.SetRadius (0.45f);
			}
			if(shootTime > 6 && boss.health <= boss.phase8 - boss.phase8p4){
				shootTime = 0;
				p7First = p7Second = p7Third = false;
				ChangePhase (6);
			}else if(shootTime > 6){
				shootTime = 0;
				p7First = p7Second = p7Third = false;
			}
		}else{
			if(bossw.health > bossw.phase8 - bossw.phase8p1){
				shootTime = 0;
			}
			if(shootTime > 1.5 && !p7First && bossw.health <= bossw.phase8 - bossw.phase8p1){
				p7First = true;
				if (difficulty == Global.RECRUIT) {
					step = 5;
					gapSize = 20;
				} else if (difficulty == Global.VETEREN) {
					step = 5;
					gapSize = 10;
				} else if (difficulty == Global.BATTLEH) {
					step = 3;
					gapSize = 6;
				}

				for(int i = 0; i < 360; i+=step){
					if(gapCounter >= gapCount){
						gapCounter = 0;
						i += gapSize;
					}
					EnemyProjectile e = Instantiate (projectiles [0], shootPos [0].position, 
						Quaternion.Euler (0, 0, startAngle + i)).GetComponent<EnemyProjectile>();
					e.Setup (player, 3.88f, 1);
					e.SetRadius (0.1f);
					gapCounter++;

				}
			}
			if(shootTime > 3 && !p7Second && bossw.health <= bossw.phase8 - bossw.phase8p2){
				p7Second = true;
				if(difficulty == Global.RECRUIT){
					step = 20;
				}else if(difficulty == Global.VETEREN){
					step = 10;
				}else if(difficulty == Global.BATTLEH){
					step = 6;
				}
				startAngle = Random.Range (0, 361);
				for(int i = 0; i < 360; i+=step){
					EnemyRedBulletCurve e = Instantiate (projectiles [4], shootPos [0].position,
						Quaternion.Euler (0, 0, i + startAngle)).GetComponent<EnemyRedBulletCurve>();
					if(difficulty == Global.BATTLEH){
						e.Setup (player, 3.1f, 1);
						e.setCurveSpeed = 8 * switchDirection;
					}else{
						e.Setup (player, 3.1f, 1);
						e.setCurveSpeed = 15 * switchDirection;
					}

				}
				switchDirection = -switchDirection;
			}
			if(shootTime > 4.5 && !p7Third && bossw.health <= bossw.phase8 - bossw.phase8p3){
				p7Third = true;
				float Rad = Mathf.Atan2 (
					player.position.y - transform.position.y,
					player.position.x - transform.position.x
				);
				float Deg = (180 / Mathf.PI) * Rad - 90;

				EnemyProjectile e = Instantiate (projectiles [1], shootPos [0].position, 
					Quaternion.Euler (0, 0, Deg)).GetComponent<EnemyProjectile>();
				e.Setup (player);
				e.SetRadius (0.45f);
			}
			if(shootTime > 6 && bossw.health <= bossw.phase8 - bossw.phase8p4){
				shootTime = 0;
				p7First = p7Second = p7Third = false;
				ChangePhase (6);
			}else if(shootTime > 6){
				shootTime = 0;
				p7First = p7Second = p7Third = false;
			}
		}

	}


	public void ChangePhase(int i){
		switch(i){
		case 1:
			phase = 1;
			shootTime = 0;
			shootDelay = 1f;
			break;
		case 2:
			phase = 2;
			shootTime = 0;
			shootDelay = 2f;
			break;
		case 3:
			phase = 3;
			shootTime = 0;
			shootDelay = 0.3f;
			break;
		case 4:
			phase = 4;
			shootTime = 0;
			shootDelay = 1f;
			//bulletReferenceCurve.setSpeed = 200f;
			//bulletReferenceCurve.setCurveSpeed = 15f;
			if (difficulty == Global.RECRUIT) {
				shootDelay = 1f;
			} else if (difficulty == Global.VETEREN) {
				shootDelay = 0.75f;
			} else if (difficulty == Global.BATTLEH) {
				//bulletReferenceCurve.setSpeed = 100f;
				//bulletReferenceCurve.setCurveSpeed = 8f;
				shootDelay = 0.5f;
			}
			switchDirection = 1;
			break;
		case 5:
			phase = 5;
			shootTime = 0;
			shootDelay = 2f;
			break;
		case 6:
			phase = 6;
			shootTime = 0;
			shootDelay = 0.08f;
			p6Angle = 0;
			if(difficulty == Global.RECRUIT){ 
				p6Step = 3.5f;
				turnAmount = 7;
			}else if(difficulty == Global.VETEREN){ 
				p6Step = 3.5f;
				turnAmount = 7;
			}else if(difficulty == Global.BATTLEH){
				p6Step = 7f;
				turnAmount = 10;
			}
			turnCount = 0;
			p6StartingAngle = 0;
			break;
		case 7:
			p7First = p7Second = p7Third = false;
			phase = 7;
			shootTime = 0;
			shootDelay = 6f;
			turnCount = 0;
			p6StartingAngle = 0;
			p6Angle = 0;
			if (difficulty == Global.RECRUIT) { 
				p6Step = 3.5f;
				turnAmount = 7;
			} else if (difficulty == Global.VETEREN) {
				p6Step = 3.5f;
				turnAmount = 7;
			} else if (difficulty == Global.BATTLEH) { 
				p6Step = 7f;
				turnAmount = 10;
			}
			break;
		default:
			break;
		}
	}
}
