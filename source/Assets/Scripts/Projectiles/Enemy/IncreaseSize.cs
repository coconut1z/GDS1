using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseSize : EnemyProjectile {

	// Use this for initialization
	protected override void Start () {
		base.Start ();
		//speed = setSpeed/45f;
		//speed = setSpeed;
		//damage = 1;
	}

	// Update is called once per frame
	protected override void Update () {
		base.Update ();
		if(radius != 0){
			transform.localScale = new Vector3 (
				transform.localScale.x + 0.1f * Time.deltaTime,
				transform.localScale.y + 0.1f * Time.deltaTime,
				transform.localScale.z + 0.1f * Time.deltaTime);
			radius += 0.1f * Time.deltaTime;
		}
	}

	public override void ProjectileMovement ()
	{
		transform.Translate (transform.up * speed * Time.deltaTime, Space.World);
	}
}
