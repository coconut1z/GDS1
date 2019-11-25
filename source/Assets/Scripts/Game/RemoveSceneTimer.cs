using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RemoveSceneTimer : MonoBehaviour {

	public float timer;

	// Use this for initialization
	void Start () {
		Invoke ("RemoveScene", timer);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void RemoveScene(){
		SceneManager.UnloadSceneAsync (gameObject.scene);
	}
}
