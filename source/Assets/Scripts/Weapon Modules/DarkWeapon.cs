using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkWeapon : WeaponModule
{

    public ParticleSystem ps;
    public ParticleSystem.EmissionModule em;
    public CircleCollider2D hitbox;
    float fireTime;
    private bool firing;

    // Use this for initialization
    protected override void Start()
    {
        base.Start();

        ps = GetComponentInChildren<ParticleSystem>();
        hitbox = GetComponentInChildren<CircleCollider2D>();
        em = ps.emission;

        shootDelay = 0.01f;
        shootTime = shootDelay;
        spread = 1f;
        displacement = new Vector2(0, 0.1f);
        originalSize = new Vector2(1, 1);
        fireTime = 0.0f;

        weaponId = Global.DARKWEAPON;
        stage = 3;

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
        //base.lookAtMouse();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        //Fire();
        Unfire();
        hitbox.transform.position = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x,
                                                Camera.main.ScreenToWorldPoint(Input.mousePosition).y - 3.4f);
    }

    public override void Shoot()
    {
        Fire();
    }

    public override int ReturnId()
    {
        //throw new NotImplementedException();
		return Global.DARKWEAPON;
    }

    void Fire()
    {
        if (InputKeys.isDown(InputKeys.SHOOT))
        {
            hitbox.enabled = true;
            em.enabled = true;
        }

        if (InputKeys.isPressed(InputKeys.SHOOT))
        {
            hitbox.enabled = true;
            em.enabled = true;
            if (!firing) {
                firing = true;
                audioManager.PlaySound("DarkWeapon");
            }
        }
    }



    void Unfire()
    {
        if (InputKeys.isUp(InputKeys.SHOOT))
        {
            audioManager.StopSound("DarkWeapon");
            em.enabled = false;
            hitbox.enabled = false;
            firing = false;
        }
    }
}
