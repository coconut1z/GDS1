using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CarrierBossHealthUI : MonoBehaviour {
    public Text phaseIndicator;
    public Image outline, phaseOne, phaseTwo, phaseThree, phaseFour, phaseFive, phaseSix;
    public GameObject boss;
    private Stage3Boss S3boss;
    private bool shiftHealthBar;
    
	// Use this for initialization
	void Start () {
        boss = GameObject.Find("Stage3Boss");
        S3boss = boss.GetComponent<Stage3Boss>();
        shiftHealthBar = false;
        phaseIndicator.text = "||||||";
	}
	
	// Update is called once per frame
    //This is disgusting but i'll make it nicer later
	void Update () {
        if (shiftHealthBar) {
            if (transform.position.y < 2.5f) { 
                transform.Translate(Vector3.up * Time.deltaTime);
            }
        }
        if (boss && S3boss.deadParts != 4) {
            phaseIndicator.text = "|||||";
            float currentHealth = S3boss.health;
            if (S3boss.health <= S3boss.phase1 && S3boss.health > S3boss.phase2) {
                currentHealth = (S3boss.health - S3boss.phase2) / (S3boss.phase1 - S3boss.phase2);
                phaseOne.fillAmount = currentHealth;
            }
            else if (S3boss.health <= S3boss.phase2 && S3boss.health > S3boss.phase3) {
                phaseIndicator.text = "||||";
                phaseOne.fillAmount = 0;
                currentHealth = (S3boss.health - S3boss.phase3) / (S3boss.phase2 - S3boss.phase3);
                phaseTwo.fillAmount = currentHealth;
            }
            else if (S3boss.health <= S3boss.phase3 && S3boss.health > S3boss.phase4) {
                phaseIndicator.text = "|||";
                phaseTwo.fillAmount = 0;
                currentHealth = (S3boss.health - S3boss.phase4) / (S3boss.phase3 - S3boss.phase4);
                phaseThree.fillAmount = currentHealth;
            }
            else if (S3boss.health <= S3boss.phase4 && S3boss.health > S3boss.phase5) {
                phaseIndicator.text = "||";
                phaseThree.fillAmount = 0;
                currentHealth = (S3boss.health - S3boss.phase5) / (S3boss.phase4 - S3boss.phase5);
                phaseFour.fillAmount = currentHealth;
            }
            else if (S3boss.health <= S3boss.phase5 && S3boss.health > S3boss.phase6) {
                phaseIndicator.text = "|";
                phaseFour.fillAmount = 0;
                currentHealth = (S3boss.health - S3boss.phase6) / (S3boss.phase5 - S3boss.phase6);
                phaseFive.fillAmount = currentHealth;
            }
            else if (S3boss.health <= S3boss.phase6) {
                phaseIndicator.text = "";
                shiftHealthBar = true;
                phaseFive.fillAmount = 0;
                if (S3boss.deadParts > 0) {
                    currentHealth = (4 - (S3boss.deadParts)) * 0.25f;
                    phaseSix.fillAmount = currentHealth;
                }
            }
        }
        else {
            gameObject.SetActive(false);
        }
	}
}
