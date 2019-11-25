using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour {

    public CanvasGroup pauseUI;
    public bool paused;

	// Use this for initialization
	void Start () {
        pauseUI.gameObject.SetActive(false);
        paused = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (!paused) {
                OpenMenu();
            }
            else {
                ResumeGame();
            }
        }
	}

    void OpenMenu() {
        pauseUI.gameObject.SetActive(true);
        Time.timeScale = 0;
        paused = true;
    }

    public void TitleScreen() {
        SceneManager.LoadScene("Title");
		Time.timeScale = 1;
    }

    public void ResumeGame() {
        pauseUI.gameObject.SetActive(false);
        Time.timeScale = 1;
        paused = false;
    }
}
