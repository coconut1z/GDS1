using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
//quick change to recommit and save you guys some headache.
public class SnekHead : MonoBehaviour {

    //the usual important stuff;
    private Rigidbody2D rb;
    public Vector2 targetA;
    public Vector2 targetB;
    public Vector2 targetC;
    public Vector2 targetP; //player
    public SnekSegment nextSegment;
    private Transform pTransform;
    public GameObject wepDropActual;
    public GameObject wepDrop;
    private DropWeaponOnDestroy drop;
    //public Transform headTransform; //not needed in head script

    //movement variables;
    private float speed;
    private float speedMIncrement;
    public float speedMultiplier;
    private float turnSpeed;
    private float snekkingAngle;
    private float snekkingMaxAngle;


    //state control;
    private bool acquiringTarget;
    private bool snekking;
    public int currentTargetNo; //1(a), 2(b), and 3(c). Arrays can start from 0, my state integers may not. side note, is this what enumerators are for?
    public Vector2 currentTarget;
    private bool turningLeft; //bool for oscillating left/right.
    private bool normalMovement; //recently privatised
    private bool preparingCharge;//recently privatised
    private bool charging;//recently privatised
    private bool postChargeWait;//recently privatised
    private bool invulnerable;

    private float chargePrepTime;
    private float chargingTime;
    private float postChargingTime;

    public float health;
    private float damageReceivedMultiplier; //ripped from enemy.cs
    public List<GameObject> noHit;//ripped from enemy.cs
    private List<string> wepIDs = new List<string>();
    private float lastClearTime;
    private float specialHitInterval = 0.2f;

    private float chargePrepTimeBase;
    private float chargingTimeBase;
    private float postChargingTimeBase;

    public int segmentsDestroyed; //counts segments destroyed.
    private int lastSegCount; //used so function only runs when another segment is destroyed.
    private int chargeInterval; //used to   
    private int segPhaseCount; //

    //lerp stuff
    private float lerpSpeed = 0.9f;
    private float startTime;
    private float journeyLength;
    private Transform lerpOrigin;

    //positioning variables;
    private float maxX; //max distance from origin for X
    private float maxY; //max distance from origin for Y
    private float minRange; //minimum difference between new targets

    //aesthetics, flair, etc;
    private SpriteRenderer sr;
    public SpriteRenderer srShield;
    private int srLayer;
    public GameObject ps;
    public GameObject sparks;


    public bool hardCodeCharge;

    //weapon use
    public GameObject bladeL;
    public GameObject bladeR;

    private AbilityRewardsDropList dropManager;

    public GameObject death1;
    public GameObject death2;


    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
        SetUpSparks(); //copied from Enemy.cs
        targetA = new Vector2(1, 4);//set some initial targets; for testing moving onto the screen.
        targetB = new Vector2(-4, 0);
        targetC = new Vector2(1, -4); //this will be overwritten by the generator immediately btw. testing with invalid
        pTransform = GameObject.FindGameObjectWithTag("Player").transform;
        targetP = pTransform.position;

        currentTargetNo = 1;//start by going for target 1/a.
        currentTarget = targetA;

        acquiringTarget = true; //these should always be the inverse of eachother, and therefore we don't need 2...
        snekking = false; //but having both could help, and certainly helps when reading.
        invulnerable = true;

        speed = 2.0f; //movement speed
        speedMIncrement = 0.5f; //amount the multiplier increases by per charge attack.
        speedMultiplier = 1.0f; //MS multiplier. apply when missing segments. Caps at 2.5/3/4.
        maxX = 7.0f;
        maxY = 4.0f;
        minRange = 4.0f;
        turningLeft = true;
        snekkingMaxAngle = 40.0f;
        snekkingAngle = snekkingMaxAngle / 2;
        turnSpeed = 60.0f;
        normalMovement = false;
        preparingCharge = false;
        charging = false;
        postChargeWait = false;
        segmentsDestroyed = 0;
        lastSegCount = 0; //if segmentsDestroyed > this number, state may update.
        health = 100.0f;
        damageReceivedMultiplier = 1;


        chargePrepTimeBase = 2.0f; //maybe alter these on difficulties.
        chargingTimeBase = 1.5f;
        postChargingTimeBase = 1.5f;

        if (Global.Difficulty == Global.RECRUIT) {
            lerpSpeed = 0.5f;
        }
        else if (Global.Difficulty == Global.VETEREN) {
            lerpSpeed = 0.75f;
        }
        else if (Global.Difficulty == Global.BATTLEH) {
            lerpSpeed = 1.0f;
        }

        //aesthetics/flair;
        sr = GetComponent<SpriteRenderer>();
        
        srShield.sortingOrder = sr.sortingOrder + 1;
        
        srLayer = 30; //should be unused atm
        drop = wepDrop.GetComponent<DropWeaponOnDestroy>();

        //weapon use
        bladeL.SetActive(false); //disables left blade
        bladeR.SetActive(false); //disables right blade

        BeginNormalMovement();
        SetHeadTransform();
        //SetPassSRLayer(); //deactive due to mysteries.

        hardCodeCharge = false;

        dropManager = GameObject.Find("AbilityDropManager").GetComponent<AbilityRewardsDropList>();
        dropManager.AddBossDropsToList(3);
    }

    private void SetChargeTarget() { //as says. for test, lets use 0,0.
        Debug.Log("H - Setting Charge Target. " + Time.time);
        //targetP = pTransform.position;
        float tempX = pTransform.position.x * UnityEngine.Random.Range(0.9f, 1.3f); //tends to overshoot to side of player slightly.
        float tempY = pTransform.position.y * UnityEngine.Random.Range(0.9f, 1.3f); //good for the snek swords.
        targetP = new Vector2(tempX, tempY);
    }


    private void SetHeadTransform() {
        if(nextSegment != null) {
            nextSegment.headTransform = transform;
        }
    }

    private void BeginNormalMovement() {
        postChargeWait = false;
        acquiringTarget = true;
        snekking = false;
        normalMovement = true;
        turningLeft = true;
        snekkingAngle = snekkingMaxAngle / 2;
        Debug.Log("H - Sending Move Normally to segments " + Time.time);
        Invoke("StartNextSegment", 0.44f - (0.09f * speedMultiplier));

    }

    private void StartNextSegment() {
        if(nextSegment != null) {
            nextSegment.BeginNormalMovement();
        }
    }

    public void StartChargeSequence() {
        Debug.Log("H - Initializing Charge Sequence " + Time.time);
        nextSegment.Invulnerable();
        normalMovement = false;
        preparingCharge = true;
        startTime = Time.time;
        chargePrepTime = 2.0f; //change soon to variable based on remaining pieces, or seeded from parent segment. EG, head decides variable, passes it on to each segment.
        //THESE RECURSIONS MUST COME LAST IN THE METHOD DEAR CHRIST
        if (nextSegment != null) {
            Debug.Log("H - Sending Charge Sequence Order to segments " + Time.time);
            nextSegment.StartChargeSequence(); //recursion! //should this be at the top or bottom of the method? dunno.
        }
    }

    private void DebugStateInitiator() {
        if (hardCodeCharge) {
            hardCodeCharge = false;
            StartChargeSequence();
        }
    }

    private void PrepareCharge() {
        if (!preparingCharge) {
            return;
        }
        //when 
        //wait 4s - speedMultiplier.
        if(chargePrepTime >= 0.0f) {
            chargePrepTime -= Time.deltaTime;

            if(chargePrepTime <= 1.0f && bladeL.activeSelf == false) {
                bladeL.SetActive(true);
                bladeR.SetActive(true);
            }

            //face player.
            float AngleRadToMouse = Mathf.Atan2(pTransform.position.y - transform.position.y, pTransform.position.x - transform.position.x);
            float AngleToDeg = (180 / Mathf.PI) * AngleRadToMouse - 90;
            transform.rotation = Quaternion.Euler(0, 0, AngleToDeg);
        }
        else {
            SetChargeTarget();
            preparingCharge = false; //stop preping charge
            charging = true; //and charge.
            lerpOrigin = transform; //sets start point to right here.
            startTime = Time.time;
            journeyLength = Vector3.Distance(lerpOrigin.position, targetP);
            if (nextSegment != null) {
                nextSegment.Invoke("StartChargeAttack", 0.1f);//delay hardcoded atm. Change to speed mult affected soon. eg, 1.5 - (0.5*speedMult) //eg 1f-0.5f
            }
        }

    }

    private void Charging() {
        //charging movement.
        if (!charging) {
            return;
        }
        
        float distCovered = (Time.time - startTime) * lerpSpeed;
        float fracJourney = distCovered / journeyLength;
        transform.position = Vector3.Lerp(lerpOrigin.position, targetP, fracJourney);
        
        
        if (Vector2.Distance(transform.position, targetP) <= 0.1f) {
            transform.position = targetP; //snap to head position
            postChargeWait = true;
        }

        if (postChargeWait) {
            //face player.
            float AngleRadToPlayer = Mathf.Atan2(pTransform.position.y - transform.position.y, pTransform.position.x - transform.position.x);
            charging = false; //ends charge attack for this segment; should remain in PostChargeWaiting phase until normalMovement is made true again.
            chargePrepTime = 5.0f; //THIS SHOULD NOT BE NEEDED! bypassed via final segment informaing of arrival.
        }

    }

    private void PostChargeWait() {
        //this function is no longer really used; final segment now starts the resume process. It's backup if the final piece somehow dies or is destroyed while heading towards the head, *after* the preceding piece reaches the head
        //wait for 4s - speedMultiplier.
        if (!postChargeWait) {
            return;
        }
        if (chargePrepTime >= 0.0f) {
            chargePrepTime -= Time.deltaTime;
        }

        if (chargePrepTime <= 0.0f) {
            Debug.Log("H - Charge sequence ended. Re-initializing SnekOperation.");
            Debug.Log("H - Sending BeginNormalMovement to Segments " + Time.time);
            //speedMultiplier += speedMIncrement;//consider returning.
            //bladeL.GetComponent<SnekBlade>().Deactivate();


            Debug.Log("de-activating blades.");
            Invoke("BeginNormalMovement", 0.55f - (0.1f * speedMultiplier));
        }
    }

    public void EndChargeWait() { //called by the final segment on reaching its destination.
        bladeR.GetComponent<SnekBlade>().Deactivate();
        bladeL.GetComponent<SnekBlade>().Deactivate();
        //bladeR.SetActive(false); //decativates bladeR
        //bladeL.SetActive(false); //deactivates bladeL
        speedMultiplier += speedMIncrement;
        Invoke("BeginNormalMovement", 0.5f - (0.1f * speedMultiplier));
    }

    // Update is called once per frame
    void FixedUpdate () {
        HealthUpdate();
        DebugStateInitiator(); //debug method for testing states.
        CheckSegments(); //initiates charge and other phases via segment count.
        PrepareCharge(); //only runs if preparingCharge = true.
        Charging();
        PostChargeWait();
        MoveForward(); //only runs if normalMovement = true.
        TurnSnek(); //only runs if normalMovement = true.
        UpdateNodes(); //only runs if normalMovement = true.
    }

    private void HealthUpdate() {
        if(health <= 0) {
            DestroyLast();
        }
    }

    private void CheckSegments() {
        if (nextSegment == null) {//if the head is the last one standing
            Debug.Log("All pieces destroyed, head last piece remaining. Removing invulnerability");
            invulnerable = false;
        }
        //hard coding, sorry
        if (segmentsDestroyed <= lastSegCount) { //if no new segments have been destroyed
            return; //nothing to do here.
        }
        
        Debug.Log("segD = " + segmentsDestroyed + " lastSegC = " + lastSegCount);
        lastSegCount = segmentsDestroyed; // if there has, note it, and run the below;
        if (Global.Difficulty == Global.RECRUIT) {
            if(segmentsDestroyed%8 == 0) {
                Debug.Log("Starting charge via Rec trigger");
                StartChargeSequence();
            }
        }
        if (Global.Difficulty == Global.VETEREN) {
            if (segmentsDestroyed % 6 == 0) {
                Debug.Log("Starting charge via Vet trigger");
                StartChargeSequence();
            }
        }
        if (Global.Difficulty == Global.BATTLEH) {
            if (segmentsDestroyed % 4 == 0) {
                Debug.Log("Starting charge via battleH trigger");
                StartChargeSequence();
            }
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

    public void DestroyLast() {
        segmentsDestroyed++;
        if (nextSegment != null) {//if there is a subsequent segment;
            nextSegment.DestroyLast(); //run this in that one's script.
        }
        else {//if this is the last piece
            Debug.Log("Destroying head");
            Destroy(gameObject);//destroy me.
        }

    }

    private void IncrementTargetNo() {
        //if we reached a, we need to update c
        if(currentTargetNo == 1) {
            GenerateTargetLetter("b");
        }
        else if(currentTargetNo == 2){
            GenerateTargetLetter("c");
        }
        else if(currentTargetNo == 3) {
            GenerateTargetLetter("a");
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
                transform.Rotate(new Vector3(0.0f, 0.0f, turnSpeed * Time.deltaTime * speedMultiplier)); //removed multiplier^2
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
    //may swap to this soon. flat speed, and easy to scale.
    private void MoveCharge() {
        transform.Translate(new Vector2(0, speed * speedMultiplier * Time.deltaTime * speedMultiplier)); //left extra 2 instead of 3 multipliers.
    }

    private void GenerateTargetLetter(string t) { //provide the letter snek head is CURRENTLY AT.
        if (t == "a") { //the snek will generate the pos of the target 1 down, eg, snek arrives at A, generates a new target at B. //this was changed so segments can access 2 prior nodes (the one just reached, and the one preceding it)
            targetA = GenerateTarget(targetC);//this was done for time; a long snake may need older points.
        }
        else if (t == "b") {
            targetB = GenerateTarget(targetA);
        }
        else if (t == "c") {
            targetC = GenerateTarget(targetB);
        }
        else {
            Debug.Log("wtf, we didn't feed a target letter into the genTL method. Send help.");
        }
    }

    private Vector2 GenerateTarget(Vector2 lastTarget) {
        Vector2 newTarget = lastTarget;
        while(Vector2.Distance(newTarget, lastTarget) < minRange) {
            //Debug.Log("attempt to gen new target. possibly regen. CurrentTargetNo = " + currentTargetNo + "distance = " + Vector2.Distance(newTarget, lastTarget));
            newTarget = new Vector2(UnityEngine.Random.Range(maxX * -1, maxX), UnityEngine.Random.Range(maxY * -1, maxY));
        }
        //Debug.Log("final distance = " + Vector2.Distance(newTarget, lastTarget));
        return newTarget;
    }


    //this absolutely should work. but it doesn't. investigate if you please. reactivate in segment code too.
    /*
    private void SetPassSRLayer() {//sets the sr layer to something high, then passes on a number-- down the line.
        sr.sortingOrder = srLayer;
        if(nextSegment != null) {
            Debug.Log("sr order =" + sr.sortingOrder);
            Debug.Log("Beginning to pass from head" + srLayer);
            srLayer--;
            nextSegment.SetPassSRLayer(1);
        }
        //result is head on top, everything cascades down nicely.
    }
    */

    private void FixPhysics() {
        Debug.Log("tfd = " + Time.fixedDeltaTime);
        Time.fixedDeltaTime = Time.fixedDeltaTime / 4; //increasing update rate by 4x temporarily. Drastically improves snek movement.
        //today I learned our bullets are using fixedUpdate. Oops. Guess we better not.
    }

    private void OnDestroy() {
        //Time.fixedDeltaTime = Time.fixedDeltaTime * 4; //returns physics to normal.
        //snek is dead. any stuff to do here?
        if (!Global.bossMedley) {
            // GameObject g = Instantiate(drop.stage2[Random.Range(0, drop.stage2.Length)]) as GameObject;
            //g.transform.position = new Vector3(2, 3, transform.position.z);
            GameObject g = Instantiate(wepDropActual) as GameObject;
            g.transform.position = new Vector3(2, 3, transform.position.z);
            g.GetComponent<WepDrop>().wepID = 101 + UnityEngine.Random.Range(1, wepDrop.GetComponent<DropWeaponOnDestroy>().stage1.Length);
        }

        Global.asteroidCancel = true;
        dropManager.DropAbility();
        Instantiate(death1, this.transform.position, Quaternion.identity);
        Instantiate(death2, this.transform.position, Quaternion.identity);

    }
    public void SetUpSparks() {
        ps = Instantiate(sparks, new Vector3(0, 0, 0), Quaternion.identity);
        ps.transform.SetParent(gameObject.transform);
        ps.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        ps.SetActive(false);
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
            health -= other.gameObject.GetComponent<Projectile>().damage * damageReceivedMultiplier;
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
        if (invulnerable) {
            return;
        }
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
