using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacePlayer : MonoBehaviour {
	Transform player;
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {
		float AngleRadToMouse = Mathf.Atan2 (
			player.position.y - transform.position.y,
			player.position.x - transform.position.x
		);
		float AngleToDeg = (180 / Mathf.PI) * AngleRadToMouse - 90;
		transform.rotation = Quaternion.Euler (0, 0, AngleToDeg);
	}
}
