using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WepDrop : MonoBehaviour {

    private AudioManager audioManager;

	public int wepID;
	private SpriteRenderer child;
	private PlayerInventory p;
	private bool added;
	private Transform player;

	// Use this for initialization
	void Start () {
		//wepID = 107;
		child = transform.GetChild (0).GetComponent<SpriteRenderer>();
		p = GameObject.FindGameObjectWithTag ("PlayerInventory").GetComponent<PlayerInventory> ();
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		added = false;
        audioManager = AudioManager.instance;
	}
	
	// Update is called once per frame
	void Update () {
		if(wepID != 0){
			GameObject g = GetWeaponUI (wepID);
			child.sprite = g.GetComponent<Image> ().sprite;
		}
		if(Vector2.Distance(player.position, transform.position) < 3.0f){
			transform.position = Vector2.MoveTowards (transform.position, player.position, Time.deltaTime * 3);
		}else{
			transform.Translate (Vector2.down * Time.deltaTime);
			if(transform.position.y < -10){
				Destroy (gameObject);
			}
		}

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

	private void OnTriggerEnter2D(Collider2D c){
		if(!added && c.gameObject.tag == "Player"){
			p.addWeapon (wepID);
			Destroy (gameObject);
			added = true;
            audioManager.PlaySound("PickupWeapon");
		}
	}

}
