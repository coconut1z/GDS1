using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage6Upper : MonoBehaviour {

	public Transform spawnPosition;
	public GameObject spawnPrefab;

	private bool particleStarted;
	private bool spin;
	private bool laser;
	private float laserTimer, laserDelay;
	private Transform player;
	private bool returnToDefault;
	private float rotationSpeed;

	// Use this for initialization
	void Start () {
		particleStarted = false;
		spin = false;
		laser = false;
		returnToDefault = false;
		laserTimer = 0;
		player = GameObject.FindGameObjectWithTag ("Player").transform;

		if(Global.Difficulty == Global.RECRUIT){
			rotationSpeed = -25f;
			laserDelay = 0.15f;
		}else if(Global.Difficulty == Global.VETEREN){
			rotationSpeed = -25f;
			laserDelay = 0.1f;
		}else if(Global.Difficulty == Global.BATTLEH){
			rotationSpeed = -25f;
			laserDelay = 0.1f;
		}

	}
	
	// Update is called once per frame
	void Update () {
		if(!particleStarted && GetComponent<ReturnToOriginalPosition>().timer >= 1){
			particleStarted = true;
			transform.GetChild (0).gameObject.SetActive (true);
			spin = true;
			SetLaser (true);
		}
		if(spin){
			transform.RotateAround (transform.parent.localPosition, Vector3.forward, rotationSpeed * Time.deltaTime);
		}
		if(laser){
			laserTimer += Time.deltaTime;
			if(laserTimer >= laserDelay){
				EnemyProjectile e = Instantiate (spawnPrefab, spawnPosition.position, 
					Quaternion.Euler (0, 0, transform.eulerAngles.z)).GetComponent<EnemyProjectile>();
				e.transform.parent = transform;
				e.Setup (player);
				laserTimer = 0;
			}
		}
		if(returnToDefault){
			SetLaser (false);
			if(Mathf.Abs(transform.localEulerAngles.z) < 3){
				transform.localRotation = Quaternion.Euler (0, 0, 0);
				spin = false;
				transform.GetChild (0).gameObject.SetActive (false);
				returnToDefault = false;
				transform.localPosition = new Vector3 (0, transform.localPosition.y, transform.localPosition.z);
			}
		}
	}

	public void SetSpin(bool b){
		spin = b;
	}

	public void SetLaser(bool b){
		transform.GetChild (0).gameObject.SetActive (b);
		if(b){
			Invoke ("DelayShoot", 0.5f);
		}else{
			laser = false;
		}
	}

	public void DelayShoot(){
		laser = true;
	}

	public void ReturnToDefault(){
		returnToDefault = true;
		SetLaser (false);
	}
}
