using UnityEngine;
using System.Collections;

public class HealthParticles: MonoBehaviour
{

    public GameObject health1;
    public GameObject health2;
    public GameObject health3;
    public GameObject health4;
    public GameObject health5;
    private GameObject player;
    private PlayerController playerController;

	// Use this for initialization
	void Start()
	{
        player = GameObject.FindWithTag("Player");
        playerController = player.GetComponent<PlayerController>();
	}

	// Update is called once per frame
	void Update()
	{
        this.transform.Rotate(0, 0, Time.deltaTime * 50f);
        this.transform.position = player.transform.position;
        if (playerController.health == 5) {
            health1.SetActive(true);
            health2.SetActive(true);
            health3.SetActive(true);
            health4.SetActive(true);
            health5.SetActive(true);
        }
        if (playerController.health == 4)
        {
            health1.SetActive(false);
            health2.SetActive(true);
            health3.SetActive(true);
            health4.SetActive(true);
            health5.SetActive(true);
        }
        if (playerController.health == 3)
        {
            health1.SetActive(false);
            health2.SetActive(true);
            health3.SetActive(false);
            health4.SetActive(true);
            health5.SetActive(true);
        }
        if (playerController.health == 2)
        {
            health1.SetActive(false);
            health2.SetActive(true);
            health3.SetActive(false);
            health4.SetActive(false);
            health5.SetActive(true);
        }
        if (playerController.health == 1)
        {
            health1.SetActive(false);
            health2.SetActive(false);
            health3.SetActive(false);
            health4.SetActive(false);
            health5.SetActive(true);
        }
        if (playerController.health == 0)
        {
            health1.SetActive(false);
            health2.SetActive(false);
            health3.SetActive(false);
            health4.SetActive(false);
            health5.SetActive(false);
        }
	}
}
