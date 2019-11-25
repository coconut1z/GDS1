using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flakgun : WeaponModule {
    int shotCount;
    // Use this for initialization
    protected override void Start() {
        base.Start();
        shootDelay = 0.75f;
        shootTime = shootDelay;
        spread = 15.0f;
        displacement = new Vector2(0, 0.1f);
        originalSize = new Vector2(0.4f, 0.4f);
        shotCount = 16;
		weaponId = Global.FLAK;
		stage = 2;
        audioManager = AudioManager.instance;
        if (audioManager == null) {
            Debug.LogError("No audio manager found in scene, whoops");
        }
    }

    // Update is called once per frame
    protected override void Update() {
        base.Update();
    }

    public override void Shoot() {
        if (shootTime > shootDelay) {
            // Instantiate (BULLET PREFAB, SHOOT POSITION, Z ROTATION + SPREAD IF THERE IS ANY)
            for(int i = 0; i < shotCount; i++) {
                Instantiate(projectiles[0], shootPos[0].position,
                Quaternion.Euler(0, 0, transform.eulerAngles.z + spread - 2 * Random.value * spread));
            }

            audioManager.PlaySound("Flak1");
            shootTime = 0;
        }
    }

	public override int ReturnId(){
		return Global.FLAK;
	}
}
