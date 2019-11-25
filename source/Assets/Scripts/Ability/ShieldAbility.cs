using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldAbility : AbilityModule {

    public GameObject shield;

    // Use this for initialization
    protected override void Start()
    {
        base.Start();
        cooldown = 40.0f;
        cooldownTimer = cooldown;
        abilityName = "Energy Barrier";
        abilityText.text = abilityName;
        cost = 60.0f;
        abilityId = Global.SHIELD;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    public override void Activate()
    {
        if (cooldownTimer > cooldown && playerController.abilityMeter > cost) {
            GameObject newShield = Instantiate(shield, player.transform.position, Quaternion.identity);

            float AngleRadToMouse = Mathf.Atan2(
            Camera.main.ScreenToWorldPoint(Input.mousePosition).y - player.transform.position.y,
            Camera.main.ScreenToWorldPoint(Input.mousePosition).x - player.transform.position.x
        );
            float AngleToDeg = (180 / Mathf.PI) * AngleRadToMouse - 90;
            newShield.transform.rotation = Quaternion.Euler(0, 0, AngleToDeg);

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
		return Global.SHIELD;
	}
}
