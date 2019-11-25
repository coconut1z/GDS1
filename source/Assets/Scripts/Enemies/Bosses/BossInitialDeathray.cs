using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossInitialDeathray : MonoBehaviour {
    private BoxCollider2D hitbox;
    private AudioManager audioManager;

    // Use this for initialization
    void Start() {
        audioManager = AudioManager.instance;
        if (audioManager == null) {
            Debug.LogError("No audio manager found in scene, whoops");
        }
        hitbox = GetComponent<BoxCollider2D>();
        Invoke("EnableHitbox", 12.1f);
        Invoke("DelayedSound", 1.5f);
        Invoke("DelayedSound2", 10.5f); //laser -1.5s
    }

    // Update is called once per frame
    void Update() {

    }
    private void DelayedSound() {
        audioManager.PlaySound("BossBeamCharge");
    }
    private void DelayedEnd() {
        audioManager.StopSound("BossBeamLoop");
    }

    private void DelayedSound2() {
        audioManager.PlaySound("BigBass1");
    }

    public void EnableHitbox() {
        hitbox.enabled = true;
        Invoke("DisableHitbox", 4.2f);
        audioManager.PlaySound("BossBeamLoop");
        audioManager.PlaySound("BossBeamLoop");
        audioManager.StopSound("BossBeamCharge");
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
