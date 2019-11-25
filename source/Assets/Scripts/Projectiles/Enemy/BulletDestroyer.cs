using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDestroyer : EnemyProjectile {

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
		/*
		GameObject[] b = GameObject.FindGameObjectsWithTag ("PlayerBullet");
		for(int i = 0; i < b.Length; i++){
			if(Vector2.Distance(b[i].transform.position, transform.position) < radius){
				Destroy (b[i]);
			}
		}
		*/
	}

	public override void ProjectileMovement ()
	{
		transform.Translate (transform.up * speed * Time.deltaTime, Space.World);
	}


}
