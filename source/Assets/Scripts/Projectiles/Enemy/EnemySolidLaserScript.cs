using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySolidLaserScript : EnemyProjectile {

	private CapsuleCollider2D c;

	// Use this for initialization
	protected override void Start () {
		base.Start ();
		c = GetComponent<CapsuleCollider2D> ();
		speed = 8f;
	}

	// Update is called once per frame
	protected override void Update () {
		base.Update ();
		if(transform.localScale.y < 0.3f){
			transform.localScale = new Vector3
				(transform.localScale.x, transform.localScale.y + Time.deltaTime * 1.5f, transform.localScale.z);
			if(transform.localScale.y > 0.3f){
				transform.localScale = new Vector3
					(transform.localScale.x, 0.3f, transform.localScale.z);
			}
		}

	}

	public override void ProjectileMovement ()
	{
		transform.Translate (transform.up * speed * Time.deltaTime, Space.World);
	}

	public override void PlayerCollision ()
	{
		
	}

}
