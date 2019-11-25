using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollBackground : MonoBehaviour {

	private float originalHeight;
	private float scrollAmount;
	private float scrollSpeed;
	private SpriteRenderer bg;

	// Use this for initialization
	void Start () {
		bg = GetComponent<SpriteRenderer> ();
		originalHeight = bg.size.y;
		scrollSpeed = 1.5f;
	}
	
	// Update is called once per frame
	void Update () {
		bg.size = new Vector2 (bg.size.x, bg.size.y + scrollSpeed * Time.deltaTime);
		if(bg.size.y >= originalHeight + 8.22f){
			bg.size = new Vector2 (bg.size.x, originalHeight);
		}
	}
}
