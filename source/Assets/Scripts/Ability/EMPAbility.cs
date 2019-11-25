using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EMPAbility : AbilityModule {

    private GameObject empObject;

    // Use this for initialization
    protected override void Start()
    {
        base.Start();
        empObject = (GameObject)(Resources.Load("EMPObject"));
        cooldown = 60.0f;
        cooldownTimer = cooldown;
        abilityName = "EMP";
        abilityText.text = abilityName;
        cost = 80.0f;
        abilityId = Global.EMP;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    public override void Activate()
    {
        if (cooldownTimer > cooldown && playerController.abilityMeter > cost) {
            Instantiate(empObject, player.transform.position, Quaternion.Euler(0, 0, 0));
            cooldownTimer = 0.0f;
            playerController.abilityMeter = playerController.abilityMeter - cost;
            playerController.usingAbility = true;
            Invoke("EndAbility", 0.1f);
        }
    }

    public override void Deactivate() { }

    public void EndAbility() {
        playerController.usingAbility = false;
    }

	public override int ReturnId() {
		return Global.EMP;
	}
}
