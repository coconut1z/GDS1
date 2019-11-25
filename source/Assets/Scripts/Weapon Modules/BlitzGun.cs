using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlitzGun : WeaponModule {

    ParticleSystem.EmissionModule em;
    ParticleSystem ps;
    PolygonCollider2D hitbox;
    float fireTime;
    //audio //copy me to any script that triggers a sound


    // Use this for initialization
    protected override void Start() {
        base.Start();

        ps = GetComponentInChildren<ParticleSystem>();
        em = ps.emission;
        hitbox = GetComponentInChildren<PolygonCollider2D>();

        shootDelay = 0.5f;
        shootTime = shootDelay;
        spread = 1f;
        displacement = new Vector2(0, 0.1f);
        originalSize = new Vector2(1, 1);
        fireTime = 0.0f;

        weaponId = Global.BLITZGUN;
        stage = 2; //recommend this be stage 2 or 3. To be balanced for great damage at short-moderate range. Carry further at own risk
                   //as the difficulty of close range dodging ramps up.
                   //caching //copy me to any script that triggers a sound
        audioManager = AudioManager.instance;
        if (audioManager == null) {
            Debug.LogError("No audio manager found in scene, whoops");
        }
    }

    // Update is called once per frame
    protected override void Update() {
        base.Update();
        if(ps.IsAlive() == true) {
            if(Time.time > fireTime + (shootDelay / 8)) {//after a short delay; (decrease number for longer lasting hitbox).
                hitbox.enabled = false;
            }
        }
    }

    public override void Shoot() {
        if (shootTime > shootDelay) {
            em.enabled = true;
            ps.Play();
            fireTime = Time.time;
            hitbox.enabled = true;
            audioManager.PlaySound("BulletSpam1");
            shootTime = 0;
        }
    }

    public override int ReturnId() {
        return Global.BLITZGUN;
    }
}
