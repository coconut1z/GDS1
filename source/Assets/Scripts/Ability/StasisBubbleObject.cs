using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StasisBubbleObject : MonoBehaviour {

    private float tempSpeed, delay, timer;
    private List<GameObject> affectedEnemies;

	// Use this for initialization
	void Start () {
        affectedEnemies = new List<GameObject>();
	}
	
	// Update is called once per frame
	void Update () {
	}

    void OnTriggerStay2D(Collider2D other) {
        if (other.gameObject.tag == "Enemy") {
            other.gameObject.GetComponent<Enemy>().speedMultiplier = 0.5f;
            other.gameObject.GetComponent<Enemy>().canShoot = false;
            affectedEnemies.Add(other.gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.tag == "Enemy") {
            other.gameObject.GetComponent<Enemy>().speedMultiplier = 1f;
            other.gameObject.GetComponent<Enemy>().canShoot = true;
            affectedEnemies.Add(other.gameObject);
        }
    }
}
