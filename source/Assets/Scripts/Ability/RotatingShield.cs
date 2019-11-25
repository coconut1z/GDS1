using UnityEngine;
using System.Collections;

public class RotatingShield : MonoBehaviour
{
    public GameObject player;
	// Use this for initialization
	void Start()
	{
        player = GameObject.FindWithTag("Player");
	}

	// Update is called once per frame
	void Update()
	{
        this.transform.Rotate(new Vector3(0, 0, Time.deltaTime * 90));
        this.transform.position = player.transform.position;
	}
}
