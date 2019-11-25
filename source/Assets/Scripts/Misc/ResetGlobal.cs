using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetGlobal : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Global.Difficulty = 3;
		Global.Stage = 1;
		Global.startAt2 = false;
		Global.startAt3 = false;
		Global.startAt4 = false;
		Global.startAt5 = false;
		Global.startAt6 = false;
		Global.bossMedley = false;


		Global.onWeapon = true;
		Global.specialBounds = 1;
		Global.stationsSpawned = false;

		Global.asteroidLevel = 1;
		Global.asteroidCancel = true;

		Global.tutorial = false;

		Global.final = false;
		Global.finalmusic = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
