using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidLarge : Enemy {

	public GameObject medium;

	// Use this for initialization
	void Start () {
		base.Start ();
		health = 99f;
		speed = Random.Range(1.3f,2f);
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
		health = 30f;
	}

	public override void Shoot ()
	{
		
	}

	public override void Death ()
	{
		DestroySelf ();
		for(int i = 0; i < 3; i++){
			Instantiate (medium, transform.position, Quaternion.Euler (0, 0, Random.Range (0, 361)));
		}
	}
}
