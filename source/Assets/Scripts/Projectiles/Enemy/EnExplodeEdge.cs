using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnExplodeEdge : EnemyProjectile {
	
	public GameObject[] projectiles;

	private float originalDistance;

	// Use this for initialization
	protected override void Start () {
		base.Start ();
		speed = 2f;
		damage = 1;
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
				Destroy (gameObject);
			}
		}catch{
			Debug.Log("Enemy Bullet Player Transform has not been set! Did you forget?");
		}
	}

	private void DestroySelf(){
		Destroy (gameObject);
		if(Global.Difficulty == Global.RECRUIT){
			for(int i = 0; i <= 4; i++){
				EnemyProjectile e = Instantiate (projectiles [0], transform.position, 
					Quaternion.Euler (0, 0, (180 + transform.eulerAngles.z) + 90 - Random.Range(0,181))).GetComponent<EnemyProjectile>();
				e.Setup (player, Random.Range (1.45f, 3.63F), 1);
				e.SetRadius (0.1f);
				if(i%2==0){
					EnemyProjectile eM = Instantiate (projectiles [1], transform.position, 
						Quaternion.Euler (0, 0, (180 + transform.eulerAngles.z) + 90 - Random.Range(0,181))).GetComponent<EnemyProjectile>();
					eM.Setup (player,Random.Range (1.45f, 3.63F), 1);
					eM.SetRadius (0.15f);
				}
				if(i%4==0){
					EnemyProjectile eL = Instantiate (projectiles [2], transform.position, 
						Quaternion.Euler (0, 0, (180 + transform.eulerAngles.z) + 90 - Random.Range(0,181))).GetComponent<EnemyProjectile>();
					eL.Setup (player, Random.Range (1.45f, 3.63F), 1);
					eL.SetRadius (0.2f);
				}
			}
		}else if(Global.Difficulty == Global.VETEREN){
			for(int i = 0; i <= 12; i++){
				EnemyProjectile e = Instantiate (projectiles [0], transform.position, 
					Quaternion.Euler (0, 0, (180 + transform.eulerAngles.z) + 90 - Random.Range(0,181))).GetComponent<EnemyProjectile>();
				e.Setup (player, Random.Range (1.45f, 3.63F), 1);
				e.SetRadius (0.1f);
				if(i%2==0){
					EnemyProjectile eM = Instantiate (projectiles [1], transform.position, 
						Quaternion.Euler (0, 0, (180 + transform.eulerAngles.z) + 90 - Random.Range(0,181))).GetComponent<EnemyProjectile>();
					eM.Setup (player,Random.Range (1.45f, 3.63F), 1);
					eM.SetRadius (0.15f);
				}
				if(i%4==0){
					EnemyProjectile eL = Instantiate (projectiles [2], transform.position, 
						Quaternion.Euler (0, 0,(180 + transform.eulerAngles.z) + 90 - Random.Range(0,181))).GetComponent<EnemyProjectile>();
					eL.Setup (player, Random.Range (1.45f, 3.63F), 1);
					eL.SetRadius (0.2f);
				}
			}
		}else if(Global.Difficulty == Global.BATTLEH){
			for(int i = 0; i <= 12; i++){
				EnemyProjectile e = Instantiate (projectiles [0], transform.position, 
					Quaternion.Euler (0, 0, (180 + transform.eulerAngles.z) + 90 - Random.Range(0,181))).GetComponent<EnemyProjectile>();
				e.Setup (player, Random.Range (1.45f, 3.63F), 1);
				e.SetRadius (0.1f);
				if(i%2==0){
					EnemyProjectile eM = Instantiate (projectiles [1], transform.position, 
						Quaternion.Euler (0, 0, (180 + transform.eulerAngles.z) + 90 - Random.Range(0,181))).GetComponent<EnemyProjectile>();
					eM.Setup (player,Random.Range (1.45f, 3.63F), 1);
					eM.SetRadius (0.15f);
				}
				if(i%4==0){
					EnemyProjectile eL = Instantiate (projectiles [2], transform.position, 
						Quaternion.Euler (0, 0, (180 + transform.eulerAngles.z) + 90 - Random.Range(0,181))).GetComponent<EnemyProjectile>();
					eL.Setup (player, Random.Range (1.45f, 3.63F), 1);
					eL.SetRadius (0.2f);
				}
			}
		}
	}

	protected override void DestroyOutOfBounds ()
	{
		if(transform.position.x > boundX2 || transform.position.x < boundX1 ||
			transform.position.y > boundY2 || transform.position.y < boundY1){
			DestroySelf ();
		}
	}

}
