using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportAbility : AbilityModule {


    public ParticleSystem startPs;
    public ParticleSystem.EmissionModule startEm;

    public ParticleSystem endPs;
    public ParticleSystem.EmissionModule endEm;

    // Use this for initialization
    protected override void Start() {
        base.Start();
        cooldown = 20.0f;
        cooldownTimer = cooldown;

        startPs = GameObject.Find("ShadowsStart").GetComponent<ParticleSystem>();
        startEm = startPs.emission;

        endPs = GameObject.Find("ShadowsEnd").GetComponent<ParticleSystem>();
        endEm = endPs.emission;

        abilityName = "Hyperspace Warp";
        abilityText.text = abilityName;

        cost = 30f;

        abilityId = Global.TELEPORT;
	}
	
	// Update is called once per frame
	protected override void Update () {
        base.Update();
	}

	public override void Activate()
	{
        if (cooldownTimer > cooldown && Camera.main.ScreenToWorldPoint(Input.mousePosition).x > -6.6
            && Camera.main.ScreenToWorldPoint(Input.mousePosition).x < 6.6 &&
            Camera.main.ScreenToWorldPoint(Input.mousePosition).y > -4.4 &&
            Camera.main.ScreenToWorldPoint(Input.mousePosition).y < 4.4 &&
            playerController.abilityMeter >= cost)
        {
            playerController.canMove = false;
            playerController.usingAbility = true;
            playerController.abilityMeter = playerController.abilityMeter - cost;
            StartCoroutine(TeleportActivate(new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y)));
            cooldownTimer = 0.0f;
            startEm.enabled = true;
            startPs.Play();
        }
	}

	public override void Deactivate() { }

	IEnumerator TeleportActivate(Vector2 location)
    {
        yield return new WaitForSeconds(0.5f);
        player.transform.position = location;
        Invoke("ReactivateMovement", 0.5f);
        endEm.enabled = true;
        //startEm.enabled = false;
        //startPs.Stop();
        endPs.Play();
    }

    public void ReactivateMovement() {
        playerController.canMove = true;
        playerController.usingAbility = false;
        Invoke("EndParticles", 1f);
    }

    void EndParticles() {
        //endEm.enabled = false;
        //endPs.Stop();
    }

	public override int ReturnId() {
		return Global.TELEPORT;
	}
}
