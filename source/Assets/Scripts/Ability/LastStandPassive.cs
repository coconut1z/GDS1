using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastStandPassive : PassiveAbility
{

    // Use this for initialization
    protected override void Start()
    {
        base.Start();
        abilityName = "Last Stand";
        abilityText.text = abilityName;
        abilityId = Global.LASTSTAND;
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (playerController.health <= 1.0f)
        {
            playerController.lastStand = 1.5f;
            cooldownImage.fillAmount = 1;


            bottomImage.color = new Color(1, 1, 1, 0.8f);
            bottomText.text = "1.5x";
        }
        else
        {
            playerController.lastStand = 1;
            cooldownImage.fillAmount = 0;

            bottomImage.color = new Color(1, 1, 1, 0.2f);
            bottomText.text = "";
        }
    }

    public override void Attach()
    {
    }

    public override void Detach()
    {
        playerController.lastStand = 1;
    }

	public override int ReturnId() {
		return Global.LASTSTAND;
	}
}
