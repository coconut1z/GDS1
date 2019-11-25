using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHomingMissile : Projectile {
    private float rotateSpeed;
    private Transform playerTransform;
    private bool proximityFix;
    private float closeRange;
    private float lifespan;
	// Use this for initialization
	protected override void Start () {
		base.Start ();
		speed = 220f;
		damage = 1;
        rotateSpeed = 80.0f;
        lifespan = 7.0f;
        closeRange = 1.2f; //how close the missile will stop turning.
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        proximityFix = false;
	}
	
	// Update is called once per frame
	protected override void Update () {
		base.Update ();
        CheckProximity();
        ChangeDirection();
        CheckLifespan();
    }

    private void CheckLifespan() {
        lifespan -= Time.deltaTime;
        if(lifespan <= 0.0f) {
            Destroy(gameObject);
        }
    }

    private void CheckProximity() {
        if(proximityFix) {return;} //if we've already locked on, skip this.
        float distance = Vector2.Distance(playerTransform.position, transform.position);
        if (distance < closeRange) {//if close enough;
            proximityFix = true; //lock heading.
            rb.angularVelocity = 0.0f;
        }
    }

    protected override void FixedUpdate(){
		base.FixedUpdate ();
	}

    private void ChangeDirection() {
        if (proximityFix) { return; }
        //print("made it to change direction");
        Vector2 direction = playerTransform.position - transform.position;
        float rotateAmount = Vector3.Cross(direction, transform.up).z;
        rb.angularVelocity = -rotateAmount * rotateSpeed;

        /*
        float AngleRadToMouse = Mathf.Atan2(
            Camera.main.ScreenToWorldPoint(Input.mousePosition).y - transform.position.y,
            Camera.main.ScreenToWorldPoint(Input.mousePosition).x - transform.position.x
        );
        float AngleToDeg = (180 / Mathf.PI) * AngleRadToMouse - 90;
        transform.rotation = Quaternion.Euler(0, 0, AngleToDeg);*/ //meet the old magic missile code.
    }

    public override void ProjectileMovement ()
	{
		rb.velocity = transform.up * speed * Time.deltaTime;
	}
}
