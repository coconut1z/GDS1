using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeSlowAbility : AbilityModule
{

    public ParticleSystem timeStopParticles;

    private ParticleSystem particles;

    // Use this for initialization
    protected override void Start()
    {
        base.Start();
        cooldown = 40f;
        cooldownTimer = cooldown;

        abilityName = "Dimensional Break";
        abilityText.text = abilityName;

        cost = 60f;

        abilityId = Global.TIMESLOW;

    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    public override void Activate()
    {
        if (cooldownTimer > cooldown && playerController.abilityMeter >= cost)
        {
            playerController.usingAbility = true;
            playerController.abilityMeter = playerController.abilityMeter - cost;

            Time.timeScale = 0.5f;

            particles = Instantiate(timeStopParticles, player.transform.position, Quaternion.identity);

            playerController.playerSpeedMultiplier *= 2f;

            cooldownTimer = 0.0f;
            Invoke("EndSwing", 3f);
        }
    }

    public override void Deactivate()
    {

    }

    public void EndSwing()
    {
        playerController.usingAbility = false;
        Time.timeScale = 1f;
        Destroy(particles.gameObject);
        playerController.playerSpeedMultiplier *= 0.5f;
    }

    public override int ReturnId()
    {
        return Global.TIMESLOW;
    }
}
