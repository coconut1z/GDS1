using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicSpawn : Enemy {

	float setSpeed;
	private float alpha;

	// Use this for initialization
	protected override void Start () {
		base.Start ();
		if(difficulty == Global.RECRUIT){
			
		}else if(difficulty == Global.VETEREN){
			
		}else if(difficulty == Global.BATTLEH){
			
		}
		canShoot = false;
		alpha = 0;
		speed = 0;
		health = 3;
		originalSpeed = speed;
		Invoke ("Initiate", 1f);
	}
	
	// Update is called once per frame
	protected override void Update () {
		base.Update ();
		alpha += Time.deltaTime;
		if(alpha >= 1){
			alpha = 1;
		}
		sr.color = new Color (sr.color.r, sr.color.g, sr.color.b, alpha);
	}

	protected override void ShootOnEnter (Collider2D col)
	{
		
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
	}
}
