using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnekMultiGun : WeaponEnemy {
    public float phase;
    private float sineRotation, sineRotator;
    private float speed;
    private float maxDelay;

    // Use this for initialization
    protected override void Start () {
        base.Start();

        shootTime = 0;
        shootDelay = 2.2f;
        sineRotation = 0;
        ChangePhase(2); //starts us off in phase 1 to test.
        if (difficulty == Global.RECRUIT) {
            maxDelay = 1.0f;
        }
        if (difficulty == Global.VETEREN) {
            maxDelay = 2.0f;
        }
        if (difficulty == Global.BATTLEH) {
            maxDelay = 4.0f;
        }
    }
	
	// Update is called once per frame
	protected override void Update () {
        base.Update();
        if (phase == 1 || phase == 1.5f || phase == 2.5f) {
            lookAtPlayer();
        }
        else if (phase == 2 || phase == 2.1f) {
            sineRotation += Time.deltaTime;
            transform.rotation = Quaternion.Euler(0, 0, 180 + sineRotator * Mathf.Sin(sineRotation));
        }
    }

    public override void Shoot() {
        if (shootTime <= shootDelay) {
            return;
        }
        shootTime = UnityEngine.Random.Range(0.0f, maxDelay);
        //Debug.Log("Snek MG trying to shoot.");
        EnemyProjectile e1 = Instantiate(projectiles[0], shootPos[0].position,
                Quaternion.Euler(0, 0, transform.eulerAngles.z - 8)).GetComponent<EnemyProjectile>();
        e1.Setup(player, 2.5f, 1);
        e1.SetRadius(0.1f);
        //quick test firing.
        if (phase == 1f) {
            //PhaseOne();
        }
        else if (phase == 1.5f) {
            //PhaseOneP5();
        }
        else if (phase == 2 || phase == 2.1f) {
            //PhaseTwo();
        }
        else if (phase == 2.5f) {
            //PhaseTwoP5();
        }
    }

    public void ChangePhase(float i) {
        if (i == 1) {
            phase = 1;
            shootDelay = 1.5f;
            shootTime = 0;
        }
        else if (i == 1.5f) {
            phase = 1.5f;
            if (difficulty == Global.RECRUIT) {
                shootDelay = 4;
            }
            else {
                shootDelay = 2;
            }
            shootTime = shootDelay;
        }
        else if (i == 2) { //main shooty mechanic.
            phase = 2;
            transform.rotation = Quaternion.Euler(0, 0, 180);
            shootTime = 0;
            sineRotator = 90;
        }
        else if (i == 2.1f) {
            phase = 2;
            transform.rotation = Quaternion.Euler(0, 0, 180);
            shootTime = 0;
            if (difficulty == Global.RECRUIT) {
                shootDelay = 0.6f;
            }
            else if (difficulty == Global.VETEREN) {
                shootDelay = 0.4f;
            }
            else if (difficulty == Global.BATTLEH) {
                shootDelay = 0.2f;
            }
            sineRotator = -90;
        }
        else if (i == 2.5f) {
            if (difficulty == Global.RECRUIT) {
                shootDelay = 3;
            }
            else if (difficulty == Global.VETEREN) {
                shootDelay = 2;
            }
            else if (difficulty == Global.BATTLEH) {
                shootDelay = 1;
            }
            phase = 2.5f;
            shootTime = 0;
        }
        else if (i == 0) {
            phase = 0;
        }
    }
}
