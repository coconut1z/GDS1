using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundExplosion : MonoBehaviour {
    private AudioManager audioManager;
    // Use this for initialization
    void Start () {
        audioManager = AudioManager.instance;
        if (audioManager == null) {
            Debug.LogError("No audio manager found in scene, whoops");
        }
        audioManager.PlaySound("ExplosionSmall1");
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
