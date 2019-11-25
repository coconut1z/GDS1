using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage6Connecter : MonoBehaviour {

	public Transform[] spawnPosition;
	public GameObject[] spawnPrefab;
	public bool leftWing;

	private bool spin;
	private Transform player;
	private bool returnToDefault;
	private float rotationSpeed;
	private bool inPosition;
	private float shootTime, shootDelay;
	private int shootAmount;

	// Use this for initialization
	void Start () {
		spin = false;
		returnToDefault = false;
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		inPosition = false;
		shootTime = 0;
		shootAmount = 0;

		if(Global.Difficulty == Global.RECRUIT){
			rotationSpeed = -200f;
			shootDelay = 0.05f;
			shootAmount = 1;
		}else if(Global.Difficulty == Global.VETEREN){
			rotationSpeed = -200f;
			shootDelay = 0.05f;
			shootAmount = 2;
		}else if(Global.Difficulty == Global.BATTLEH){
			rotationSpeed = -200f;
			shootDelay = 0.05f;
			shootAmount = 3;
		}

	}
	
	// Update is called once per frame
	void Update () {
		
		if(!inPosition && GetComponent<ReturnToOriginalPosition>().timer >= 1){
			inPosition = true;
			spin = true;
			GetComponentInParent<Stage6Boss> ().StartSpikePhase ();
		}

		if(spin){
			transform.RotateAround (transform.parent.localPosition, Vector3.forward, rotationSpeed * Time.deltaTime);
		}

		if (shootTime >= shootDelay && spin) {
			for(int i = 0; i < shootAmount; i++){
				if(leftWing){
					EnemyProjectile e = Instantiate (spawnPrefab[Random.Range(0,3)], spawnPosition[i].position, 
						Quaternion.Euler (0, 0, transform.localEulerAngles.z + 90 + Random.Range(-15,15))).GetComponent<EnemyProjectile>();
					e.Setup (player, Random.Range(1.4f, 1.7f), 1);
					e.SetRadius (0.28f);
				}else{
					EnemyProjectile e = Instantiate (spawnPrefab[Random.Range(0,3)], spawnPosition[i].position, 
						Quaternion.Euler (0, 0, transform.localEulerAngles.z - 90 + Random.Range(-15,15))).GetComponent<EnemyProjectile>();
					e.Setup (player, Random.Range(1.4f, 1.7f), 1);
					e.SetRadius (0.28f);
				}
			}
			shootTime = 0;
		}else{
			shootTime += Time.deltaTime;
		}
			
		if(returnToDefault){
			if(Mathf.Abs(transform.localEulerAngles.z) < 3){
				transform.localRotation = Quaternion.Euler (0, 0, 0);
				spin = false;
				returnToDefault = false;
				transform.localPosition = new Vector3 (transform.localPosition.x, 0, transform.localPosition.z);
				//transform.GetChild (0).gameObject.SetActive (false);
			}
		}
	}

	public void SetSpin(bool b){
		spin = b;
	}

	public void ReturnToDefault(){
		returnToDefault = true;
		shootDelay = 999999;
	}
}
