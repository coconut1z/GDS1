using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrenzyAbility : AbilityModule {

    //private bool frenzied;
    private float abilityCost;

    // Use this for initialization
    protected override void Start () {
        base.Start();
        cooldown = 10f;
        cooldownTimer = cooldown;        
        cost = 50.0f;
        abilityName = "Frenzy";
        abilityText.text = abilityName;
        abilityId = Global.FRENZY;

    }
	
	// Update is called once per frame
	protected override void Update () {
        base.Update();
        
    }

    public override void Activate() {
        playerController.usingAbility = true;
        PlayerController.frenzied = true;
        playerController.abilityMeter = playerController.abilityMeter - cost;
    }

    public override void Deactivate() {
        PlayerController.frenzied = false;
        playerController.usingAbility = false;
    }

	public override int ReturnId() {
		return Global.FRENZY;
	}
}
