using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour {

	public float speed;
	public float rotation;
	// Use this for initialization
	void Start () {
		rotation = 0;
	}
	
	// Update is called once per frame
	void Update () {
		rotation += speed * Time.deltaTime;
		transform.rotation = Quaternion.Euler (transform.rotation.x, transform.rotation.y, rotation);
	}
}
