using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage6Lower : MonoBehaviour {

	//public Transform spawnPosition;
	//public GameObject spawnPrefab;

	private bool particleStarted;
	private bool spin;
	private Transform player;
	private bool returnToDefault;
	private float rotationSpeed;

	// Use this for initialization
	void Start () {
		particleStarted = false;
		spin = false;
		returnToDefault = false;
		player = GameObject.FindGameObjectWithTag ("Player").transform;

		if(Global.Difficulty == Global.RECRUIT){
			rotationSpeed = 25f;
		}else if(Global.Difficulty == Global.VETEREN){
			rotationSpeed = 30f;
		}else if(Global.Difficulty == Global.BATTLEH){
			rotationSpeed = 30f;
		}

	}
	
	// Update is called once per frame
	void Update () {
		if(!particleStarted && GetComponent<ReturnToOriginalPosition>().timer >= 1){
			particleStarted = true;
			transform.GetChild (0).gameObject.SetActive (true);
			spin = true;
			//Transform e = Instantiate (spawnPrefab, spawnPosition.position,
			//Quaternion.Euler (0, 0, 180)).transform;
			transform.GetChild(1).gameObject.SetActive(true);
			EnemyRedBulletDynamicNoDelete[] eb = transform.GetChild(1).GetComponentsInChildren<EnemyRedBulletDynamicNoDelete> ();
			if(Global.Difficulty == Global.RECRUIT){
				transform.GetChild (1).localScale = new Vector3 (2f, 2f, 1);
			}else if(Global.Difficulty == Global.VETEREN){
				transform.GetChild (1).localScale = new Vector3 (1.7f, 1.7f, 1);
			}
			for(int i = 0; i < eb.Length; i++){
				if(Global.Difficulty != Global.BATTLEH){
					if(i == 288){
						for(int j = 288; j < eb.Length; j++){
							Destroy (eb [j].gameObject);
						}
						i = 999;
					}
				}
				eb [i].Setup (player, 2f, 1f);
				eb [i].SetRadius (0.13f);
			}
			//e.parent = transform;
		}
		if(spin){
			transform.RotateAround (transform.parent.localPosition, Vector3.forward, rotationSpeed * Time.deltaTime);
		}
		if(returnToDefault){
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

	public void ReturnToDefault(){
		returnToDefault = true;
		Destroy (transform.GetChild (1).gameObject);
	}
}
