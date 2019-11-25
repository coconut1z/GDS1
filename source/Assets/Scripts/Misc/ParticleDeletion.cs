using UnityEngine;
using System.Collections;

public class ParticleDeletion : MonoBehaviour
{
	// Use this for initialization
	void Start()
	{
        Invoke("DestroySelf", 5.0f);
	}

	// Update is called once per frame
	void Update()
	{
			
	}

    void DestroySelf() {
        Destroy(this.gameObject);
    }
}
