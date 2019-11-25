using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SatelliteCannonMKII : WeaponModule
{

    float offsetX1;
    float offsetX2;
    float offsetX3;
    float offsetY1;
    float offsetY2;
    float offsetY3;

    Vector2 blast1;
    Vector2 blast2;
    Vector2 blast3;

    // Use this for initialization
    protected override void Start()
    {
        base.Start();
        shootDelay = 2.0f;
        shootTime = shootDelay;
        spread = 1f;
        displacement = new Vector2(0, 0.1f);
        originalSize = new Vector2(1, 1);
        weaponId = Global.SATCANNON2;
        stage = 1;
        //damage = loads
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    public override void Shoot()
    {
        if (shootTime > shootDelay)
        {
            // Instantiate (BULLET PREFAB, SHOOT POSITION, Z ROTATION + SPREAD IF THERE IS ANY)

            offsetX1 = Random.Range(-0.5f, 0.5f);
            offsetX2 = Random.Range(-1.5f, 1.5f);
            offsetX3 = Random.Range(-2f, 2f);
            offsetY1 = Random.Range(-1f, 1f);
            offsetY2 = Random.Range(-1.5f, 1.5f);
            offsetY3 = Random.Range(-2f, 1f);

            blast1 = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x + offsetX1,
                                           Camera.main.ScreenToWorldPoint(Input.mousePosition).y + offsetY1);

            blast2 = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x + offsetX2,
                                           Camera.main.ScreenToWorldPoint(Input.mousePosition).y + offsetY2);

            blast3 = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x + offsetX3,
                                           Camera.main.ScreenToWorldPoint(Input.mousePosition).y + offsetY3);

            Aim1();
            Invoke("Aim2", 0.25f);
            Invoke("Aim3", 0.75f);

            StartCoroutine(DelayBlast1(blast1));

            StartCoroutine(DelayBlast2(blast2));


            StartCoroutine(DelayBlast3(blast3));

            shootTime = 0;
        }
    }

    void Aim1()
    {
        Instantiate(projectiles[1], blast1, Quaternion.identity);

    }

    void Aim2()
    {

        Instantiate(projectiles[1], blast2, Quaternion.identity);

    }

    void Aim3()
    {

        Instantiate(projectiles[1], blast3, Quaternion.identity);
    }

    public override int ReturnId()
    {
        return Global.SATCANNON2;
    }

    IEnumerator DelayBlast1(Vector2 position)
    {
        yield return new WaitForSeconds(0.5f);
        Instantiate(projectiles[0], position, Quaternion.identity);
    }

    IEnumerator DelayBlast2(Vector2 position)
    {
        yield return new WaitForSeconds(0.75f);
        Instantiate(projectiles[0], position, Quaternion.identity);
    }

    IEnumerator DelayBlast3(Vector2 position)
    {
        yield return new WaitForSeconds(1.25f);
        Instantiate(projectiles[0], position, Quaternion.identity);
    }
}
