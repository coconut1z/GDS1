using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnToOriginalPosition : MonoBehaviour {

	public Vector3 currentPosition;
	private Vector3 originalPosition;
	public float timer;
    public bool inPosition = false;
    private AudioManager audioManager;

    // Use this for initialization
    void Start () {
        audioManager = AudioManager.instance;
        if (audioManager == null) {
            Debug.LogError("No audio manager found in scene, whoops");
        }
    }
	
	// Update is called once per frame
	void Update () {
		if(timer < 1){
			timer += Time.deltaTime * 0.5f;
			transform.localPosition = Vector3.Lerp (currentPosition, originalPosition, timer);
		}
        if (!inPosition) {
            if(timer >= 1) {
                audioManager.PlaySound("HeavyMetalLock1");
                inPosition = true;
            }
        }
	}

	public void CustomStart(){
		originalPosition = transform.localPosition;
		transform.localPosition = currentPosition;
		timer = 0;
		gameObject.SetActive (false);
	}
}
