﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRedBulletDynamic : EnemyProjectile {

	private Vector2 lookToWorld;
	private Vector2 endPointLocal;

	// Use this for initialization
	protected override void Start () {
		base.Start ();

		if(transform.position != Vector3.zero){
			lookToWorld = transform.position;
			endPointLocal = transform.localPosition;
			transform.localPosition = Vector2.zero;
			float AngleRad = Mathf.Atan2 (lookToWorld.y - transform.position.y, lookToWorld.x - transform.position.x);
			float AngleToDeg = (180 / Mathf.PI) * AngleRad - 90;
			transform.rotation = Quaternion.Euler (0, 0, AngleToDeg);
		}
	}
	
	// Update is called once per frame
	protected override void Update () {
		base.Update ();
		if(lookToWorld != Vector2.zero){
			if(Mathf.Abs(transform.localPosition.x) >= Mathf.Abs(endPointLocal.x) && Mathf.Abs(transform.localPosition.y) >= Mathf.Abs(endPointLocal.y)){
				speed = 0;
			}
		}
	}

	public override void ProjectileMovement ()
	{
		transform.Translate(transform.up * speed * Time.deltaTime, Space.World);
	}
}
