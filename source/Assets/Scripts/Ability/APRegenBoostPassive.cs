using UnityEngine;
using System.Collections;

public class APRegenBoostPassive : PassiveAbility
{
    
    // Use this for initialization
	protected override void Start()
	{
        base.Start();
        abilityName = "Energy Booster";
        abilityText.text = abilityName;
        abilityId = Global.SPEEDBOOST;
	}

	// Update is called once per frame
	protected override void Update()
	{
        Attach();
	}

    public override void Attach()
    {
        if (player.transform.hasChanged) {
            playerController.abilityRechargeMultiplier = 1.0f;
            player.transform.hasChanged = false;
        }
        else {
            playerController.abilityRechargeMultiplier = 2.0f;
        }
    }

    public override void Detach()
    {
        playerController.abilityRechargeMultiplier = 1.0f;
    }

	public override int ReturnId() {
		return Global.APREGENBOOST;
	}

}
