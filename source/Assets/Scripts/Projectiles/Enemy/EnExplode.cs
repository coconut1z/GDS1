using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnExplode : EnemyProjectile {
	
	public GameObject[] projectiles;

	private Vector2 endPosition;
	private Vector2 startPosition;
	private float originalDistance;

	// Use this for initialization
	protected override void Start () {
		base.Start ();
		speed = 1.45f;
		damage = 2;
		endPosition = player.position;
		startPosition = transform.position;
	}
	
	// Update is called once per frame
	protected override void Update () {
		base.Update ();
		if(Vector2.Distance(transform.position, endPosition) <= 0.1f){
			Invoke ("DestroySelf", 0.5f);
			speed = 0;
			endPosition = new Vector2 (100, 100);
		}
	}

	public override void ProjectileMovement ()
	{
		transform.Translate (transform.up * speed * Time.deltaTime, Space.World);
	}

	public override void PlayerCollision ()
	{
		try{
			if(Vector2.Distance(player.position, transform.position) < radius){
				player.GetComponent<PlayerController> ().TakeDamage (damage);
				DestroySelf();
			}
		}catch{
			Debug.Log("Enemy Bullet Player Transform has not been set! Did you forget?");
		}
	}

	private void DestroySelf(){
		Destroy (gameObject);
		if(Global.Difficulty == Global.RECRUIT){
			for(int i = 0; i < 15; i++){
				EnemyProjectile e = Instantiate (projectiles [0], transform.position, 
					Quaternion.Euler (0, 0, Random.Range(0,361))).GetComponent<EnemyProjectile>();
				e.Setup (player, Random.Range (1.45f, 3.28f), 1);
				e.SetRadius (0.1f);
			}
		}else if(Global.Difficulty == Global.VETEREN){
			for(int i = 0; i <= 20; i++){
				EnemyProjectile e = Instantiate (projectiles [0], transform.position, 
					Quaternion.Euler (0, 0, Random.Range(0,361))).GetComponent<EnemyProjectile>();
				e.Setup (player, Random.Range (2.18f, 3.63F), 1);
				e.SetRadius (0.1f);
				if(i%2==0){
					EnemyProjectile eM = Instantiate (projectiles [1], transform.position, 
						Quaternion.Euler (0, 0, Random.Range(0,361))).GetComponent<EnemyProjectile>();
					eM.Setup (player, Random.Range (2.18f, 3.63F), 1);
					eM.SetRadius (0.15f);
				}
			}
		}else if(Global.Difficulty == Global.BATTLEH){
			for(int i = 0; i <= 32; i++){
				EnemyProjectile e = Instantiate (projectiles [0], transform.position, 
					Quaternion.Euler (0, 0, Random.Range(0,361))).GetComponent<EnemyProjectile>();
				e.Setup (player, Random.Range (1.45f, 3.63F), 1);
				e.SetRadius (0.1f);
				if(i%2==0){
					EnemyProjectile eM = Instantiate (projectiles [1], transform.position, 
						Quaternion.Euler (0, 0, Random.Range(0,361))).GetComponent<EnemyProjectile>();
					eM.Setup (player,Random.Range (1.45f, 3.63F), 1);
					eM.SetRadius (0.15f);
				}
				if(i%4==0){
					EnemyProjectile eL = Instantiate (projectiles [2], transform.position, 
						Quaternion.Euler (0, 0, Random.Range(0,361))).GetComponent<EnemyProjectile>();
					eL.Setup (player, Random.Range (1.45f, 3.63F), 1);
					eL.SetRadius (0.2f);
				}
			}
		}
	}

}
