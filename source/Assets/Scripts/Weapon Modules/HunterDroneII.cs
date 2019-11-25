using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HunterDroneII : MonoBehaviour {
    private Vector2 mousePos;
    private float rotateSpeed;
    private float damage;
    private float speed;
    private Rigidbody2D rb;
    private CircleCollider2D targetingCollider;
    private float closeRange;
    public float targetMaxRange;
    private float multiplier;
    private bool close;
    private bool hunting;
    private float lastTargetTime;
    private float targetingInterval;
    public List<GameObject> targets = new List<GameObject>();
    public GameObject targetGameObject;
    public HunterDroneGunII parentGun;
    //weapon stuff
    public DroneIIShotty fg;
    private float randomTimer;
    private bool targetOffset;

    // Use this for initialization
    void Start() {
        damage = 2.3f; //was 15dmg
        speed = 5.0f; //was 4.0f
        rotateSpeed = 500.0f;
        rb = GetComponent<Rigidbody2D>();
        closeRange = 1.2f;
        targetMaxRange = 3.5f;
        multiplier = 1.0f;
        close = false;
        hunting = false;
        lastTargetTime = Time.time;
        targetingInterval = 1.0f;
        targetingCollider = GetComponent<CircleCollider2D>();
        targetGameObject = null;
        parentGun = GetComponentInParent<HunterDroneGunII>();
        fg = GetComponentInChildren<DroneIIShotty>();
        randomTimer = 0.0f;
        targetOffset = false;
    }

    // Update is called once per frame
    private void Update() {
        if (parentGun == null) { Destroy(gameObject); } //if parent gun is deleted, delete the drone.
        if (!hunting) { return; } //unless docked
        if (targets.Count <= 0) { return; } //unless no targets
        Shoot(); //moar dakka
        //Debug.Log("Trying to shoot lol");
    }

    private void Shoot() {
        fg.Shoot();
    }

    private void LateStart() {
        //findDefaultWeapons();
    }


    private void CheckRangeFromMouse() {
        float distance = Vector2.Distance(mousePos, transform.position);
        if (distance > closeRange) {
            multiplier = 1.0f;
            close = false;
        }
        else {
            multiplier = (0.7f * distance) + 0.35f;
            close = true;
        }
    }

    private void FixedUpdate() {
        if (!hunting) { return; } //if we're not hunting, do nothing.
        ChangeDirection(); //rotates drone
        MoveDrone(); //moves drone forward
        CheckRangeFromMouse(); //as described
        CheckTargetDelay(); //only search for new target every 1s (or on killing target)
    }

    private void CheckTargetDelay() {
        if (Time.time > lastTargetTime + targetingInterval) { //if > 1s since last new target
            FindNewTarget();
        }
    }

    private void FindNewTarget() {
        PurgeDestroyedEnemies();
        PurgeFarEnemies();

        if (targets.Count == 0) {
            return;
        }

        float distance = 0.0f;
        float closestDistance = Mathf.Infinity;

        int currentClosest = 0;
        for (int i = 0; i < targets.Count; i++) {//for each target in list
            distance = Vector2.Distance(targets[i].transform.position, transform.position);//find distance between target and this drone.
            if (distance < closestDistance) {//if new closest;
                currentClosest = i;
                closestDistance = distance;
            }
        }
        targetGameObject = targets[currentClosest]; //WE FINALLY HAVE OUR FUCKING TARGET TO SHOOT AT.
    }

    private void PurgeFarEnemies() {
        float distance = 0;
        for (int i = 0; i < targets.Count; i++) {//for each target in list
            distance = Vector2.Distance(targets[i].transform.position, transform.position);//find distance between target and this drone.
            if (distance > targetMaxRange && targets[i].tag == "Enemy") {//if > 3.5f distance
                targets.RemoveAt(i);//remove it from the list.
                i--;//decrement due to removal.
            }
        }
    }

    private void PurgeDestroyedEnemies() {
        for (int i = 0; i < targets.Count; i++) {
            if (targets[i] == null) {
                //print("Removing " + targets[i].ToString() + " at entry " + i);
                targets.RemoveAt(i);
                i--;//decrement due to removal.
            }
        }
    }

    private void ChangeDirection() {
        UpdateMousePos();
        randomTimer -= Time.deltaTime;
        if (randomTimer <= 0.0f) {
            EmergencyTP();
            targetOffset = !targetOffset; //flip bool
            randomTimer = 1.0f; //reset timer to 1s
        }
        float tempX = 0.0f;
        if (targetOffset) {
            tempX = 0.3f;
        }

        Vector2 direction = mousePos - rb.position;
        float rotateAmount = Vector3.Cross(direction, transform.up).z + tempX;
        rb.angularVelocity = -rotateAmount * rotateSpeed * multiplier;
    }

    private void UpdateMousePos() {
        mousePos = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x,
            Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
    }

    private void MoveDrone() {
        rb.velocity = transform.up * speed * multiplier;
    }


    public void StartHunt() {
        hunting = true;
        transform.parent = null;
        targetingCollider.enabled = true;
    }

    private void EmergencyTP() {
        //returns drone to player if out of bounds.
        Vector3 vp = transform.position;
        if (vp.x > 9 || vp.x < -9 || vp.y > 8 || vp.y < -8) {
            transform.position = GameObject.FindGameObjectWithTag("Player").transform.position;
        }//could also reset to 0,0, but that's even uglier.
    }

    private void OnTriggerStay2D(Collider2D other) {
        PurgeDestroyedEnemies();
        PurgeFarEnemies();
        if (other.CompareTag("Enemy") || other.CompareTag("Boss")) {//if collider belongs to an enemy or a boss
            if (targets.Contains(other.gameObject)) { return; }//check if targetslist already contains said enemy;//if so, skip
            targets.Add(other.gameObject); //thus; if enemy/boss, and non-dupe; add to list.
            //Debug.Log("added new object to list;" + other.gameObject.ToString());
            //Debug.Log("Entry 3 is currently;" + targets[2].ToString());
        }
    }
}
