using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperEnemyWeapon : WeaponEnemy {
    float chargeTimeProgress; //local timer
    float fireTime; //time before firing.
    float bulletSpeed;
    LineRenderer lr;
    private EnemyRedBulletSpeed br1;

    // Use this for initialization
    protected override void Start () {
		base.Start ();
		shootDelay = 1.5f; //effectively, time before beginning to recharge.
		shootTime = shootDelay;
        chargeTimeProgress = 0.0f;
        bulletSpeed = 2000.0f;
        if(difficulty == Global.RECRUIT) { fireTime = 6.0f; };
        if (difficulty == Global.VETEREN) { fireTime = 5.0f; };
        if (difficulty == Global.BATTLEH) { fireTime = 3.5f; };
        lr = GetComponent<LineRenderer>();
        br1 = projectiles[0].GetComponent<EnemyRedBulletSpeed>();
		br1.setSpeed (bulletSpeed); //testing speed set

        HideLaser();
    }

    private void HideLaser() {
        for(int i = lr.positionCount; i >= 0; i--) {//What is wrong with me
            lr.SetPosition(i, new Vector3(-500, -500));
        }

    }

    // Update is called once per frame
    protected override void Update () {
		base.Update ();
		lookAtPlayer ();
	}

	public override void Shoot(){
        

        if (shootTime > shootDelay){
            chargeTimeProgress += Time.deltaTime;
            renderLaserSight();
            if (chargeTimeProgress > fireTime) {
                
                EnemyProjectile e = Instantiate(projectiles[0], shootPos[0].position,
					Quaternion.Euler(0, 0, transform.eulerAngles.z)).GetComponent<EnemyProjectile>(); //Fire the sniper shot.
				e.Setup(player, 36f, 1f);
				e.SetRadius (0.2f);
                chargeTimeProgress = 0.0f;
            }
            else {
                return;
            }
            shootTime = 0;
            HideLaser();
        }
	}

    private void renderLaserSight() {
        //nothing yet. Will think about it.
        lr.SetPosition(0, transform.position);
        lr.SetPosition(1, player.position);
    }
}
