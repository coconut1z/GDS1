using UnityEngine;
using System.Collections;

public class AbilityReward : MonoBehaviour
{
    public SpriteRenderer sr;
    public AbilityModule abilityToBeAdded;
    float t;
    bool fading;
    bool fadingIn;
    bool moving;

    GameObject player;
    private AudioManager audioManager;


    float f;
	// Use this for initialization
	void Start()
	{
        
        fading = false;
        PlayerInventory playerInventory = GameObject.Find("PlayerInventory").GetComponent<PlayerInventory>();
        playerInventory.addAbility(abilityToBeAdded.ReturnId());
        Invoke("StartFadeOut", 2f);
        Invoke("DestroySelf", 4f);
        fadingIn = true;
        moving = false;
        player = GameObject.FindWithTag("Player");
        audioManager = AudioManager.instance;
        if (audioManager == null)
        {
            //Debug.LogError("No audio manager found in scene, whoops");
        }
        audioManager.PlaySound("AbilityReward");

	}

	// Update is called once per frame
	void Update()
	{
        t += Time.deltaTime/2;
        f += Time.deltaTime;
        if (fading) {
            FadeOut(t);
        }
        if (fadingIn) {
            FadeIn(f);
        }
        if (moving){
            Vector3 direction = player.transform.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
            Quaternion lookRotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 4);
            transform.Translate(new Vector2(0, 10f * Time.deltaTime));
        }

	}

    void FadeOut(float t)
    {
        sr.color = new Color(1f, 1f, 1f, Mathf.SmoothStep(1.0f, 0.0f, t));
    }

    void FadeIn(float f)
    {
        sr.color = new Color(1f, 1f, 1f, Mathf.SmoothStep(0.0f, 1.0f, f));
    }

    void StartFadeOut()
    {
        t = 0;
        fading = true;
        fadingIn = false;
        moving = true;
    }

    void DestroySelf() {
        Destroy(gameObject);
    }
}
