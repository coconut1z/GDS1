using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage6LaserProng : MonoBehaviour {

	private bool inPosition;
	public Transform shootPos;
    public GameObject deathray;
    public GameObject initialDeathray;

    // Use this for initialization
    void Start () {
		inPosition = false;
	}
	
	// Update is called once per frame
	void Update () {
		
		if(!inPosition && GetComponent<ReturnToOriginalPosition>().timer >= 1){
			inPosition = true;
		}

	}

	public void ReturnToDefault(){
		
	}

	// Laser that breaks screen
	public void BreakLaser(){ //reactives deathray particles object which should start the deathray particle sequence (including activating collider at right time).
        Invoke("ActualLaserActivate", 0.01f);
        
	}

    private void ActualLaserActivate() {
        initialDeathray.SetActive(true);
    }

	// Actual laser shot for final attack
	public void Shoot(){
		//Instantiate (laser, shootPos.position, Quaternion.Euler (0, 0, transform.eulerAngles.z + 180));
        Instantiate(deathray, shootPos.position, Quaternion.Euler(0, 0, transform.eulerAngles.z));
	}

}
