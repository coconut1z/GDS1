using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialSpawner : MonoBehaviour {

	public GameObject g;
	private float spawnTime;

	// Use this for initialization
	void Start () {
		Global.Difficulty = 1;
		spawnTime = 1;
	}
	
	// Update is called once per frame
	void Update () {
		if(spawnTime <= 0){
			GameObject r = Instantiate (g) as GameObject;
			r.transform.position = transform.position;
			r.transform.rotation = Quaternion.Euler (0, 0, 180);
			spawnTime = 1;
		}
		spawnTime -= Time.deltaTime;
	}
}
