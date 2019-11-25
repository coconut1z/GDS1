using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Railgun : WeaponModule {
    public ParticleSystem psL;
    public ParticleSystem psR;
    //audio //copy me to any script that triggers a sound
    // Use this for initialization
    protected override void Start() {
        base.Start();
        shootDelay = 0.5f;
        spoolDelay = 1.0f;
        shootTime = shootDelay;
        spread = 1f;
        displacement = new Vector2(0, 0.1f);
        originalSize = new Vector2(1, 1);
        weaponId = Global.RAILGUN;
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
        if(psL.isPlaying == false) {
            psL.Play();
            psR.Play();
        }
        
        if (shootTime > shootDelay) {
            spoolTime += Time.deltaTime;
            if(spoolTime > spoolDelay) {
                FireShot();
                spoolTime = 0.0f;
            }
        }
    }

    private void FireShot() {
        audioManager.PlaySound("PlasmaShot1");
        // Instantiate (BULLET PREFAB, SHOOT POSITION, Z ROTATION + SPREAD IF THERE IS ANY)
        Instantiate(projectiles[0], shootPos[0].position,                   //it tis, each instantiate is a new bullet -john
            Quaternion.Euler(0, 0, transform.eulerAngles.z));
        shootTime = 0;
        psL.Stop();
        psR.Stop();
    }

    public override int ReturnId() {
        return Global.RAILGUN;
    }

}
