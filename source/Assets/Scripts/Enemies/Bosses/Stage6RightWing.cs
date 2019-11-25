using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage6RightWing : MonoBehaviour {

	private bool inPosition;
	private bool finalFade;
	private bool finalFadeback;
	private SpriteRenderer[] sr;

	// Use this for initialization
	void Start () {
		inPosition = false;
		finalFade = false;
		finalFadeback = false;
		sr = new SpriteRenderer[2];
		sr [0] = transform.GetChild (0).GetComponent<SpriteRenderer> ();
		sr [1] = transform.GetChild (1).GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		
		if(!inPosition && GetComponent<ReturnToOriginalPosition>().timer >= 1){
			inPosition = true;
			transform.GetChild (0).GetComponent<Stage6DecreaseGun> ().ChangePhase (1);
			transform.GetChild (1).GetComponent<Stage6DecreaseGun> ().ChangePhase (1);
		}

		if(finalFade){
			if(sr[0].color.r > 0.5f){
				sr [0].color = new Color (sr [0].color.r - Time.deltaTime * 0.3f, 
					sr [0].color.b - Time.deltaTime * 0.3f, 
					sr [0].color.g - Time.deltaTime * 0.3f);
				sr [1].color = new Color (sr [1].color.r - Time.deltaTime * 0.3f, 
					sr [1].color.b - Time.deltaTime * 0.3f, 
					sr [1].color.g - Time.deltaTime * 0.3f);
			}else{
				transform.GetChild (0).GetComponent<SpriteRenderer> ().sortingOrder = 0;
				transform.GetChild (1).GetComponent<SpriteRenderer> ().sortingOrder = 0;
				finalFade = false;
			}
		}


		if(finalFadeback){
			transform.GetChild (0).GetComponent<SpriteRenderer> ().sortingOrder = 2;
			transform.GetChild (1).GetComponent<SpriteRenderer> ().sortingOrder = 2;
			if(sr[0].color.r < 1){
				sr [0].color = new Color (sr [0].color.r + Time.deltaTime * 0.3f, 
					sr [0].color.b + Time.deltaTime * 0.3f, 
					sr [0].color.g + Time.deltaTime * 0.3f);
				sr [1].color = new Color (sr [1].color.r - Time.deltaTime * 0.3f, 
					sr [1].color.b + Time.deltaTime * 0.3f, 
					sr [1].color.g + Time.deltaTime * 0.3f);
			}else{
				finalFadeback = false;
			}
		}

	}

	public void ReturnToDefault(){
		transform.GetChild (0).GetComponent<Stage6DecreaseGun> ().ChangePhase (0);
		transform.GetChild (1).GetComponent<Stage6DecreaseGun> ().ChangePhase (0);
	}

	public void SecondPhase(){
		transform.GetChild (0).GetComponent<Stage6DecreaseGun> ().ChangePhase (1);
	}

	public void StartFinalFade(){
		finalFade = true;
	}

	public void StartFinalFadeback(){
		finalFadeback = true;
	}

}
