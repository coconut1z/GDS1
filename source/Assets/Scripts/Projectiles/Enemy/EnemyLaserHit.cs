using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLaserHit : MonoBehaviour {

	private Rigidbody2D rb;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
	}

	private void OnTriggerEnter2D(Collider2D c){
		if(c.tag == "Player"){
			c.GetComponent<PlayerController> ().TakeDamage (1);
		}
	}

}
