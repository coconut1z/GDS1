using UnityEngine;
using System.Collections;

public class SpeedBoostPassive : PassiveAbility
{
    
    // Use this for initialization
	protected override void Start()
	{
        base.Start();
        abilityName = "Racing Engines";
        abilityText.text = abilityName;
        abilityId = Global.SPEEDBOOST;
	}

	// Update is called once per frame
	protected override void Update()
	{
		
	}

    public override void Attach()
    {
        playerController.speed = 6.0f;
    }

    public override void Detach()
    {
        playerController.speed = 4.0f;
    }

	public override int ReturnId() {
		return Global.SPEEDBOOST;
	}

}
