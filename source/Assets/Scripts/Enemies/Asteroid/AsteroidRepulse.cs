using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidRepulse : Enemy {

	private Transform player;
	private float step;

	// Use this for initialization
	void Start () {
		base.Start ();
		health = 99f;
		speed = Random.Range(1.5f,2.6f);
		Invoke ("Vulnerable", 0.1f);
		originalSpeed = speed;
		sr = GetComponentInChildren<SpriteRenderer>();
		originalColor = sr.color;
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		step = 0;
	}
	
	// Update is called once per frame
	void Update () {
		step = -Time.deltaTime;
		base.Update ();
		if (transform.position.x < 6.6f && transform.position.x > -6 - 6f && transform.position.y < 4.4f && transform.position.y > -4.4f) {
			if(player.position.x < 6.6f && player.position.x > -6 - 6f && player.position.y < 4.4f && player.position.y > -4.4f){
				if(Vector2.Distance(player.position, transform.position) < 4f){
					player.position = Vector2.MoveTowards (player.position, transform.position, step);
				}
			}
		}

	}

	public void Vulnerable(){
		health = 15f;
	}

	public override void Shoot ()
	{
		
	}

	public override void Death ()
	{
		DestroySelf ();
	}
}
