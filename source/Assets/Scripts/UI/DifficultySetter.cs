using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultySetter : MonoBehaviour {

	public int difficulty;

	void Awake(){
		DontDestroyOnLoad (this);
	}
}
