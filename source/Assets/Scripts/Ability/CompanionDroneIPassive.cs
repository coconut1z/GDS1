using UnityEngine;
using System.Collections;

public class CompanionDroneIPassive : PassiveAbility
{

    public CompanionDroneI drone;
    private CompanionDroneI activeDrone;

    // Use this for initialization
    protected override void Start()
    {
        base.Start();
        abilityName = "Fire Support";
        abilityText.text = abilityName;
        abilityId = Global.COMPANIONDRONEI;
    }

    // Update is called once per frame
    protected override void Update()
    {
        //Attach();
    }

    public override void Attach()
    {
        activeDrone = Instantiate(drone, this.transform.position, Quaternion.identity);
        activeDrone.StartHunt();
    }

    public override void Detach()
    {
        Destroy(activeDrone.gameObject);
    }

    public override int ReturnId()
    {
        return Global.COMPANIONDRONEI;
    }

}