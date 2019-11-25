using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smoke : MonoBehaviour {
    SpriteRenderer sr;
    private float fadeInterval;
    private float lastFade;
    private float fadeAmount;

	// Use this for initialization
	void Start () {
        sr = GetComponent<SpriteRenderer>();
        lastFade = Time.time;
        fadeInterval = 0.1f;
        fadeAmount = 0.05f; //goes from 1-0, increment by about this amount.
	}
	
	// Update is called once per frame
	void Update () {
        fade();
        checkFaded();
	}

    private void checkFaded() {
        if(sr.color.a >= 0.1f) {
            return;
        }
        else {
            Destroy(gameObject);
        }
    }

    private void fade() {
        if(Time.time > lastFade + fadeInterval) {
            float alpha = sr.color.a - fadeAmount;
            sr.color = new Color(1.0f, 1.0f, 1.0f, alpha);
        }
    }
}
