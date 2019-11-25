using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikierboi : Enemy {

	// Use this for initialization
	protected override void Start () {
		base.Start ();
		if(difficulty == Global.RECRUIT){
			
		}else if(difficulty == Global.VETEREN){
			
		}else if(difficulty == Global.BATTLEH){
			
		}
		speed = 1.8f;
		health = 50;
		originalSpeed = speed;
		sr = GetComponentInChildren<SpriteRenderer> ();
		originalColor = sr.color;
        Invoke("MakeVisible", 1.0f);
	}
	
	// Update is called once per frame
	protected override void Update () {
		base.Update ();
	}

	public override void Shoot(){
		foreach(WeaponEnemy wep in weapons){
			wep.Shoot ();
		}
	}

	public override void Death(){
		Destroy (gameObject);
	}

    private void MakeVisible() {
        visible = true;
    }
}
