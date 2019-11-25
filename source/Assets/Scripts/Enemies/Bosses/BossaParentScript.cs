using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class BossaParentScript : MonoBehaviour {

    private Rigidbody2D playerRb;
    private Vector2 targetPosition;
    private Vector2 playerPosition;
    private float pathfindDelay;
    private float lastPathfindTime;
    private float offsetY;
    private float speed;
    private float deathSpeed;
    private float speedVariance;
    private BossaCoreScript[] bossaPieces;
    private BossaCoreScript bossaCore;
    private bool dying;
    private float lastAnimationTime;

    public GameObject death1;
    public GameObject death2;

    // Use this for initialization
    void Start () {
        playerRb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>(); //player's rigidbody, for pathfind targeting.
        pathfindDelay = 1.0f; //delay between setting a new course.
        lastPathfindTime = Time.time; //last time a course was set.
        offsetY = 5.0f; //sets the gap Bossa attempts to keep between it and the player.
        speed = 1.0f; //ship speed.
        deathSpeed = 0.1f; //ship speed when dead.
        speedVariance = 0.3f; //unused, to randomise movement times.
        bossaPieces = GetComponentsInChildren<BossaCoreScript>(); //component piece scripts.
        
        dying = false; //death flag.
        lastAnimationTime = 0.0f; //used to animate death.
	}
	
	// Update is called once per frame
	void Update () {
        playerPosition = playerRb.transform.position;//updates playerPosition (uneeded, but makes code neater?).
        setCourse();
        moveBossa();
        checkLife();
        enactDeath();
	}

    private void enactDeath() {
        if (!dying) {
            return;
        }
        //float angle = (Mathf.Sin((Time.time - lastAnimationTime) / 180))-0.0f; //test for rotation
        float xShake = (Mathf.Sin(Time.time*5))/100; //5 cycles per second, /100 for magnitude.
        Vector2 vec1 = new Vector2(xShake, 0.0f);
        transform.Translate(vec1);
        if(transform.position.y < -7.0f) { //if off screen while dying;
            Destroy(gameObject); //delete Bossa.
        }
    }

    private void checkLife() {
        if (dying) {//if you're already dying;
            return;//don't need to be here.
        }

        int x = 3;
        foreach(BossaCoreScript i in bossaPieces) {//for each boss piece
            if(i.getHealth() <= 0) {//if the getHealth func gives <0
                x--;
            }
        }
        if (x > 0) {
            return;
        }
        else {
            //dying = true; //mark for death.
            //speed = deathSpeed; //update speed to dying speed.
            //lastAnimationTime = Time.time;
            //insert cue for next scene here.

            Instantiate(death1, transform.position, Quaternion.identity);
            Instantiate(death2, transform.position, Quaternion.identity);

			GameObject[] goArray = SceneManager.GetSceneByName ("Main").GetRootGameObjects ();
			if (goArray.Length > 0)
			{
				for(int i = 0; i < goArray.Length; i++){
					GameObject rootGo = goArray[i];
					if(rootGo.name.Equals("StageManager")){
						rootGo.GetComponent<StageManager> ().LoadStage1Part25();
					}
				}
			}
            Destroy(gameObject);
        }


    }

    private void moveBossa() {
        transform.position = Vector2.Lerp(transform.position, targetPosition, Time.deltaTime * speed);
    }

    private void setCourse() {
        if (dying) {
            targetPosition = new Vector2(transform.position.x, -12.0f);
            return;
        }
        if(Time.time < lastPathfindTime + pathfindDelay) {//if it hasn't been x time since last pathfind;
            return;//return to main update.
        }//otherwise, set a course.
        float xR = UnityEngine.Random.Range(-1.5f, 1.5f); //sets X random variance.
        float yR = UnityEngine.Random.Range(-2.0f, 2.0f); //sets Y random variance.
        targetPosition = new Vector2(playerPosition.x + xR, playerPosition.y + yR + offsetY); //combines player position with variance and Y offset.
        xR = Mathf.Clamp(targetPosition.x, -7.0f, 7.0f); //prevents exceeding X boundary.
        yR = Mathf.Clamp(targetPosition.y, -2.0f, 4.0f); //prevents exceeding Y boundary.
        targetPosition = new Vector2(xR, yR); //resets targetPosition with clamped values.
        lastPathfindTime = Time.time; //note the calculation time.
    }
}
