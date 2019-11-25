using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StasisBubbleAbility : AbilityModule {

    public GameObject stasisBubble;
    private GameObject sb;
    private bool bubbleExists;

    // Use this for initialization
    protected override void Start() {
        base.Start();
        cooldown = 20.0f;
        cooldownTimer = cooldown;
        bubbleExists = false;
        abilityName = "Stasis Bubble";
        abilityText.text = abilityName;
        cost = 50f;
        abilityId = Global.STASISBUBBLE;
	}
	
	// Update is called once per frame
	protected override void Update () {
        base.Update();
	}

    private void LateUpdate() {
        RemoveBubble();
    }

    public override void Activate() {
        if (!bubbleExists && cooldownTimer > cooldown && playerController.abilityMeter >= cost) {
            bubbleExists = true;
            playerController.usingAbility = true;
            playerController.abilityMeter = playerController.abilityMeter - cost;
            sb = Instantiate(stasisBubble, new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0), Quaternion.identity);
            cooldownTimer = 0;
        }
    }

    public override void Deactivate() {
    }

    void RemoveBubble() {
        if (bubbleExists && cooldownTimer > 7f) {
            playerController.usingAbility = false;
            Destroy(sb);
            bubbleExists = false;
        }
    }

	public override int ReturnId() {
		return Global.STASISBUBBLE;
	}
}
