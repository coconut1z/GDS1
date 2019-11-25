using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//GHETTO WEAPON CODE

public class WeaponManagerScript : MonoBehaviour {

    public WeaponModule[] weaponList;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void DropRandomWeapon(Vector2 pos) {
        //int randomWeaponPos = Random.Range(0, weaponList.Length - 1);
        //WeaponModule randomWeapon = weaponList[randomWeaponPos];
        //Instantiate(randomWeapon, pos, Quaternion.identity);
        //print("Weapon Dropped");
    }
}
