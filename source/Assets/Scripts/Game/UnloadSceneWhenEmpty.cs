using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UnloadSceneWhenEmpty : MonoBehaviour
{
	private Scene s;
	private GameObject[] gameObjects;
	// Use this for initialization
	void Start ()
	{
		s = gameObject.scene;
		Invoke ("CheckIfEmpty", 1.0f);

	}

	void CheckIfEmpty(){
		gameObjects = s.GetRootGameObjects ();
		foreach(GameObject g in gameObjects){
			if (g.GetComponent<Enemy>() != null){
				Invoke ("CheckIfEmpty", 1.0f);
				return;
			}
		}
		SceneManager.UnloadSceneAsync (s);
	}
}

