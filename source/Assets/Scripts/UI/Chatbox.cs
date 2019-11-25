using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chatbox : MonoBehaviour {

	public GameObject inventoryPanel;
	private StageManager stageManager;
	private List<string> activeName;
	private List<string> activeText;
	private List<int> activeImage;
	private int position;
	private int size;
	private Text nameText;
	private Text mainText;
	ChatTest chatTest;
	private int part;
	private Transform imageTransform;

	// Use this for initialization
	void Start () {
		nameText = transform.GetChild (1).GetComponent<Text> ();
		mainText = transform.GetChild (2).GetComponent<Text> ();
		chatTest = new ChatTest ();
		SetActiveText (chatTest.returnChatByPart(1));
		stageManager = GameObject.Find ("StageManager").GetComponent<StageManager>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Global.bossMedley){
			position = size;
		}
		if (InputKeys.isPressed (InputKeys.SHOOT)) {
			position++;
			if (position < size) {
				UpdateText ();
			}
		} else if (Input.GetKeyDown (KeyCode.Space)) {
			{
				position = size;
				UpdateText ();
			}
		}
		if(position >= size){
			if(part % 2 == 0){
				SetActiveText (chatTest.returnChatByPart(part+1));
				stageManager.ChangeStage (gameObject, part/2 + 1);
				gameObject.SetActive (false);
			}else{
				inventoryPanel.SetActive (true);
			}

		}
	}

	private void UpdateText(){
		nameText.text = activeName [position];
		mainText.text = activeText [position];
		for(int i = 0; i < imageTransform.childCount; i++){
			imageTransform.GetChild (i).gameObject.SetActive (false);
		}
		imageTransform.GetChild (activeImage [position]).gameObject.SetActive (true);

	}

	public void SetActiveText(Chat chat){
		activeName = chat.name;
		activeText = chat.text;
		activeImage = chat.imageNumber;
		nameText.text = activeName [0];
		mainText.text = activeText [0];
		imageTransform = transform.GetChild (0);
		for(int i = 0; i < imageTransform.childCount; i++){
			imageTransform.GetChild (i).gameObject.SetActive (false);
		}
		imageTransform.GetChild (activeImage [0]).gameObject.SetActive (true);
		position = 0;
		size = chat.text.Count;
		part = chat.part;
	}
}
