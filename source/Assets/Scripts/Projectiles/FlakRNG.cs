using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlakRNG : Projectile {
    private float lifespan;
    private float preLifespan;
    private float spawnTime;
    private Collider2D hitbox;
    private bool collisionEnabled;
    private float finalSpeed;
    private float priorTime;
    private float tickInterval;

    // Use this for initialization
    protected override void Start() {
        collisionEnabled = false;
        base.Start();
        hitbox = GetComponent<Collider2D>();
        hitbox.enabled = false;
        speed = UnityEngine.Random.Range(2.5f, 10.0f);
        finalSpeed = 8.0f;
        damage = 0.3f * damageMultiplier;
        lifespan = 4.0f; //how long flak stays on the field for
        preLifespan = 0.05f; //how long flak does not affect enemies
        spawnTime = Time.time;
        tickInterval = 0.1f; //time between projectile speed adjustment
        priorTime = Time.time; //
    }

    // Update is called once per frame
    protected override void Update() {
        base.Update();
        if (Time.time > (spawnTime + preLifespan) && !collisionEnabled) {
            hitbox.enabled = true;//enable the collider. This flak will interact with enemies again.
            //Debug.Log("Hitbox re-enabled");
            collisionEnabled = true;
        }
        if (Time.time > (spawnTime + lifespan)) {
            Destroy(gameObject);
        }

        speedClampDelay();

    }

    private void speedClamp() {
        
        if(speed < (finalSpeed * 0.98f)) {
            speed = speed * 1.05f;
        }
        else if(speed > (finalSpeed * 0.98f)) {
            speed = speed * 0.95f;
        }
        
    }

    private void speedClampDelay() {
        if (Time.time > (priorTime + tickInterval)) {
            speedClamp();
        }
    }

    protected override void FixedUpdate() {
        base.FixedUpdate();
    }

    public override void ProjectileMovement() {
        rb.velocity = transform.up * speed;
    }
}
