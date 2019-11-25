using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletRingPassive : PassiveAbility
{
    public GameObject bullets;

    // Use this for initialization
    protected override void Start()
    {
        base.Start();
        abilityName = "Bullet Hail";
        abilityText.text = abilityName;
        abilityId = Global.BULLETRING;

        cooldown = 15;
        cooldownTimer = 15;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        //Attach();
    }

    public override void Attach()
    {
        InvokeRepeating("ShootRing", 0f, 15f);
    }

    public override void Detach()
    {
        CancelInvoke("ShootRing");
    }

    public void ShootRing() {
        cooldownTimer = 0;
        for (int i = 0; i < 360; i+= 20) {
            Instantiate(bullets, player.transform.position,
                Quaternion.Euler(0, 0, i));
        }
    }

    public override int ReturnId()
    {
        return Global.BULLETRING;
    }
}
