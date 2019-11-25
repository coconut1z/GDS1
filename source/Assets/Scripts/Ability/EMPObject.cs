using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EMPObject : MonoBehaviour {
    SpriteRenderer sr;
    // Use this for initialization
    void Start() {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update() {
        float si = Time.deltaTime * 90;
        transform.localScale += new Vector3(si, si, si);
        CheckScale();
        CheckBullets();
    }

    private void CheckScale() {
        if(transform.localScale.x > 100.0f) {
            Destroy(gameObject);
        }
    }

    public virtual void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.tag == "EnemyBullet") {
            // EXPENSIVE
            Destroy(col.gameObject); //Tells the bullet's projectile superclass script to off itself.
        }
    }

    private void CheckBullets() {
        GameObject[] g = GameObject.FindGameObjectsWithTag("EnemyBullet");
        for (int i = 0; i < g.Length; i++) {
            if (GetComponent<Collider2D>().bounds.Contains(g[i].transform.position)) {
                Destroy(g[i]);
            }
        }
    }
}