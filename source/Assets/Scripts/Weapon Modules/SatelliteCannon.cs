using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SatelliteCannon : WeaponModule {



    // Use this for initialization
    protected override void Start () {
        base.Start ();
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
    protected override void Update () {
        base.Update ();
    }
        
    public override void Shoot(){
        if(shootTime > shootDelay){
            // Instantiate (BULLET PREFAB, SHOOT POSITION, Z ROTATION + SPREAD IF THERE IS ANY)
            Aim();
            Vector2 aimBlast = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x,
                                           Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
            StartCoroutine(DelayBlast(aimBlast));
            shootTime = 0;
        }
    }

    void Aim() 
    {
        Instantiate(projectiles[1], new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x,
            Camera.main.ScreenToWorldPoint(Input.mousePosition).y), Quaternion.identity);
    }

    public override int ReturnId(){
        return Global.SATCANNON;
    }

    IEnumerator DelayBlast(Vector2 position) {
        yield return new WaitForSeconds(0.5f);
        Instantiate(projectiles[0], position, Quaternion.identity);
    }
}
