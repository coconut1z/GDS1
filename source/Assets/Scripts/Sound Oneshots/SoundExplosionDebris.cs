using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundExplosionDebris : MonoBehaviour {
    // Use this for initialization
    private AudioManager audioManager;

    void Start() {
        audioManager = AudioManager.instance;
        if (audioManager == null) {
            Debug.LogError("No audio manager found in scene, whoops");
        }
        //audioManager.PlaySound("MetalDebris1");
    }
}
