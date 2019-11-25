using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponEnemy : MonoBehaviour {

	public Transform player;
	public Transform[] shootPos; // Positions where bullets spawn
	public GameObject[] projectiles; // Bullet Prefabs
	public virtual float shootTime { get; set;} // How much Time.DeltaTime has it been since last shot
	public virtual float shootDelay { get; set;} // How much Time.DeltaTime to shoot
	public virtual float spread { get; set;}
	protected int difficulty;

	// Use this for initialization
	protected virtual void Start () {
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		shootTime = 0;
		shootDelay = 0;
		spread = 0;
		difficulty = Global.Difficulty;
	}

	// Update is called once per frame
	protected virtual void Update () {
		shootTime += Time.deltaTime;
	}

	// Shoot functionality for weapon
	public abstract void Shoot();

	// Rotates the weapon towards mouse
	protected virtual void lookAtPlayer(){
		float AngleRadToMouse = Mathf.Atan2 (
			player.position.y - transform.position.y,
			player.position.x - transform.position.x
		);
		float AngleToDeg = (180 / Mathf.PI) * AngleRadToMouse - 90;
		transform.rotation = Quaternion.Euler (0, 0, AngleToDeg);
	}
}	