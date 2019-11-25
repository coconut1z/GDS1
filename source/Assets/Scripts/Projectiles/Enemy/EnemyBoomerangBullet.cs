using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoomerangBullet : EnemyProjectile {
    
    /// <summary>
    /// Uncomment/comment the stuff in ProjectileMovement if you want the old behaviour where
    /// it goes to the player's pos instead of going a set distance
    /// </summary>
    /*private Vector3 playerPos;*/
    private Vector3 parentPos;
    public float maxDistance = 7f;
    private float currentDistance;
    private bool back;

	// Use this for initialization
	protected override void Start () {
		base.Start ();
		speed = 5;
        /*playerPos = player.transform.position;*/
        parentPos = transform.root.position;
        currentDistance = 0;
        back = false;
    }

	// Update is called once per frame
	protected override void Update () {
		base.Update ();
        parentPos = transform.root.position;
    }

    public override void ProjectileMovement () {
        float travel = speed * Time.deltaTime;
        if (!back) {
            transform.Translate(transform.up * travel, Space.World);
            currentDistance += travel;
            back = currentDistance >= maxDistance;
            /*transform.position = Vector3.MoveTowards(transform.position, playerPos, travel);
            if (Vector3.Distance(transform.position, playerPos) < 0.1f) {
                back = true;
            }*/
        }
        else {
            this.GetComponent<SpriteRenderer>().flipY = true;
            transform.position = Vector3.MoveTowards(transform.position, parentPos, travel);
            if (Vector3.Distance(transform.position, parentPos) < 0.1f) {
                Invoke("DestroySelf", 0.01f);   
            }
        }
    }

    private void DestroySelf() {
        Destroy(gameObject);
    }
}
