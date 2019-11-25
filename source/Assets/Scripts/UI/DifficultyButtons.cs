using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DifficultyButtons : MonoBehaviour {

	public GameObject difficulty;
	public GameObject bmdifficulty;

	public void Tutorial(){
		SceneManager.LoadScene ("Tutorial");
	}

	public void Recruit(){
		//SceneManager.LoadScene("Main");
		//setter.difficulty = 1;
		Global.Difficulty = 1;
		difficulty.SetActive (true);
	}

	public void Veteren(){
		//SceneManager.LoadScene("Main");
		//setter.difficulty = 2;
		Global.Difficulty = 2;
		difficulty.SetActive (true);
	}

	public void BattleHardened(){
		//SceneManager.LoadScene("Main");
		//setter.difficulty = 3;
		Global.Difficulty = 3;
		difficulty.SetActive (true);
	}

    public void BossMedley() {
        
        //setter.difficulty = 3;
		bmdifficulty.SetActive(true);
        
    }

	public void BM1(){
		Global.Difficulty = 1;
		Global.bossMedley = true;
		SceneManager.LoadScene("Main");
	}

	public void BM2(){
		Global.Difficulty = 2;
		Global.bossMedley = true;
		SceneManager.LoadScene("Main");
	}

	public void BM3(){
		Global.Difficulty = 3;
		Global.bossMedley = true;
		SceneManager.LoadScene("Main");
	}

	public void Stage1(){
		SceneManager.LoadScene("Main");
	}

	public void Stage2(){
		Global.startAt2 = true;
		SceneManager.LoadScene("Main");
	}
	public void Stage3(){
		Global.startAt3 = true;
		SceneManager.LoadScene("Main");
	}
	public void Stage4(){
		Global.startAt4 = true;
		SceneManager.LoadScene("Main");
	}
	public void Stage5(){
		Global.startAt5 = true;
		SceneManager.LoadScene("Main");
	}
	public void Stage6(){
		Global.startAt6 = true;
		SceneManager.LoadScene("Main");
	}
	public void Back(){
		transform.parent.gameObject.SetActive (false);
	}

	public void ExitB(){
		Application.Quit ();
	}

}
