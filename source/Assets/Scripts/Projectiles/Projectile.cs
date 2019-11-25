using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour {

	protected Rigidbody2D rb;
	public virtual float damage { get; set;}
	public virtual float speed { get; set;}
    public virtual float damageMultiplier { get; set; }
	public virtual Vector2 velocityDisplacement{ get; set;}
    public virtual PlayerController playerController { get; set; }
    public GameObject particleHit;
    public GameObject particleShoot;
    public bool onScreen;

    protected virtual float boundX1 { get; set; }
    protected virtual float boundX2 { get; set; }
    protected virtual float boundY1 { get; set; }
    protected virtual float boundY2 { get; set; }

	// Use this for initialization
	protected virtual void Start () {
		rb = GetComponent<Rigidbody2D> ();
		speed = 0;
        damage = 0;
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        damageMultiplier = playerController.playerDamageMultiplier * playerController.compositeWeaponry * playerController.lastStand;
        velocityDisplacement = Vector2.zero;

        onScreen = true;

        if (particleShoot)
        {
            Instantiate(particleShoot, transform.position, Quaternion.identity);
        }

        if (Global.specialBounds == 2)
        {
            boundX1 = -11f;
            boundX2 = 9f;
            boundY1 = -6.4f;
            boundY2 = 6.4f;
        }
        else if (Global.specialBounds == 3)
        {
            boundX1 = -17f;
            boundX2 = 17f;
            boundY1 = -9f;
            boundY2 = 9f;
        }
        else
        {
            boundX1 = -6f;
            boundX2 = 6f;
            boundY1 = -4f;
            boundY2 = 4f;
        }
	}
	
	// Update is called once per frame
	protected virtual void Update () {

	}

	protected virtual void FixedUpdate(){
		ProjectileMovement ();
	}

	public abstract void ProjectileMovement ();

	protected virtual void OnTriggerExit2D(Collider2D col){
		if(col.gameObject.tag == "GameArea"){
            //Invoke("LaunchProjectile", 2);
            //particleHit = null;
            Invoke("DestroySelf", 0.5f);
            //prevents particles and other OnDestroy stuff causing issues on the border of the screen.
		}
	}

    protected virtual void DestroySelf() {
        Destroy(gameObject);
    }
	protected virtual void OnDestroy()
	{
        if (transform.position.x > boundX2 || transform.position.x < boundX1 ||
            transform.position.y > boundY2 || transform.position.y < boundY1)
        {
            particleHit = null;
        }

        if (particleHit && onScreen) {
            Instantiate(particleHit, transform.position, Quaternion.identity);
        }
	}

	// OLD method for out of bounds
	/*
	protected virtual void OnBecameInvisible(){
		Destroy (gameObject);
	}
	*/
}
