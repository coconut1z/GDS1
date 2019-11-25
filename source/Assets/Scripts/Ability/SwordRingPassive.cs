using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordRingPassive : PassiveAbility
{
    public GameObject swordRing;

    // Use this for initialization
    protected override void Start()
    {
        base.Start();
        abilityName = "Blade Hurricane";
        abilityText.text = abilityName;
        abilityId = Global.SWORDRING;

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
        Instantiate(swordRing, player.transform.position, Quaternion.identity);
    }

    public override int ReturnId()
    {
        return Global.SWORDRING;
    }
}
