using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SuperclusterBossUI : MonoBehaviour {
    public Image outline, phaseOne, phaseTwo, phaseThree, phaseFour, phaseFive, phaseSix, phaseSeven, phaseEight;
    public Text phaseIndicator;
    public GameObject boss;
    private Stage6Boss S6boss;
    
	// Use this for initialization
	void Start () {
        boss = GameObject.Find("Supercluster");
        S6boss = boss.GetComponent<Stage6Boss>();
        phaseIndicator.text = "8";
	}
	
	// Update is called once per frame
    //This is disgusting but i'll make it nicer later
	void Update () {
        if (boss) {
            float currentHealth = S6boss.health;
            phaseIndicator.text = "|||||||";
            if (S6boss.health <= S6boss.phase1 && S6boss.health > S6boss.phase2) {
                currentHealth = (S6boss.health - S6boss.phase2) / (S6boss.phase1 - S6boss.phase2);
                phaseOne.fillAmount = currentHealth;
            }
            else if (S6boss.health <= S6boss.phase2 && S6boss.health > S6boss.phase3) {
                phaseIndicator.text = "||||||";
                phaseOne.fillAmount = 0;
                currentHealth = (S6boss.health - S6boss.phase3) / (S6boss.phase2 - S6boss.phase3);
                phaseTwo.fillAmount = currentHealth;
            }
            else if (S6boss.health <= S6boss.phase3 && S6boss.health > S6boss.phase4) {
                phaseIndicator.text = "|||||";
                phaseTwo.fillAmount = 0;
                currentHealth = (S6boss.health - S6boss.phase4) / (S6boss.phase3 - S6boss.phase4);
                phaseThree.fillAmount = currentHealth;
            }
            else if (S6boss.health <= S6boss.phase4 && S6boss.health > S6boss.phase5) {
                phaseIndicator.text = "||||";
                phaseThree.fillAmount = 0;
                currentHealth = (S6boss.health - S6boss.phase5) / (S6boss.phase4 - S6boss.phase5);
                phaseFour.fillAmount = currentHealth;
            }
            else if (S6boss.health <= S6boss.phase5 && S6boss.health > S6boss.phase6) {
                phaseIndicator.text = "|||";
                phaseFour.fillAmount = 0;
                currentHealth = (S6boss.health - S6boss.phase6) / (S6boss.phase5 - S6boss.phase6);
                phaseFive.fillAmount = currentHealth;
            }
            else if (S6boss.health <= S6boss.phase6 && S6boss.health > S6boss.phase7) {
                phaseIndicator.text = "||";
                phaseFive.fillAmount = 0;
                currentHealth = (S6boss.health - S6boss.phase7) / (S6boss.phase6 - S6boss.phase7);
                phaseSix.fillAmount = currentHealth;
            }
            else if (S6boss.health <= S6boss.phase7 && S6boss.health > S6boss.phase8) {
                phaseIndicator.text = "|";
                phaseSix.fillAmount = 0;
                currentHealth = (S6boss.health - S6boss.phase8) / (S6boss.phase7 - S6boss.phase8);
                phaseSeven.fillAmount = currentHealth;
            }
            else if (S6boss.health <= S6boss.phase8) {
                phaseIndicator.text = "";
                phaseSeven.fillAmount = 0;
                currentHealth = (S6boss.health) / (S6boss.phase8);
                phaseEight.fillAmount = currentHealth;
                Invoke("Suicide", 5f);
            }
        }
        else {
            gameObject.SetActive(false);
        }
	}

    private void Suicide() {
        gameObject.SetActive(false);
    }
}
