using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class OKButton : MonoBehaviour {

	public StageManager stageManager;
	public InventoryPanel ip;
	public string position;
    public AbilityInventoryPanel aip;
	private PlayerInventory inventory;
	private bool clicked;
   
	private AbilitiesManager abilitiesManager;


	private PlayerController player;

	private void Start(){
		//Time.timeScale = 5;
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerController>();
        player.canShoot = false;
		abilitiesManager = player.GetComponent<AbilitiesManager> ();
		position = "S1P1";
		inventory = GameObject.Find ("PlayerInventory").GetComponent<PlayerInventory>();
		if(Global.startAt2){
			position = "S1END";
			inventory.addWeapon (Global.CANNON2);
			inventory.addWeapon (Global.SHOTGUN2);
			inventory.SortInventory ();
			inventory.SortAbilityInventory ();
		}
		if(Global.startAt3){
			inventory.addWeapon (Global.RNG2);
			inventory.addWeapon (Global.SATCANNON);
            //Stage 1 Boss Rewards
            inventory.addAbility(Global.DRONEMKII);
            inventory.addAbility(Global.ROCKETRING);
            inventory.addAbility(Global.COMPANIONDRONEII);

            //Stage 2 Boss Rewards
            inventory.addAbility(Global.HEAL);
            inventory.addAbility(Global.REGENERATOR);
            //inventory.addAbility(Global.STASISBUBBLE);
			position = "S2END";
			inventory.SortInventory ();
			inventory.SortAbilityInventory ();
		}
		if(Global.startAt4){
			inventory.addWeapon (Global.RPR);
			inventory.addWeapon (Global.LAS1);
			position = "S3END";
			//Stage 1 Boss Rewards
			inventory.addAbility(Global.DRONEMKII);
			inventory.addAbility(Global.ROCKETRING);
			inventory.addAbility(Global.COMPANIONDRONEII);

			//Stage 2 Boss Rewards
			inventory.addAbility(Global.HEAL);
			inventory.addAbility(Global.REGENERATOR);
			//inventory.addAbility(Global.STASISBUBBLE);

			//Stage 1 Boss Rewards
			inventory.addAbility(Global.SWORDRING);
			inventory.addAbility(Global.SHIELD);
			inventory.addAbility(Global.SWORD);
			inventory.addAbility(Global.SHIELDPASSIVE);


			inventory.SortInventory ();
			inventory.SortAbilityInventory ();
		}
		if(Global.startAt5){
			inventory.addWeapon (Global.SNIPER2);
			inventory.addWeapon (Global.FLAMETHROWER);
			position = "S4END";


			inventory.addAbility(Global.DRONEMKII);
			inventory.addAbility(Global.ROCKETRING);
			inventory.addAbility(Global.COMPANIONDRONEII);

			//Stage 2 Boss Rewards
			inventory.addAbility(Global.HEAL);
			inventory.addAbility(Global.REGENERATOR);
			//inventory.addAbility(Global.STASISBUBBLE);

			//Stage 1 Boss Rewards
			inventory.addAbility(Global.SWORDRING);
			inventory.addAbility(Global.SHIELD);
			inventory.addAbility(Global.SWORD);
			inventory.addAbility(Global.SHIELDPASSIVE);

			inventory.addAbility(Global.PHASE);
			inventory.addAbility(Global.TELEPORT);
			inventory.addAbility(Global.EMP);

			inventory.SortInventory ();
			inventory.SortAbilityInventory ();
		}
		if(Global.startAt6){
			inventory.addWeapon (Global.MJOLNIR);
			inventory.addWeapon (Global.RPR2);
			position = "S5END";

			inventory.addAbility(Global.DRONEMKII);
			inventory.addAbility(Global.ROCKETRING);
			inventory.addAbility(Global.COMPANIONDRONEII);

			//Stage 2 Boss Rewards
			inventory.addAbility(Global.HEAL);
			inventory.addAbility(Global.REGENERATOR);
			//inventory.addAbility(Global.STASISBUBBLE);

			//Stage 1 Boss Rewards
			inventory.addAbility(Global.SWORDRING);
			inventory.addAbility(Global.SHIELD);
			inventory.addAbility(Global.SWORD);
			inventory.addAbility(Global.SHIELDPASSIVE);

			inventory.addAbility(Global.PHASE);
			inventory.addAbility(Global.TELEPORT);
			inventory.addAbility(Global.EMP);

			inventory.addAbility(Global.BFOLASER);
			inventory.addAbility(Global.TIMESLOW);

			inventory.SortInventory ();
			inventory.SortAbilityInventory ();
		}
        if (Global.bossMedley){
            inventory.addWeapon(Global.RNG);
            inventory.addWeapon(Global.BLITZGUN);
            inventory.addWeapon(Global.REDOX);
            inventory.addWeapon(Global.PLAYERICEWEP);
            position = "S1M";
            inventory.SortInventory();
            inventory.SortAbilityInventory();
        }
		//ip.gameObject.SetActive (false);
	}

	private void OnEnable(){
		clicked = true;
		Invoke ("AllowClick", 1.5f);
	}

	private void AllowClick(){
		clicked = false;
	}

	public void CloseWindow(){
		if(clicked){
			return;
		}
		clicked = true;
		if(!Global.tutorial){
			player.canShoot = true;
		}
		AssignWeapons ();
		AssignAbilities ();
		if(stageManager != null){
            if(position.Equals("S1P1")){
				if(Global.tutorial){
                    //stageManager.LoadStage3();
				}else{
                    //stageManager.LoadStage6Boss();
                    //stageManager.LoadStage3();
                    //stageManager.LoadStage5Boss();
                    stageManager.LoadStage1();
                }

			}else if(position.Equals("S1P2")){
				stageManager.LoadStage1Part2 ();
			}else if(position.Equals("S1P3")){
                stageManager.LoadStage1Part3 ();
			}else if(position.Equals("S1END")){
				stageManager.LoadStage2 ();
			}else if(position.Equals("S2P2")){
				stageManager.LoadStage2Part2 ();
			}else if(position.Equals("S2P3")){
				stageManager.LoadStage2Part3 ();
			}else if(position.Equals("S2END")){
				stageManager.LoadStage3 ();
			}else if(position.Equals("S3END")){
				stageManager.LoadStage4 ();
            }else if (position.Equals("S3P2"))
            {
                stageManager.LoadStage3Part2();
            }else if(position.Equals("S4P2")){
				stageManager.LoadStage4Part2 ();
			}else if(position.Equals("S4PB")){
				stageManager.LoadStage4Boss ();
			}else if(position.Equals("S4END")){
				stageManager.LoadStage5 ();
			}else if(position.Equals("S5P2")){
				stageManager.LoadStage5Part2 ();
			}else if(position.Equals("S5PB")){
				stageManager.LoadStage5Boss ();
            }else if(position.Equals("S5END")){
                stageManager.LoadStage6Part1();
            }else if(position.Equals("S6P2")){
                stageManager.LoadStage6Part2();
            }else if(position.Equals("S6PB")){
                stageManager.LoadStage6Boss();
            }


            else if (position.Equals("S1M")) {
                //stageManager.LoadBossMedley1();

                stageManager.LoadBossMedley1();

                inventory.addWeapon(Global.CANNON2);
                inventory.addWeapon(Global.SHOTGUN2);
                inventory.addWeapon(Global.SNIPER);
                inventory.addWeapon(Global.MINIGUN);
                inventory.addWeapon(Global.VELKOZ);
                inventory.addWeapon(Global.FLAK);
                inventory.addWeapon(Global.GUIDEDLAUNCHER);

                inventory.addAbility(Global.ROCKETRING);
                inventory.addAbility(Global.DRONEMKII);

            }else if (position.Equals("S2M"))
            {
                stageManager.LoadBossMedley3();

                inventory.addWeapon(Global.RPR);
                inventory.addWeapon(Global.LAS1);
                inventory.addWeapon(Global.SPLITTER);
                //inventory.addWeapon(Global.DARKWEAPON);
                inventory.addWeapon(Global.RNG3);

                inventory.addAbility(Global.SWORD);
                inventory.addAbility(Global.SWORDRING);
                inventory.addAbility(Global.SHIELD);

                inventory.addAbility(Global.REGENERATOR);
                inventory.addAbility(Global.HEAL);

            }else if (position.Equals("S3M"))
            {
                stageManager.LoadBossMedley5();

                inventory.addWeapon(Global.CANNON3);
                inventory.addWeapon(Global.RPR2);
                inventory.addWeapon(Global.SHOTGUN3);
                inventory.addWeapon(Global.MEGAMINIGUN);
                inventory.addWeapon(Global.MJOLNIR);

                inventory.addAbility(Global.EMP);
                inventory.addAbility(Global.BFOLASER);
                inventory.addAbility(Global.SHIELDPASSIVE);

            }else if (position.Equals("S4M"))
            {
                stageManager.LoadBossMedley6();

            }
                



            
           //s aip.gameObject.SetActive(true);

		}
		transform.parent.gameObject.SetActive (false);
	}

	public void AssignWeapons(){
		Transform equippedWeapons = GameObject.Find ("EquippedWeapons").transform;
		for(int i = 2; i < equippedWeapons.childCount; i++){
			player.ReplaceWeapon (equippedWeapons.GetChild (i).GetComponent<WeaponDraggable> ().weaponPrefab, 
				equippedWeapons.GetChild (i).GetComponent<WeaponDraggable> ().slot);
		}
		/*
		List<GameObject> unequippedWeapons = new List<GameObject> ();
		for(int i = 0; i < transform.parent.childCount; i++){
			string name = transform.parent.GetChild (i).name;
			if(!name.Equals("WeaponsText") && !name.Equals("OKButton") &&
				!name.Equals("EquippedWeapons")){
				name = name.Replace ("(Clone)", "");
				GameObject test = (GameObject)(Resources.Load ("InventoryPrefabs/" + name));
				print ("InventoryPrefabs/" + name);
				unequippedWeapons.Add (test);

			}
		}
		*/
		ip.ReassignInventory ();
	}

	public void AssignAbilities() {
		Transform equippedAbilities = GameObject.Find("EquippedAbilities").transform;
		for (int i = 4; i < equippedAbilities.childCount; i++)
		{
			/*player.ReplaceWeapon(equippedAbilities.GetChild(i).GetComponent<AbilityDraggable>().abilityPrefab,
                equippedAbilities.GetChild(i).GetComponent<AbilityDraggable>().slot);*/
			abilitiesManager.ReplaceAbility(equippedAbilities.GetChild(i).GetComponent<AbilityDraggable>().abilityPrefab,
				equippedAbilities.GetChild(i).GetComponent<AbilityDraggable>().slot);
		}

		aip.ReassignInventory ();
	}

}
