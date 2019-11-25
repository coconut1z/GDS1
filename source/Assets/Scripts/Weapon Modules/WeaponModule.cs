using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponModule : MonoBehaviour {
	
	public Transform[] shootPos; // Positions where bullets spawn
	public GameObject[] projectiles; // Bullet Prefabs
	public virtual SpriteRenderer sr {get; set;}
	public virtual float shootTime { get; set;} // How much Time.DeltaTime has it been since last shot
	public virtual float shootDelay { get; set;} // How much Time.DeltaTime to shoot
	public virtual float spread { get; set;} // Recoil / Spread of the weapon
	public virtual bool equipped { get; set;} // If the weapon is equipped or floating around
	public virtual Vector2 displacement {get; set;} // Weapons displacement on the weapon slot position
	public virtual Vector2 originalSize {get; set;}
    public virtual float spoolTime { get; set; } // How much wind-up time before shooting (Minigun).
    public virtual float spoolDelay { get; set; } // How much wind-up time before shooting (Minigun).
    public virtual bool spooling { get; set; } // Is the weapon spinning? (Should spin before, and during firing) (Minigun).
    //Sorry Benson, couldn't figure out how to confine this to the minigun script.
    public virtual Animator anim { get; set; } // quick test to grab an animator.
	public virtual int weaponId { get; set;}
	public virtual int stage { get; set;}
    //Ghetto weapon drops
    public bool dropped;
    public AudioManager audioManager;


    // Use this for initialization
    protected virtual void Start () {
		sr = GetComponent<SpriteRenderer> ();
		shootTime = 0;
		shootDelay = 0;
		spread = 0;
		//equipped = false;
		displacement = Vector2.zero;
        spoolTime = 0;
        spoolDelay = 0;
        spooling = false;
        anim = null;
        Dropped(true);
        audioManager = AudioManager.instance;
        if (audioManager == null)
        {
            Debug.LogError("No audio manager found in scene, whoops");
        }
	}
	
	// Update is called once per frame
	protected virtual void Update () {
		if(equipped){
			lookAtMouse ();
			shootTime += Time.deltaTime;
		}else{
			transform.Translate (new Vector3 (0, -1 * Time.deltaTime, 0));
		}
		if(transform.position.y < - 10 && !equipped){
			Destroy (gameObject);
		}
    }

	// Shoot functionality for weapon
	public abstract void Shoot();

	public abstract int ReturnId ();

	// Rotates the weapon towards mouse
	protected virtual void lookAtMouse(){
		float AngleRadToMouse = Mathf.Atan2 (
			Camera.main.ScreenToWorldPoint(Input.mousePosition).y - transform.position.y,
			Camera.main.ScreenToWorldPoint(Input.mousePosition).x - transform.position.x
		);
		float AngleToDeg = (180 / Mathf.PI) * AngleRadToMouse - 90;
		transform.rotation = Quaternion.Euler (0, 0, AngleToDeg);
	}

    //Ghetto Weapon Code
    public void Dropped(bool dropped) {
        this.dropped = dropped;
    }

    protected virtual void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "GameArea")
        {
           // Destroy(gameObject);
        }
    }

	public void OverrideStart(){
		Start ();
	}
}	
