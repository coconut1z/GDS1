using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour {

	public GameObject stageNodeReference;
	public static StageProgressTracker stageProgressTracker;
	public static int waves;
	public GameObject chatbox;

	private int nextStage;
	private List<StageItem> stageItems;

    public GameObject moveScene;
    private GameObject player;


	void Awake (){
	    //Time.timeScale = 5;
		//Resolution res = Screen.currentResolution;
		//if(res.refreshRate == 60)
		//	QualitySettings.vSyncCount = 1;
		//if(res.refreshRate == 120)
		//	QualitySettings.vSyncCount = 2;
		QualitySettings.vSyncCount = 0;
		//try{
			//setter = GameObject.Find ("Difficulty").GetComponent<DifficultySetter> ();
			//Global.Difficulty = setter.difficulty;
			//Destroy (setter.gameObject);
		//}catch{
		//	print("Assuming starting from main scene rather than title, Setting difficulty to 1");
			//Global.Difficulty = 1;
		//}

	}

	// Use this for initialization
	void Start () {
		waves = 0;
		stageItems = new List<StageItem> ();
		stageProgressTracker = stageNodeReference.GetComponent<StageProgressTracker> ();
		Invoke ("LateStart", 0.01f);
        player = GameObject.FindWithTag("Player");
    }

	private void LateStart(){
		if(Global.tutorial){
			LoadTutorial ();
		}
	}
	
	// Update is called once per frame
	void Update () {
		for(int i = 0; i < stageItems.Count; i++){
			stageItems [i].UpdateTimer();
			if(stageItems[i].IsOverTime()){
				stageItems [i].LoadSceneAsync ();
				stageItems.RemoveAt (i);
				i--;
			}
		}
	}

	public void IncrementSafeScene(){
		stageProgressTracker.setActiveNode (waves);
		if(waves > 0){
			stageProgressTracker.setCompleteNode (waves-1);
		}
		waves++;
	}

	public void LoadTutorial(){
		stageProgressTracker.setupTutorialNodes ();
	}

	public void LoadStage1(){
		Global.Stage = 1;
        AddSceneToBeLoaded ("S1P1", 1.0f);
		AddSceneToBeLoaded ("S1P2", 5.0f);
		AddSceneToBeLoaded ("S1P3", 12.0f);
		AddSceneToBeLoaded ("S1P4", 18.0f);
		AddSceneToBeLoaded ("S1P5", 30.0f);
		AddSceneToBeLoaded ("S1P6", 38.0f);
		AddSceneToBeLoaded ("S1P7", 45.0f);
    }

    public void LoadStage1Part2(){
		AddSceneToBeLoaded ("S1P8", 1f);

	}

	public void LoadStage1Part25(){
		AddSceneToBeLoaded ("S1P9", 1f);
		AddSceneToBeLoaded ("S1P10", 11f);
		AddSceneToBeLoaded ("J3", 23f);
	}

	public void LoadStage1Part3(){
		AddSceneToBeLoaded ("S1PB", 1f);
	}

	public void LoadStage2(){
		Global.Stage = 2;
		stageItems.Clear ();
		waves = 0;
		stageProgressTracker.setupStage2Nodes ();

        AddSceneToBeLoaded("S2P1", 3.0f);     //C5
        AddSceneToBeLoaded("S2P2", 8.0f);     //C6
        AddSceneToBeLoaded("S2P3", 16.0f);    //C7
        AddSceneToBeLoaded("S2P4", 22.0f);    //C4
        AddSceneToBeLoaded("S2P5", 34.0f);    //C8
        AddSceneToBeLoaded("S2P6", 44.0f);    //J1
    }

	public void LoadStage2Part2(){
		AddSceneToBeLoaded("S2P7", 1.0f);    //C3
		AddSceneToBeLoaded("S2P8", 9.0f);    //J2
		AddSceneToBeLoaded("S2P9", 18.0f);    //J4
		AddSceneToBeLoaded("S2P10", 28.0f);   //J5
		AddSceneToBeLoaded("S2P11", 38.0f);   //J3
	}

	public void LoadStage2Part3(){
        AddSceneToBeLoaded("S2PB", 1.0f);   //S2PB
    }


	public void LoadStage3(){
		Global.Stage = 3;
		stageItems.Clear ();
		waves = 0;
		stageProgressTracker.setupStage3Nodes ();

        AddSceneToBeLoaded("S3AS", 1.0f);
        AddSceneToBeLoaded("S3P1", 8.0f);
        AddSceneToBeLoaded("S3P2", 23.0f);
        AddSceneToBeLoaded("S3P3", 38.0f);
        AddSceneToBeLoaded("S3P4", 53.0f);

    }

    public void LoadStage3Part2() {
        AddSceneToBeLoaded("S3P5", 1.0f);
        AddSceneToBeLoaded("S3P6", 16.0f);
        AddSceneToBeLoaded("S3PB", 33.0f);
    }

    public void testStage3() {
        AddSceneToBeLoaded("S3AS", 1.0f);
        AddSceneToBeLoaded("S3PB", 1.0f);
    }

	public void LoadStage4(){
		Global.Stage = 4;
		stageItems.Clear ();
		waves = 0;
		stageProgressTracker.setupStage4Nodes ();

		AddSceneToBeLoaded("S4P1", 1.0f);
		AddSceneToBeLoaded("S4P2", 22.0f);
		AddSceneToBeLoaded("S4P3", 30.0f);
		AddSceneToBeLoaded("S4P4", 42.0f);
		AddSceneToBeLoaded("S4P5", 52.0f);
		AddSceneToBeLoaded("S4P6", 70.0f); 
	}


	public void LoadStage4Part2(){
		AddSceneToBeLoaded("S4P7", 1);
		AddSceneToBeLoaded("S4P8", 19.0f);
		AddSceneToBeLoaded("S4P9", 20.0f);
		AddSceneToBeLoaded("S4P10", 40.0f);
	}

	public void LoadStage4Boss(){
        AddSceneToBeLoaded("S4PB", 1.0f);   //Blue ship boss
	}

	public void LoadStage5(){
		Global.Stage = 5;
		stageItems.Clear ();
		waves = 0;
		stageProgressTracker.setupStage5Nodes ();
		AddSceneToBeLoaded("S5P1", 1f);
		AddSceneToBeLoaded("S5P2", 16f);
		AddSceneToBeLoaded("S5P3", 26f);
		AddSceneToBeLoaded("S5P4", 41f);
		AddSceneToBeLoaded("S5P5", 51f);
		AddSceneToBeLoaded("S5P6", 66f); 
	}

	public void LoadStage5Part2(){
		AddSceneToBeLoaded("S5P7", 1f);
		AddSceneToBeLoaded("S5P8", 21f);       //part one of joint wave
		AddSceneToBeLoaded("S5P9", 23.5f);     //part two of join wave - 2.5 seconds after the first
		AddSceneToBeLoaded("S5P10", 26f);      //part three of joint wave - 2.5 seconds after the second
		AddSceneToBeLoaded("S5P11", 27);      //part four of joint wave - 1 second after the third
		AddSceneToBeLoaded("S5P12", 47f);
		AddSceneToBeLoaded("S5P13", 65f);
	}

	public void LoadStage5Boss(){
        AddSceneToBeLoaded ("S5PB", 1.0f);  //space station boss
    }

    public void LoadStage6Part1() {
		Global.Stage = 6;
		stageItems.Clear ();
		waves = 0;
		stageProgressTracker.setupStage6Nodes ();
        AddSceneToBeLoaded("S6P1", 1.0f);
        AddSceneToBeLoaded("S6P2", 16.0f);
        AddSceneToBeLoaded("S6P3", 36.0f);
        AddSceneToBeLoaded("S6P4", 53.0f);
        AddSceneToBeLoaded("S6P5", 69.0f);
    }

    public void LoadStage6Part2()
    {
        AddSceneToBeLoaded("S6P6", 1.0f);
        AddSceneToBeLoaded("S6P7", 21.0f);
        AddSceneToBeLoaded("S6P8", 41.0f);
        AddSceneToBeLoaded("S6P9", 56.0f);
        AddSceneToBeLoaded("S6P10", 61.0f);
    }

	public void LoadStage6Boss(){
		Global.Stage = 6;
        AddSceneToBeLoaded ("Warning", 1.0f);
        AddSceneToBeLoaded ("S6PB", 5.0f);  //supercluster boss
    }

    public void LoadBossMedley1() {
        AddSceneToBeLoaded("S1Medley", 1.0f);
    }

    public void LoadBossMedley2()
    {
        AddSceneToBeLoaded("S2Medley", 1.0f);
    }

    public void LoadBossMedley3()
    {
        AddSceneToBeLoaded("S3AS", 1.0f);
        AddSceneToBeLoaded("S3Medley", 5.0f);
    }

    public void LoadBossMedley4()
    {
        AddSceneToBeLoaded("S4Medley", 1.0f);
    }

    public void LoadBossMedley5()
    {
        AddSceneToBeLoaded("S5Medley", 1.0f);
    }

    public void LoadBossMedley6()
    {
        //AddSceneToBeLoaded("AsteroidsMedley", 1.0f);
        LoadStage6Boss();
    }

    public void ChangeStage(GameObject chatbox, int nextStage){
		this.chatbox = chatbox;
		this.nextStage = nextStage;
		//if(this.nextStage == 4){
		//	this.nextStage = 5;
		//}
		//if(this.nextStage == 3){
		//	this.nextStage = 4;
		//}
		Invoke ("DelayChatbox", 2f);

        player.transform.position = new Vector3(0, 0, 0);
        player.GetComponent<PlayerController>().tpStartPs.Play();

        Invoke("StageAnimation", 0.5f);
	}

    private void StageAnimation() {
        player.GetComponent<PlayerController>().tpEndPs.Play();
        Instantiate(moveScene, new Vector3(0, 0, 0), Quaternion.identity);

    }

	private void DelayChatbox(){
		chatbox.SetActive (true);
		Global.Stage = nextStage;
	}

	private void AddSceneToBeLoaded(string sceneName, float delay){
		stageItems.Add (new StageItem (sceneName, delay));
	}

	private class StageItem{

		public string sceneName;
		public float delay;
		public float timer;

		public StageItem(string sceneName, float delay){
			this.sceneName = sceneName;
			this.delay = delay;
			timer = 0;
		}

		public void LoadSceneAsync(){
			SceneManager.LoadSceneAsync (sceneName, LoadSceneMode.Additive);
			if(waves > 0){
				stageProgressTracker.setCompleteNode (waves-1);
			}
			stageProgressTracker.setActiveNode (waves);
			waves++;
		}

		public void UpdateTimer(){
			timer += Time.deltaTime;
		}

		public bool IsOverTime(){
			return timer >= delay;
		}

	}
}
