using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebrisOnDeath : MonoBehaviour {

    public GameObject debris;
    private GameObject g;

	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnDestroy() {
        g = Instantiate(debris);
        g.transform.position = transform.position;
        g.transform.rotation = transform.rotation;
    }
}
