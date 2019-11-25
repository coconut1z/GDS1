using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShockzone : EnemyProjectile {
    float lifespan;
    float rotationSpeed;

    // Use this for initialization
    protected override void Start() {
        base.Start();
        rotationSpeed = 2000.0f;
        speed = 2.0f;
        damage = 1;
        lifespan = 5.0f;
		radius = 0.3f;
    }

    // Update is called once per frame
    protected override void Update() {
        base.Update();
    }

    //protected override void FixedUpdate() {
    //    base.FixedUpdate();
    //}

    public override void ProjectileMovement() {
        lifespan -= Time.deltaTime;
        transform.Rotate(Vector3.forward * (rotationSpeed * Time.deltaTime));
        if (lifespan <= 0) {
            Destroy(gameObject);
        }
    }
}
