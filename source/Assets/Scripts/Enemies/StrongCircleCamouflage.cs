using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrongCircleCamouflage : MonoBehaviour {

    SpriteRenderer shipSr;
    SpriteRenderer[] weaponSr;
    bool fadingIn;
    bool fadingOut;
    float tempTime;
    Color opaque;
    Color transparent;
    public bool justSpawned;

	// Use this for initialization
	void Start () {
        gameObject.GetComponent<Enemy>().canShoot = false;
        shipSr = GetComponent<SpriteRenderer>();
        weaponSr = GetComponentsInChildren<SpriteRenderer>();
        fadingIn = false;
        fadingOut = false;
        justSpawned = true;
        tempTime = Time.time;
        opaque = new Color(1f, 1f, 1f, 1f);
        transparent = new Color(1f, 1f, 1f, 0f);
        shipSr.color = opaque;
        InvokeRepeating("StartFadeOut", 0.0f, 4.0f);
        InvokeRepeating("StartFadeIn", 2.0f, 4.0f);
	}
	
	// Update is called once per frame
    void Update () {
        float t = Time.time - tempTime;
        if (fadingOut) {
            FadeOut(t);
        }
        else if (fadingIn) {
            FadeIn(t);
        }
    }
	
    void FadeOut (float t) {
        if (justSpawned) {
            shipSr.color = new Color(1f, 1f, 1f, 0f);
            foreach (SpriteRenderer sr in weaponSr) {
                sr.color = new Color(1f, 1f, 1f, 0f);
            }
        }
        else {
            shipSr.color = new Color(1f, 1f, 1f, Mathf.SmoothStep(1.0f, 0.0f, t));
            foreach (SpriteRenderer sr in weaponSr) {
                sr.color = new Color(1f, 1f, 1f, Mathf.SmoothStep(1.0f, 0.0f, t));
            }
        }
    }

    void FadeIn (float t) {
        gameObject.GetComponent<StrongCircleStatic>().startShooting = true;
        shipSr.color = new Color(1f, 1f, 1f, Mathf.SmoothStep(0.0f, 1.0f, t));
        foreach (SpriteRenderer sr in weaponSr)
        {
            sr.color = new Color(1f, 1f, 1f, Mathf.SmoothStep(0.0f, 1.0f, t));
        }
    }

    void StartShooting() {
        gameObject.GetComponent<StrongCircleStatic>().startShooting = true;
    }

    void resetFade() {
        fadingIn = false;
        fadingOut = false;
    }

    void StartFadeOut() {
        resetFade();
        tempTime = Time.time;
        fadingOut = true;
        print("fadingOut");
    }

    void StartFadeIn()
    {
        resetFade();
        justSpawned = false;
        tempTime = Time.time;
        fadingIn = true;
        print("fadingIn");
    }
}
