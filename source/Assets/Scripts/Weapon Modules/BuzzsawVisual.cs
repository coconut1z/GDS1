using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuzzsawVisual : MonoBehaviour {
    private Buzzsaw buzzsaw;
    private bool fired;
    private SpriteRenderer sr;
	// Use this for initialization
	void Start () {
        buzzsaw = GetComponentInParent<Buzzsaw>();
        sr = GetComponent<SpriteRenderer>();
        fired = false;
	}
	
	// Update is called once per frame
	void Update () {
        fired = buzzsaw.fired;
        if (fired) {
            sr.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        }
        else {
            sr.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        }
	}
}
