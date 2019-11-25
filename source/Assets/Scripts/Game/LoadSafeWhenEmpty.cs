using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSafeWhenEmpty : MonoBehaviour
{
	public string nextPartName;
	public int chatPart;
	private Scene s;
	private GameObject[] gameObjects;
	private Chatbox chatbox;
    private GameObject player;
	// Use this for initialization
	void Start ()
	{
		s = gameObject.scene;
		Invoke ("CheckIfEmpty", 1.0f);
        player = GameObject.FindWithTag("Player");
	}

	void CheckIfEmpty(){
		gameObjects = s.GetRootGameObjects ();
		foreach(GameObject g in gameObjects){
			if (g.GetComponent<Enemy>() != null || g.tag.Equals("Boss")){
				Invoke ("CheckIfEmpty", 1.0f);
				return;
			}
		}
        if (nextPartName.Equals("S3P2")) {
            Global.asteroidCancel = true;
        }
		Invoke ("OpenInventory", 5.0f);
	}

	private void OpenInventory(){
		GameObject[] goArray = SceneManager.GetSceneByName ("Main").GetRootGameObjects ();
		GameObject canvas = null;
		StageManager sm = null;
        player.GetComponent<PlayerController>().canShoot = false;
		if (goArray.Length > 0)
		{
			for(int i = 0; i < goArray.Length; i++){
				GameObject rootGo = goArray[i];
				if(rootGo.name.Equals("Canvas")){
					canvas = rootGo;
					chatbox = rootGo.transform.GetChild(4).GetComponent<Chatbox> ();
				}
				if(rootGo.tag == "EnemyBullet"){
					Destroy (rootGo);
				}
				if(rootGo.name.Equals("StageManager")){
					sm = rootGo.GetComponent<StageManager> ();
				}
				if(rootGo.name.Equals("PlayerInventory")){
					rootGo.GetComponent<PlayerInventory> ().SortInventory ();
				}
			}
		}

		if(chatPart != 0){
			ChatTest c = new ChatTest ();
			chatbox.gameObject.SetActive (true);
			chatbox.SetActiveText (c.returnChatByPart(chatPart));
			canvas.transform.GetChild (5).gameObject.GetComponent<InventoryPanel> ().PopulateList ();
			canvas.transform.GetChild (5).gameObject.GetComponent<AbilityInventoryPanel> ().PopulateList ();
			canvas.transform.GetChild (5).GetComponentInChildren<OKButton> ().position = nextPartName;
		}else{
			canvas.transform.GetChild (5).gameObject.SetActive (true);
			canvas.transform.GetChild (5).gameObject.GetComponent<InventoryPanel> ().PopulateList ();
			canvas.transform.GetChild (5).gameObject.GetComponent<AbilityInventoryPanel> ().PopulateList ();
			canvas.transform.GetChild (5).GetComponentInChildren<OKButton> ().position = nextPartName;
			if(!nextPartName.Equals("S1END") && !nextPartName.Equals("S2END") && !nextPartName.Equals("S3END")
				&& !nextPartName.Equals("S4END") && !nextPartName.Equals("S5END")){
				sm.IncrementSafeScene ();
			}
		}
			
		int sceneCount = SceneManager.sceneCount;
		for(int i = 0; i < sceneCount && sceneCount >= 3; i++){
			Scene scene = SceneManager.GetSceneAt (i);
			if(scene.name != "Main" && scene.name != "S3AS"){
				SceneManager.UnloadSceneAsync (scene);
			}
		}

		SceneManager.UnloadSceneAsync (s);
	}

}

