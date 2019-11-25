using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoPanelScriptHover : MonoBehaviour {

	private Text nameT, damage, firerate;
	private Image extra;
	private Image extraActual;
	private WeaponDraggable wep;
	private Image image;

	// Use this for initialization
	void Start () {
		nameT = transform.GetChild (0).GetComponent<Text> ();
		damage = transform.GetChild (1).GetComponent<Text> ();
		firerate = transform.GetChild (2).GetComponent<Text> ();
		extra = transform.GetChild (3).GetComponent<Image> ();
		extraActual = transform.GetChild (3).GetChild(0).GetComponent<Image>();
		image = GetComponent<Image> ();
	}
	
	// Update is called once per frame
	void Update () {
		/*
		wep = transform.GetChild (4).GetComponent<WeaponDraggable> ();
		//if(wep != null && nameT.text != wep.nameUI){
		if(wep != null && (wep.nameUI != nameT.text || image.color != TierColor(wep.getWeaponId()))){
			nameT.text = wep.nameUI;
			//damage.text = "Damage: " + wep.damageUI.ToString();
			//firerate.text = "Firerate: " + wep.firerateUI.ToString ();
			for(int i = 0; i < 10; i++){
				damage.transform.GetChild (i).GetComponent<Image> ().color = new Color (100f/255f, 100f/255f, 100f/255f);
				firerate.transform.GetChild (i).GetComponent<Image> ().color = new Color (100f/255f, 100f/255f, 100f/255f);
			}
			for(int i = 0; i < (int)wep.damageUI && i < 10; i++){
				damage.transform.GetChild (i).GetComponent<Image> ().color = new Color (1f, 1f, 1f);
			}
			for(int i = 0; i < (int)wep.firerateUI && i < 10; i++){
				firerate.transform.GetChild (i).GetComponent<Image> ().color = new Color (1f, 1f, 1f);
			}
			extra.color = TierColor (wep.getWeaponId ());
			image.color = TierColor (wep.getWeaponId ());
			Sprite a = GetExtra (wep.getWeaponId ());
			if(a != null){
				extraActual.sprite = a;
			}
		}
		*/
	}


	public void HoverSet(string name, int dmg, int spd, WeaponDraggable w){
		wep = w;
		nameT = transform.GetChild (0).GetComponent<Text> ();
		damage = transform.GetChild (1).GetComponent<Text> ();
		firerate = transform.GetChild (2).GetComponent<Text> ();
		extra = transform.GetChild (3).GetComponent<Image> ();
		extraActual = transform.GetChild (3).GetChild(0).GetComponent<Image>();
		image = GetComponent<Image> ();
		nameT.text = wep.nameUI;
		//damage.text = "Damage: " + wep.damageUI.ToString();
		//firerate.text = "Firerate: " + wep.firerateUI.ToString ();
		for(int i = 0; i < 10; i++){
			damage.transform.GetChild (i).GetComponent<Image> ().color = new Color (100f/255f, 100f/255f, 100f/255f);
			firerate.transform.GetChild (i).GetComponent<Image> ().color = new Color (100f/255f, 100f/255f, 100f/255f);
		}
		for(int i = 0; i < (int)wep.damageUI && i < 10; i++){
			damage.transform.GetChild (i).GetComponent<Image> ().color = new Color (1f, 1f, 1f);
		}
		for(int i = 0; i < (int)wep.firerateUI && i < 10; i++){
			firerate.transform.GetChild (i).GetComponent<Image> ().color = new Color (1f, 1f, 1f);
		}
		extra.color = TierColor (wep.getWeaponId ());
		image.color = TierColor (wep.getWeaponId ());
		Sprite a = GetExtra (wep.getWeaponId ());
		if(a != null){
			extraActual.sprite = a;
		}

	}

	private Color TierColor(int ID){
		if(ID < 199){
			return new Color (255f / 255f, 255f / 255f, 255f / 255f);
		}else if(ID < 299){
			return new Color (208f / 255f, 236f / 255f, 255f / 255f);
		}else if(ID < 399){
			return new Color (202f / 255f, 204f / 255f, 255f / 255f);
		}else if(ID < 499){
			return new Color (214f / 255f, 181f / 255f, 216f / 255f);
		}else if(ID < 599){
			return new Color (255f / 255f, 216f / 255f, 146f / 255f);
		}else if(ID < 699){
			return new Color (255f / 255f, 173f / 255f, 184f / 255f);
		}
		return new Color(1f,1f,1f);
	}

	private Sprite GetExtra(int weaponId){
		switch(weaponId)
		{
		case Global.BASIC:
			return Resources.Load <Sprite>("Extra/Basic");
		case Global.SHOTGUN:
			return Resources.Load <Sprite>("Extra/Shotgun1");
		case Global.CANNON:
			return Resources.Load <Sprite>("Extra/Cannon1");
		case Global.FLAK:
			return Resources.Load <Sprite>("Extra/FlakGun");
		case Global.SATCANNON:
			return Resources.Load <Sprite>("Extra/SatelliteCannon");
		case Global.SNIPER:
			return Resources.Load <Sprite>("Extra/Sniper");
		case Global.RNG:
			return Resources.Load <Sprite>("Extra/RNG1");
		case Global.CANNON2:
			return Resources.Load <Sprite>("Extra/Cannon2");
		case Global.SHOTGUN2:
			return Resources.Load <Sprite>("Extra/Shotgun2");
		case Global.GUIDEDLAUNCHER:
			return Resources.Load <Sprite>("Extra/GuidedMissile");
		case Global.HELLFIRE:
			return Resources.Load <Sprite>("Extra/Hellfire");
		case Global.MINIGUN:
			return Resources.Load <Sprite>("Extra/Minigun");
		case Global.VELKOZ:
			return Resources.Load <Sprite>("Extra/Velkoz");
		case Global.PLASMACOIL:
			return Resources.Load <Sprite>("Extra/Plasmacoil");
		case Global.LAS1:
			return Resources.Load <Sprite>("Extra/Laser1");
		case Global.FLAMETHROWER:
			return Resources.Load <Sprite>("Extra/Flamethrower");
		case Global.CANNON3:
			return Resources.Load <Sprite>("Extra/Cannon3");
		case Global.PLAYERICEWEP:
			return Resources.Load <Sprite>("Extra/PlayerIce");
		case Global.PLAYERICEWEP2:
			return Resources.Load <Sprite>("Extra/PlayerIce2");
		case Global.REDOX:
			return Resources.Load <Sprite>("Extra/Redox");
		case Global.REDOX2:
			return Resources.Load <Sprite>("Extra/RedoxMKII");
		case Global.SHOTGUN3:
			return Resources.Load <Sprite>("Extra/Shotgun3");
		case Global.SPLITTER:
			return Resources.Load <Sprite>("Extra/Splitter");
		case Global.RNG2:
			return Resources.Load <Sprite>("Extra/RNG2");
		case Global.RNG3:
			return Resources.Load <Sprite>("Extra/RNG3");
		case Global.RPR:
			return Resources.Load <Sprite>("Extra/RPR1");
		case Global.RPR2:
			return Resources.Load <Sprite>("Extra/RPR2");
		case Global.DARKWEAPON:
			return Resources.Load <Sprite>("Extra/DarkWeapon");
		case Global.SATCANNON2:
			return Resources.Load <Sprite>("Extra/SatelliteCannonMKII");
		case Global.SNIPER2:
			return Resources.Load <Sprite>("Extra/SniperMKII");
		case Global.MJOLNIR:
			return Resources.Load <Sprite>("Extra/Mjolnir");
		case Global.HDRONEI:
			return Resources.Load <Sprite>("Extra/HD1");
		case Global.HDRONEII:
			return Resources.Load <Sprite>("Extra/HD2");
		case Global.HDRONEIII:
			return Resources.Load <Sprite>("Extra/HD3");
		case Global.BUZZSAW:
			return Resources.Load <Sprite>("Extra/Buzzsaw");
		case Global.MEGAMINIGUN:
			return Resources.Load <Sprite>("Extra/Megaminigun");
		case Global.BOUNCELAUNCHER:
			return Resources.Load <Sprite>("Extra/BounceBlade");
		case Global.BLITZGUN:
			return Resources.Load <Sprite>("Extra/BlitzGun");
		case Global.RAILGUN:
			return Resources.Load <Sprite>("Extra/ParticleCannon");
		default:
			return Resources.Load <Sprite>("Extra/Basic");
		}
	}
}
