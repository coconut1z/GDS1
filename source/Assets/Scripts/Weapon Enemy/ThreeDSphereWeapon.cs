using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThreeDSphereWeapon : WeaponEnemy {

	private int step;
	private List<KeyValuePair<Transform,CurveTimeDelete>> bullets;

	// Use this for initialization
	protected override void Start () {
		base.Start ();
		bullets = new List<KeyValuePair<Transform,CurveTimeDelete>> ();
		if(difficulty == Global.RECRUIT){
			step = 72;
			shootDelay = 1.5f;
		}else if(difficulty == Global.VETEREN){
			step = 72;
			shootDelay = 1f;
		}else if(difficulty == Global.BATTLEH){
			step = 45;
			shootDelay = 1f;
		}


	}

	// Update is called once per frame
	protected override void Update () {
		base.Update ();
		//lookAtPlayer ();
		for(int i = 0; i < bullets.Count; i++){
			if(bullets[i].Key == null){
				bullets.RemoveAt (i);
				i--;
			}
		}
		for(int i = 0; i < bullets.Count; i++){
			float distance = Vector2.Distance(bullets[i].Key.position, transform.position);
			bullets [i].Key.localScale = new Vector3 (
				0.035f * distance/2.5f, 
				0.035f * distance/2.5f,
				0.035f);
			bullets[i].Value.SetRadius(0.1f*distance/4);
		}
	}

	public override void Shoot(){
		if(shootTime > shootDelay){
			
			for(int i = 0; i < 360/step; i++){
				CurveTimeDelete e = Instantiate (projectiles [0], shootPos [0].position, 
					Quaternion.Euler (0, 0, transform.eulerAngles.z + i*step)).
					GetComponent<CurveTimeDelete> ();
				e.setCurveSpeed = 35;
				e.Setup (player, 2, 1);
				e.SetRadius (0.1f);
				bullets.Add (new KeyValuePair<Transform, CurveTimeDelete>(e.transform, e));
			}
			shootTime = 0;
		}
	}
}
