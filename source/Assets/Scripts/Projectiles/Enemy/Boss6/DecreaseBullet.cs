using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecreaseBullet : EnemyProjectile {
	private float randSize;
	// Use this for initialization
	protected override void Start () {
		base.Start ();
		//speed = setSpeed/45f;
		//speed = setSpeed;
		//damage = 1;
		radius = 0.8f;
		randSize = Random.Range(0.2f, 0.6f);
	}

	// Update is called once per frame
	protected override void Update () {
		base.Update ();
		if(transform.localScale.x > randSize){
			transform.localScale = transform.localScale * (1 - Time.deltaTime * 0.5f);
			radius = 0.8f * transform.localScale.x;
		}
	}

	public override void ProjectileMovement ()
	{
		transform.Translate (transform.up * speed * Time.deltaTime, Space.World);
	}
}
