using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinXY : MonoBehaviour {

	public float speed;
	public float rotation;
	public bool x;
	public bool y;
	// Use this for initialization
	void Start () {
		rotation = 0;
	}
	
	// Update is called once per frame
	void Update () {
		rotation += speed * Time.deltaTime;
		if(x){
			transform.rotation = Quaternion.Euler (rotation, transform.rotation.y, transform.rotation.z);
		}
		if(y){
			transform.rotation = Quaternion.Euler (transform.rotation.x, rotation, transform.rotation.z);
		}

	}
}
