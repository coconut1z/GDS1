using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class StationBoss : MonoBehaviour {
    public Image outline, phaseOne, phaseTwo, phaseThree, phaseFour, phaseFive, phaseSix, phaseSeven, phaseEight, phaseNine;
    public Text phaseIndicator;
    public GameObject boss;
    private Stage5Boss S5boss;
    
	// Use this for initialization
	void Start () {
        boss = GameObject.Find("Stage5Boss");
        S5boss = boss.GetComponent<Stage5Boss>();
        phaseIndicator.text = "9";
	}
	
	// Update is called once per frame
    //This is disgusting but i'll make it nicer later
	void Update () {
        if (boss) {
            float currentHealth = S5boss.health;
            phaseIndicator.text = "||||||||";
            if (S5boss.health <= S5boss.phase1 && S5boss.health > S5boss.phase2) {
                currentHealth = (S5boss.health - S5boss.phase2) / (S5boss.phase1 - S5boss.phase2);
                phaseOne.fillAmount = currentHealth;
            }
            else if (S5boss.health <= S5boss.phase2 && S5boss.health > S5boss.phase3) {
                phaseIndicator.text = "|||||||";
                phaseOne.fillAmount = 0;
                currentHealth = (S5boss.health - S5boss.phase3) / (S5boss.phase2 - S5boss.phase3);
                phaseTwo.fillAmount = currentHealth;
            }
            else if (S5boss.health <= S5boss.phase3 && S5boss.health > S5boss.phase4) {
                phaseIndicator.text = "||||||";
                phaseTwo.fillAmount = 0;
                currentHealth = (S5boss.health - S5boss.phase4) / (S5boss.phase3 - S5boss.phase4);
                phaseThree.fillAmount = currentHealth;
            }
            else if (S5boss.health <= S5boss.phase4 && S5boss.health > S5boss.phase5) {
                phaseIndicator.text = "|||||";
                phaseThree.fillAmount = 0;
                currentHealth = (S5boss.health - S5boss.phase5) / (S5boss.phase4 - S5boss.phase5);
                phaseFour.fillAmount = currentHealth;
            }
            else if (S5boss.health <= S5boss.phase5 && S5boss.health > S5boss.phase6) {
                phaseIndicator.text = "||||";
                phaseFour.fillAmount = 0;
                currentHealth = (S5boss.health - S5boss.phase6) / (S5boss.phase5 - S5boss.phase6);
                phaseFive.fillAmount = currentHealth;
            }
            else if (S5boss.health <= S5boss.phase6 && S5boss.health > S5boss.phase7) {
                phaseIndicator.text = "|||";
                phaseFive.fillAmount = 0;
                currentHealth = (S5boss.health - S5boss.phase7) / (S5boss.phase6 - S5boss.phase7);
                phaseSix.fillAmount = currentHealth;
            }
            else if (S5boss.health <= S5boss.phase7 && S5boss.health > S5boss.phase8) {
                phaseIndicator.text = "||";
                phaseSix.fillAmount = 0;
                currentHealth = (S5boss.health - S5boss.phase8) / (S5boss.phase7 - S5boss.phase8);
                phaseSeven.fillAmount = currentHealth;
            }
            else if (S5boss.health <= S5boss.phase8 && S5boss.health > S5boss.phase9) {
                phaseIndicator.text = "|";
                phaseSeven.fillAmount = 0;
                currentHealth = (S5boss.health - S5boss.phase9) / (S5boss.phase8 - S5boss.phase9);
                phaseEight.fillAmount = currentHealth;
            }
            else if (S5boss.health <= S5boss.phase9) {
                phaseIndicator.text = "";
                phaseEight.fillAmount = 0;
                currentHealth = (S5boss.health) / (S5boss.phase9);
                phaseNine.fillAmount = currentHealth;
            }
        }
        else {
            gameObject.SetActive(false);
        }
	}
}
