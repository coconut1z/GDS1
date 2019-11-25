using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingSword : Projectile
{
    private float spawnTime;
    private float lifespan;
    private Vector2 mousePos;
    private float rotateSpeed;
    //private Rigidbody2D rb;

    // Use this for initialization
    protected override void Start()
    {
        base.Start();
        spawnTime = Time.time;
        lifespan = 3.0f;
        damage = 10f * damageMultiplier;
        speed = 10.0f;
        rotateSpeed = 100.0f;
        rb = GetComponent<Rigidbody2D>();

        this.particleHit = (GameObject)(Resources.Load("SwordExplosion"));
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        checkLifespan();
    }

    private void checkLifespan()
    {
        if (Time.time > spawnTime + lifespan)
        {
            Destroy(gameObject);
        }
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        changeDirection();
    }

    private void changeDirection()
    {
        UpdateMousePos();

        Vector2 direction = mousePos - rb.position;
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

    private void UpdateMousePos()
    {
        mousePos = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x,
            Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
    }

    public override void ProjectileMovement()
    {
        rb.velocity = transform.up * speed;
    }

}
