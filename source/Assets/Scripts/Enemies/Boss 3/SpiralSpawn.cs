using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiralSpawn : Enemy {

	private bool startAngleShift;
	private float angleShiftAmount;
	private float alpha;

	// Use this for initialization
	protected override void Start () {
		base.Start ();
		health = 15;
		speed = 0;
		originalSpeed = speed;
		angleShiftAmount = 90;
		alpha = 0;
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
		if(Vector2.Distance(transform.position, Vector2.zero) > 3.8f && speed != 0){
			speed = 0;
			startAngleShift = true;
		}
		if(startAngleShift && angleShiftAmount > 0){
			float shift = 100 * Time.deltaTime;
			transform.rotation = Quaternion.Euler (0,0,transform.eulerAngles.z + shift);
			angleShiftAmount -= shift;
		}
		transform.RotateAround (Vector3.zero, Vector3.forward, 50 * Time.deltaTime);
	}

	public override void Shoot(){
		foreach(WeaponEnemy wep in weapons){
			wep.Shoot ();
		}
	}

	private void Initiate(){
		speed = 3;
		canShoot = true;
	}

	public override void Death(){
		Destroy (gameObject);
	}
}
