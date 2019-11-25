using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Game object that shows the player where the blast will land when they fire the
 * satellite cannon
 */
public class SatelliteCannonAim : MonoBehaviour {
    private AudioManager audioManager;

    // Use this for initialization
    void Start () {
        Invoke("StopAim", 0.5f);
        audioManager = AudioManager.instance;
        if (audioManager == null) {
            Debug.LogError("No audio manager found in scene, whoops");
        }
        audioManager.PlaySound("LockOn1");
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void StopAim()
    {
        Destroy(gameObject);
    }
}
