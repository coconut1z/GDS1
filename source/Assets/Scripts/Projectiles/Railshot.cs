using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Railshot : Projectile {
    public GameObject explosionPrefab;

    // Use this for initialization
    protected override void Start() {
        base.Start();
        //overriding rb with this.
        speed = 900f;
        damage = 7.0f * damageMultiplier; //will also instantiate an explosion with 20 aoe damage.
    }

    // Update is called once per frame
    protected override void Update() {
        base.Update();
    }

    protected override void FixedUpdate() {
        base.FixedUpdate();
    }

    public override void ProjectileMovement() {
        rb.velocity = transform.up * speed * Time.deltaTime;
    }

    protected override void OnDestroy() {
        base.OnDestroy(); //spawns a particle on hit if there is one provided.
        Instantiate(explosionPrefab, transform.position, transform.rotation); //instantiates the explosion at the same position, but without making it a child.
        transform.DetachChildren();
    }
}
