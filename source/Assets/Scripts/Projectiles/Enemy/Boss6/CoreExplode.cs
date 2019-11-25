using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreExplode : EnemyProjectile {
	
	public GameObject[] projectiles;

	// Use this for initialization
	protected override void Start () {
		base.Start ();
		radius = 0.4f;
		damage = 1;
		Invoke ("DestroySelf", 1.75f);
	}
	
	// Update is called once per frame
	protected override void Update () {
		base.Update ();
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
				EnemyProjectile e = Instantiate (projectiles [Random.Range(0,projectiles.Length)], transform.position, 
					Quaternion.Euler (0, 0, Random.Range(0,361))).GetComponent<EnemyProjectile>();
				e.Setup (player, Random.Range (1.45f, 3.28f), 1);
				e.SetRadius (0.1f);
			}
		}else if(Global.Difficulty == Global.VETEREN){
			for(int i = 0; i <= 20; i++){
				EnemyProjectile e = Instantiate (projectiles [Random.Range(0,projectiles.Length)], transform.position, 
					Quaternion.Euler (0, 0, Random.Range(0,361))).GetComponent<EnemyProjectile>();
				e.Setup (player, Random.Range (2.18f, 3.63F), 1);
				e.SetRadius (0.1f);
			}
		}else if(Global.Difficulty == Global.BATTLEH){
			for(int i = 0; i <= 32; i++){
				EnemyProjectile e = Instantiate (projectiles [Random.Range(0,projectiles.Length)], transform.position, 
					Quaternion.Euler (0, 0, Random.Range(0,361))).GetComponent<EnemyProjectile>();
				e.Setup (player, Random.Range (1.45f, 3.63F), 1);
				e.SetRadius (0.1f);
			}
		}
	}

}
