using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLaser : EnemyProjectile {

    // Use this for initialization
    protected override void Start()
    {
        base.Start();
        speed = 2f;
        damage = 0;
		radius = 0.25f;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    public override void ProjectileMovement()
    {
		transform.Translate(transform.up * speed * Time.deltaTime, Space.World);
    }

    public override void PlayerCollision()
    {

    }
}
