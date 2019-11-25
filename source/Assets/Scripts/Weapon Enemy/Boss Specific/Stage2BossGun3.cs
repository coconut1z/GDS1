using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage2BossGun3 : WeaponEnemy {

	private int phase;
	private bool MovingRight;
	private float degStep;
	private float shootAmount;

	// Use this for initialization
	protected override void Start () {
		base.Start ();
		//bulletReference = projectiles [0].GetComponent<DynamicDelayBoss2> ();
		//ChangePhase (1);
		phase = 0;
	}

	// Update is called once per frame
	protected override void Update () {
		base.Update ();
		if(transform.localPosition.y < 1.45f && phase == 1){
			transform.localPosition = new Vector2 (0, transform.localPosition.y + Time.deltaTime * 1.5f);
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
		default:
			break;
		}

	}

	private void PhaseOne(){
		
		if(shootTime > shootDelay){
			for(int i = 0; i < shootAmount ; i++){
				if(difficulty == Global.RECRUIT){
					EnemyProjectile e1 = Instantiate (projectiles [0], shootPos [0].position, 
						Quaternion.identity).GetComponent<EnemyProjectile>();
					e1.Setup (player);
					e1.SetRadius (0.1f);
					if(i % 2 == 0){
						EnemyProjectile e2 = Instantiate (projectiles [1], shootPos [0].position, 
							Quaternion.identity).GetComponent<EnemyProjectile>();
						e2.Setup (player);
						e2.SetRadius (0.15f);
					}
					if(i % 4 == 0){
						EnemyProjectile e3 = Instantiate (projectiles [2], shootPos [0].position, 
							Quaternion.identity).GetComponent<EnemyProjectile>();
						e3.Setup (player);
						e3.SetRadius (0.2f);
					}
				}else if(difficulty == Global.VETEREN){
					EnemyProjectile e1 = Instantiate (projectiles [0], shootPos [1].position, 
						Quaternion.identity).GetComponent<EnemyProjectile>();
					EnemyProjectile e11 = Instantiate (projectiles [0], shootPos [2].position, 
						Quaternion.identity).GetComponent<EnemyProjectile>();
					e1.Setup (player);
					e1.SetRadius (0.1f);
					e11.Setup (player);
					e11.SetRadius (0.1f);
					if(i % 2 == 0){
						EnemyProjectile e2 = Instantiate (projectiles [1], shootPos [1].position, 
							Quaternion.identity).GetComponent<EnemyProjectile>();
						EnemyProjectile e21 = Instantiate (projectiles [1], shootPos [2].position,
							Quaternion.identity).GetComponent<EnemyProjectile>();
						e2.Setup (player);
						e2.SetRadius (0.15f);
						e21.Setup (player);
						e21.SetRadius (0.15f);
					}
					if(i % 4 == 0){
						EnemyProjectile e3 = Instantiate (projectiles [2], shootPos [1].position, 
							Quaternion.identity).GetComponent<EnemyProjectile>();
						EnemyProjectile e31 = Instantiate (projectiles [2], shootPos [2].position, 
							Quaternion.identity).GetComponent<EnemyProjectile>();
						e3.Setup (player);
						e3.SetRadius (0.2f);
						e31.Setup (player);
						e31.SetRadius (0.2f);
					}
				}else if(difficulty == Global.BATTLEH){
					EnemyProjectile e1 = Instantiate (projectiles [0], shootPos [1].position, 
						Quaternion.identity).GetComponent<EnemyProjectile>();
					EnemyProjectile e11 = Instantiate (projectiles [0], shootPos [2].position, 
						Quaternion.identity).GetComponent<EnemyProjectile>();
					EnemyProjectile e12 = Instantiate (projectiles [0], shootPos [0].position, 
						Quaternion.identity).GetComponent<EnemyProjectile>();
					e1.Setup (player);
					e1.SetRadius (0.1f);
					e11.Setup (player);
					e11.SetRadius (0.1f);
					e12.Setup (player);
					e12.SetRadius (0.1f);
					if(i % 2 == 0){
						EnemyProjectile e2 = Instantiate (projectiles [1], shootPos [1].position, 
							Quaternion.identity).GetComponent<EnemyProjectile>();
						EnemyProjectile e21 = Instantiate (projectiles [1], shootPos [2].position, 
							Quaternion.identity).GetComponent<EnemyProjectile>();
						EnemyProjectile e22 = Instantiate (projectiles [1], shootPos [0].position, 
							Quaternion.identity).GetComponent<EnemyProjectile>();
						e2.Setup (player);
						e2.SetRadius (0.15f);
						e21.Setup (player);
						e21.SetRadius (0.15f);
						e22.Setup (player);
						e22.SetRadius (0.15f);
					}
					if(i % 4 == 0){
						EnemyProjectile e3 = Instantiate (projectiles [2], shootPos [1].position, 
							Quaternion.identity).GetComponent<EnemyProjectile>();
						EnemyProjectile e31 = Instantiate (projectiles [2], shootPos [2].position, 
							Quaternion.identity).GetComponent<EnemyProjectile>();
						EnemyProjectile e32 = Instantiate (projectiles [2], shootPos [0].position, 
							Quaternion.identity).GetComponent<EnemyProjectile>();
						e3.Setup (player);
						e3.SetRadius (0.2f);
						e31.Setup (player);
						e31.SetRadius (0.2f);
						e32.Setup (player);
						e32.SetRadius (0.2f);
					}
				}
			}
			shootTime = 0;
		}
	}

	private void PhaseTwo(){

		if(shootTime > shootDelay){
			for(int i = 0; i < shootAmount ; i++){
				if(difficulty == Global.RECRUIT){
					EnemyProjectile e1 = Instantiate (projectiles [3], shootPos [0].position, 
						Quaternion.identity).GetComponent<EnemyProjectile>();
					e1.Setup (player);
					e1.SetRadius (0.1f);
					if(i % 2 == 0){
						EnemyProjectile e2 = Instantiate (projectiles [4], shootPos [0].position, 
							Quaternion.identity).GetComponent<EnemyProjectile>();
						e2.Setup (player);
						e2.SetRadius (0.15f);
					}
					if(i % 4 == 0){
						EnemyProjectile e3 = Instantiate (projectiles [5], shootPos [0].position,
							Quaternion.identity).GetComponent<EnemyProjectile>();
						e3.Setup (player);
						e3.SetRadius (0.2f);
					}
				}else if(difficulty == Global.VETEREN){
					EnemyProjectile e1 = Instantiate (projectiles [3], shootPos [1].position, 
						Quaternion.identity).GetComponent<EnemyProjectile>();
					EnemyProjectile e11 = Instantiate (projectiles [3], shootPos [2].position, 
						Quaternion.identity).GetComponent<EnemyProjectile>();
					e1.Setup (player);
					e1.SetRadius (0.1f);
					e11.Setup (player);
					e11.SetRadius (0.1f);
					if(i % 2 == 0){
						EnemyProjectile e2 = Instantiate (projectiles [4], shootPos [1].position,
							Quaternion.identity).GetComponent<EnemyProjectile>();
						EnemyProjectile e21 = Instantiate (projectiles [4], shootPos [2].position,
							Quaternion.identity).GetComponent<EnemyProjectile>();
						e2.Setup (player);
						e2.SetRadius (0.15f);
						e21.Setup (player);
						e21.SetRadius (0.15f);
					}
					if(i % 4 == 0){
						EnemyProjectile e3 = Instantiate (projectiles [5], shootPos [1].position, 
							Quaternion.identity).GetComponent<EnemyProjectile>();
						EnemyProjectile e31 = Instantiate (projectiles [5], shootPos [2].position,
							Quaternion.identity).GetComponent<EnemyProjectile>();
						e3.Setup (player);
						e3.SetRadius (0.2f);
						e31.Setup (player);
						e31.SetRadius (0.2f);
					}
				}else if(difficulty == Global.BATTLEH){
					EnemyProjectile e1 = Instantiate (projectiles [3], shootPos [1].position,
						Quaternion.identity).GetComponent<EnemyProjectile>();
					EnemyProjectile e11 = Instantiate (projectiles [3], shootPos [2].position, 
						Quaternion.identity).GetComponent<EnemyProjectile>();
					EnemyProjectile e12 = Instantiate (projectiles [3], shootPos [0].position, 
						Quaternion.identity).GetComponent<EnemyProjectile>();
					e1.Setup (player);
					e1.SetRadius (0.1f);
					e11.Setup (player);
					e11.SetRadius (0.1f);
					e12.Setup (player);
					e12.SetRadius (0.1f);
					if(i % 2 == 0){
						EnemyProjectile e2 = Instantiate (projectiles [4], shootPos [1].position, 
							Quaternion.identity).GetComponent<EnemyProjectile>();
						EnemyProjectile e21 = Instantiate (projectiles [4], shootPos [2].position, 
							Quaternion.identity).GetComponent<EnemyProjectile>();
						EnemyProjectile e22 = Instantiate (projectiles [4], shootPos [0].position,
							Quaternion.identity).GetComponent<EnemyProjectile>();
						e2.Setup (player);
						e2.SetRadius (0.15f);
						e21.Setup (player);
						e21.SetRadius (0.15f);
						e22.Setup (player);
						e22.SetRadius (0.15f);
					}
					if(i % 4 == 0){
						EnemyProjectile e3 = Instantiate (projectiles [5], shootPos [1].position, 
							Quaternion.identity).GetComponent<EnemyProjectile>();
						EnemyProjectile e31 = Instantiate (projectiles [5], shootPos [2].position,
							Quaternion.identity).GetComponent<EnemyProjectile>();
						EnemyProjectile e32 = Instantiate (projectiles [5], shootPos [0].position, 
							Quaternion.identity).GetComponent<EnemyProjectile>();
						e3.Setup (player);
						e3.SetRadius (0.2f);
						e31.Setup (player);
						e31.SetRadius (0.2f);
						e32.Setup (player);
						e32.SetRadius (0.2f);
					}
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
			shootDelay = 2f;
			MovingRight = true;
			if(difficulty == Global.RECRUIT){
				shootAmount = 8;
			}else if(difficulty == Global.VETEREN){
				shootAmount = 12;
			}else if(difficulty == Global.BATTLEH){
				shootAmount = 16;
			}
			break;
		case 2:
			phase = 2;
			shootTime = 0;
			shootDelay = 4f;
			MovingRight = true;
			if(difficulty == Global.RECRUIT){
				shootAmount = 4;
			}else if(difficulty == Global.VETEREN){
				shootAmount = 6;
			}else if(difficulty == Global.BATTLEH){
				shootAmount = 8;
			}
			break;
		default:
			break;
		}
	}
}
