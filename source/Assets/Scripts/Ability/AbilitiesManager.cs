using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilitiesManager : MonoBehaviour {
    
    public AbilityModule[] abilitySlots;
    public List<Image> abilityCooldownImages;
    public List<Text> abilityNameTexts;
    //public List<Image> abilityIconImages;
    public List<Image> abilityOnCooldownImages;

    public List<Image> abilityBottomImages;
    public List<Text> abilityBottomTexts;

    public List<Text> abilityButtonTexts;

    int abilitiesCount;

	// Use this for initialization
	void Start () {
        abilitiesCount = abilitySlots.Length;

        InstantiateAbilities();
        AllocateAbilityUI();
        AttachPassives();
	}
	
	// Update is called once per frame
	void Update () {
        if (abilitiesCount > 0) {
            ActivateAbility1();
            DeactivateAbility1();
        }
        if (abilitiesCount > 1)
        {
            ActivateAbility2();
            DeactivateAbility2();
        }
        if (abilitiesCount > 2)
        {
            ActivateAbility3();
            DeactivateAbility3();
        }
        if (abilitiesCount > 3)
        {
            ActivateAbility4();
            DeactivateAbility4();
        }
	}

    public void ReplaceAbility(GameObject abi, int slot)
    {
        GameObject abilityObj = Instantiate(abi) as GameObject;


        AbilityModule ability = abilityObj.GetComponent<AbilityModule>();
        AbilityModule unequip = abilitySlots[slot - 1];

        if (unequip.passive) {
            PassiveAbility unequipPassive = unequip.GetComponent<PassiveAbility>();
            unequipPassive.Detach();
        }

        Destroy(unequip.gameObject);

        if(ability.passive) {
            PassiveAbility equipPassive = ability.GetComponent<PassiveAbility>();
            equipPassive.Attach();
        }

        abilitySlots[slot - 1] = ability;

        abilitySlots[slot - 1].cooldownImage = abilityCooldownImages[slot - 1];
        abilityCooldownImages[slot - 1].sprite = abilitySlots[slot - 1].abilityCircleIcon;
        abilityOnCooldownImages[slot - 1].sprite = abilitySlots[slot - 1].abilityCircleIcon;
        abilitySlots[slot - 1].abilityText = abilityNameTexts[slot - 1];

        abilitySlots[slot - 1].bottomImage = abilityBottomImages[slot - 1];
        abilityBottomImages[slot - 1].sprite = abilitySlots[slot - 1].abilitySquareIcon;

        abilitySlots[slot - 1].bottomText = abilityBottomTexts[slot - 1];

        //abilitySlots[slot - 1].abilityIcon = abilityIconImages[slot - 1];

        abilitySlots[slot - 1].abilityButtonText = abilityButtonTexts[slot - 1];
    }

    public void InstantiateAbilities() {

        for (int i = 0; i < abilitiesCount; i++)
        {
            AbilityModule ability = Instantiate(abilitySlots[i]);
            abilitySlots[i] = ability;
        }
    }

    public void AttachPassives() {
        foreach (AbilityModule ab in abilitySlots) {
            if (ab.passive) {
                PassiveAbility passiveAbility = ab.GetComponent<PassiveAbility>();
                passiveAbility.Attach();
            }
        }
    }

    public void AllocateAbilityUI() {
        if (abilitiesCount > 0)
        {
            abilitySlots[0].cooldownImage = abilityCooldownImages[0];
            abilityCooldownImages[0].sprite = abilitySlots[0].abilityCircleIcon;
            abilityOnCooldownImages[0].sprite = abilitySlots[0].abilityCircleIcon;
            abilitySlots[0].abilityText = abilityNameTexts[0];
            //abilitySlots[0].abilityIcon = abilityIconImages[0];

            abilitySlots[0].abilityButtonText = abilityButtonTexts[0];

            abilitySlots[0].bottomImage = abilityBottomImages[0];
            abilityBottomImages[0].sprite = abilitySlots[0].abilitySquareIcon;
            abilitySlots[0].bottomText = abilityBottomTexts[0];

        }
        if (abilitiesCount > 1)
        {
            abilitySlots[1].cooldownImage = abilityCooldownImages[1];
            abilityCooldownImages[1].sprite = abilitySlots[1].abilityCircleIcon;
            abilityOnCooldownImages[1].sprite = abilitySlots[1].abilityCircleIcon;
            abilitySlots[1].abilityText = abilityNameTexts[1];
            //abilitySlots[1].abilityIcon = abilityIconImages[1];

            abilitySlots[1].abilityButtonText = abilityButtonTexts[1];

            abilitySlots[1].bottomImage = abilityBottomImages[1];
            abilityBottomImages[1].sprite = abilitySlots[1].abilitySquareIcon;
            abilitySlots[1].bottomText = abilityBottomTexts[1];
        }
        if (abilitiesCount > 2)
        {
            abilitySlots[2].cooldownImage = abilityCooldownImages[2];
            abilityCooldownImages[2].sprite = abilitySlots[2].abilityCircleIcon;
            abilityOnCooldownImages[2].sprite = abilitySlots[2].abilityCircleIcon;
            abilitySlots[2].abilityText = abilityNameTexts[2];
            //abilitySlots[2].abilityIcon = abilityIconImages[2];

            abilitySlots[2].abilityButtonText = abilityButtonTexts[2];

            abilitySlots[2].bottomImage = abilityBottomImages[2];
            abilityBottomImages[2].sprite = abilitySlots[2].abilitySquareIcon;
            abilitySlots[2].bottomText = abilityBottomTexts[2];
        }
        if (abilitiesCount > 3)
        {
            abilitySlots[3].cooldownImage = abilityCooldownImages[3];
            abilityCooldownImages[3].sprite = abilitySlots[3].abilityCircleIcon;
            abilityOnCooldownImages[3].sprite = abilitySlots[3].abilityCircleIcon;
            abilitySlots[3].abilityText = abilityNameTexts[3];
            //abilitySlots[3].abilityIcon = abilityIconImages[3];

            abilitySlots[3].abilityButtonText = abilityButtonTexts[3];

            abilitySlots[3].bottomImage = abilityBottomImages[3];
            abilityBottomImages[3].sprite = abilitySlots[3].abilitySquareIcon;
            abilitySlots[3].bottomText = abilityBottomTexts[3];
        }
    }

    public void ActivateAbility1()
    {
        if (InputKeys.isDown(InputKeys.ABL1) && !abilitySlots[0].passive)
        {
            abilitySlots[0].Activate();
        }
    }

    public void ActivateAbility2()
    {
        if (InputKeys.isDown(InputKeys.ABL2) && !abilitySlots[1].passive)
        {
           abilitySlots[1].Activate();
        }
    }

    public void ActivateAbility3()
    {
        if (InputKeys.isDown(InputKeys.ABL3) && !abilitySlots[2].passive)
        {
            abilitySlots[2].Activate();
        }
    }

    public void ActivateAbility4()
    {
        if (InputKeys.isDown(InputKeys.ABL4) && !abilitySlots[3].passive)
        {
            abilitySlots[3].Activate();
        }
    }

    public void DeactivateAbility1() 
    {
        if (InputKeys.isUp(InputKeys.ABL1) && !abilitySlots[0].passive)
        {
            abilitySlots[0].Deactivate();
        }
    }

    public void DeactivateAbility2()
    {
        if (InputKeys.isUp(InputKeys.ABL2) && !abilitySlots[1].passive)
        {
            abilitySlots[1].Deactivate();
        }
    }

    public void DeactivateAbility3()
    {
        if (InputKeys.isUp(InputKeys.ABL3) && !abilitySlots[2].passive)
        {
            abilitySlots[2].Deactivate();
        }
    }

    public void DeactivateAbility4()
    {
        if (InputKeys.isUp(InputKeys.ABL4) && !abilitySlots[3].passive)
        {
            abilitySlots[3].Deactivate();
        }
    }
}
