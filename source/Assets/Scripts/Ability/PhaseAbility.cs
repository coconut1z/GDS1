using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseAbility : AbilityModule {

    bool phased;
    public SpriteRenderer shipSr;
    public SpriteRenderer[] weaponSr;
    float trueCost;

	// Use this for initialization
	protected override void Start () {
        base.Start();
        phased = false;
        shipSr = player.GetComponent<SpriteRenderer>();
        weaponSr = player.GetComponentsInChildren<SpriteRenderer>();
        cooldown = 30.0f;
        cooldownTimer = cooldown;
        abilityName = "Phase";
        abilityText.text = abilityName;
        cost = 1f;
        trueCost = 50f;
        abilityId = Global.PHASE;
	}
	
	// Update is called once per frame
	protected override void Update () {
        base.Update();
        if (phased) {
            playerController.abilityMeter -= trueCost * Time.deltaTime;
        }
	}


	public override void Activate()
	{
        if (cooldownTimer > cooldown && playerController.abilityMeter > cost) {
            phased = true;
            playerController.usingAbility = true;
            playerController.canShoot = false;
            player.GetComponent<Collider2D>().enabled = false;
            playerController.phased = true;

            shipSr.color = new Color(1f, 1f, 1f, 0.5f);
            foreach (SpriteRenderer sr in weaponSr)
            {
                if (sr)
                {
                    sr.color = new Color(1f, 1f, 1f, 0.5f);
                }
            }

        }
        else {
            Deactivate();
        }

	}

    public override void Deactivate()
    {
        phased = false;
        playerController.usingAbility = false;
        playerController.canShoot = true;
        player.GetComponent<Collider2D>().enabled = true;
        playerController.phased = false;

        shipSr.color = new Color(1f, 1f, 1f, 1f);
        foreach (SpriteRenderer sr in weaponSr)
        {
            if (sr)
            {
                sr.color = new Color(1f, 1f, 1f, 1f);
            }
        }

        if (cooldownTimer > cooldown) {
            cooldownTimer = 0.0f;
        }
    }

	public override int ReturnId() {
		return Global.PHASE;
	}
}
