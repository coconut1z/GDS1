using UnityEngine;
using System.Collections;

public abstract class EnemyProjectile : MonoBehaviour
{

	protected virtual float damage { get; set;}
	protected virtual float speed { get; set;}
	protected virtual float radius { get; set;}
	protected virtual float boundX1 { get; set;}
	protected virtual float boundX2 { get; set;}
	protected virtual float boundY1 { get; set;}
	protected virtual float boundY2 { get; set;}
	public virtual Transform player { get; set;}

	// Use this for initialization
	protected virtual void Start ()
	{
		if(Global.specialBounds == 2){
			boundX1 = -12f;
			boundX2 = 10f;
			boundY1 = -7.4f;
			boundY2 = 7.4f;
		}else if(Global.specialBounds == 3){
			boundX1 = -18f;
			boundX2 = 18f;
			boundY1 = -10f;
			boundY2 = 10f;
		}else{
			boundX1 = -7f;
			boundX2 = 7f;
			boundY1 = -5f;
			boundY2 = 5f;
		}

	}

	// Update is called once per frame
	protected virtual void Update () {
		ProjectileMovement ();
		DestroyOutOfBounds ();
		PlayerCollision ();
		//Debug.Log (speed);
	}


	protected virtual void DestroyOutOfBounds(){
		if(transform.position.x > boundX2 || transform.position.x < boundX1 ||
			transform.position.y > boundY2 || transform.position.y < boundY1){
			Destroy (gameObject);
		}
	}

	public abstract void ProjectileMovement ();

	public virtual void PlayerCollision (){
		try{
			if(Vector2.Distance(player.position, transform.position) < radius){
                if (!player.GetComponent<PlayerController>().phased) {
                    player.GetComponent<PlayerController>().TakeDamage(damage);
					if(gameObject.tag != "EnemyBulletNoDelete"){
						Destroy(gameObject);
					}
                }
			}
		}catch{
			Debug.Log("Enemy Bullet Player Transform has not been set! Did you forget?");
		}

	}

	public void setSpeed(float speed){
		this.speed = speed;
	}

	public void Setup (Transform player){
		this.player = player;
	}

	public void Setup (Transform player, float speed){
		this.player = player;
		this.speed = speed;
	}

	public void Setup (Transform player, float speed, float damage){
		this.player = player;
		this.speed = speed;
		this.damage = damage;
	}

	public void SetRadius(float radius){
		this.radius = radius;
	}
}

