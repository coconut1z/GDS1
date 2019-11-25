using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealAbility : AbilityModule
{

    bool healing;
    GameObject healEffect;
    float playerHealth;
    float targetHealth;
    ParticleSystem ps;
    ParticleSystem.EmissionModule em;
    float healTimer;
    float trueCost;

    // Use this for initialization
    protected override void Start()
    {
        base.Start();
        cooldown = 20.0f;
        cooldownTimer = cooldown;
        abilityName = "Repair Ship";
        abilityText.text = abilityName;
        cost = 50f;
        trueCost = 25f;
        healing = false;
        abilityId = Global.PHASE;
        healEffect = GameObject.Find("HealEffect");
        ps = healEffect.GetComponent<ParticleSystem>();
        em = ps.emission;
        healTimer = 0;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (healing)
        {
            playerController.abilityMeter -= trueCost * Time.deltaTime;
            playerController.UpdateHealth();
        }
    }


    public override void Activate()
    {
        if (cooldownTimer > cooldown && playerController.abilityMeter > 1 && playerController.health < 5f)
        {
            playerController.health += Time.deltaTime / 2;
            playerController.usingAbility = true;
            playerController.canShoot = false;
            playerController.canMove = false;
            em.enabled = true;
            healing = true;
            healTimer += Time.deltaTime/2;

            //float currentHealth = Mathf.Floor(playerController.health);

            if (playerController.damaged)
            {

                Deactivate();
            }

            if (healTimer > 1) {
                Deactivate();
            }
        }
        else {

            Deactivate();

        }

    }

    public override void Deactivate()
    {
        healing = false;

        if (healTimer > 0.8)
        {
            cooldown = 20.0f;
            cooldownTimer = 0.0f;
        }
        else if (healTimer > 0)
        {
            cooldown = 5.0f;
            cooldownTimer = 0.0f;
        }

        playerController.usingAbility = false;
        playerController.canShoot = true;
        playerController.canMove = true;
        playerController.health = Mathf.Floor(playerController.health + 0.2f);
        //healEffect.GetComponent<SpriteRenderer>().enabled = false;
        em.enabled = false;
        healTimer = 0;
        playerController.UpdateHealth();
    }

	public override int ReturnId() {
		return Global.HEAL;
	}
}
