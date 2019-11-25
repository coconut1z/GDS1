using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingEnemyWeapon : WeaponEnemy {

	// Use this for initialization
	protected override void Start () {
		base.Start ();

		if(difficulty == Global.RECRUIT){
			shootDelay = 3f;
			shootTime = 0;
		}else if(difficulty == Global.VETEREN){
			shootDelay = 2f;
			shootTime = 1;
		}else if(difficulty == Global.BATTLEH){
			shootDelay = 2f;
			shootTime = 1;
		}
			
	}

	// Update is called once per frame
	protected override void Update () {
		base.Update ();
		lookAtPlayer ();
	}

	public override void Shoot(){
		if(shootTime > shootDelay){
			// Instantiate (BULLET PREFAB, SHOOT POSITION, Z ROTATION + SPREAD IF THERE IS ANY)
			if(difficulty == Global.RECRUIT){
				Transform t = Instantiate (projectiles [0], shootPos [0].position, 
					Quaternion.Euler (0, 0, transform.eulerAngles.z + spread - 2 * Random.value * spread))
					.transform;
				// Get children in the gameobject
				for(int i = 0; i < t.childCount; i++){
					EnemyProjectile e = t.GetChild(i).GetComponent<EnemyProjectile> ();
					e.Setup (player, 2.18f, 1); // Setup the child bullet
					e.SetRadius (0.1f); // Setup radius
				}
			}else if(difficulty == Global.VETEREN){
				Transform t = Instantiate (projectiles [0], shootPos [0].position, 
					Quaternion.Euler (0, 0, transform.eulerAngles.z + spread - 2 * Random.value * spread))
					.transform;
				for(int i = 0; i < t.childCount; i++){
					EnemyProjectile e = t.GetChild(i).GetComponent<EnemyProjectile> ();
					e.Setup (player, 2.18f, 1);
					e.SetRadius (0.1f);
				}
			}else if(difficulty == Global.BATTLEH){
				Transform t = Instantiate (projectiles [1], shootPos [0].position, 
					Quaternion.Euler (0, 0, transform.eulerAngles.z + spread - 2 * Random.value * spread))
					.transform;
				for(int i = 0; i < t.childCount; i++){
					EnemyProjectile e = t.GetChild(i).GetComponent<EnemyProjectile> ();
					e.Setup (player, 3f, 1);
					e.SetRadius (0.1f);
				}
			}
			shootTime = 0;
		}
	}
}
