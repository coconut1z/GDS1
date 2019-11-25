using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbiter : MonoBehaviour {
    //attached object will orbit the player.
    //enable sprite or gizmos to see targeting.
    Transform pTransform;
    public Vector2 Velocity = new Vector2(1, 0);

    [Range(0, 5)]
    public float RotateSpeed = 1f;
    [Range(0, 5)]
    public float Radius = 1f;

    private Vector2 _centre;
    private float _angle;

    // Use this for initialization
    void Start () {
        pTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
	
	// Update is called once per frame
	void Update () {
        Orbit();
	}

    private void Orbit() {
        _angle += RotateSpeed * Time.deltaTime;
        var offset = new Vector2(Mathf.Sin(_angle), Mathf.Cos(_angle)) * Radius;
        Vector2 tempVec = pTransform.position;
        transform.position = tempVec + offset;
    }

    void OnDrawGizmos() {
        Gizmos.DrawSphere(pTransform.position, 0.1f);
        Gizmos.DrawLine(pTransform.position, transform.position);
    }

}
