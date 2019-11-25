using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flamethrower : WeaponModule
{

    public ParticleSystem.EmissionModule em;
    public ParticleSystem ps;
    public PolygonCollider2D hitbox;
    float fireTime;
    private bool firing;
    private float timerRate = 0.25f;
    private float audioTimer = 0.25f; //same as above, to allow instant sound on the first call.

    // Use this for initialization
    protected override void Start()
    {
        base.Start();

        ps = GetComponentInChildren<ParticleSystem>();
        em = ps.emission;
        hitbox = GetComponentInChildren<PolygonCollider2D>();

        shootDelay = 0.01f;
        shootTime = shootDelay;
        spread = 1f;
        displacement = new Vector2(0, 0.1f);
        originalSize = new Vector2(1, 1);
        fireTime = 0.0f;

        weaponId = Global.FLAMETHROWER;
        stage = 1;

        hitbox.enabled = false;
        em.enabled = false;

        audioManager = AudioManager.instance;
        if (audioManager == null) {
            Debug.LogError("No audio manager found in scene, whoops");
        }
        firing = false;
    }

	protected override void lookAtMouse()
	{
        float AngleRadToMouse = Mathf.Atan2(
            Camera.main.ScreenToWorldPoint(Input.mousePosition).y - transform.position.y,
            Camera.main.ScreenToWorldPoint(Input.mousePosition).x - transform.position.x
        );
        float AngleToDeg = (180 / Mathf.PI) * AngleRadToMouse - 90;
        if (AngleToDeg > 15) {
            AngleToDeg = 25;
        }
        else if (AngleToDeg < -15) {
            AngleToDeg = -25;
        }
        transform.rotation = Quaternion.Euler(0, 0, AngleToDeg);
	}

	// Update is called once per frame
	protected override void Update()
    {
        base.Update();
        //Fire();
        Unfire();
    }

    public override void Shoot()
    {
        Fire();
    }

    public override int ReturnId()
    {
        //throw new NotImplementedException();
		return Global.FLAMETHROWER;
    }

    void Fire() {
		if (InputKeys.isDown(InputKeys.SHOOT) && equipped) {
            hitbox.enabled = true;
            em.enabled = true;
            audioTimer += Time.deltaTime;
            if (audioTimer >= timerRate) {//will only send audio request every .25s.
                audioTimer -= timerRate;
                audioManager.PlayLoopedSound("Flamethrower1");
                print("TOLD MANAGER TO PLAY SOUND");
            }
        }

		if (InputKeys.isPressed(InputKeys.SHOOT) && equipped)
        {
            hitbox.enabled = true;
            em.enabled = true;
            if (!firing) {
                firing = true;
                //audioManager.PlaySound("Flamethrower1");
            }
        }
    }



    void Unfire() {
        if (InputKeys.isUp(InputKeys.SHOOT)) {
            em.enabled = false;
            hitbox.enabled = false;
            if (firing) {
                firing = false;
                audioManager.StopSound("Flamethrower1");
                audioTimer = timerRate;
            }
        }
    }
}
