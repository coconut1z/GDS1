using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RNGWeaponMkII : WeaponModule {

    private int rng;

	// Use this for initialization
	protected override void Start () {
		base.Start ();
		shootDelay = 0.16f / 1.5f;
		shootTime = shootDelay;
		spread = 6f;
        displacement = new Vector2(0, 0.1f);
        originalSize = new Vector2(1, 1);
		weaponId = Global.RNG2;
        stage = 3;
	}
	
	// Update is called once per frame
	protected override void Update () {
		base.Update ();
	}
		
	public override void Shoot(){
		if(shootTime > shootDelay){
            int chance = Chance();

            if (chance == 0)
            {
                audioManager.PlaySound("BasicGun");
            }
            else if (chance == 1)
            {
                audioManager.PlaySound("BasicGun");
            }
            else if (chance == 2)
            {
                audioManager.PlaySound("Cannon");
            }
            else if (chance == 3)
            {
                audioManager.PlaySound("Sniper");
            }

			// Instantiate (BULLET PREFAB, SHOOT POSITION, Z ROTATION + SPREAD IF THERE IS ANY)
			Instantiate (projectiles [chance], shootPos [0].position, 
				Quaternion.Euler (0, 0, transform.eulerAngles.z + spread - 2 * Random.value * spread));
			shootTime = 0;
            audioManager.PlaySound("Basic");
		}
	}

    public int Chance() {
        rng = Random.Range(0, 99);
        if (rng >= 0 && rng < 39) {
            return 0;
        }
        else if (rng >= 40 && rng < 80) {
            return 1;
        }
        else if (rng >= 81 && rng < 91) {
            return 2;
        }
        else if (rng >= 92 && rng < 100) {
            return 3;
        }
        else return 0;
    }

	public override int ReturnId(){
        return Global.RNG2;
    }
}
