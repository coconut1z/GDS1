using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage2BossGun2 : WeaponEnemy {

	private int phase;
	private float LeftRightStep;
	private float LeftRightAmount;
	private bool MovingRight;
	private float degStep;
	private float shootCount;
	private float shootAmount;
	private float shootCooldown;

	// Use this for initialization
	protected override void Start () {
		base.Start ();
		//ChangePhase (1);
		LeftRightAmount = 0;
	}

	// Update is called once per frame
	protected override void Update () {
		base.Update ();
		lookAtPlayer ();
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
		
		if(shootTime > shootDelay){
			EnemyProjectile e1 = Instantiate (projectiles [0], shootPos [1].position, 
				Quaternion.Euler (0, 0, transform.eulerAngles.z - 30f)).GetComponent<EnemyProjectile>();
			EnemyProjectile e2 = Instantiate (projectiles [0], shootPos [2].position, 
				Quaternion.Euler (0, 0, transform.eulerAngles.z + 30f)).GetComponent<EnemyProjectile>();
			e1.Setup (player, 5.55f, 1);
			e1.SetRadius (0.1f);
			e2.Setup(player, 5.55f, 1);
			e2.SetRadius (0.1f);
			shootTime = 0;
		}
	}

	private void PhaseTwo(){
		transform.rotation = Quaternion.Euler (0, 0, transform.eulerAngles.z + degStep*Time.deltaTime);
		shootCooldown -= Time.deltaTime;
		if(shootTime > shootDelay){
			if(shootCooldown <= 0){
				degStep = 30f;
				EnemyProjectile e1 = Instantiate (projectiles [0], shootPos [0].position, 
					Quaternion.Euler (0, 0, transform.eulerAngles.z)).GetComponent<EnemyProjectile>();
				EnemyProjectile e2 = Instantiate (projectiles [0], shootPos [1].position, 
					Quaternion.Euler (0, 0, transform.eulerAngles.z - 30f)).GetComponent<EnemyProjectile>();
				EnemyProjectile e3 = Instantiate (projectiles [0], shootPos [2].position, 
					Quaternion.Euler (0, 0, transform.eulerAngles.z + 30f)).GetComponent<EnemyProjectile>();
				e1.Setup (player, 2.22f, 1);
				e1.SetRadius (0.1f);
				e2.Setup(player, 2.22f, 1);
				e2.SetRadius (0.1f);
				e3.Setup(player, 2.22f, 1);
				e3.SetRadius (0.1f);
				shootAmount++;
			}
			if(shootAmount >= shootCount){
				degStep = 200f;
				shootAmount = 0;
				if(difficulty == Global.RECRUIT){
					shootCooldown = 0.6f;
				}else if(difficulty == Global.VETEREN){
					shootCooldown = 0.3f;
				}else if(difficulty == Global.BATTLEH){
					shootCooldown = 0.3f;
				}
			}
			shootTime = 0;
		}
	}

	private void PhaseThree(){
		
		shootCooldown -= Time.deltaTime;
		if(shootTime > shootDelay){
			if(shootCooldown <= 0){
				degStep = 30f;
				EnemyProjectile e1 = Instantiate (projectiles [0], shootPos [0].position, 
					Quaternion.Euler (0, 0, transform.eulerAngles.z)).GetComponent<EnemyProjectile>();
				EnemyProjectile e2 = Instantiate (projectiles [0], shootPos [1].position, 
					Quaternion.Euler (0, 0, transform.eulerAngles.z - 30f)).GetComponent<EnemyProjectile>();
				EnemyProjectile e3 = Instantiate (projectiles [0], shootPos [2].position, 
					Quaternion.Euler (0, 0, transform.eulerAngles.z + 30f)).GetComponent<EnemyProjectile>();
				e1.Setup (player, 1.83f, 1);
				e1.SetRadius (0.1f);
				e2.Setup(player, 1.83f, 1);
				e2.SetRadius (0.1f);
				e3.Setup(player, 1.83f, 1);
				e3.SetRadius (0.1f);
				shootAmount++;
			}
			if(shootAmount >= shootCount){
				degStep = 200f;
				shootAmount = 0;
				if(difficulty == Global.RECRUIT){
					shootCooldown = 1.5f;
				}else if(difficulty == Global.VETEREN){
					shootCooldown = 1.5f;
				}else if(difficulty == Global.BATTLEH){
					shootCooldown = 1.5f;
				}
			}
			shootTime = 0;
		}
	}

	public void ChangePhase(int i){
		switch(i){
		case 1:
			phase = 1;
			shootTime = 0;
			shootDelay = 0.1f;
			LeftRightStep = 1f;
			MovingRight = true;
			transform.rotation = Quaternion.Euler (0, 0, 180);
			break;
		case 2:
			phase = 2;
			shootTime = 0;
			shootDelay = 0.05f;
			degStep = 200f;
			shootAmount = 0;
			shootCooldown = 0.5f;
			if(difficulty == Global.RECRUIT){
				shootCount = 1;
				shootCooldown = 0.6f;
			}else if(difficulty == Global.VETEREN){
				shootCount = 2;
				shootCooldown = 0.45f;
			}else if(difficulty == Global.BATTLEH){
				shootCount = 4;
				shootCooldown = 0.3f;
			}
			break;
		case 3:
			phase = 3;
			shootTime = 0;
			shootDelay = 0.05f;
			LeftRightStep = 2f;
			MovingRight = true;
			if(difficulty == Global.RECRUIT){
				shootCount = 1;
				shootCooldown = 1.5f;
			}else if(difficulty == Global.VETEREN){
				shootCount = 2;
				shootCooldown = 1.5f;
			}else if(difficulty == Global.BATTLEH){
				shootCount = 4;
				shootCooldown = 1.5f;
			}
			transform.rotation = Quaternion.Euler (0, 0, 180);
			break;
		default:
			break;
		}
	}

	protected override void lookAtPlayer(){
		if(phase == 1){
			if(MovingRight){
				LeftRightAmount += LeftRightStep * Time.deltaTime;
				if(LeftRightAmount > 2f){
					MovingRight = false;
				}
			}else if(!MovingRight){
				LeftRightAmount -= LeftRightStep * Time.deltaTime;
				if(LeftRightAmount < -2f){
					MovingRight = true;
				}
			}
			float AngleRadToMouse = Mathf.Atan2 (
				player.position.y - transform.position.y,
				player.position.x + LeftRightAmount - transform.position.x);

			float AngleToDeg = (180 / Mathf.PI) * AngleRadToMouse - 90;
			transform.rotation = Quaternion.Euler (0, 0, AngleToDeg);
		}else if(phase == 3){
			if(MovingRight){
				LeftRightAmount += LeftRightStep * Time.deltaTime;
				if(LeftRightAmount > 4f){
					MovingRight = false;
				}
			}else if(!MovingRight){
				LeftRightAmount -= LeftRightStep * Time.deltaTime;
				if(LeftRightAmount < -4f){
					MovingRight = true;
				}
			}
			float AngleRadToMouse = Mathf.Atan2 (
				player.position.y + LeftRightAmount - transform.position.y,
				player.position.x - transform.position.x);

			float AngleToDeg = (180 / Mathf.PI) * AngleRadToMouse - 90;
			transform.rotation = Quaternion.Euler (0, 0, AngleToDeg);
		}
	}
}
