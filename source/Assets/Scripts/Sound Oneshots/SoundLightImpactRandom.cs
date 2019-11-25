using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundLightImpactRandom : MonoBehaviour {
    private AudioManager audioManager;

    void Start() {
        string randomLI = "LI";
        int rng = UnityEngine.Random.Range(1, 9); //min inclusive, max exclusive.
        randomLI = "randomLI" + rng.ToString();
        //Debug.Log("randomLI = " + randomLI);
        audioManager = AudioManager.instance;
        if (audioManager == null) {
            //Debug.LogError("No audio manager found in scene, whoops");
        }
        audioManager.PlaySound(randomLI);
    }
}