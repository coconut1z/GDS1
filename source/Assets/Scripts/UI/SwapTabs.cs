using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwapTabs : MonoBehaviour {

	public GameObject weaponScroll;
	public GameObject abilityScroll;
	public Image abilityImage;
	public Image weaponImage;

	void Start(){
	}

	public void SwapToAbility(){
		if(Global.onWeapon){
			Destroy (transform.parent.GetComponent<EquipmentDragger> ());
			transform.parent.gameObject.AddComponent<AbilityDragger> ();
			weaponScroll.SetActive (false);
			abilityScroll.SetActive (true);
			Global.onWeapon = false;
			abilityImage.color = new Color (abilityImage.color.r, abilityImage.color.b, abilityImage.color.g, 100f / 255f);
			weaponImage.color = new Color (weaponImage.color.r, weaponImage.color.b, weaponImage.color.g, 150f / 255f);
		}
	}

	public void SwapToWeapon(){
		if(!Global.onWeapon){
			Destroy (transform.parent.GetComponent<AbilityDragger> ());
			transform.parent.gameObject.AddComponent<EquipmentDragger> ();
			weaponScroll.SetActive (true);
			abilityScroll.SetActive (false);
			Global.onWeapon = true;
			weaponImage.color = new Color (weaponImage.color.r, weaponImage.color.b, weaponImage.color.g, 100f / 255f);
			abilityImage.color = new Color (abilityImage.color.r, abilityImage.color.b, abilityImage.color.g, 150f / 255f);
		}
	}
}
