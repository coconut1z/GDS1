using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSystemBossMedley : MonoBehaviour
{


    //0 small
    //1 med
    //2 large
    //3 gravity
    //4 repulse
    //5 fast
    public GameObject[] asteroids;
    public Vector2 topleft, topright, botleft, botright;
    private int oldLevel;

    // Use this for initialization
    void Start()
    {
        oldLevel = 0;
    }

    // Update is called once per frame
    void Update()
    {

        // If you want to manually cancel all coroutines
        if (Global.asteroidCancel)
        {
            Global.asteroidCancel = false;
            StopAllCoroutines();
        }


        // This is so you only activate coroutine once (It self automates after activating once)
        // It also cancels the previous old coroutines
        if (oldLevel != Global.asteroidLevel)
        {
            oldLevel = Global.asteroidLevel;
            StopAllCoroutines();
            SpawnAsteroids(Global.asteroidLevel);
        }
    }

    // Conant u can change how asteroids spawn by changing the global asteroid level in ur waves
    // create these to change ur stuffs
    private void SpawnAsteroids(int level)
    {
        switch (level)
        {
            case 1:
                Level1();
                break;
            case 2:
                Level2();
                break;
            case 3:
                Level3();
                break;
            case 4:
                Level4();
                break;
            case 5:
                Level5();
                break;
            case 6:
                Level6();
                break;
            case 7:
                Level7();
                break;
            case 8:
                Level8();
                break;
            case 9:
                Level9();
                break;
            case 10:
                Level10();
                break;
            case 11:
                Level11();
                break;
            case 12:
                Level12();
                break;
            case 13:
                Empty();
                break;
            default:
                break;
        }
    }

    // EXAMPLES

    // spawn small ones from top
    private void Level1()
    {
        if (Global.Difficulty == Global.RECRUIT)
        {
            StartCoroutine(Spawn(asteroids[0], true, false, false, false, 0.9f, 1.2f));
        }
        else if (Global.Difficulty == Global.VETEREN)
        {
            StartCoroutine(Spawn(asteroids[0], true, false, false, false, 0.5f, 0.9f));
        }
        else if (Global.Difficulty == Global.BATTLEH)
        {
            StartCoroutine(Spawn(asteroids[0], true, false, false, false, 0.3f, 0.6f));
        }
    }

    // spawn medium from top
    private void Level2()
    {
        if (Global.Difficulty == Global.RECRUIT)
        {
            StartCoroutine(Spawn(asteroids[1], true, false, false, false, 1.9f, 2.1f));
        }
        else if (Global.Difficulty == Global.VETEREN)
        {
            StartCoroutine(Spawn(asteroids[1], true, false, false, false, 1.3f, 1.8f));
        }
        else if (Global.Difficulty == Global.BATTLEH)
        {
            StartCoroutine(Spawn(asteroids[1], true, false, false, false, 1, 1.5f));
        }
    }

    // spawn large from top
    private void Level3()
    {
        if (Global.Difficulty == Global.RECRUIT)
        {
            StartCoroutine(Spawn(asteroids[2], true, false, false, false, 2.8f, 4.8f));
        }
        else if (Global.Difficulty == Global.VETEREN)
        {
            StartCoroutine(Spawn(asteroids[2], true, false, false, false, 2.4f, 4.4f));
        }
        else if (Global.Difficulty == Global.BATTLEH)
        {
            StartCoroutine(Spawn(asteroids[2], true, false, false, false, 2f, 4f));
        }
    }

    // spawn small ones top, left and right
    private void Level4()
    {
        if (Global.Difficulty == Global.RECRUIT)
        {
            StartCoroutine(Spawn(asteroids[0], true, false, false, false, 1.7f, 2.5f));
            StartCoroutine(Spawn(asteroids[0], false, false, true, false, 1.7f, 2.5f));
            StartCoroutine(Spawn(asteroids[0], false, false, false, true, 1.7f, 2.5f));
        }
        else if (Global.Difficulty == Global.VETEREN)
        {
            StartCoroutine(Spawn(asteroids[0], true, false, false, false, 1.3f, 2.1f));
            StartCoroutine(Spawn(asteroids[0], false, false, true, false, 1.3f, 2.1f));
            StartCoroutine(Spawn(asteroids[0], false, false, false, true, 1.3f, 2.1f));
        }
        else if (Global.Difficulty == Global.BATTLEH)
        {
            StartCoroutine(Spawn(asteroids[0], true, false, false, false, 1f, 1.8f));
            StartCoroutine(Spawn(asteroids[0], false, false, true, false, 1f, 1.8f));
            StartCoroutine(Spawn(asteroids[0], false, false, false, true, 1f, 1.8f));
        }
    }

    //custom level - medium and large from top
    private void Level5()
    {
        if (Global.Difficulty == Global.RECRUIT)
        {
            StartCoroutine(Spawn(asteroids[1], true, false, false, false, 1.8f, 2.3f));
            StartCoroutine(Spawn(asteroids[2], true, false, false, false, 2.8f, 4.8f));
        }
        else if (Global.Difficulty == Global.VETEREN)
        {
            StartCoroutine(Spawn(asteroids[1], true, false, false, false, 1.4f, 1.9f));
            StartCoroutine(Spawn(asteroids[2], true, false, false, false, 2.4f, 4.4f));
        }
        else if (Global.Difficulty == Global.BATTLEH)
        {
            StartCoroutine(Spawn(asteroids[1], true, false, false, false, 1f, 1.5f));
            StartCoroutine(Spawn(asteroids[2], true, false, false, false, 2f, 4f));
        }
    }

    //custom level - medium and gravity from top
    private void Level6()
    {
        if (Global.Difficulty == Global.RECRUIT)
        {
            StartCoroutine(Spawn(asteroids[1], true, false, false, false, 1.7f, 2.2f));
            StartCoroutine(Spawn(asteroids[3], true, false, false, false, 2.9f, 4.9f));
        }
        else if (Global.Difficulty == Global.VETEREN)
        {
            StartCoroutine(Spawn(asteroids[1], true, false, false, false, 1.5f, 2.0f));
            StartCoroutine(Spawn(asteroids[3], true, false, false, false, 2.5f, 4.5f));
        }
        else if (Global.Difficulty == Global.BATTLEH)
        {
            StartCoroutine(Spawn(asteroids[1], true, false, false, false, 1.2f, 1.7f));
            StartCoroutine(Spawn(asteroids[3], true, false, false, false, 2.2f, 4.2f));
        }
    }

    //custom level - medium from top, repulse from sides
    private void Level7()
    {
        if (Global.Difficulty == Global.RECRUIT)
        {
            StartCoroutine(Spawn(asteroids[1], true, false, false, false, 2.1f, 2.6f));
            StartCoroutine(Spawn(asteroids[4], false, false, true, false, 3.8f, 4.8f));
            StartCoroutine(Spawn(asteroids[4], false, false, false, true, 4.8f, 5.8f));
        }
        else if (Global.Difficulty == Global.VETEREN)
        {
            StartCoroutine(Spawn(asteroids[1], true, false, false, false, 1.9f, 2.4f));
            StartCoroutine(Spawn(asteroids[4], false, false, true, false, 3.4f, 4.4f));
            StartCoroutine(Spawn(asteroids[4], false, false, false, true, 4.4f, 5.4f));
        }
        else if (Global.Difficulty == Global.BATTLEH)
        {
            StartCoroutine(Spawn(asteroids[1], true, false, false, false, 1.5f, 2.0f));
            StartCoroutine(Spawn(asteroids[4], false, false, true, false, 3f, 4f));
            StartCoroutine(Spawn(asteroids[4], false, false, false, true, 4f, 5f));
        }
    }

    //custom level - medium and fast from top
    private void Level8()
    {
        if (Global.Difficulty == Global.RECRUIT)
        {
            StartCoroutine(Spawn(asteroids[1], true, false, false, false, 1.6f, 2.5f));
            StartCoroutine(Spawn(asteroids[5], true, false, false, false, 2.0f, 2.8f));
        }
        else if (Global.Difficulty == Global.VETEREN)
        {
            StartCoroutine(Spawn(asteroids[1], true, false, false, false, 1.3f, 2.2f));
            StartCoroutine(Spawn(asteroids[5], true, false, false, false, 1.7f, 2.5f));
        }
        else if (Global.Difficulty == Global.BATTLEH)
        {
            StartCoroutine(Spawn(asteroids[1], true, false, false, false, 1.0f, 1.9f));
            StartCoroutine(Spawn(asteroids[5], true, false, false, false, 1.4f, 2.2f));
        }
    }

    //0 small
    //1 med
    //2 large
    //3 gravity
    //4 repulse
    //5 fast

    //custom level - fast from top, gravity from sides
    private void Level9()
    {
        if (Global.Difficulty == Global.RECRUIT)
        {
            StartCoroutine(Spawn(asteroids[5], true, false, false, false, 2f, 2.7f));
            StartCoroutine(Spawn(asteroids[3], false, false, true, false, 2.3f, 3.3f));
            StartCoroutine(Spawn(asteroids[3], false, false, false, true, 2.6f, 3.6f));
        }
        else if (Global.Difficulty == Global.VETEREN)
        {
            StartCoroutine(Spawn(asteroids[5], true, false, false, false, 1.6f, 2.3f));
            StartCoroutine(Spawn(asteroids[3], false, false, true, false, 1.9f, 2.9f));
            StartCoroutine(Spawn(asteroids[3], false, false, false, true, 2.2f, 3.2f));
        }
        else if (Global.Difficulty == Global.BATTLEH)
        {
            StartCoroutine(Spawn(asteroids[5], true, false, false, false, 1.2f, 1.9f));
            StartCoroutine(Spawn(asteroids[3], false, false, true, false, 1.5f, 2.5f));
            StartCoroutine(Spawn(asteroids[3], false, false, false, true, 1.8f, 2.8f));
        }
    }

    //custom level - fast top and sides
    private void Level10()
    {
        if (Global.Difficulty == Global.RECRUIT)
        {
            StartCoroutine(Spawn(asteroids[5], true, false, false, false, 1.8f, 2.7f));
            StartCoroutine(Spawn(asteroids[5], false, false, true, false, 1.9f, 2.4f));
            StartCoroutine(Spawn(asteroids[5], false, false, false, true, 2.1f, 2.6f));
        }
        else if (Global.Difficulty == Global.VETEREN)
        {
            StartCoroutine(Spawn(asteroids[5], true, false, false, false, 1.5f, 2.4f));
            StartCoroutine(Spawn(asteroids[5], false, false, true, false, 1.6f, 2.1f));
            StartCoroutine(Spawn(asteroids[5], false, false, false, true, 1.8f, 2.3f));
        }
        else if (Global.Difficulty == Global.BATTLEH)
        {
            StartCoroutine(Spawn(asteroids[5], true, false, false, false, 1f, 1.9f));
            StartCoroutine(Spawn(asteroids[5], false, false, true, false, 1.2f, 1.7f));
            StartCoroutine(Spawn(asteroids[5], false, false, false, true, 1.4f, 1.9f));
        }
    }

    //custom level - gravity and repulse from sides
    private void Level11()
    {
        if (Global.Difficulty == Global.RECRUIT)
        {
            StartCoroutine(Spawn(asteroids[3], false, false, true, false, 2.0f, 2.4f));
            StartCoroutine(Spawn(asteroids[4], false, false, false, true, 2.1f, 2.5f));
        }
        else if (Global.Difficulty == Global.VETEREN)
        {
            StartCoroutine(Spawn(asteroids[3], false, false, true, false, 1.6f, 2.0f));
            StartCoroutine(Spawn(asteroids[4], false, false, false, true, 1.7f, 2.1f));
        }
        else if (Global.Difficulty == Global.BATTLEH)
        {
            StartCoroutine(Spawn(asteroids[3], false, false, true, false, 1.2f, 1.6f));
            StartCoroutine(Spawn(asteroids[4], false, false, false, true, 1.3f, 1.7f));
        }
    }

    //custom level - medium, fast, repulse from top, and small from side
    private void Level12()
    {
        if (Global.Difficulty == Global.RECRUIT)
        {
            StartCoroutine(Spawn(asteroids[1], true, false, false, false, 2.0f, 2.4f));
            StartCoroutine(Spawn(asteroids[5], true, false, false, false, 2.2f, 2.4f));
            StartCoroutine(Spawn(asteroids[4], true, false, false, false, 2.3f, 2.9f));
            StartCoroutine(Spawn(asteroids[0], false, false, true, false, 1.6f, 2.1f));
        }
        else if (Global.Difficulty == Global.VETEREN)
        {
            StartCoroutine(Spawn(asteroids[1], true, false, false, false, 1.6f, 2.0f));
            StartCoroutine(Spawn(asteroids[5], true, false, false, false, 1.8f, 2.0f));
            StartCoroutine(Spawn(asteroids[4], true, false, false, false, 1.9f, 2.5f));
            StartCoroutine(Spawn(asteroids[0], false, false, true, false, 1.2f, 1.7f));
        }
        else if (Global.Difficulty == Global.BATTLEH)
        {
            StartCoroutine(Spawn(asteroids[1], true, false, false, false, 1.2f, 1.6f));
            StartCoroutine(Spawn(asteroids[5], true, false, false, false, 1.4f, 1.6f));
            StartCoroutine(Spawn(asteroids[4], true, false, false, false, 1.5f, 2.1f));
            StartCoroutine(Spawn(asteroids[0], false, false, true, false, 0.8f, 1.3f));
        }
    }

    private void Empty() {
        
    }

    /*
     * Params
     * a Asteroid to spawn (See numbers above for which asteroids)
     * top spawn asteroid from top?
     * bot ^
     * right ^
     * left ^
     * delayMin minimum delay before spawning asteroid
     * delayMax ^ max
     */
    IEnumerator Spawn(GameObject a, bool top, bool bot, bool left, bool right, float delayMin, float delayMax)
    {
        yield return new WaitForSeconds(Random.Range(delayMin, delayMax));
        if (top)
        {
            GameObject e = Instantiate(a) as GameObject;
            e.transform.position = new Vector2(Random.Range(topleft.x, topright.x), topleft.y);
            float angle = Mathf.Atan2(
                Random.Range(-9.7f, 9.7f) - e.transform.position.y,
                Random.Range(-17.4f, 17.4f) - e.transform.position.x
            ) * (180 / Mathf.PI) - 90;
            e.transform.rotation = Quaternion.Euler(0, 0, angle);
        }
        if (bot)
        {
            GameObject e = Instantiate(a) as GameObject;
            e.transform.position = new Vector2(Random.Range(botleft.x, botright.x), botleft.y);
            float angle = Mathf.Atan2(
                Random.Range(-9.7f, 9.7f) - e.transform.position.y,
                Random.Range(-17.4f, 17.4f) - e.transform.position.x
            ) * (180 / Mathf.PI) - 90;
            e.transform.rotation = Quaternion.Euler(0, 0, angle);
        }
        if (left)
        {
            GameObject e = Instantiate(a) as GameObject;
            e.transform.position = new Vector2(botleft.x, Random.Range(botleft.y, topleft.y));
            float angle = Mathf.Atan2(
                Random.Range(-9.7f, 9.7f) - e.transform.position.y,
                Random.Range(-17.4f, 17.4f) - e.transform.position.x
            ) * (180 / Mathf.PI) - 90;
            e.transform.rotation = Quaternion.Euler(0, 0, angle);
        }
        if (right)
        {
            GameObject e = Instantiate(a) as GameObject;
            e.transform.position = new Vector2(botright.x, Random.Range(botright.y, topright.y));
            float angle = Mathf.Atan2(
                Random.Range(-9.7f, 9.7f) - e.transform.position.y,
                Random.Range(-17.4f, 17.4f) - e.transform.position.x
            ) * (180 / Mathf.PI) - 90;
            e.transform.rotation = Quaternion.Euler(0, 0, angle);
        }
        StartCoroutine(Spawn(a, top, bot, left, right, delayMin, delayMax));
    }

}
