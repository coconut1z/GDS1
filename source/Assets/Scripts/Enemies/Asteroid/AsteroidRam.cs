using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidRam : Enemy {

	// Use this for initialization
	void Start () {
		base.Start ();
		health = 99f;
		speed = Random.Range(4,6f);
		Invoke ("Vulnerable", 0.1f);
		originalSpeed = speed;
		sr = GetComponentInChildren<SpriteRenderer>();
		originalColor = sr.color;
	}
	
	// Update is called once per frame
	void Update () {
		base.Update ();
	}

	public void Vulnerable(){
		health = 4f;
	}

	public override void Shoot ()
	{
		
	}

	public override void Death ()
	{
		DestroySelf ();
	}
}
