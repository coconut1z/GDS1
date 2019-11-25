using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidLevelChanger : MonoBehaviour {

    public int newAsteroidLevel;
    private GameObject snek;

	// Use this for initialization
	void Start () {
        snek = GameObject.Find("SnekHead");
        Global.asteroidLevel = newAsteroidLevel;
	}
	
	// Update is called once per frame
	void Update () {
        if (snek != null) {
            if (snek.GetComponent<SnekHead>().segmentsDestroyed >= 4 && snek.GetComponent<SnekHead>().segmentsDestroyed < 8) {
                Global.asteroidLevel = 4;
            }
            if (snek.GetComponent<SnekHead>().segmentsDestroyed >= 8 && snek.GetComponent<SnekHead>().segmentsDestroyed < 12) {
                Global.asteroidLevel = 10;
            }
            if (snek.GetComponent<SnekHead>().segmentsDestroyed >= 12 && snek.GetComponent<SnekHead>().segmentsDestroyed < 16) {
                Global.asteroidLevel = 9;
            }
            if (snek.GetComponent<SnekHead>().segmentsDestroyed >= 16 && snek.GetComponent<SnekHead>().segmentsDestroyed < 20) {
                Global.asteroidLevel = 11;
            }
            if (snek.GetComponent<SnekHead>().segmentsDestroyed >= 20 && snek.GetComponent<SnekHead>().segmentsDestroyed < 23) {
                Global.asteroidLevel = 12;
            }
            if (snek.GetComponent<SnekHead>().segmentsDestroyed >= 24 && snek.GetComponent<SnekHead>().health <= 0) {
                Global.asteroidCancel = true;
            }
        }
    }
}
