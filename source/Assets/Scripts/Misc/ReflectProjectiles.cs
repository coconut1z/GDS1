using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectProjectiles : MonoBehaviour {

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    public virtual void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.tag == "PlayerBullet") {
            col.transform.Rotate(Vector3.right);
        }
        if (col.gameObject.tag == "PlayerBulletPenetrate") {

        }
    }
}
