﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HunterDroneGunI : WeaponModule {
    public HunterDroneI HD1;
    private bool fired;

    // Use this for initialization
    protected override void Start() {
        base.Start();
        shootDelay = 30.0f;
        shootTime = shootDelay;
        displacement = new Vector2(0, 0.35f);
        originalSize = new Vector2(1, 1);
        weaponId = Global.HDRONEI;
        stage = 2;
        HD1 = GetComponentInChildren<HunterDroneI>();
        fired = false;
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
        
        if (fired) { return; } //only fire once. MAY CHANGE TO CHECK HD1. Seeing if this works for now.
        if (shootTime > shootDelay) {
            HD1.StartHunt();//activate ze drone!
            shootTime = 0;
            fired = true;
            audioManager.PlaySound("ElectronicAwake1"); 
        }
    }

    public override int ReturnId() {
        return Global.HDRONEI;
    }
}
