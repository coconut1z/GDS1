using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EBCircleParent1 : Projectile {

    // Use this for initialization
    protected override void Start() {
        base.Start();
        speed = 2f;
        damage = 1;
        //rb.velocity = transform.up * speed * Time.deltaTime;
        //Debug.Log("Set projectile velocity...again");
        rb.AddForce((transform.up * speed), ForceMode2D.Impulse);
    }

    // Update is called once per frame
    protected override void Update() {
        base.Update();
    }

    protected override void FixedUpdate() {
        base.FixedUpdate();
    }

    public override void ProjectileMovement() {
        
        
    }
}
