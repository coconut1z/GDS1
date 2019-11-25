using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//attach this script to a game object. It's now the rider.
//attach empty gameobjects with particle systems as children of the rider.
//the rider will constantly update its position with a target gameobject's transform.
//particles won't have any world velocity, but will otherwise work fine

    //on spawn, it will detach itself from the parent, and ride along.

public class ParticleRider : MonoBehaviour {
    public Transform targetTransform;
    public float postDeathTime = 0.5f;

	// Use this for initialization
	void Start () {
        if (targetTransform == null) {
            Debug.Log("rider has no parent transform. Plz add.");
            //This was done automatically, and was meant to work. Doesn't due to parent issues. fix if ya like, its an automation thing.
            //targetTransform = GetComponentsInParent<Transform>()[1]; //make sure you use this if you want a parent's component, but you have a component too.
        }
        transform.parent = null;
        transform.position = targetTransform.position;
        transform.localScale = targetTransform.localScale;
        transform.rotation = targetTransform.rotation;
    }
	
	// Update is called once per frame
	void Update () {
        if (targetTransform != null) {
            transform.position = targetTransform.position;
        }
        else {
            postDeathTime -= Time.deltaTime;
            if(postDeathTime <= 0.0f) {
                Destroy(gameObject);
            }
        }
    }
}
