using UnityEngine;
using System.Collections;

public class Sniper2Bullet : Projectile {
    private AudioManager audioManager;

    // Use this for initialization
    protected override void Start()
    {
        base.Start();
        //speed = 1200f;
        damage = 14f * damageMultiplier * 0.75f;
        Invoke("DeactivateHitbox", 0.2f);
        Invoke("DestroySelf", 1.0f);
        audioManager = AudioManager.instance;
        if (audioManager == null) {
            Debug.LogError("No audio manager found in scene, whoops");
        }
        //audioManager.PlaySound("SpamBullet3");
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void ProjectileMovement()
    {
        //rb.velocity = transform.up * speed * Time.deltaTime;
    }

    void DeactivateHitbox()
    {
        GetComponent<BoxCollider2D>().enabled = false;
    }

    void DestroySelf()
    {
        Destroy(this.gameObject);
    }
}
