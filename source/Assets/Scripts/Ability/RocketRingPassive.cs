using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketRingPassive : PassiveAbility
{
    public GameObject bullets;

    // Use this for initialization
    protected override void Start()
    {
        base.Start();
        abilityName = "Rocket Storm";
        abilityText.text = abilityName;
        abilityId = Global.ROCKETRING;

        cooldown = 30;
        cooldownTimer = 30;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        //Attach();
    }

    public override void Attach()
    {
        InvokeRepeating("ShootRing", 0f, 30f);
    }

    public override void Detach()
    {
        CancelInvoke("ShootRing");
    }

    public void ShootRing()
    {
        cooldownTimer = 0;
        for (int i = 0; i < 360; i += 40)
        {
            Instantiate(bullets, player.transform.position,
                Quaternion.Euler(0, 0, i));
        }
    }

    public override int ReturnId()
    {
        return Global.ROCKETRING;
    }
}
