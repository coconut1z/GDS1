using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddParticle : MonoBehaviour {

	// Use this for initialization
	void Start () {
		for(int i = 0; i < transform.childCount; i++){
			Transform t = transform.GetChild (i);
			t.gameObject.AddComponent<ParticleSystem> ();
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
