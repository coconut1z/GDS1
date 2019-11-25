using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowSpawn : Enemy {

	float setSpeed;
	Stage3BossPart part;
	private float alpha;
	private Transform player;
	private bool startFollow;

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
		health = 3;
		originalSpeed = speed;
		Invoke ("Initiate", 1f);
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
			transform.rotation = Quaternion.Slerp (transform.rotation, lookRotation, Time.deltaTime );
		}
	}

	public override void Shoot(){
		foreach(WeaponEnemy wep in weapons){
			wep.Shoot ();
		}
	}

	public override void Death(){
		Destroy (gameObject);
	}

	private void Initiate(){
		speed = 3;
		canShoot = true;
		startFollow = true;
	}
}
