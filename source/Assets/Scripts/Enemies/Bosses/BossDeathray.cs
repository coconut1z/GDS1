using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDeathray : MonoBehaviour {
    private BoxCollider2D hitbox;
    private AudioManager audioManager;

    // Use this for initialization
    void Start () {
        audioManager = AudioManager.instance;
        if (audioManager == null) {
            Debug.LogError("No audio manager found in scene, whoops");
        }
        hitbox = GetComponent<BoxCollider2D>();
        Invoke("EnableHitbox", 2.1f);
        Invoke("DelayedSound", 1.5f);
        audioManager.PlaySound("BossFastCharge");
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void EnableHitbox() {
        hitbox.enabled = true;
        Invoke("DisableHitbox", 1.5f);
        audioManager.PlaySound("BossBeamLoop");
        audioManager.StopSound("BossFastCharge");
    }
    private void DelayedEnd() {
        audioManager.StopSound("BossBeamLoop");
    }

    private void DelayedSound() {
        audioManager.PlaySound("BigBass2");
    }

    private void DisableHitbox() {
        hitbox.enabled = false;
        Invoke("DelayedEnd", 0.8f);
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            PlayerController pScript = other.GetComponent<PlayerController>();
            pScript.DamagePlayer(1f);
        }
    }
}
