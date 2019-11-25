using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFlak : Projectile {
    private float lifespan;
    private float preLifespan;
    private float spawnTime;
    private Collider2D hitbox;
    private bool enabled;

    // Use this for initialization
    protected override void Start() {
        enabled = false;
        base.Start();
        hitbox = GetComponent<Collider2D>();
        hitbox.enabled = false;
        speed = 4.0f;
        damage = 0.1f * damageMultiplier;
        lifespan = 4.0f; //how long flak stays on the field for
        preLifespan = 0.25f; //how long flak does not affect enemies
        spawnTime = Time.time;
    }

    // Update is called once per frame
    protected override void Update() {
        base.Update();
        if(Time.time > (spawnTime + preLifespan) && !enabled) {
            hitbox.enabled = true;//enable the collider. This flak will interact with enemies again.
            //Debug.Log("Hitbox re-enabled");
            enabled = true;
        }
        if(Time.time > (spawnTime + lifespan)) {
            Destroy(gameObject);
        }

    }

    protected override void FixedUpdate() {
        base.FixedUpdate();
    }

    public override void ProjectileMovement() {
        rb.velocity = transform.up * speed;
    }
}
