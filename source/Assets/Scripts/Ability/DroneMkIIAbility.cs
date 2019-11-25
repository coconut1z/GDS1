using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneMkIIAbility : AbilityModule {

    public GameObject droneMkIIPrefab;
    private GameObject newDrone;
    private bool escorts;
    private float playerX, playerY;

	// Use this for initialization
	protected override void Start () {
        base.Start();
        cooldown = 30f;
        cost = 40f;
        cooldownTimer = cooldown;
        escorts = false;
        abilityName = "Summon Drones Mk II";
        abilityText.text = abilityName;
        abilityId = Global.DRONEMKII;
	}
	
	// Update is called once per frame
	protected override void Update () {
        base.Update();
        DestroyEscorts();
    }

    public override void Activate()
	{
        if (!escorts && cooldownTimer > cooldown && playerController.abilityMeter >= cost) {
            escorts = true;
            playerController.usingAbility = true;
            playerController.abilityMeter = playerController.abilityMeter - cost;
            GetPlayerPos();
            newDrone = Instantiate(droneMkIIPrefab, new Vector3(playerX, playerY), Quaternion.identity);
            newDrone.transform.SetParent(player.transform);
            newDrone.transform.localRotation = Quaternion.identity;
            cooldownTimer = 0;
        }
    }

    public override void Deactivate() {
    }

    void DestroyEscorts() {
        //drones last for seven seconds
        if (escorts && cooldownTimer > 5) {
            playerController.usingAbility = false;
            Destroy(newDrone);
            escorts = false;
        }
    }

    void GetPlayerPos() {
        playerX = player.transform.position.x;
        playerY = player.transform.position.y;
    }

	public override int ReturnId() {
		return Global.DRONEMKII;
	}
}
