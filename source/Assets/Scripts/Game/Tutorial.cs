using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour {

	public GameObject[] tut;
	public Transform equipped;
	public GameObject abilityactive;
	public Transform inventPanel;
	public GameObject okcheck;
	public int pos;
	public StageProgressTracker stageProgressTracker;
	public GameObject spawner;

	private bool node;
	private PlayerController p;

	// Use this for initialization
	void Start () {
		pos = 0;
		p = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerController> ();
		p.canMove = false;
		p.canShoot = false;
		Global.tutorial = true;
		node = false;

		stageProgressTracker.setActiveNode (0);
	}
	
	// Update is called once per frame
	void Update () {
		print (pos);
		if(pos < tut.Length && !tut[pos].activeSelf){
			if(pos == 6 && !inventPanel.gameObject.activeSelf){
				tut [pos].SetActive (true);
			}else if(pos != 6){
				tut [pos].SetActive (true);
			}

		}

		if(Input.GetKeyDown(KeyCode.G)){
			SceneManager.LoadScene("Title");
			Global.tutorial = false;
		}

		if(Input.GetKeyDown(KeyCode.Mouse0)){
			if(pos != 2 && pos != 3 && pos != 4 && pos != 5 && pos != 10 && pos != 11 && pos != 9){
				pos++;
				for(int i = 0; i < tut.Length; i++){
					tut [i].SetActive (false);
				}
			}
		}
		if(pos == 2){
			if((equipped.GetChild(3).name == "Basic (1)" && equipped.GetChild(2).name == "Basic (2)")||
				(equipped.GetChild(2).name == "Basic (1)" && equipped.GetChild(3).name == "Basic (2)")){
				
			}else{
				pos++;
				for(int i = 0; i < tut.Length; i++){
					tut [i].SetActive (false);
				}
			}
		}
		if(pos == 3){
			if(!abilityactive.activeSelf){

			}else{
				pos++;
				for(int i = 0; i < tut.Length; i++){
					tut [i].SetActive (false);
				}
			}
		}
		if(pos == 4){
			if(inventPanel.GetComponent<AbilityDragger>()){
				if(inventPanel.GetComponent<AbilityDragger>().tutDrag){
					pos++;
					for(int i = 0; i < tut.Length; i++){
						tut [i].SetActive (false);
					}
				}
			}
		}
		if(pos == 5){
			if(!okcheck.activeSelf){
				pos++;
				for(int i = 0; i < tut.Length; i++){
					tut [i].SetActive (false);
				}
				p.canMove = false;
				p.canShoot = false;
			}
		}
		/*if(pos == 8){
			if (Input.GetKeyDown (KeyCode.Mouse0)) {
				for(int i = 0; i < tut.Length; i++){
					tut [i].SetActive (false);
				}
				stageProgressTracker.setActiveNode (1);
				stageProgressTracker.setCompleteNode (0);
				pos++;
			}
		}
		*/
		if(pos == 6){
			if(!okcheck.gameObject.activeSelf){
				if (Input.GetKeyDown (KeyCode.Mouse0)) {
					for(int i = 0; i < tut.Length; i++){
						tut [i].SetActive (false);
					}
					pos++;
				}
			}
		}
		if(pos == 9 && !node){
			node = true;
			stageProgressTracker.setCompleteNode (0);
			stageProgressTracker.setActiveNode (1);
		}
		if(pos == 9 && node){
			if (InputKeys.isDown (InputKeys.UP) ||
			    InputKeys.isDown (InputKeys.DOWN) ||
			    InputKeys.isDown (InputKeys.LEFT) ||
				InputKeys.isDown (InputKeys.RIGHT)){
				p.canMove = true;;
				pos++;
				for(int i = 0; i < tut.Length; i++){
					tut [i].SetActive (false);
				}
			}
		}
		if(pos == 10 && Input.GetKeyDown(KeyCode.Mouse0)){
			pos++;
			for(int i = 0; i < tut.Length; i++){
				tut [i].SetActive (false);
			}
			p.canShoot = true;
		}
		if(pos == 11){
			if(!spawner.activeSelf){
				spawner.SetActive (true);
			}
		}
	}
}
