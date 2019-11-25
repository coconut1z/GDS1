using UnityEngine;
using System.Collections;

public class ShieldPassive : PassiveAbility
{

    public GameObject shield;
    private GameObject activeShield;

    // Use this for initialization
    protected override void Start()
    {
        base.Start();
        abilityName = "Guardian Angel";
        abilityText.text = abilityName;
        abilityId = Global.SHIELDPASSIVE;
    }

    // Update is called once per frame
    protected override void Update()
    {

    }

    public override void Attach()
    {
        activeShield = Instantiate(shield, player.transform.position, Quaternion.identity);
    }

    public override void Detach()
    {
        Destroy(activeShield.gameObject);
    }

    public override int ReturnId()
    {
        return Global.SHIELDPASSIVE;
    }

}
