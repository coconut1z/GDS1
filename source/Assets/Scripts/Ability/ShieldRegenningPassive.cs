using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldRegenningPassive : PassiveAbility {
    //Generates a shield that takes 1 damage before the player health does.
    //Regenerates this shield every x seconds (20 default)
    

    private float shieldCooldown;
    private float Coutdowntimer;

	// Use this for initialization
	protected override void Start () {
        abilityName = "Shield Generator";
        abilityText.text = abilityName;
        abilityId = Global.SHIELDREGEN;

        shieldCooldown = 20.0f;
        Coutdowntimer = shieldCooldown;
	}
	
	// Update is called once per frame
	protected override void Update () {
        
	}

    private void FixedUpdate() {
        if(playerController.shield < 1.0f) { //only countdown if player has no shield yet.
            Coutdowntimer -= Time.deltaTime;
        }
        if (Coutdowntimer <= 0.0f) {//therefore, countdown can only hit 0 when the player has no shield.
            playerController.shield = 1.0f; //give shield.
            Coutdowntimer = shieldCooldown; //reset countdown to cooldown.
        }
        //I put this in fixed update to run less often.
        //let me know if there's a good reason to put it in Update()? Cheers!
    }

    public override void Attach() { //apply shield on spawn if equipping module.
        playerController.shield = 1.0f;
    }

    public override void Detach() {//remove generated shield if module is removed.
        if(playerController.shield != 0.0f) {
            playerController.shield = 0.0f;
        }
    }

    public override int ReturnId() {
        return Global.SHIELDREGEN;
    }
}
