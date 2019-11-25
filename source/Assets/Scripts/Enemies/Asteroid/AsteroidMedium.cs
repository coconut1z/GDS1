using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidMedium : Enemy {

	public GameObject small;

	// Use this for initialization
	void Start () {
		base.Start ();
		health = 99f;
		speed = Random.Range(1.5f,2.6f);
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
		health = 15f;
	}

	public override void Shoot ()
	{
		
	}

	public override void Death ()
	{
		DestroySelf ();
		for(int i = 0; i < 3; i++){
			Instantiate (small, transform.position, Quaternion.Euler (0, 0, Random.Range (0, 361)));
		}
	}
}
