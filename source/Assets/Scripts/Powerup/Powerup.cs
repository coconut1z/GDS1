using UnityEngine;
using System.Collections;

public abstract class Powerup : MonoBehaviour
{
    public virtual float durationTimer { get; set; }
    public virtual float duration { get; set; }
    public virtual GameObject player { get; set; }
    public virtual PlayerController playerController { get; set; }
    public virtual bool activated { get; set; }



    // Use this for initialization
    protected virtual void Start()
    {
        durationTimer = 0;
        duration = 0;
        player = GameObject.FindWithTag("Player");
        playerController = player.GetComponent<PlayerController>();
        activated = false;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        durationTimer -= Time.deltaTime;
        if (durationTimer < 0 && activated) {
            activated = false;
            Expire();
        }
    }


    public virtual void OnTriggerEnter2D(Collider2D collision)
	{
        if (collision.GetComponent<Collider2D>().tag == "Player")
        {
            activated = true;
            this.GetComponent<SpriteRenderer>().enabled = false;
            durationTimer = duration;
            Initiate();
            this.GetComponent<Collider2D>().enabled = false;
        }
	}

	public abstract void Initiate();

    public abstract void Expire();
}
