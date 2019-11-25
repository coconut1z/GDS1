using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserSwitcher : MonoBehaviour {

	public GameObject laserSprite;
	public GameObject actualLaser;
	public float timer;

	// Use this for initialization
	void Start () {
		Invoke ("Activate", timer);
		Invoke ("Deactivate", timer + 0.4f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void Activate(){
		actualLaser.SetActive (true);
	}

	private void Deactivate(){
		laserSprite.SetActive (false);
	}

}
