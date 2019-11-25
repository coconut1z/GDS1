using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactoryGun : WeaponEnemy {
    public float spawnTimeBase;
    private float spawnTime;
    private Sprite spawnShip; //the ship the factory generates
    private SpriteRenderer sr; //the 'weapon' which is actually going to be the spawnShip Sprite.


	// Use this for initialization
	protected override void Start () {
        base.Start();
        shootDelay = 1f;
        shootTime = shootDelay;
        spawnTime = spawnTimeBase;
        if (difficulty == Global.RECRUIT) { spawnTime = spawnTimeBase * 1.5f; };
        if (difficulty == Global.VETEREN) { spawnTime = spawnTimeBase * 1.0f; };
        if (difficulty == Global.BATTLEH) { spawnTime = spawnTimeBase * 0.66f; };
        //spawn rate changes with difficulty, multiplying the input spawnTimeBase.
        // 1.5x recruit, 1x veteran, 0.66x fkmeupfam.
        sr = GetComponent<SpriteRenderer>();
        spawnShip = projectiles[0].GetComponent<SpriteRenderer>().sprite;
        sr.sprite = spawnShip;
        sr.color = projectiles[0].GetComponent<SpriteRenderer>().color;
        transform.localScale = projectiles[0].GetComponent<Transform>().localScale*2;

        shootTime -= 1.0f;
    }
	
	// Update is called once per frame
	protected override void Update () {
        base.Update();
        SpawningAnimation();
    }

    private void SpawningAnimation() {
        float progressRatio = shootTime;
        //Debug.Log("pr = " + progressRatio);
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, progressRatio);
    }

    public override void Shoot() {
        if (shootTime > spawnTime) {
            Instantiate(projectiles[0], shootPos[0].position,
                    Quaternion.Euler(0, 0, transform.eulerAngles.z));
            shootTime = 0;
        }
        
    }
}
