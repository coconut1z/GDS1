using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HospitalShip : Enemy {

    // Use this for initialization
    protected override void Start() {
        base.Start();
        health = 60;
        speed = 3f;
        originalSpeed = speed;
    }

    // Update is called once per frame
    protected override void Update() {
        base.Update();
    }

    public override void Shoot() {
        //no weapons, will not shoot.
    }

    public override void Death() {
        PlayerController playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        if(playerScript.health <= 4.01f) { //HARD CODED. playerController has no maxHealth atm, nor an official healing function. May revisit. Doubt it.
            playerScript.health++;
            Debug.Log("player healed by hospital to " + playerScript.health);
        }
        Destroy(gameObject);
    }
}
