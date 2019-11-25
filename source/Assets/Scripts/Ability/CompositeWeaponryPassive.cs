using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompositeWeaponryPassive : PassiveAbility {

    // Use this for initialization
    protected override void Start() {
        base.Start();
        abilityName = "Composite Weaponry";
        abilityText.text = abilityName;
        abilityId = Global.COMPOSITEWEAPONRY;
    }

    // Update is called once per frame
    protected override void Update() {
        if (playerController.lives == 3) { 
            playerController.compositeWeaponry = 1.0f;
            cooldownImage.fillAmount = 0;

            bottomImage.color = new Color(1, 1, 1, 0.2f);
        }
        else if (playerController.lives == 2) { 
            playerController.compositeWeaponry = 1.1f;
            cooldownImage.fillAmount = 1;

            bottomImage.color = new Color(1, 1, 1, 0.5f);
            bottomText.text = "1.1x";
        }
        else if (playerController.lives == 1) { 
            playerController.compositeWeaponry = 1.2f; 
            cooldownImage.fillAmount = 1;

            bottomImage.color = new Color(1, 1, 1, 0.5f);
            bottomText.text = "1.2x";
        }
    }

    public override void Attach() {
    }

    public override void Detach() {
        playerController.compositeWeaponry = 1.0f;
    }

	public override int ReturnId() {
		return Global.COMPOSITEWEAPONRY;
	}
}
