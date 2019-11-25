using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurveTimeDelete : EnemyProjectile {

	//public float setSpeed;
	public float setCurveSpeed;

	// Use this for initialization
	protected override void Start () {
		base.Start ();
		//speed = setSpeed;
		damage = 1;
		radius = 0.1f;
		Invoke ("DestroySelf", 10f);
	}

	// Update is called once per frame
	protected override void Update () {
		base.Update ();
		/*
		if(rb.rotation > 180){
			rb.rotation = 180;
		}else if(rb.rotation < -180){
			rb.rotation = -180;
		}
		*/
	}

	//protected override void FixedUpdate(){
	//	base.FixedUpdate ();
	//	rb.MoveRotation (rb.rotation + setCurveSpeed * Time.deltaTime);
	//}

	public override void ProjectileMovement ()
	{
		transform.Translate (transform.up * speed * Time.deltaTime, Space.World);
		transform.rotation = Quaternion.Euler(0,0,transform.eulerAngles.z + setCurveSpeed * Time.deltaTime);
	}

	private void DestroySelf(){
		Destroy (gameObject);
	}

	protected override void DestroyOutOfBounds ()
	{
		
	}
}
