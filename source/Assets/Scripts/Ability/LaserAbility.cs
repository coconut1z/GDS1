using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserAbility : AbilityModule {

    public GameObject BFOLaser;
    private float previousSpeed;
    private float speedModifier;
    private PlayerController pScript;

    // Use this for initialization
    protected override void Start() {
        base.Start();
        cooldown = 60.0f;
        cooldownTimer = cooldown;
        abilityName = "BFO Laser"; // it stands for Bi-lateral Focused Orbital Laser. I swear.
        abilityText.text = abilityName;
        cost = 90.0f; //I mean have you seen the size of that thing?
        abilityId = Global.BFOLASER;
        speedModifier = 0.5f; // divides player speed by x during use. fixes by taking previous speed before use.
        previousSpeed = playerController.playerNormalSpeed;
        pScript = player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    protected override void Update() {
        base.Update();
    }

    private void EndOfLaser() {
        //if the ability is done being used;
        playerController.usingAbility = false;
        playerController.playerNormalSpeed = previousSpeed;
    }//you ever look at the word 'ability' enough times and find it starts to look really funny?

    public override void Activate() {
        if (cooldownTimer > cooldown && playerController.abilityMeter >= cost) {
            playerController.usingAbility = true;
            playerController.abilityMeter = playerController.abilityMeter - cost;
            pScript.playerSpeedMultiplier *= 0.5f; //reduce player movement speed.
            CreateLaser();
            playerController.playerNormalSpeed *= speedModifier;
            cooldownTimer = 0.0f;
            Invoke("EndOfLaser", 5.0f);
        }
    }
    
    private void CreateLaser() {
        GameObject G = Instantiate(BFOLaser, new Vector3(player.transform.position.x, player.transform.position.y), Quaternion.identity);
        G.transform.parent = player.transform;
		G.transform.rotation = player.transform.rotation;
        G.transform.localPosition = new Vector3(0, 0, 0);
        G.transform.localScale = new Vector3(6.66f, 6.66f, 6.66f);
    }

    public override void Deactivate() {
        //uneeded?
    }

    public override int ReturnId() {
        return Global.BFOLASER;
    }
    //ability, ability, ability, ability, ability, ability, ability, ability, ability, ability, ability, ability, ability, ability, ability, ability, ability, ability, ability, ability, ability, ability, ability, ability, ability, ability, ability.
}