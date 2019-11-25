using UnityEngine;
using System.Collections;

public class SuperShipParticles : MonoBehaviour
{

    private GameObject player;
	// Use this for initialization
	void Start()
	{
        player = GameObject.FindWithTag("Player");
	}

	// Update is called once per frame
	void Update()
	{
        this.transform.position = player.transform.position;
	}
}
