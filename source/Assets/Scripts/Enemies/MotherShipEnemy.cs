using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotherShipEnemy : Enemy {

    public GameObject babyShip;

	// Use this for initialization
	protected override void Start () {
		base.Start ();
		health = 60;
		speed = 2f;
		originalSpeed = speed;
	}

	// Update is called once per frame
	protected override void Update () {
		base.Update ();
	}

	public override void Shoot(){
		foreach(WeaponEnemy wep in weapons){
			wep.Shoot ();
		}
	}

	public override void Death(){
        SpawnBabies();
        Invoke("ActualDeath", 0.001f);
    }

    private void SpawnBabies() {
        Vector3 testV = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        Instantiate(babyShip, testV, Quaternion.Euler(0, 0, transform.eulerAngles.z + 90.0f));
        Instantiate(babyShip, testV, Quaternion.Euler(0, 0, transform.eulerAngles.z + 135.0f));
        Instantiate(babyShip, testV, Quaternion.Euler(0, 0, transform.eulerAngles.z - 90));
        Instantiate(babyShip, testV, Quaternion.Euler(0, 0, transform.eulerAngles.z - 135.0f));
        Debug.Log("spawned");
    }

    private void ActualDeath() {
        Destroy(gameObject);
    }
}
