using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour {

	public GameObject[] stage1Weapons;
	// 0 basic 1 cannon 2 shotgun 3 flak 4 satcannon 5 sniper 6 wave
	public GameObject[] stage2Weapons;
    // 0 cannon2 1 shotgun2 2 guidedlauncher 3 hellfire 4 minigun 5 velkoz
    //public List<GameObject> inventory;

    public GameObject[] abilities;

	public List<int> inventory;
    public List<int> abilitiesInventory;

	// Use this for initialization
	void Start () {	
		inventory = new List<int> ();


		inventory.Add(Global.CANNON);
		inventory.Add(Global.SHOTGUN);
		/*inventory.Add(Global.RPR2);
		inventory.Add(Global.RPR2);
		inventory.Add(Global.SHOTGUN2);
		inventory.Add(Global.SHOTGUN3);*/


        //inventory.Add(Global.BASIC);
        // if (Global.startAt3 == false)
        // {
            /*
          inventory.Add(Global.CANNON2);
          inventory.Add(Global.CANNON3);
          inventory.Add(Global.MINIGUN);
          inventory.Add(Global.PLASMACOIL);
          inventory.Add(Global.PLAYERICEWEP);
          inventory.Add(Global.PLAYERICEWEP2);
          inventory.Add(Global.REDOX);
          inventory.Add(Global.SHOTGUN2);
          inventory.Add(Global.SHOTGUN3);
          inventory.Add(Global.SNIPER);
          inventory.Add(Global.SPLITTER);
          inventory.Add(Global.REDOX2);
          inventory.Add(Global.VELKOZ);
          inventory.Add(Global.FLAK);
          inventory.Add(Global.RNG);
          inventory.Add(Global.RNG2);
          inventory.Add(Global.RNG3);
          inventory.Add(Global.FLAMETHROWER);
          inventory.Add(Global.GUIDEDLAUNCHER);
          inventory.Add(Global.HELLFIRE);
          inventory.Add(Global.LAS1);
          inventory.Add(Global.LAS1);
          inventory.Add(Global.RPR);
          inventory.Add(Global.RPR2);
          inventory.Add(Global.SATCANNON);
          inventory.Add(Global.SATCANNON2);
          inventory.Add(Global.DARKWEAPON);
          inventory.Add(Global.SNIPER2);
          inventory.Add(Global.MJOLNIR);
          inventory.Add(Global.HDRONEI);
          inventory.Add(Global.HDRONEII);
          inventory.Add(Global.HDRONEIII);
          inventory.Add(Global.RAILGUN);
          inventory.Add(Global.BLITZGUN);
          inventory.Add(Global.BOUNCELAUNCHER);
          inventory.Add(Global.BUZZSAW);
          inventory.Add(Global.MEGAMINIGUN);
          inventory.Add(Global.SHOTGUN);
          inventory.Add(Global.CANNON);
          inventory.Add(Global.FLAMETHROWER);

        inventory.Add(Global.CANNON2);
        inventory.Add(Global.CANNON3);
        inventory.Add(Global.MINIGUN);
        inventory.Add(Global.PLASMACOIL);
        inventory.Add(Global.PLAYERICEWEP);
        inventory.Add(Global.PLAYERICEWEP2);
        inventory.Add(Global.REDOX);
        inventory.Add(Global.SHOTGUN2);
        inventory.Add(Global.SHOTGUN3);
        inventory.Add(Global.SNIPER);
        inventory.Add(Global.SPLITTER);
        inventory.Add(Global.REDOX2);
        inventory.Add(Global.VELKOZ);
        inventory.Add(Global.FLAK);
        inventory.Add(Global.RNG);
        inventory.Add(Global.RNG2);
        inventory.Add(Global.RNG3);
        inventory.Add(Global.FLAMETHROWER);
        inventory.Add(Global.GUIDEDLAUNCHER);
        inventory.Add(Global.HELLFIRE);
        inventory.Add(Global.LAS1);
        inventory.Add(Global.LAS1);
        inventory.Add(Global.RPR);
        inventory.Add(Global.RPR2);
        inventory.Add(Global.SATCANNON);
        inventory.Add(Global.SATCANNON2);
        inventory.Add(Global.DARKWEAPON);
        inventory.Add(Global.SNIPER2);
        inventory.Add(Global.MJOLNIR);
        inventory.Add(Global.HDRONEI);
        inventory.Add(Global.HDRONEII);
        inventory.Add(Global.HDRONEIII);
        inventory.Add(Global.RAILGUN);
        inventory.Add(Global.BLITZGUN);
        inventory.Add(Global.BOUNCELAUNCHER);
        inventory.Add(Global.BUZZSAW);
        inventory.Add(Global.MEGAMINIGUN);
        inventory.Add(Global.SHOTGUN);
        inventory.Add(Global.CANNON);
        inventory.Add(Global.FLAMETHROWER);
*/
        // }

        //inventory.Add(Global.SNIPER);
        //inventory.Add(Global.HDRONEII);
        //inventory.Add(Global.HDRONEI);
        //inventory.Add(Global.RPR2);
        //inventory.Add(Global.RPR2);
        //inventory.Add(Global.SATCANNON);

        //Comment for merging

        abilitiesInventory = new List<int>();

        //Starting abilities do not need to be added as they are already in the UI
        /*abilitiesInventory.Add(Global.BULLETRING);
        abilitiesInventory.Add(Global.DRONE);
        abilitiesInventory.Add(Global.STASISBUBBLE);
        abilitiesInventory.Add(Global.REGENERATOR);

        */

        //Default abilities (do not need to be unlocked)

        abilitiesInventory.Add(Global.SPEEDBOOST);
        abilitiesInventory.Add(Global.COMPANIONDRONEI);
        abilitiesInventory.Add(Global.APREGENBOOST);
		/*
            //Stage 1 Boss Rewards
            abilitiesInventory.Add(Global.DRONEMKII);
            abilitiesInventory.Add(Global.ROCKETRING);
            abilitiesInventory.Add(Global.COMPANIONDRONEII);
            abilitiesInventory.Add(Global.PHASE);


            //Stage 2 Boss Rewards
            abilitiesInventory.Add(Global.HEAL);
            abilitiesInventory.Add(Global.COMPOSITEWEAPONRY);
            abilitiesInventory.Add(Global.LASTSTAND);

            //Stage 3 Boss Rewards
            abilitiesInventory.Add(Global.SWORD);
            abilitiesInventory.Add(Global.SHIELD);
            abilitiesInventory.Add(Global.SWORDRING);
            abilitiesInventory.Add(Global.SHIELDPASSIVE);

            //Stage 4 Boss Rewards
            abilitiesInventory.Add(Global.TELEPORT);
            abilitiesInventory.Add(Global.TIMESLOW);

            //Stage 5 Boss Rewards
            abilitiesInventory.Add(Global.BFOLASER);
            abilitiesInventory.Add(Global.EMP);
		*/
		Invoke ("LateStart", 0.01f);
    }

	private void LateStart(){
		SortInventory ();
		SortAbilityInventory ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void addWeapon (int weapon){
		//if(Global.Stage == 1){
		//	inventory.Add (stage1Weapons [weapon]);
		//}else if(Global.Stage == 2){
		//	inventory.Add (stage2Weapons [weapon]);
		//}
		inventory.Add(weapon);
	}

    public void addAbility (int ability) {
        abilitiesInventory.Add(ability);
    }

	public void ReassignInventory(List<int> newInv){
		inventory.Clear ();
		for(int i = 0; i < newInv.Count; i++){
			inventory.Add (newInv[i]);
		}
		//inventory.Sort ();
		//inventory.Reverse ();
	}

    public void ReassignAbilityInventory(List<int> newInv)
    {
        abilitiesInventory.Clear();
        for (int i = 0; i < newInv.Count; i++)
        {
            abilitiesInventory.Add(newInv[i]);
        }
        //inventory.Sort ();
        //inventory.Reverse ();
    }

	public void SortInventory(){
		inventory.Sort ();
		inventory.Reverse ();
	}

    public void SortAbilityInventory() {
        abilitiesInventory.Sort();
        abilitiesInventory.Reverse();
    }
}
