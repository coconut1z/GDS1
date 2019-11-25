using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitterGun : WeaponModule {

    // Use this for initialization
    protected override void Start() {
        base.Start();
        shootDelay = 0.18f;
        shootTime = shootDelay;
        spread = 3.5f;
		originalSize = new Vector2 (1, 1);
		displacement = new Vector2 (0, 0.35f);
        weaponId = Global.SPLITTER;
        stage = 4;
    }

    // Update is called once per frame
    protected override void Update() {
        base.Update();
    }

    public override void Shoot() {
        if (shootTime > shootDelay) {
            // Instantiate (BULLET PREFAB, SHOOT POSITION, Z ROTATION + SPREAD IF THERE IS ANY)
            Instantiate(projectiles[0], shootPos[0].position,
                Quaternion.Euler(0, 0, transform.eulerAngles.z + spread - 2 * Random.value * spread));
            shootTime = 0;
            audioManager.PlaySound("Minigun");
        }
    }

	public override int ReturnId(){
		return Global.SPLITTER;
	}
}