using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnekBlade : MonoBehaviour {

    private Quaternion originalQuat;
    public bool isLeft;

	// Use this for initialization
	void Start () {
        originalQuat = transform.localRotation;
        Deactivate();
	}
	
	// Update is called once per frame
	void Update () {
        //Debug.Log("lrEZ = " + transform.localRotation.eulerAngles.z);
		if(transform.localRotation.eulerAngles.z > 10) {
            if(isLeft == false) {
                transform.Rotate(transform.forward, 80 * Time.deltaTime);
            }
            else {
                transform.Rotate(transform.forward, -80 * Time.deltaTime);
            }
        }
	}
    public void Deactivate() {
        transform.localRotation = originalQuat;
        gameObject.SetActive(false);
    }

    private void OnDisable() {
        transform.localRotation = originalQuat;
        //should not be needed. but just in case.
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player") {
            other.GetComponent<PlayerController>().TakeDamage(1); //1 damage.
        }
    }
}
