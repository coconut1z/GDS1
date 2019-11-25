using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mjolnir : WeaponModule
{



    // Use this for initialization
    protected override void Start()
    {
        base.Start();
        shootDelay = 1.5f;
        shootTime = shootDelay;
        spread = 1f;
        displacement = new Vector2(0, 0.1f);
        originalSize = new Vector2(1, 1);
        weaponId = Global.MJOLNIR;
        stage = 1;
        //damage = loads
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    public override void Shoot()
    {
        if (shootTime > shootDelay)
        {
            // Instantiate (BULLET PREFAB, SHOOT POSITION, Z ROTATION + SPREAD IF THERE IS ANY)

            Instantiate(projectiles[0], new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x,
           Camera.main.ScreenToWorldPoint(Input.mousePosition).y + 6f), Quaternion.identity);
        
            shootTime = 0;
        }
    }

    protected override void lookAtMouse()
    {
        //base.lookAtMouse();
    }


    public override int ReturnId()
    {
        return Global.MJOLNIR;
    }

}
