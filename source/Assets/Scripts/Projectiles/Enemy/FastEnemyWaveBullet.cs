using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastEnemyWaveBullet : EnemyProjectile {

    private float frequency = 20f;
    private float magnitude = 0.5f;
    //need lifeTime so bullets dont move at the same time
    private float lifeTime;             
    private Vector3 direction;
    private Vector3 pos;

    // Use this for initialization
    protected override void Start () {
		base.Start ();
		speed = 6f;
		damage = 1;
        direction = transform.right;
        pos = transform.position;
    }
	
	// Update is called once per frame
	protected override void Update () {
		base.Update ();
	}

    public override void ProjectileMovement()
    {
        //rb.velocity = transform.up * speed * Time.deltaTime;
        lifeTime += Time.deltaTime;
        pos += transform.up * speed * Time.deltaTime;
        transform.position = pos + direction * Mathf.Sin(lifeTime * frequency) * magnitude;
    }
}
