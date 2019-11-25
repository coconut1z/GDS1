using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegeneratorPassive : PassiveAbility
{
    ParticleSystem ps;
    ParticleSystem.EmissionModule em;

    GameObject regenEffect;

    // Use this for initialization
    protected override void Start()
    {
        base.Start();
        abilityName = "Regenerator";
        abilityText.text = abilityName;
        abilityId = Global.REGENERATOR;


        regenEffect = GameObject.Find("RegeneratorEffect");
        ps = regenEffect.GetComponent<ParticleSystem>();
        em = ps.emission;

        cooldown = 30;

    }

    // Update is called once per frame
    protected override void Update()
    {
        //Attach();
        if (playerController.damageTimer > cooldown && playerController.health < 5f)
        {
            Regenerate();
            em.enabled = true;
        }
        else
        {
            em.enabled = false;
            if (playerController.health > 5f) {
                playerController.health = 5f;
            }
        }

        if (playerController.damageTimer > cooldown)
        {
            cooldownImage.fillAmount = 1;
            bottomImage.fillAmount = 1;
            bottomText.text = "";

            bottomImage.color = new Color(1, 1, 1, 0.5f);
        }
        else
        {
            cooldownImage.fillAmount = 1 - ((cooldown - playerController.damageTimer) / cooldown);
            bottomImage.fillAmount = 1 - ((cooldown - playerController.damageTimer) / cooldown);
            bottomText.text = Mathf.Round(cooldown - playerController.damageTimer) + "";

            bottomImage.color = new Color(1, 1, 1, 0.2f);
        }

    }

    public override void Attach()
    {
        
    }

    public override void Detach()
    {
        em.enabled = false;
    }

    public void Regenerate() {
        playerController.health += Time.deltaTime/10;
        playerController.UpdateHealth();
    }

    public override int ReturnId()
    {
        return Global.REGENERATOR;
    }
}
