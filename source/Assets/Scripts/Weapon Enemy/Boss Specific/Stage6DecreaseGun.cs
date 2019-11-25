using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage6DecreaseGun : WeaponEnemy {

	private float phase;


	// Use this for initialization
	protected override void Start () {
		base.Start ();

		shootTime = 0;
		shootDelay = 0.25f;
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
				step = 90;
			}else if(difficulty == Global.VETEREN){
				step = 60;
			}else if(difficulty == Global.BATTLEH){
				step = 45;
			}
			for(int i = 0; i < 361; i+= step){
				EnemyProjectile e = Instantiate (projectiles [0], shootPos [0].position + (transform.up * Random.Range(-0.5f, 0.5f)),
					Quaternion.Euler (0, 0, i + startAngle + Random.Range(-10,10))).GetComponent<EnemyProjectile> ();
				e.Setup (player, Random.Range(2f,3f),1);
			}
			shootTime = 0;
		}
	}
		

	public void ChangePhase(float i){
		if (i == 1){
			phase = 1;
			shootDelay = 0.25f;
			shootTime = 0;
		}
	}
}
