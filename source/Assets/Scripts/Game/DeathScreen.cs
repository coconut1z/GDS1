using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DeathScreen : MonoBehaviour {

    GameObject player;
    PlayerController playerController;
    GameObject[] bullets;
    public GameObject reviveParticles;

	// Use this for initialization
	void Start () {
        Time.timeScale = 0.0f;
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<PlayerController>();
        bullets = GameObject.FindGameObjectsWithTag("PlayerBullet");
	}
	
	// Update is called once per frame
	void Update () {
        
	}


    public void LoadTitle() {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("Title");
    }

    public void Continue() {
        playerController.lives = 3f;
        playerController.UpdateLives();
        playerController.health = 5f;
        Time.timeScale = 1.0f;
        DestroyAllBullets();
        player.GetComponent<PlayerController>().ResumeGame();
        SceneManager.UnloadSceneAsync("Death");
    }

    void DestroyAllBullets() {
        foreach(GameObject bullet in bullets) {
            Destroy(bullet.gameObject);
        }
    }
}
