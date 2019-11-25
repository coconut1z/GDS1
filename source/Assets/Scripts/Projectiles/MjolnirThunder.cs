using UnityEngine;
using System.Collections;

public class MjolnirThunder : Projectile
{
    private AudioManager audioManager;
    // Use this for initialization
    protected override void Start()
    {
        base.Start();
        speed = 0f;
        damage = 15f * damageMultiplier;
        //Invoke("BlastEnd", 1f);
        Invoke("DeactivateHitbox", 0.3f);
        audioManager = AudioManager.instance;
        if (audioManager == null) {
           // Debug.LogError("No audio manager found in scene, whoops");
        }
        audioManager.PlaySound("Thunder1");
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

    void DeactivateHitbox() {
        GetComponent<BoxCollider2D>().enabled = false;
    }

    void BlastEnd()
    {
        Destroy(gameObject);
    }
}
