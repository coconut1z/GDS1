using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReticleManager : MonoBehaviour {
	public Texture2D cursorTexture;
	public CursorMode cursorMode = CursorMode.Auto;
	public Vector2 hotSpot;
	// Use this for initialization

	void Awake(){
		hotSpot = new Vector2 (cursorTexture.width / 2, cursorTexture.height / 2);
		Cursor.SetCursor (cursorTexture, hotSpot, cursorMode);
	}

	void Start () {

	}

    void Update() {
        HoverCheck();
    }

    private void HoverCheck() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1000.0f)) {
            if (hit.collider.tag == "UIDraggable") {
                Debug.Log("Detected UI Draggable");
            }
        }
    }
}
