using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteComponentTimer : MonoBehaviour {

	public Rigidbody2D rb;
	public float timer;
	// Use this for initialization
	void Start () {
		Destroy(rb, timer);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
