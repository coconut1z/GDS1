using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RNGWeaponMkIII : WeaponModule {

    private int rng;

	// Use this for initialization
	protected override void Start () {
		base.Start ();
		shootDelay = 0.15f / 1.65f;
		shootTime = shootDelay;
		spread = 6f;
        displacement = new Vector2(0, 0.1f);
        originalSize = new Vector2(1, 1);
		weaponId = -1;
		stage = 5;
	}
	
	// Update is called once per frame
	protected override void Update () {
		base.Update ();
	}
		
	public override void Shoot(){
		if(shootTime > shootDelay){
            // Instantiate (BULLET PREFAB, SHOOT POSITION, Z ROTATION + SPREAD IF THERE IS ANY)
            int chance = Chance();
            if (chance == 0) {
                audioManager.PlaySound("BasicGun");
            }
            else if (chance == 1) {
                audioManager.PlaySound("Cannon");
            }
            else if (chance == 2){
                audioManager.PlaySound("Redox");
            }
            else if (chance == 3) {
                audioManager.PlaySound("Cannon");
            }
            else if (chance == 4) {
                audioManager.PlaySound("Cannon");
            }
            else if (chance == 5) {
                audioManager.PlaySound("Sniper");
            }
			Instantiate (projectiles [chance], shootPos [0].position, 
				Quaternion.Euler (0, 0, transform.eulerAngles.z + spread - 2 * Random.value * spread));
			shootTime = 0;

		}
	}

    public int Chance() {
        rng = Random.Range(0, 120);
        if (rng >= 0 && rng < 49) {
            return 0;//RNG bullet
        }
        else if (rng >= 50 && rng < 79) {
            return 1;//RPR bullet
        }
        else if (rng >= 80 && rng < 99) {
            return 2;//Redox bullet
        }
        else if (rng >= 100 && rng < 109) {
            return 3;//Cannon bullet
        }
        else if (rng >= 110 && rng < 115) {
            return 4;//Splitter bullet
        }
        else if (rng >= 116 && rng < 120) {
            return 5;//Sniper bullet
        }
        else return 0;
    }

	public override int ReturnId(){
		return Global.RNG3;
	}
}
