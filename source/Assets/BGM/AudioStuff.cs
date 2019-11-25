using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioStuff : MonoBehaviour {

	public AudioClip stage2;
	public AudioClip stage3;
	public AudioClip stage4;
	public AudioClip stage5;
	public AudioClip stage6;
	public AudioClip final;
	private bool changed2;
	private bool changed3;
	private bool changed4;
	private bool changed5;
	private bool changed6;
	private bool changedFinal;

	// Use this for initialization
	void Start () {
		changed2 = false;
		changed3 = false;
		changed4 = false;
		changed5 = false;
		changed6 = false;
		changedFinal = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(Global.Stage == 2 && !changed2){
			changed2 = true;
			GetComponent<AudioSource> ().clip = stage2;
			GetComponent<AudioSource> ().Play ();
		}else if(Global.Stage == 3 && !changed3){
			changed3 = true;
			GetComponent<AudioSource> ().clip = stage3;
			GetComponent<AudioSource> ().Play ();
		}else if(Global.Stage == 4 && !changed4){
			changed4 = true;
			GetComponent<AudioSource> ().clip = stage4;
			GetComponent<AudioSource> ().Play ();
		}else if(Global.Stage == 5 && !changed5){
			changed5 = true;
			GetComponent<AudioSource> ().clip = stage5;
			GetComponent<AudioSource> ().Play ();
		}else if(Global.Stage == 6 && !changed6){
			changed6 = true;
			GetComponent<AudioSource> ().clip = stage6;
			GetComponent<AudioSource> ().Play ();
		}else if(Global.Stage == 6 && !changedFinal && Global.finalmusic){
			changedFinal = true;
			GetComponent<AudioSource> ().clip = final;
			GetComponent<AudioSource> ().Play ();
		}
	}
}
