using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buzzsaw : WeaponModule {
    //SawBlade sawBlade;
    public bool fired;
    public SawBlade sawBlade;

    //audio //copy me to any script that triggers a sound

    // Use this for initialization
    protected override void Start () {
		base.Start ();
		shootDelay = 3.0f;
		shootTime = shootDelay;
		spread = 1f;
        displacement = new Vector2(0, 0.1f);
        originalSize = new Vector2(1, 1);
        fired = false;
        weaponId = Global.BUZZSAW;
        audioManager = AudioManager.instance;
        if (audioManager == null) {
            Debug.LogError("No audio manager found in scene, whoops");
        }
    }
	
	// Update is called once per frame
	protected override void Update () {
		base.Update ();
	}
		
	public override void Shoot(){
		if(!fired){
            sawBlade.Launch();
            fired = true;
            audioManager.PlaySound("BulletSpam1");
		}
	}

	public override int ReturnId(){
		return Global.BUZZSAW;
	}
}
