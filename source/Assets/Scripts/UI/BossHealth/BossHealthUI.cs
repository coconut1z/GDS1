using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class BossHealthUI : MonoBehaviour {
    public Image outline, phaseOne, phaseTwo, phaseThree, phaseFour, phaseFive, phaseSix, phaseSeven, phaseEight;
    public Text phaseIndicator;
    public GameObject boss;
    private Stage2Boss S2boss;
    
	// Use this for initialization
	void Start () {
        boss = GameObject.Find("Stage2Boss");
        S2boss = boss.GetComponent<Stage2Boss>();
        phaseIndicator.text = "8";
	}
	
	// Update is called once per frame
    //This is disgusting but i'll make it nicer later
	void Update () {
        if (boss) {
            float currentHealth = S2boss.health;
            phaseIndicator.text = "|||||||";
            if (S2boss.health <= S2boss.phase1 && S2boss.health > S2boss.phase2) {
                currentHealth = (S2boss.health - S2boss.phase2) / (S2boss.phase1 - S2boss.phase2);
                phaseOne.fillAmount = currentHealth;
            }
            else if (S2boss.health <= S2boss.phase2 && S2boss.health > S2boss.phase3) {
                phaseIndicator.text = "||||||";
                phaseOne.fillAmount = 0;
                currentHealth = (S2boss.health - S2boss.phase3) / (S2boss.phase2 - S2boss.phase3);
                phaseTwo.fillAmount = currentHealth;
            }
            else if (S2boss.health <= S2boss.phase3 && S2boss.health > S2boss.phase4) {
                phaseIndicator.text = "|||||";
                phaseTwo.fillAmount = 0;
                currentHealth = (S2boss.health - S2boss.phase4) / (S2boss.phase3 - S2boss.phase4);
                phaseThree.fillAmount = currentHealth;
            }
            else if (S2boss.health <= S2boss.phase4 && S2boss.health > S2boss.phase5) {
                phaseIndicator.text = "||||";
                phaseThree.fillAmount = 0;
                currentHealth = (S2boss.health - S2boss.phase5) / (S2boss.phase4 - S2boss.phase5);
                phaseFour.fillAmount = currentHealth;
            }
            else if (S2boss.health <= S2boss.phase5 && S2boss.health > S2boss.phase6) {
                phaseIndicator.text = "|||";
                phaseFour.fillAmount = 0;
                currentHealth = (S2boss.health - S2boss.phase6) / (S2boss.phase5 - S2boss.phase6);
                phaseFive.fillAmount = currentHealth;
            }
            else if (S2boss.health <= S2boss.phase6 && S2boss.health > S2boss.phase7) {
                phaseIndicator.text = "||";
                phaseFive.fillAmount = 0;
                currentHealth = (S2boss.health - S2boss.phase7) / (S2boss.phase6 - S2boss.phase7);
                phaseSix.fillAmount = currentHealth;
            }
            else if (S2boss.health <= S2boss.phase7 && S2boss.health > S2boss.phase8) {
                phaseIndicator.text = "|";
                phaseSix.fillAmount = 0;
                currentHealth = (S2boss.health - S2boss.phase8) / (S2boss.phase7 - S2boss.phase8);
                phaseSeven.fillAmount = currentHealth;
            }
            else if (S2boss.health <= S2boss.phase8) {
                phaseIndicator.text = "";
                phaseSeven.fillAmount = 0;
                currentHealth = (S2boss.health) / (S2boss.phase8);
                phaseEight.fillAmount = currentHealth;
            }
        }
        else {
            gameObject.SetActive(false);
        }
	}
}
