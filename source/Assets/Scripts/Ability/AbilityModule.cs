using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class AbilityModule : MonoBehaviour
{

    public virtual float cooldownTimer { get; set; }
    public virtual float cooldown { get; set; }
    public virtual GameObject player {get; set;}
    public virtual PlayerController playerController { get; set; }
    public virtual string abilityName { get; set; }
    public virtual Image cooldownImage { get; set; }
    public virtual Text abilityText { get; set; }
    public virtual bool passive { get; set; }
    public virtual float cost { get; set; }
    //public virtual Image abilityIcon { get; set; }
    public virtual int abilityId { get; set; }
    public virtual Text abilityButtonText { get; set; }

    public Sprite abilityCircleIcon;
    public Sprite abilitySquareIcon;

    public virtual Image bottomImage { get; set; }
    public virtual Text bottomText { get; set; }

    public string defaultButton;

    bool fadingIn;
    bool fadingOut;
    float tempTime;

    // Use this for initialization
    protected virtual void Start() {
        cooldownTimer = 0;
        cooldown = 0;
        cost = 0;
        player = GameObject.FindWithTag("Player");
        playerController = player.GetComponent<PlayerController>();
        abilityName = "Placeholder";
        passive = false;
        GetComponent<SpriteRenderer>().enabled = false;
        //abilityIcon.enabled = true;
        abilityButtonText.enabled = true;

        defaultButton = abilityButtonText.text;
	}
	
	// Update is called once per frame
	protected virtual void Update () {
        cooldownTimer += Time.deltaTime;
        if (cooldownTimer > cooldown) {
            cooldownImage.fillAmount = 1;
            bottomImage.fillAmount = 1;
            bottomText.text = "";

            bottomImage.color = new Color(1, 1, 1, 0.5f);
        }
        else {
            cooldownImage.fillAmount = 1 - ((cooldown - cooldownTimer) / cooldown);
            bottomImage.fillAmount = 1 - ((cooldown - cooldownTimer) / cooldown);
            bottomText.text = Mathf.Round(cooldown - cooldownTimer) + "";
            bottomImage.color = new Color(1, 1, 1, 0.2f);
        }

        NotEnoughEnergyUpdate();
	}

    public virtual void SetNames() {
        abilityText.text = abilityName;
    }

    public virtual void NotEnoughEnergyUpdate() {
        if (playerController.abilityMeter < cost) {
            abilityButtonText.text = "Not enough energy";
        }
        else {
            abilityButtonText.text = defaultButton;
        }
    }

    public abstract void Activate();

    public abstract void Deactivate();

	public abstract int ReturnId ();

}
