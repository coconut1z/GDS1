using UnityEngine;
using System.Collections;

public abstract class PassiveAbility : AbilityModule
{
    // Use this for initialization
    protected override void Start()
    {
        base.Start();
        passive = true;
        abilityName = "Passive";
        Attach();

        //abilityIcon.enabled = false;
        abilityButtonText.enabled = false;

        bottomImage.fillAmount = 1;
        cooldownImage.fillAmount = 1;
        bottomImage.color = new Color(1, 1, 1, 0.5f);

        bottomText.text = "";
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

	public override void Activate()
	{
		
	}

	public override void Deactivate()
	{
		
	}

	public abstract void Attach();
    public abstract void Detach();
}