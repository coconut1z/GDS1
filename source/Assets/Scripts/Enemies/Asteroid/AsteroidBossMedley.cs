using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidBossMedley : MonoBehaviour
{

    public int newAsteroidLevel;
    private GameObject snek;
    public float counter;

    // Use this for initialization
    void Start()
    {
        Global.asteroidLevel = newAsteroidLevel;
        counter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Global.final) {
            counter += Time.deltaTime;
        }

        if (counter <= 0) {
            Global.asteroidLevel = 13;
        }
        else if (Mathf.Floor(counter/20) % 5 == 0) {
            Global.asteroidLevel = 4;
        }
        else if (Mathf.Floor(counter / 20) % 5 == 1)
        {
            Global.asteroidLevel = 5;
        }
        else if (Mathf.Floor(counter / 20) % 5 == 2)
        {
            Global.asteroidLevel = 8;
        }
        else if (Mathf.Floor(counter / 20) % 5 == 3)
        {
            Global.asteroidLevel = 4;
        }
        else if (Mathf.Floor(counter / 20) % 5 == 4)
        {
            Global.asteroidLevel = 5;
        }
        else if (Mathf.Floor(counter / 20) % 5 == 5)
        {
            Global.asteroidLevel = 8;
        }
    }
}
