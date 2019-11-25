using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryPanel : MonoBehaviour {

	public Transform equippedTransform;
	public GameObject chatBox;
	private Vector2[] epos;
	private List<int> weapons;
	private List<int> equippedWeapons;
	private PlayerInventory playerInventory;
	private GameObject infoPanel;
	private Transform unequippedPanel;
	private Image i;

	// Use this for initialization
	void Start () {
		weapons = new List<int> ();
		equippedWeapons = new List<int> ();
		epos = new Vector2[2];
		equippedWeapons.Add (equippedTransform.GetChild (2).GetComponent<WeaponDraggable>().weaponPrefab.GetComponent<WeaponModule>().weaponId);
		equippedWeapons.Add (equippedTransform.GetChild (3).GetComponent<WeaponDraggable>().weaponPrefab.GetComponent<WeaponModule>().weaponId);
		playerInventory = GameObject.Find("PlayerInventory").GetComponent<PlayerInventory>();
		epos[0] = new Vector2 (-609, 164);
		epos[1] = new Vector2 (-461, 164);
		infoPanel = (GameObject)(Resources.Load ("InventoryPrefabs/InfoPanel"));
		i = GetComponent<Image> ();
		Invoke ("LateStart", 0.1f);
	}
	
	// Update is called once per frame
	void Update () {
		if(chatBox.activeSelf){
			chatBox.SetActive (false);
		}
	}


	public void PopulateList(){
		for(int i = 0; i < transform.childCount; i++){
			if(!transform.GetChild(i).name.Equals("OKButton") &&
				!transform.GetChild(i).name.Equals("EquippedWeapons") &&
				!transform.GetChild(i).name.Equals("WeaponsText") &&
				!transform.GetChild(i).name.Equals("ScrollWep") &&
				!transform.GetChild(i).name.Equals("AbilityOKButton") &&
				!transform.GetChild(i).name.Equals("EquippedAbilities") &&
				!transform.GetChild(i).name.Equals("AbilitiesText") &&
				!transform.GetChild(i).name.Equals("ScrollAbility") &&
				!transform.GetChild(i).name.Equals("AbilityInventoryButton") &&
				!transform.GetChild(i).name.Equals("AbilityWeaponButton")){
				Destroy (transform.GetChild (i).gameObject);
			}
			if(transform.GetChild(i).name.Equals("ScrollWep")){
				unequippedPanel = transform.GetChild (i).GetChild(0).GetChild(0);
				for(int j = 0; j < unequippedPanel.childCount; j++){
					Destroy (unequippedPanel.GetChild (j).gameObject);
				}
			}
		}

		weapons.Clear ();
		for(int i = 0; i < playerInventory.inventory.Count; i++){
			weapons.Add (playerInventory.inventory [i]);
		}

		float stepY = 260;
		float stepX = 0;
		int row = 1;
		unequippedPanel.GetComponent<RectTransform> ().offsetMin = new Vector2 (
			unequippedPanel.GetComponent<RectTransform> ().offsetMin.x, 0);
		unequippedPanel.GetComponent<RectTransform> ().offsetMax = new Vector2 (
			unequippedPanel.GetComponent<RectTransform> ().offsetMax.x, 1);
		unequippedPanel.GetComponent<RectTransform> ().anchorMin = new Vector2 (0, 0);
		unequippedPanel.GetComponent<RectTransform> ().anchorMax = new Vector2 (0, 1);
		unequippedPanel.GetComponent<RectTransform> ().pivot = new Vector2 (0, 1);
		for(int i = 0; i < weapons.Count; i++){
			GameObject g = Instantiate(infoPanel) as GameObject;
			GameObject wep = Instantiate(GetWeaponUI (weapons [i])) as GameObject;
			wep.transform.parent = g.transform;
			wep.transform.localScale = new Vector3 (0.75f, 0.75f, 1);
			g.transform.parent = transform;
			g.transform.localPosition = new Vector2 (stepX * 325 + -180, stepY);
			g.transform.localScale = Vector3.one;
			wep.GetComponent<RectTransform> ().anchorMax = new Vector2 (0, 1);
			wep.GetComponent<RectTransform> ().anchorMin = new Vector2 (0, 1);
			g.transform.parent = unequippedPanel;
			stepX++;
			if((i+1) % 3 == 0){
				stepY -= 225;
				stepX = 0;
				row++;
				if(row > 3){
					//stepY = 112.5f;
					Vector2 size = unequippedPanel.GetComponent<RectTransform> ().sizeDelta;
					unequippedPanel.GetComponent<RectTransform> ().offsetMin = new Vector2 (
						unequippedPanel.GetComponent<RectTransform> ().offsetMin.x, 
						unequippedPanel.GetComponent<RectTransform> ().offsetMin.y - 225);
					//unequippedPanel.localPosition = new Vector3 (unequippedPanel.localPosition.x, 
					//	unequippedPanel.localPosition.y + 225f/2f, 0);
					//stepY -= 225f/2f;
				}
			}
			wep.GetComponent<RectTransform> ().localPosition = new Vector3 (-80, 45, 0);
		}



		/*
		for(int i = 0; i < weapons.Count; i++){
			GameObject g = Instantiate (weapons [i]) as GameObject;
			if(g.GetComponent<WeaponDraggable>().stage == 1){
				g.transform.parent = transform;
				g.transform.localPosition = new Vector2 (step + -180, 300);
				step += 125;
				g.transform.localScale = Vector3.one;
			}else{
				//Destroy (g);
			}
		}
		step = 0;
		for(int i = 0; i < weapons.Count; i++){
			GameObject g = Instantiate (weapons [i]) as GameObject;
			if(g.GetComponent<WeaponDraggable>().stage == 2){
				g.transform.parent = transform;
				g.transform.localPosition = new Vector2 (step + -180, 100);
				step += 125;
				g.transform.localScale = Vector3.one;
			}else{
				//Destroy (g);
			}
		}	
		*/
	}

	private GameObject GetWeaponUI(int weaponId){
		switch(weaponId)
        {
    		case Global.BASIC:
    			return (GameObject)(Resources.Load ("InventoryPrefabs/Basic"));
    		case Global.SHOTGUN:
    			return (GameObject)(Resources.Load ("InventoryPrefabs/Shotgun"));
    		case Global.CANNON:
    			return (GameObject)(Resources.Load ("InventoryPrefabs/Cannon"));
    		case Global.FLAK:
    			return (GameObject)(Resources.Load ("InventoryPrefabs/Flak"));
    		case Global.SATCANNON:
    			return (GameObject)(Resources.Load ("InventoryPrefabs/SatelliteCannon"));
    		case Global.SNIPER:
    			return (GameObject)(Resources.Load ("InventoryPrefabs/Sniper"));
    		case Global.RNG:
    			return (GameObject)(Resources.Load ("InventoryPrefabs/RNG"));
    		case Global.CANNON2:
    			return (GameObject)(Resources.Load ("InventoryPrefabs/CannonMkII"));
    		case Global.SHOTGUN2:
    			return (GameObject)(Resources.Load ("InventoryPrefabs/ShotgunMKII"));
    		case Global.GUIDEDLAUNCHER:
    			return (GameObject)(Resources.Load ("InventoryPrefabs/GuidedLauncher"));
    		case Global.HELLFIRE:
    			return (GameObject)(Resources.Load ("InventoryPrefabs/HellfireLauncher"));
    		case Global.MINIGUN:
    			return (GameObject)(Resources.Load ("InventoryPrefabs/Minigun"));
    		case Global.VELKOZ:
    			return (GameObject)(Resources.Load ("InventoryPrefabs/VelkozGun"));
            case Global.PLASMACOIL:
                return (GameObject)(Resources.Load("InventoryPrefabs/Plasmacoil"));
    		case Global.LAS1:
			    return (GameObject)(Resources.Load("InventoryPrefabs/LaserMKI"));
            case Global.FLAMETHROWER:
                return (GameObject)(Resources.Load("InventoryPrefabs/Flamethrower"));
            case Global.CANNON3:
                return (GameObject)(Resources.Load("InventoryPrefabs/CannonMkIII"));
            case Global.PLAYERICEWEP:
                return (GameObject)(Resources.Load("InventoryPrefabs/PlayerIceWeapon"));
            case Global.PLAYERICEWEP2:
                return (GameObject)(Resources.Load("InventoryPrefabs/PlayerIceWeaponMKII"));
            case Global.REDOX:
                return (GameObject)(Resources.Load("InventoryPrefabs/Redox"));
            case Global.REDOX2:
                return (GameObject)(Resources.Load("InventoryPrefabs/RedoxMKII"));
            case Global.SHOTGUN3:
                return (GameObject)(Resources.Load("InventoryPrefabs/ShotgunMKIII"));
            case Global.SPLITTER:
                return (GameObject)(Resources.Load("InventoryPrefabs/Splitter"));
            case Global.RNG2:
                return (GameObject)(Resources.Load("InventoryPrefabs/RNGMKII"));
            case Global.RNG3:
                return (GameObject)(Resources.Load("InventoryPrefabs/RNGMKIII"));
            case Global.RPR:
                return (GameObject)(Resources.Load("InventoryPrefabs/RPR"));
            case Global.RPR2:
                return (GameObject)(Resources.Load("InventoryPrefabs/RPRMKII"));
            case Global.DARKWEAPON:
                return (GameObject)(Resources.Load("InventoryPrefabs/DarkWeapon"));
            case Global.SATCANNON2:
                return (GameObject)(Resources.Load("InventoryPrefabs/SatelliteCannonMKII"));
            case Global.SNIPER2:
                return (GameObject)(Resources.Load("InventoryPrefabs/SniperMKII"));
            case Global.MJOLNIR:
                return (GameObject)(Resources.Load("InventoryPrefabs/Mjolnir"));
            case Global.HDRONEI:
                return (GameObject)(Resources.Load("InventoryPrefabs/HunterDrone"));
            case Global.HDRONEII:
                return (GameObject)(Resources.Load("InventoryPrefabs/HunterDroneMKII"));
            case Global.HDRONEIII:
                return (GameObject)(Resources.Load("InventoryPrefabs/HunterDroneMKIII"));
            case Global.BUZZSAW:
                return (GameObject)(Resources.Load("InventoryPrefabs/Buzzsaw"));
            case Global.MEGAMINIGUN:
                return (GameObject)(Resources.Load("InventoryPrefabs/Megaminigun"));
            case Global.BOUNCELAUNCHER:
                return (GameObject)(Resources.Load("InventoryPrefabs/BounceLauncher"));
            case Global.BLITZGUN:
                return (GameObject)(Resources.Load("InventoryPrefabs/BlitzGun"));
            case Global.RAILGUN:
                return (GameObject)(Resources.Load("InventoryPrefabs/Railgun"));
            default:
                return (GameObject)(Resources.Load("InventoryPrefabs/Basic"));
		}
	}

    //Comment for merging

	public void ReassignEquipped(){
		//equippedWeapons.Clear();
		//equippedWeapons.Add (equippedTransform.GetChild (0).GetComponent<WeaponDraggable>().weaponPrefab.GetComponent<WeaponModule>().weaponId);
		//equippedWeapons.Add (equippedTransform.GetChild (1).GetComponent<WeaponDraggable>().weaponPrefab.GetComponent<WeaponModule>().weaponId);
	}

	public void ReassignInventory(){
		playerInventory.ReassignInventory (weapons);
	}

	private void LateStart(){
		PopulateList ();
		for (int i = 0; i < equippedWeapons.Count; i++){
			GameObject g = Instantiate(GetWeaponUI (equippedWeapons [i])) as GameObject;
			g.transform.position = epos [i];
		}
	}

	public void RemoveEquipped(int i){
		equippedWeapons.Remove (i);
	}

	public void AddEquipped(int i){
		equippedWeapons.Add (i);
	}

	public void RemoveWeapon(int i){
		weapons.Remove (i);
	}

	public void AddWeapon(int i){
		weapons.Add (i);
	}

}
