using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnekSegment : MonoBehaviour {
    //the usual important stuff;
    private Rigidbody2D rb;
    public Vector2 targetA;
    public Vector2 targetB;
    public Vector2 targetC;
    public Vector2 targetP; //player
    public SnekSegment nextSegment;
    private Transform pTransform;
    public Transform headTransform;
    public GameObject weapon;
    private WeaponEnemy wep;
    private SnekMultiGun wepScript;
    private GameObject explosionParticles;

    //movement variables;
    private float speed;
    public float speedMultiplier;
    private float turnSpeed;
    private float snekkingAngle;
    private float snekkingMaxAngle;

    //state control;
    private bool acquiringTarget; //private after testing
    private bool snekking; //private after testing
    public int currentTargetNo; //1(a), 2(b), and 3(c). Arrays can start from 0, my state integers may not. side note, is this what enumerators are for?
    public Vector2 currentTarget;
    private bool turningLeft; //bool for oscillating left/right.
    //private bool lastInLine; //to the throne :P
    public bool normalMovement;
    public bool preparingCharge;
    public bool readyToCharge;
    public bool charging;
    public bool postChargeWait;
    private bool invulnerable;
    private bool canShoot;

    private float chargePrepTime;
    private float chargingTime; //presently unused - for advanced speeds
    private float postChargingTime; //presently unused - for advanced speeds

    public float health;
    private float healthMax;
    private float damageReceivedMultiplier; //ripped from enemy.cs
    public List<GameObject> noHit; //ripped from enemy.cs
    private List<string> wepIDs;
    private float lastClearTime;
    private float specialHitInterval = 0.2f;

    //positioning variables;
    public SnekHead headScript;
    //lerp stuff
    private float lerpSpeed;
    private float startTime;
    private float journeyLength;
    private Transform lerpOrigin;
    private float rotationOrigin;
    private Quaternion QOrigin;

    //aesthetics and flair
    public SpriteRenderer sr;
    private SpriteRenderer srShield;
    private float shieldGlowCount;
    public GameObject ps;
    public GameObject sparks;
    public GameObject explosion; //bypassed; added hellfire explosion particles from resource folder.
    //private int srLayer; //removed due to not having any effect. somehow.

    //weapon use


    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
        wep = weapon.GetComponent<WeaponEnemy>();
        wepScript = weapon.GetComponent<SnekMultiGun>(); //could just shoot using this. but ah well.
        SetUpSparks(); //copied from Enemy.cs
        targetA = new Vector2(1, 4);//set some initial targets; for testing moving onto the screen.
        targetB = new Vector2(-4, 0);
        targetC = new Vector2(1, -4); //this will be overwritten by the generator immediately btw. testing with invalid
        pTransform = GameObject.FindGameObjectWithTag("Player").transform;
        targetP = pTransform.position;
        headTransform = headScript.GetComponent<Transform>();
        canShoot = false;
        wepIDs = new List<string>();
        explosionParticles = GameObject.Find("Hellfire Explosion Deluxe");


        currentTargetNo = 1;//start by going for target 1/a.
        currentTarget = targetA;

        acquiringTarget = true; //these should always be the inverse of eachother, and therefore we don't need 2...
        snekking = false; //but having both could help, and certainly helps when reading.
        invulnerable = false;

        speed = 2.0f; //movement speed
        speedMultiplier = 1.0f; //MS multiplier. apply when missing segments, from 1 to ~2.

        normalMovement = false;
        turningLeft = true;
        snekkingMaxAngle = 40.0f;
        snekkingAngle = snekkingMaxAngle / 2;
        turnSpeed = 60.0f;

        
        //aesthetics
        sr = GetComponent<SpriteRenderer>();
        srShield = GetComponentsInChildren<SpriteRenderer>()[1];
        srShield.enabled = true;
        srShield.sortingOrder = sr.sortingOrder + 1;
        srShield.enabled = false;
        //if this doesnt work, come back, enable the renderer, change value, then disable.

        if (Global.Difficulty == Global.RECRUIT) {
            lerpSpeed = 0.5f;
            healthMax = 25.0f;
        }
        else if (Global.Difficulty == Global.VETEREN) {
            lerpSpeed = 0.75f;
            healthMax = 25.0f;
        }
        else if (Global.Difficulty == Global.BATTLEH) {
            lerpSpeed = 1.0f;
            healthMax = 25.0f;
        }
        health = healthMax;
        damageReceivedMultiplier = 1;
        SetHeadTransform();
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        Shoot();
        HealthUpdate();
        
        PrepareCharge(); //only runs while preparingCharge = t
        ChargeAttack(); //only runs while charging = t
        PostChargeWaiting();
        UpdateVitalVariables();
        MoveForward();
        TurnSnek();
        UpdateNodes();
	}

    private void Shoot() {
        if (!canShoot) {
            return;
        }
        wep.Shoot();
    }

    private void HealthUpdate() {
        if(health <= 0) {
            //this piece has been destroyed. simulate this by destroying
            //the last piece in the line.
            TakeNextHealth();
            headScript.DestroyLast();
            Explode();
        }
        if (!invulnerable) {
            return;
        }
        //if we're invulnerable, flash that shield
        shieldGlowCount += Time.deltaTime*3; //faster sinewave
    }

    public void TakeNextHealth() { //sets own health to next in line. //repeats down line. //simulates death in a middle piece.
        if(nextSegment != null) {
            health = nextSegment.health; //health = next down the line
            nextSegment.TakeNextHealth(); //next down line does above.
        }
        else {
            health = healthMax; //irrelevant since this piece is about to be destroyed by DestroyLast();
        }
    }

    private void PostChargeWaiting() {
        if (!postChargeWait) {
            return;
        }
        
        
        //dunno, do nothing here?
    }

    public void StartChargeSequence() {
        //THESE RECURSIONS MUST COME LAST IN THE METHOD DEAR CHRIST
        normalMovement = false;
        preparingCharge = true;
        startTime = Time.time;
        lerpOrigin = transform;
        QOrigin = transform.rotation;
        rotationOrigin = transform.rotation.z;
        journeyLength = Vector3.Distance(lerpOrigin.position, headTransform.position);
        chargePrepTime = 2.0f; //change soon to variable based on remaining pieces, or seeded from parent segment. EG, head decides variable, passes it on to each segment.
        if (nextSegment != null) {
            nextSegment.StartChargeSequence(); //recursion! //should this be at the top or bottom of the method? dunno. //BOTTOM IT TURNS OUT
        }
    }

    private void PrepareCharge() {
        //while PrepareCharging, if not at head position;
        //  face head
        //  lerp towards head
        //if near head position
        //  snap position to head
        //  readyToCharge = true
        //if(readyToCharge)
        //  rotation = head rotation. DO NOT USE PLAYER.

        //the preceding segment / head will enable the charge motion and disable readyToCharge and preparingCharge after ~.1s delay.
        
        if (!preparingCharge) {
            return;
        }
        chargePrepTime -= Time.deltaTime; //don't think this is needed now?

        //fuckme
        if (!readyToCharge) {
            //lerp towards head.
            //need to make face head, but save that for soon.

            float distCovered = (Time.time - startTime) * lerpSpeed;
            float fracJourney = distCovered / journeyLength;
            transform.position = Vector3.Lerp(lerpOrigin.position, headTransform.position, fracJourney);
            //ok. dist covered goes from 0 to 1 nicely enough. //turns out it exceeds it somehow. Meh.
            
            transform.rotation = Quaternion.Lerp(QOrigin, headTransform.rotation, distCovered*1.5f); //works fairly nicely.
            

            if (Vector2.Distance(transform.position, headTransform.position) <= 0.01f && readyToCharge == false) {
                transform.position = headTransform.position; //snap to head position
                readyToCharge = true;
            }
            /*
            if(fracJourney >= 0.950f) { //once most of the way through the journey
                transform.rotation = headTransform.rotation; //snap to head rotation
            }*/
        }

        if (readyToCharge) {
            transform.rotation = headTransform.rotation; //snap to head rotation
        }
        
        //done! once the StartChargeAttack begins, it will set preparingCharge to false, and readyToCharge to false, finishing this method.

    }

    public void StartChargeAttack() {//to be invoked by preceding seg/head with delay. Will invoke same method in subsequent segment if found, with delay.
        //Debug.Log("STARTING charge, by" + gameObject.name + "AT " + startTime);
        startTime = Time.time;
        lerpOrigin = transform;
        journeyLength = Vector3.Distance(lerpOrigin.position, headScript.targetP);
        preparingCharge = false;
        readyToCharge = false;
        charging = true;
        canShoot = false;
        if (nextSegment != null) {
            nextSegment.Invoke("StartChargeAttack", 0.1f); //delay hardcoded atm. Change to speed mult affected soon. eg, 1.5 - (0.5*speedMult) //eg 1f-0.5f
        }
    }

    private void ChargeAttack() {
        if (!charging) {
            return;
        }
        normalMovement = false;

        float distCovered = (Time.time - startTime) * lerpSpeed;
        float fracJourney = distCovered / journeyLength;
        transform.position = Vector3.Lerp(lerpOrigin.position, headScript.targetP, fracJourney);
        

        if (Vector2.Distance(transform.position, headScript.targetP) <= 0.1f) {
            transform.position = headScript.targetP; //snap to head position
            postChargeWait = true;
            currentTargetNo = headScript.currentTargetNo;
            //bit of brute force. Placed here to update once, and before the head gets a new target somewhere
            //Should now only break on extremely long snakes.
            //FINAL FIX! update currentTarget (the vector) to the current target. Fuckit, cheat.
            currentTarget = headScript.currentTarget;
            //meant to take this from self, but this should do.

        if(nextSegment == null) {//if the tail currently...
                Debug.Log("Activated tail command");
                headScript.EndChargeWait();//get the head to start moving again. will take .4 to .1s depending on speed Multiplier.

            }
        }

        if (postChargeWait) {
            transform.rotation = headTransform.rotation; //snap to head rotation
            charging = false; //ends charge attack for this segment; should remain in PostChargeWaiting phase until normalMovement is made true again.
        }
    }

    private void SetHeadTransform() {
        if (nextSegment != null) {
            nextSegment.headTransform = headTransform;
        }
    }

    public void BeginNormalMovement() {
        ForceUpdateNodes(); //should only do this once, but forcing it for safety sake.
        if (charging) {
            Debug.Log("Cancelled normal movement due to charging. Should be caused by early seg destruction during the 'unspooling' phase.");
            return;
        }
        canShoot = true;
        postChargeWait = false;
        acquiringTarget = true;
        snekking = false;
        normalMovement = true;
        turningLeft = true;
        snekkingAngle = snekkingMaxAngle / 2;
        ResetHealth();
        Invoke("StartNextSegment", 0.44f - (0.09f * speedMultiplier));

    }

    private void StartNextSegment() {
        if (nextSegment != null) {//I should have just included this in the above, but anyway.
            nextSegment.BeginNormalMovement();
        }
    }

    private void UpdateVitalVariables() {
        speedMultiplier = headScript.speedMultiplier;
    }

    public void DestroyLast() {
        if (nextSegment != null) {//if there is a subsequent segment;
            nextSegment.DestroyLast(); //run this in that one's script.
        }
        else {//if this is the last piece
            Debug.Log("Destroying segment at time" + Time.time);
            Destroy(gameObject);//destroy me.
        }

    }

    private void UpdateNodes() {
        if (!normalMovement) {
            return;
        }
        //check the current target's range. Nodes need only be updated if we've reached our target node after all.
        float targetDistance = Vector2.Distance(currentTarget, transform.position);
        bool reachedTarget = false;
        if(targetDistance < 0.2f * speedMultiplier + 0.7f) {//if within certain range of target
            reachedTarget = true; //we've reached target 
            
        }
        if (reachedTarget) {//(this writing is a bit verbose, but easier to read)
            IncrementTargetNo(); //we've reached the current target node. Set no. to next node, and generate next node while we're at it.
            acquiringTarget = true;
            snekking = false;
        }
    }

    private void ForceUpdateNodes() {
        targetA = headScript.targetA;
        targetB = headScript.targetB;
        targetC = headScript.targetC;
    }

    private void IncrementTargetNo() {
        //if we reached a, we need to update c
        if(currentTargetNo == 1) {
            UpdateSegmentNodes();
        }
        else if(currentTargetNo == 2){
            UpdateSegmentNodes();
        }
        else if(currentTargetNo == 3) {
            UpdateSegmentNodes();
        }
        else {
            Debug.Log("Wtf. invalid currentTargetNo (" + currentTargetNo + "). Rip IncrTN");
        }
        currentTarget = nextNode();
        currentTargetNo++;
        if(currentTargetNo >= 4) {
            currentTargetNo = 1;
        }
    }

    private void UpdateSegmentNodes() {
        targetA = headScript.targetA;
        targetB = headScript.targetB;
        targetC = headScript.targetC;
    }

    private void TurnSnek() {
        if (!normalMovement) {
            return;
        }
        //TEST BASIC TURN METHOD FOR NODE FINDING PURPOSES
        /*
        float AngleRadToTarget = Mathf.Atan2(
            currentTarget.y - transform.position.y, currentTarget.x - transform.position.x);
        float AngleToDeg = (180 / Mathf.PI) * AngleRadToTarget - 90;
        transform.rotation = Quaternion.Euler(0, 0, AngleToDeg);
        */
        // /END OF BASIC TURN METHOD

        if (acquiringTarget) {//if turning toward nav point
            //turn snek
            if (turningLeft) {
                transform.Rotate(new Vector3(0.0f, 0.0f, turnSpeed * Time.deltaTime * speedMultiplier));//removed multiplier^2
            }
            else if (!turningLeft) {
                transform.Rotate(new Vector3(0.0f, 0.0f, turnSpeed * Time.deltaTime *-1.0f * speedMultiplier)); //removed multiplier^2
            }
            
            //compare current vector with desired vector.
            Vector2 tVector2 = new Vector2(transform.position.x, transform.position.y);
            float dot = Vector2.Dot(transform.up, (currentTarget - tVector2).normalized); //returns 1 if facing target. returns -1 if facing opposite target.
            //Debug.Log("Dot = " + dot);
            //if vectors <3 degrees off or w/ever
                //snap to new intended vector.
                //acquiringTarget == false; snekking = true;
                //set new desired vector
            if(dot >= 0.999f) {//test value. try higher soon.
                //snap to wanted direction.
                float AngleRadToTarget = Mathf.Atan2(
                currentTarget.y - transform.position.y, currentTarget.x - transform.position.x);
                float AngleToDeg = (180 / Mathf.PI) * AngleRadToTarget - 90;
                transform.rotation = Quaternion.Euler(0, 0, AngleToDeg);
                //state control;
                snekkingAngle = snekkingMaxAngle/2;
                acquiringTarget = false;
                snekking = true;
            }

        }
        if (snekking) {//if in oscillation mode
            //
            if (turningLeft) {
                transform.Rotate(new Vector3(0.0f, 0.0f, turnSpeed * Time.deltaTime * speedMultiplier));//removed multiplier^2
                snekkingAngle += turnSpeed * Time.deltaTime * speedMultiplier;//removed multiplier^2
            }
            if (!turningLeft) {
                transform.Rotate(new Vector3(0.0f, 0.0f, turnSpeed * Time.deltaTime * -1 * speedMultiplier));//removed multiplier^2
                snekkingAngle -= turnSpeed * Time.deltaTime * speedMultiplier;//removed multiplier^2
            }

            if(snekkingAngle >= snekkingMaxAngle) {
                turningLeft = false;
            }
            if(snekkingAngle <= 0.0f) {
                turningLeft = true;
            }
        }

        //private test
        Vector2 testV = new Vector2(3.0f, 4.0f);
    }

    private Vector2 nextNode() {
        if (currentTargetNo == 1) {
            return targetB;
        }
        else if (currentTargetNo == 2) {
            return targetC;
        }
        else if (currentTargetNo == 3) {
            return targetA;
        }
        else {
            Debug.Log("FUCK. Error in nextNode(), returning 0.0 vector.");
            return new Vector2(0, 0);
        }
    }

    private void MoveForward() {
        if (!normalMovement) {
            return;
        }
        transform.Translate(new Vector2(0, speed * speedMultiplier * Time.deltaTime));//removed multiplier^2
    }

    private void MoveCharge() {
        transform.Translate(new Vector2(0, speed * speedMultiplier * Time.deltaTime * speedMultiplier));//reduced mult from ^3 to ^2
    }

    public void ResetHealth() { //to be done with every charge attack. Vicious.
        health = healthMax;
        srShield.enabled = false;
        invulnerable = false;
        if (nextSegment != null) {
            nextSegment.ResetHealth();
        }
    }

    public void Invulnerable() { //I mean, its not literally, but close enough.
        health = healthMax * 20; //if you can kill a segment 20 times over in ~6s, fuckit, I'll allow it.
        shieldGlowCount = 0;
        srShield.enabled = true;
        invulnerable = true;
        if(nextSegment != null) {
            nextSegment.Invulnerable();
        }
    }
    //this should work. but it doesn't. reactivate and tinker with (head too) if you like.
    /*
    public void SetPassSRLayer(int layerCount) {
        Debug.Log("entered segment srOrder setter.");
        Debug.Log("sr order =" + sr.sortingOrder);
        Debug.Log("Arrived in child segment. srLayer =" + srLayer);
        srLayer -= layerCount;
        sr.sortingOrder = srLayer;
        layerCount++;
        Debug.Log("survived setting order.");
        //Debug.Log("sr sO =" + sr.sortingOrder);
        
        if (nextSegment != null) {
            //Debug.Log("Beginning to pass" + srLayer);
            nextSegment.SetPassSRLayer(layerCount);
        }
    }
    */

    public void SetUpSparks() {
        ps = Instantiate(sparks, new Vector3(0, 0, 0), Quaternion.identity);
        ps.transform.SetParent(gameObject.transform);
        ps.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        ps.SetActive(false);
    }

    private void Explode() {
        Instantiate(explosionParticles, transform.position, transform.rotation, transform);
    }

    public void HitFeedback() {
        sr.color = new Color(255.0f, 0.0f, 0.0f);
        Invoke("EndFlashSprite", 0.05f);
    }
    public void EndFlashSprite() {
        sr.color = new Color(1.0f, 1.0f, 1.0f);
    }

    public void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") {
            other.GetComponent<PlayerController>().TakeDamage(1); //1 damage.
        }
        if (invulnerable) {
            return;
        }
        if (other.gameObject.name.Contains("RedoxBullet")) {
            ps.SetActive(true);
            health -= other.gameObject.GetComponent<Projectile>().damage * damageReceivedMultiplier;
            damageReceivedMultiplier += 0.15f;
            Destroy(other.gameObject);
            HitFeedback();
            return;
        }
        if (other.gameObject.name.Contains("PlayerIceBullet")) {
            //freezeTimer = 1.5f;
            //StartFreeze();
            //frozen = true;
            health -= other.gameObject.GetComponent<Projectile>().damage * damageReceivedMultiplier;
            Destroy(other.gameObject);
            return;
        }
        if (other.gameObject.tag == "PlayerBullet") {
            // EXPENSIVE //and yet here we are.
            //Debug.Log("Trying to damage");
            health -= other.gameObject.GetComponent<Projectile>().damage* damageReceivedMultiplier;
            //Debug.Log("Should have damaged. hp = " + health);
            Destroy(other.gameObject); //Tells the bullet's projectile superclass script to off itself.
            HitFeedback();
}
        if (other.gameObject.tag == "Sword") {
            // This fucks it up for some reason so I hardcoded damage here
            //health -= col.GetComponent<Sword>().damage;
            health -= 10.0f * damageReceivedMultiplier;
            HitFeedback();
        }
        if (other.gameObject.tag == "PlayerBulletPenetrate") {
            if (noHit.Count != 0) {
                for (int i = 0; i < noHit.Count; i++) {
                    if (noHit[i] == null) {
                        noHit.RemoveAt(i);
                        i--;
                    }
                }
                foreach (GameObject g in noHit) {
                    if (g != other.gameObject) {
                        noHit.Add(other.gameObject);
                        health -= other.gameObject.GetComponent<Projectile>().damage * damageReceivedMultiplier;
                    }
                }
            }
            else {
                //this seems bad! What if bullets need to pass over/through something with a collider?
                //removing for now. -J

                noHit.Add(other.gameObject);
                health -= other.gameObject.GetComponent<Projectile>().damage * damageReceivedMultiplier;
            }
            HitFeedback();
        }
    }

    public void DamageEnemy(float damage, string wepID) {
        if (Time.time > lastClearTime + specialHitInterval) {//if its been long enough...
            wepIDs.Clear();//blank the list!
            lastClearTime = Time.time;//reset last time.
        }
        foreach (string listWepID in wepIDs) { //check each member of the list.
            if (wepID == listWepID) {//if the list already has an ID in it...
                return;//piss off, you've hit me recently!
            }
        }//so if you don't return...
        health = health - damage; //guess it's a fresh hit. deal the damage... //aware -= is a thing. paranoid atm.
        HitFeedback(); //flash the sprite...
        wepIDs.Add(wepID);//and put the weapon on the naughty list.
    }
}
