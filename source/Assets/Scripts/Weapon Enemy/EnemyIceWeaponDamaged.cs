using UnityEngine;
using System.Collections;

public class EnemyIceWeaponDamaged : WeaponEnemy
{
    // Use this for initialization
    protected override void Start()
    {
        base.Start();
        if (difficulty == Global.RECRUIT)
        {
            shootDelay = 3.5f;
        }
        else if (difficulty == Global.VETEREN)
        {
            shootDelay = 3f;
        }
        else if (difficulty == Global.BATTLEH)
        {
            shootDelay = 2f;
        }
        shootTime = shootDelay;
        spread = 6f;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        lookAtPlayer();
    }

    public override void Shoot()
    {
        if (shootTime > shootDelay)
        {
            // Instantiate (BULLET PREFAB, SHOOT POSITION, Z ROTATION + SPREAD IF THERE IS ANY)
            EnemyProjectile e = Instantiate(projectiles[0], shootPos[0].position,
                Quaternion.Euler(0, 0, transform.eulerAngles.z + spread - 2 * Random.value * spread))
				.GetComponent<EnemyProjectile>();
			e.Setup (player);
            shootTime = 0;
        }
    }
}
