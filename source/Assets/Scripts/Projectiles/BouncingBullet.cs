using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncingBullet : Projectile {

    //I deeply regret this idea.
    //This bullet bounces between enemies in close proximity, striking each enemy once.
    //If no valid enemy is nearby, it destroys itself.
    //First shot is ordinary.

    private AudioManager audioManager;

    public List<string> previousEnemies = new List<string>();
    public Transform currentT;
    private BouncingBulletDamager BBD;
    private CircleCollider2D searchBox;
    private float searchTime;
    public float timer;
    public bool searching; //searching for target.
    private float rotateSpeed;
    public bool firstFired;
    private float soundTimer;
    private bool canPlay;
    private float lifetime;

    // Use this for initialization
    protected override void Start() {
        base.Start();
        speed = 360.0f; //set to ~100 when done testing
        damage = 1f * damageMultiplier; //shouldn't ever deal this damage.
        BBD = GetComponentInParent<BouncingBulletDamager>();
        searchBox = GetComponent<CircleCollider2D>();
        searchBox.enabled = false;
        searchTime = 1.0f;
        timer = searchTime;
        rotateSpeed = 1200.0f;
        soundTimer = 0.0f;
        canPlay = true;
        firstFired = true;
        audioManager = AudioManager.instance;
        if (audioManager == null) {
            Debug.LogError("No audio manager found in scene, whoops");
        }
        audioManager.PlaySound("CrossbowSound1");
        lifetime = 8.0f;
    }

    // Update is called once per frame
    protected override void Update() {
        base.Update(); //hey, why DO we update this when there's nothing there?
        if (firstFired) {
            return;
        }
        lifetime -= Time.deltaTime;//emergency lifespan
        if(lifetime <= 0.0f) {//if you run out
            Destroy(gameObject);//kill self.
        }
        SoundTimer();

        if(currentT == null) {//checks for target dying (eg, to another weapon).
            if (!searching) { //currentT should be null briefly while searching, so only if we're not seraching...
                BeginSearch();//begin search.
            }
        }
        else { //if we have a target (and are therefore, not searching)
            FaceTarget();
        }

        if (!searching) { //if we're not searching for a target, the hitbox is on and we're looking for a target.
            timer -= Time.deltaTime;
            if (timer <= 0.0f) {//if searched for too long
                Destroy(gameObject); //destroy seeker.
            }
        }

    }

    private void SoundTimer() {
        if (canPlay) { return; } //if blade is ready to make sound, skip this function
        soundTimer += Time.deltaTime; //otherwise, add to the timer.
        if(soundTimer >= 0.2f) { //after .2s
            canPlay = true; //we can make a sound.
        }
    }

    private void FaceTarget() {
        Vector2 vecT = currentT.position;
        Vector2 direction = vecT - rb.position;
        float rotateAmount = Vector3.Cross(direction, transform.up).z;
        rb.angularVelocity = -rotateAmount * rotateSpeed;
    }

    protected override void FixedUpdate() {
        base.FixedUpdate();
    }

    public override void ProjectileMovement() {
        rb.velocity = transform.up * speed * Time.deltaTime;
    }

    public void HitTarget() {
        rb.angularVelocity = 0.0f; //stop turning ffs.
        firstFired = false; //disables first launch behaviour.
        if (canPlay) {
            audioManager.PlaySound("Metal1");
        }
        BeginSearch();
        //instantiate massive spark explosion here.
    }

    private void BeginSearch() {
        currentT = null;
        searchBox.enabled = true;
        timer = searchTime;
        searching = true;
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (!searching) { return; }//don't even check colliders if already going toward a target.
        if(other.tag == "Enemy" || other.tag == "Boss") {
            foreach(string str in previousEnemies) { //for each name in the list
                if(str == other.name) { //if list name == current collided name
                    return; //skip it.
                }
            }
            //effectively else{
            previousEnemies.Add(other.name);
            currentT = other.transform;
            searchBox.enabled = false;
            searching = false; //we found an enemy to hunt. stop searching.
        }
    }
}
