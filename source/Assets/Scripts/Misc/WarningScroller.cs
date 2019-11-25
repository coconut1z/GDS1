using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarningScroller : MonoBehaviour {

	private SpriteRenderer sr;

	// Use this for initialization
	void Start () {
		sr = GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		sr.size = new Vector2 (sr.size.x + Time.deltaTime * 2, sr.size.y);
	}
}
