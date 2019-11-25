using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisableRaycast : MonoBehaviour {

	private Image target;

	// Use this for initialization
	void Start () {
		target = GetComponent<Image> ();
		target.raycastTarget = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
