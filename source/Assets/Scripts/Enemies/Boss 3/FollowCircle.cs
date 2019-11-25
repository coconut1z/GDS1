using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCircle : Enemy {

	float setSpeed;
	Stage3BossPart part;
	private float alpha;
	private Transform player;
	private bool startFollow;
	public GameObject deathBullet;

	// Use this for initialization
	protected override void Start () {
		base.Start ();
		if(difficulty == Global.RECRUIT){
			
		}else if(difficulty == Global.VETEREN){
			
		}else if(difficulty == Global.BATTLEH){
			
		}
		startFollow = false;
		alpha = 0;
		speed = 0;
		health = 1;
		originalSpeed = speed;
		Invoke ("Initiate", 1f);
		Invoke ("Death", 7f);
		player = GameObject.FindGameObjectWithTag ("Player").transform;
	}
	
	// Update is called once per frame
	protected override void Update () {
		base.Update ();
		alpha += Time.deltaTime;
		if(alpha >= 1){
			alpha = 1;
		}
		sr.color = new Color (sr.color.r, sr.color.g, sr.color.b, alpha);
		if(startFollow){
			Vector3 direction = player.position - transform.position;
			float angle = Mathf.Atan2 (direction.y, direction.x) * Mathf.Rad2Deg - 90;
			Quaternion lookRotation = Quaternion.AngleAxis (angle, Vector3.forward);
			transform.rotation = Quaternion.Slerp (transform.rotation, lookRotation, Time.deltaTime * 2);
		}
	}

	public override void Shoot(){
		foreach(WeaponEnemy wep in weapons){
			wep.Shoot ();
		}
	}

	public override void Death(){
		EnemyProjectile e = Instantiate (deathBullet, transform.position, 
			    Quaternion.Euler (0, 0, Random.Range (0, 360)))
				.GetComponent<EnemyProjectile>();
			e.Setup (player, 1f, 1);
			e.SetRadius (0.15f);
		Destroy (gameObject);
	}

	private void Initiate(){
		speed = 4;
		canShoot = true;
		startFollow = true;
	}

	public override void OnTriggerExit2D (Collider2D col)
	{
		
	}
}
