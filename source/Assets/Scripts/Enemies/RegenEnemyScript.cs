using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegenEnemyScript : Enemy {

    private float maxHealth;
    public GameObject regenEffect;

	// Use this for initialization
	protected override void Start () {
		base.Start ();
		if(difficulty == Global.RECRUIT){
			
		}else if(difficulty == Global.VETEREN){
			
		}else if(difficulty == Global.BATTLEH){
			
		}
		speed = 3f;
		health = 27;
        maxHealth = health;
		originalSpeed = speed;
        regenEffect.SetActive(false);
        StartCoroutine(Regenerate());
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
		Destroy (gameObject);
	}

    public IEnumerator Regenerate() {
        while (true) {
            if (health < maxHealth) {
                regenEffect.SetActive(true);
                health += 3;
                yield return new WaitForSeconds(1);
            }
            else {
                regenEffect.SetActive(false);
                yield return null;
            }
        }
    }
}
