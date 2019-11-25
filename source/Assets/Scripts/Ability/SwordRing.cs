using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SwordRing : MonoBehaviour
{
    public List<GameObject> swords;
    public GameObject player;
    public List<BoxCollider2D> swordsCol;
    public List<SpriteRenderer> swordsSr;
    bool fadingIn;
    float t;

	// Use this for initialization
	void Start()
	{
        player = GameObject.FindWithTag("Player");

        foreach (GameObject sword in swords) {
            swordsSr.Add(sword.GetComponent<SpriteRenderer>());
            swordsCol.Add(sword.GetComponent<BoxCollider2D>());
        }

        fadingIn = true;

        foreach(BoxCollider2D col in swordsCol) {
            col.enabled = false;
        }

        Invoke("ArmSwords", 5);
	}

	// Update is called once per frame
	void Update()
	{
        t += Time.deltaTime * 0.2f;
        if (fadingIn) {
            FadeIn(t);
            Spin();
        }
	}

    void Spin() 
    {
        this.transform.Rotate(0, 0, Time.deltaTime * 200f);
        this.transform.position = player.transform.position;
    }

    void FadeSwordsIn()
    {
        
    }

    void FadeIn(float t)
    {
        foreach (SpriteRenderer sr in swordsSr)
        {
            sr.color = new Color(1f, 1f, 1f, Mathf.SmoothStep(0.0f, 1.0f, t));
        }
    }

    void ArmSwords() {
        fadingIn = false;
        foreach (GameObject sword in swords) {
            sword.AddComponent<Rigidbody2D>();
            sword.AddComponent<HomingSword>();
        }
        foreach (BoxCollider2D col in swordsCol) {
            col.enabled = true;
        }
    }
}
