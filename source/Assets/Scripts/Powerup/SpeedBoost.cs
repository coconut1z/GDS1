using UnityEngine;
using System.Collections;

public class SpeedBoost : Powerup
{
    protected override void Start()
    {
        base.Start();
        duration = 3.0f;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

	public override void OnTriggerEnter2D(Collider2D collision)
	{
		base.OnTriggerEnter2D(collision);
	}

	public override void Initiate()
    {
        playerController.speedModified = true;
        playerController.playerNormalSpeed = 8f;
    }

    public override void Expire() 
    {
        playerController.speedModified = false;
        playerController.playerNormalSpeed = 4f;
        Destroy(this.gameObject);
    }
}
