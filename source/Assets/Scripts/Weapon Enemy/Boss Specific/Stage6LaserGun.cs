using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage6LaserGun : WeaponEnemy {

	private float phase;


	// Use this for initialization
	protected override void Start () {
		base.Start ();

		shootTime = 0;
		shootDelay = 2;
	}

	// Update is called once per frame
	protected override void Update () {
		base.Update ();
	}

	public override void Shoot(){
		if(phase == 1f){
			PhaseOne ();
		}
	}

	private void PhaseOne(){
		if(shootTime >= shootDelay){
			int startAngle = Random.Range (0, 361);
			int step = 0;
			if(difficulty == Global.RECRUIT){
				step = 45;
			}else if(difficulty == Global.VETEREN){
				step = 20;
			}else if(difficulty == Global.BATTLEH){
				step = 15;
			}
			for(int i = 0; i < 361; i+= step){
				EnemyProjectile e = Instantiate (projectiles [0], shootPos [0].position + (transform.up * Random.Range(-0.3f, 0.3f)),
					Quaternion.Euler (0, 0, i + startAngle + Random.Range(-5,5))).GetComponent<EnemyProjectile> ();
				e.Setup (player, 2,1);

				EnemyProjectile e1 = Instantiate (projectiles [1], shootPos [0].position + (transform.up * Random.Range(-0.3f, 0.3f)),
					Quaternion.Euler (0, 0, i + startAngle + Random.Range(-5,5))).GetComponent<EnemyProjectile> ();
				e1.Setup (player, Random.Range(1.5f,3.5f),1);
				e1.SetRadius (0.1f);
				if(Global.Difficulty == Global.BATTLEH){
					EnemyProjectile e2 = Instantiate (projectiles [1], shootPos [0].position + (transform.up * Random.Range(-0.3f, 0.3f)),
						Quaternion.Euler (0, 0, i + startAngle + Random.Range(-5,5))).GetComponent<EnemyProjectile> ();
					e2.Setup (player, Random.Range(1.5f,3.5f),1);
					e2.SetRadius (0.1f);
				}

			}
			shootTime = 0;
		}
	}
		

	public void ChangePhase(float i){
		if (i == 1){
			phase = 1;
			shootDelay = 1f;
			shootTime = 0;
		}
	}
}
