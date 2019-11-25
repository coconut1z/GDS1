using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAbility : AbilityModule {

    public GameObject sword;
    public Animator swordAnim;
    public AudioSource genji;

    // Use this for initialization
    protected override void Start()
    {
        base.Start();
        cooldown = 20f;
        cooldownTimer = cooldown;
        genji = GetComponent<AudioSource>();

        sword = GameObject.Find("Sword");
        swordAnim = sword.GetComponent<Animator>();
        //sword.SetActive(false);
        sword.GetComponentInChildren<BoxCollider2D>().enabled = false;
        sword.GetComponent<SpriteRenderer>().enabled = false;

        abilityName = "Energy Blade";
        abilityText.text = abilityName;

        cost = 20f;

        abilityId = Global.SWORD;

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
            playerController.swinging = true;
            //sword.SetActive(true);
            sword.GetComponentInChildren<BoxCollider2D>().enabled = true;
            sword.GetComponent<SpriteRenderer>().enabled = true;
            swordAnim.SetBool("Active", true);
            cooldownTimer = 0.0f;
            Invoke("EndSwing", 2.1f);
            genji.Play();
        }
    }

    public override void Deactivate() {
        
    }

    public void EndSwing() {
        playerController.usingAbility = false;
        playerController.swinging = false;
        sword.GetComponentInChildren<BoxCollider2D>().enabled = false;
        sword.GetComponent<SpriteRenderer>().enabled = false;
        swordAnim.SetBool("Active", false);
        //sword.SetActive(false);
    }

	public override int ReturnId() {
		return Global.SWORD;
	}
}
