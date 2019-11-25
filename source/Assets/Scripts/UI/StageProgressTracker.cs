using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageProgressTracker : MonoBehaviour {

	public Image[] stageNodes;
	public Sprite[] nodeImages;
    public static int waveCount;
	private int flashes;
	private Image flashImage;
	void Start(){
		waveCount = 0;
		stageNodes [14].enabled = false;
		stageNodes [15].enabled = false;
//		stageNodes [16].enabled = false;
		if(Global.bossMedley){
			bossMedley ();
		}
	}

	public void setActiveNode(int node){
		flashes = 8;
		stageNodes [node].color = new Color (255f/255f, 49f/255f, 49f/255f);
		//print (stageNodes [node].color);
//		print (node);
        waveCount++;
		flashImage = stageNodes [node];
		FlashNode ();
//        Debug.Log("Stage: " + Global.Stage + " Wave: " + waveCount);
	}

	private void FlashNode(){
		if(flashImage.color.b == 49f/255f){
			flashImage.color = new Color (1f, 1f, 1f);
		}else{
			flashImage.color = new Color (255f/255f, 49f/255f, 49f/255f);
		}
		flashes--;
		if(flashes > 0){
			Invoke ("FlashNode", 0.4f);
		}
	}

	public void setCompleteNode(int node){
		stageNodes [node].color = new Color (80f/255f, 80f/255f, 80f/255f);
		CancelInvoke ();
	}

	private void resetNodes(){
		for(int i = 0; i < stageNodes.Length; i++){
			stageNodes [i].color = Color.white;
            waveCount = 0;
		}
	}

	public void setupTutorialNodes(){
		for(int i = 0; i < stageNodes.Length; i++){
			stageNodes [i].gameObject.SetActive (false);
		}
		stageNodes [0].gameObject.SetActive(true);
		stageNodes [1].gameObject.SetActive(true);
		stageNodes [2].gameObject.SetActive(true);
		stageNodes [3].gameObject.SetActive(true);
		stageNodes [0].sprite = nodeImages [0];
		stageNodes [1].sprite = nodeImages [0];
		stageNodes [2].sprite = nodeImages [1];
		stageNodes [3].sprite = nodeImages [2];
	}

	public void setupStage2Nodes(){
		resetNodes ();
		stageNodes [8].sprite = nodeImages [0];
		stageNodes [7].sprite = nodeImages [0];
		stageNodes [6].sprite = nodeImages [1];
	}

	public void setupStage3Nodes(){
		resetNodes();
		for(int i = 0; i < stageNodes.Length; i++){
			stageNodes [i].sprite = nodeImages [0];
		}
		for(int i = 7; i < stageNodes.Length; i++){
			stageNodes [i].enabled = false;
		}
		stageNodes [6].sprite = nodeImages [2];

	}

	public void setupStage4Nodes(){
		resetNodes ();
		for(int i = 0; i < stageNodes.Length; i++){
			stageNodes [i].sprite = nodeImages [0];
		}
		stageNodes [8].enabled = true;
		stageNodes [7].enabled = true;
		stageNodes [9].enabled = true;
		stageNodes [10].enabled = true;
		stageNodes [11].enabled = true;
		stageNodes [12].enabled = true;
		stageNodes [13].enabled = false;
		stageNodes [12].sprite = nodeImages [2];
		stageNodes [11].sprite = nodeImages [1];
		stageNodes [6].sprite = nodeImages [1];
	}

	public void setupStage5Nodes(){
		resetNodes ();
		stageNodes [13].enabled = true;
		stageNodes [14].enabled = true;
		stageNodes [15].enabled = true;
		for(int i = 0; i < stageNodes.Length; i++){
			stageNodes [i].sprite = nodeImages [0];
		}
		stageNodes [6].sprite = nodeImages [1];
		stageNodes [14].sprite = nodeImages [1];
		stageNodes [15].sprite = nodeImages [2];
	}

	public void setupStage6Nodes(){
		resetNodes ();
		stageNodes [12].enabled = false;
		stageNodes [13].enabled = false;
		stageNodes [14].enabled = false;
		stageNodes [15].enabled = false;
		for(int i = 0; i < 11; i++){
			stageNodes [i].enabled = true;
		}
		for(int i = 0; i < stageNodes.Length; i++){
			stageNodes [i].sprite = nodeImages [0];
		}
		stageNodes [5].sprite = nodeImages [1];
		stageNodes [11].sprite = nodeImages [2];
	}

	public void bossMedley(){
		for(int i = 0; i < stageNodes.Length; i++){
			stageNodes [i].enabled = false;
		}
	}
}
