using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EBCircleParent2 : MonoBehaviour {
    private Rigidbody2D rb;
    private float speed;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        speed = 0.2f;
        rb.AddForce(rb.transform.up * speed, ForceMode2D.Impulse); //go right, impulse force.
    }
	
	// Update is called once per frame
	void Update () {

	}
}
