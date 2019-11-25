using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EBCircleChild2 : MonoBehaviour {

    
    private Rigidbody2D rb;
    public Rigidbody2D rbP;

    private float maxSpeed;
    private float speed;
    private float duration;
    private int step;
    private bool moved;
    private float lastTime;

    // Use this for initialization
    void Start() {
        //rbP = gameObject.GetComponentInParent(typeof(Rigidbody2D)) as Rigidbody2D;
        rb = GetComponent<Rigidbody2D>();
        maxSpeed = 50.0f;
        speed = 0.0f;
        duration = 2.0f;
        moved = false;
        lastTime = Time.time;
        //this seems to enable velocity reading. Do not remove.
        rb.velocity = rbP.velocity; //sets to parent speed; it makes no sense to need this line, and yet it does.
    }

    // Update is called once per frame
    void Update() {
        moveChild();
        calcSpeed();

        
    }

    void moveChild() {
        if (Time.time > 2.5f && !moved) {//after 2.5s
            rb.velocity = rbP.velocity; //sets to parent speed; it makes no sense to need this line, and yet it does.
            Vector2 vec1 = rb.transform.up * speed * Time.deltaTime; //calculate the additional speed into a vector.
            rb.velocity += vec1;
            //Vector2 vec3 = new Vector2(rb.GetVector(rbP.position));
        }
    }

    void calcSpeed() {
        speed = (Time.time - lastTime) / duration * maxSpeed;
        Debug.Log(speed);
    }
}