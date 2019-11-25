using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuicideBomber : Enemy
{
    public GameObject player;
    public GameObject suicideBomb;
    public GameObject warning;
    public bool triggered;


    // Use this for initialization
    protected override void Start()
    {
        base.Start();
        player = GameObject.FindWithTag("Player");

        if (difficulty == Global.RECRUIT)
        {

        }
        else if (difficulty == Global.VETEREN)
        {

        }
        else if (difficulty == Global.BATTLEH)
        {

        }
        speed = 3f;
        health = 25;
        originalSpeed = speed;
        triggered = false;
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (speedMultiplier < 0.4f)
        {
            speedMultiplier = 0.4f;
        }
        if (damageReceivedMultiplier > 1.45f)
        {
            damageReceivedMultiplier = 1.45f;
        }
        ps.transform.position = gameObject.transform.position;
        if (health <= 0)
        {
            DropWeapon();
            Death();
        }
        if (canMove)
        {
            //transform.Translate(new Vector2(0, speed * speedMultiplier * Time.deltaTime));
            /*transform.LookAt(player.transform.position);
            transform.Rotate(new Vector3(0, -90, -90), Space.Self);
            if (Vector2.Distance(this.transform.position, player.transform.position) > 1f) {
                transform.Translate(new Vector2(speed * Time.deltaTime, speed * Time.deltaTime));
            }*/

            if (!triggered) {
                Vector3 direction = player.transform.position - transform.position;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
                Quaternion lookRotation = Quaternion.AngleAxis(angle, Vector3.forward);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 4);
                transform.Translate(new Vector2(0, speed * speedMultiplier * Time.deltaTime));
            }

            if (Vector2.Distance(this.transform.position, player.transform.position) < 1.5f && !triggered) {
                speed = 0;
                triggered = true;
                Instantiate(warning, this.transform.position, Quaternion.identity);
                Invoke("Suicide", 0.5f);
            }

        }
        if (canShoot)
        {
            Shoot();
        }
        if (frozen)
        {
            Freeze();
        }
    }

    public override void Shoot()
    {

    }

    void Suicide() {
        Instantiate(suicideBomb, this.transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }

    public override void Death()
    {
        Destroy(gameObject);
    }

}
