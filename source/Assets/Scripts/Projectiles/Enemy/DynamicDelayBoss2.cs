using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicDelayBoss2 : EnemyProjectile {
	
	private Vector2 lookToWorld;
	private Vector2 endPointLocal;
	private Vector2 endPointPlayer;
	private float delay;
	private bool canStartCount;

	// Use this for initialization
	protected override void Start () {
		base.Start ();

		if(transform.position != Vector3.zero){
			lookToWorld = new Vector2 (6f - Random.value * 12, Random.Range(25,45)/10f);
			endPointLocal = lookToWorld;
			//transform.localPosition = Vector2.zero;
			float AngleRad = Mathf.Atan2 (lookToWorld.y - transform.position.y, lookToWorld.x - transform.position.x);
			float AngleToDeg = (180 / Mathf.PI) * AngleRad - 90;
			transform.rotation = Quaternion.Euler (0, 0, AngleToDeg);
		}
		delay = 0;
		speed = 6.35f;
		damage = 1;
		canStartCount = false;
	}
	
	// Update is called once per frame
	protected override void Update () {
		base.Update ();
		if(lookToWorld != Vector2.zero && !canStartCount){
			//if(Mathf.Abs(transform.position.x) >= Mathf.Abs(endPointLocal.x) && Mathf.Abs(transform.position.y) >= Mathf.Abs(endPointLocal.y)){
			if(Vector2.Distance(transform.position, endPointLocal) < 0.1f){
				transform.position = endPointLocal;
				speed = 0;
				delay = 2;
				canStartCount = true;
			}
		}
		if(delay > 0 && canStartCount){
			delay -= Time.deltaTime;
		}else if(canStartCount && delay <= 0){
			endPointPlayer = new Vector2 (player.position.x + 2 - Random.value*4, player.position.y + 2 - Random.value*4);
			float AngleRad = Mathf.Atan2 (endPointPlayer.y - transform.position.y, endPointPlayer.x - transform.position.x);
			float AngleToDeg = (180 / Mathf.PI) * AngleRad - 90;
			transform.rotation = Quaternion.Euler (0, 0, AngleToDeg);
			delay = 99;
			speed = Random.Range (2.2f, 4f);
		}
	}

	public override void ProjectileMovement ()
	{
		transform.Translate(transform.up * speed * Time.deltaTime, Space.World);
	}
}
