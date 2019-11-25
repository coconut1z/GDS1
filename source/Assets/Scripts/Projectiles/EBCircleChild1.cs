using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EBCircleChild1 : MonoBehaviour {
    //This bullet should have no innate velocity, and should move via a parent, insubstantial and invisble bullet.

    //This used to use the projectile superclass. This fucked things up when trying to use children and parent behaviour.
    //If you feel like seeing for yourself, child bullets would move ~8 pixels. Every ~.3s. Made no fucking sense whatsoever.
    //Gave up, made free-standing script.

    private float speed;
    private float damage;
    private Rigidbody2D rb;
    private float updateInterval;
    private float lastUpdate;

    // Use this for initialization
    void Start() {
        speed = 1.0f;
        damage = 1;
        updateInterval = 1.0f;
        lastUpdate = Time.time;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update() {
    }

    void FixedUpdate() {
        if(Time.time > lastUpdate+ updateInterval) {
            ProjectileMovement();
            lastUpdate = Time.time; //update after a second, every second. For testing mainly.
        }
        
    }

    private void ProjectileMovement() {
        //rb.velocity = transform.up * speed * Time.deltaTime;
        //rb.transform.localPosition = 
        //rb.velocity = transform.up * 0.25f;
        rb.AddForce((transform.up * speed), ForceMode2D.Impulse);
    }
}
