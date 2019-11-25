using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WepNameUI : MonoBehaviour {

	public Transform a;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		WeaponDraggable[] w = a.GetComponentsInChildren<WeaponDraggable> ();
		for(int i = 0; i < w.Length; i++){
			transform.GetChild (w [i].slot - 1).GetComponent<Text> ().text = w [i].nameUI;
		}
	}
}
