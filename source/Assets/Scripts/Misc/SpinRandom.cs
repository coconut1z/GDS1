using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinRandom : MonoBehaviour {

	public float speedStart;
	public float speedEnd;
	public float rotation;

	private float speed;

	// Use this for initialization
	void Start () {
		rotation = 0;
		speed = Random.Range (speedStart, speedEnd);
	}
	
	// Update is called once per frame
	void Update () {
		rotation += speed * Time.deltaTime;
		transform.rotation = Quaternion.Euler (transform.rotation.x, transform.rotation.y, rotation);
	}
}
