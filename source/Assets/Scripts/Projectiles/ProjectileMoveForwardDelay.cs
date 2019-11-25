using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMoveForwardDelay : MonoBehaviour {

	public float speed;
	public float delay;
	private Projectile[] cp;
	private Rigidbody2D rb;

	// Use this for initialization
	void Start () {
		//cp = GetComponentsInChildren<Projectile> ();
		//rb = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		if(delay <= 0){
			transform.Translate (transform.up * speed * Time.deltaTime, Space.World);
			//for(int i = 0; i < cp.Length; i++){
				//rb.velocity = transform.up * speed * Time.deltaTime;
				//cp [i].velocityDisplacement = rb.velocity;
			//}	
		}else{
			delay -= Time.deltaTime;
		}
	}
}
