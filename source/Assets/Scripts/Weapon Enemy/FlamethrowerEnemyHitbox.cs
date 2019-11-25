using UnityEngine;
using System.Collections;

public class FlamethrowerEnemyHitbox: MonoBehaviour
{
    private AudioManager audioManager;
    // Use this for initialization
    void Start()
	{
        //caching //copy me to any script that triggers a sound
        audioManager = AudioManager.instance;
        if (audioManager == null) {
            Debug.LogError("No audio manager found in scene, whoops");
        }
        
    }

    void LateStart() {

    }
	// Update is called once per frame
	void Update()
	{
			
	}

    void FixedUpdate() {
        audioManager.PlayLoopedSound("Flamethrower1");
    }
	private void OnTriggerStay2D(Collider2D collision)
	{
        if (collision.GetComponent<Collider2D>().tag == "Player") {
            GameObject player = collision.gameObject;
            player.GetComponent<PlayerController>().TakeDamage(1);
        }
	}

    private void OnDestroy() {
        audioManager.StopSound("Flamethrower1");
    }
}
