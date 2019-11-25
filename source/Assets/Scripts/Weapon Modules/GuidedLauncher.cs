using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuidedLauncher : WeaponModule {

	// Use this for initialization
	protected override void Start () {
		base.Start ();
		shootDelay = 0.34f;
		shootTime = shootDelay;
		spread = 1f;
        displacement = new Vector2(0, 0.1f);
        originalSize = new Vector2(0.25f, 0.25f);
		stage = 2;
		weaponId = Global.GUIDEDLAUNCHER;
	}
	
	// Update is called once per frame
	protected override void Update () {
		base.Update ();
	}
		
	public override void Shoot(){
		if(shootTime > shootDelay){
			// Instantiate (BULLET PREFAB, SHOOT POSITION, Z ROTATION + SPREAD IF THERE IS ANY)
            Instantiate(projectiles[0], shootPos[0].position, 
                Quaternion.Euler(0, 0, transform.eulerAngles.z));
            Debug.Log("wtf");
            shootTime = 0;
            audioManager.PlaySound("MissileLauncher");
            Debug.Log("why");
        }
	}

	public override int ReturnId(){
		return Global.GUIDEDLAUNCHER;
	}
}
