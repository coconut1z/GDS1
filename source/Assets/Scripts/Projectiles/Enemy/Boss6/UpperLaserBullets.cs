using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpperLaserBullets : EnemyProjectile {
	
	public GameObject[] projectiles;

	private float originalDistance;

	// Use this for initialization
	protected override void Start () {
		base.Start ();
		speed = 5f;
		damage = 0;
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
		
	}

	private void DestroySelf(){
		Destroy (gameObject);
		if(Global.Difficulty == Global.RECRUIT){
			for(int i = 0; i <= Random.Range(0,2); i++){
				EnemyProjectile e = Instantiate (projectiles [Random.Range(0,3)], transform.position + (transform.up * -0.3f), 
					Quaternion.Euler (0, 0, (180 + transform.eulerAngles.z) + 90 - Random.Range(0,181))).GetComponent<EnemyProjectile>();
				e.Setup (player, Random.Range (1.45f, 3.63F), 1);
				e.SetRadius (0.1f);
			}
		}else if(Global.Difficulty == Global.VETEREN){
			for(int i = 0; i <= 2; i++){
				EnemyProjectile e = Instantiate (projectiles [Random.Range(0,3)], transform.position + (transform.up * -0.3f), 
					Quaternion.Euler (0, 0, (180 + transform.eulerAngles.z) + 90 - Random.Range(0,181))).GetComponent<EnemyProjectile>();
				e.Setup (player, Random.Range (1.45f, 3.63F), 1);
				e.SetRadius (0.1f);
			}
		}else if(Global.Difficulty == Global.BATTLEH){
			for(int i = 0; i <= 5; i++){
				EnemyProjectile e = Instantiate (projectiles [Random.Range(0,3)], transform.position + (transform.up * -0.3f), 
					Quaternion.Euler (0, 0, (180 + transform.eulerAngles.z) + 90 - Random.Range(0,181))).GetComponent<EnemyProjectile>();
				e.Setup (player, Random.Range (1.45f, 3.63F), 1);
				e.SetRadius (0.1f);
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
