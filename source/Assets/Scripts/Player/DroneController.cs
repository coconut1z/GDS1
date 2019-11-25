using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DroneController : MonoBehaviour {

	public List<WeaponModule> weaponModules;
	public GameObject[] weaponSlots;
    public static bool canMove;
    public static bool canShoot;
	private SpriteRenderer sr;
	private int maxWeapons;
    private int maxAbilities;
	private float speed;
	private float health;
	private bool isQuitting;
	private float invulnTimer;
	private float invulnDelay;
    private float abilityRechargeTimer;
    private float abilityRechargeDelay;



	// Use this for initialization
	void Start () {
        weaponModules = new List<WeaponModule>();
		sr = GetComponent<SpriteRenderer> ();
		maxWeapons = 2;
		Invoke ("LateStart", 0.01f);
        Invoke("Suicide", 8f);
		canShoot = true;

	}
	
	// Update is called once per frame
	void Update () {
        if (canShoot) {
            Shoot();
        }
	}
		
	private void findDefaultWeapons(){
        GameObject[] weapons = GameObject.FindGameObjectsWithTag("Weapon");
		for(int i = 0; i < weapons.Length ; i++){
			for(int j = 0; j < weaponSlots.Length ; j++){
				if(weapons[i].transform.parent == weaponSlots[j].transform){
					weaponModules.Add (weapons [i].GetComponent<WeaponModule> ());
					weaponModules [weaponModules.Count-1].equipped = true;
					weaponModules [weaponModules.Count-1].sr.sortingOrder = 2;
					j = weaponSlots.Length;
                    //Ghetto weapon code
                    weapons[i].GetComponent<WeaponModule>().Dropped(false);
				}
			}

		}
	}

	private void Shoot(){
		if(InputKeys.isDown(InputKeys.SHOOT)){
			foreach(WeaponModule wep in weaponModules){
				wep.Shoot ();
			}
		}
	}

	private void OnApplicationQuit(){
		foreach (Projectile o in Object.FindObjectsOfType<Projectile>()) {
			DestroyImmediate(o);
		}
	}

	private void LateStart(){
		findDefaultWeapons ();
	}

    private void Suicide() {
        Destroy(gameObject);
    }
}
