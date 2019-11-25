using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VictoryScreen : MonoBehaviour {

    public Text continuesText;
    public Text difficultyText;
    public Text timeTakenText;
    public GameObject player;

	// Use this for initialization
	void Start () {
        player = GameObject.FindWithTag("Player");
        continuesText.text = "Continues used: " + player.GetComponent<PlayerController>().continuesUsed;
        if (Global.bossMedley) {
            timeTakenText.text = player.GetComponent<PlayerController>().timeTakenText;
        }
        else {
            timeTakenText.text = "";
        }

        if (Global.Difficulty == 1) {
            difficultyText.text = "Difficulty: Recruit";
        }
        else if (Global.Difficulty == 2)
        {
            difficultyText.text = "Difficulty: Veteran";
        }
        else if (Global.Difficulty == 3)
        {
            difficultyText.text = "Difficulty: Battle Hardened";
        }
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    public void LoadTitle() {
        SceneManager.LoadScene("Title");
    }
}
