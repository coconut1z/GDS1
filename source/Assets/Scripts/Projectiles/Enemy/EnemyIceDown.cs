using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIceDown : EnemyProjectile {

	public Transform hit1;
	public Transform hit2;
	private bool drop;
	private float accel;
	// Use this for initialization
	protected override void Start () {
		base.Start ();
		//speed = setSpeed/45f;
		//speed = setSpeed;
		//damage = 1;
		damage = 1;
		Invoke ("DestroySelf", 10f);
		drop = false;
		accel = 1;
	}

	// Update is called once per frame
	protected override void Update () {
		base.Update ();
		if(transform.position.y > 5 && ! drop){
			drop = true;
			transform.position = new Vector3 (Random.Range (-6.9f, 6.9f), transform.position.y, transform.position.z);
		}
	}

	public override void ProjectileMovement ()
	{
		if(!drop){
			transform.Translate (transform.up * speed * Time.deltaTime, Space.World);
		}else{
			transform.rotation = Quaternion.Euler (0, 0, 180);
			transform.Translate (transform.up * speed * accel * Time.deltaTime, Space.World);
		}
		accel += Time.deltaTime;
	}

	public override void PlayerCollision (){
		try{
			if(Vector2.Distance(player.position, hit1.position) < 0.09f){
				player.GetComponent<PlayerController> ().TakeDamage (damage);
				Destroy (gameObject);
			}else if(Vector2.Distance(player.position, hit2.position) < 0.07f){
				player.GetComponent<PlayerController> ().TakeDamage (damage);
				Destroy (gameObject);
			}
		}catch{
			Debug.Log("Enemy Bullet Player Transform has not been set! Did you forget?");
		}

	}


	private void DestroySelf(){
		Destroy (gameObject);
	}

	protected override void DestroyOutOfBounds(){
		
	}
}
