using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Analytics;

public class PlayerController : MonoBehaviour {

	public List<WeaponModule> weaponModules;
	public GameObject[] weaponSlots;
    public Text livesText;
    public Image lives1, lives2, lives3;
	public PlayerInventory inventory;
    [SerializeField] private Image healthBar;
    [SerializeField] private Image abilityBar;
    public bool canMove;
    public bool canShoot;
    public bool usingAbility;
    public bool phased;
    public float abilityMeter;
    public static bool frenzied;//will gladly move/change if any suggestion on how to let weapons check the ability module only if the player has it equipped.
    private SpriteRenderer sr;
	private int maxWeapons;
    private int maxAbilities;
	public float speed;
	public float health;
    public float shield; //currently used for; shieldRegenningPassive.cs
    public float lives;
    private GameObject screenWipe;
	private bool isQuitting;
	private float invulnTimer;
	private float invulnDelay;
    private float abilityRechargeTimer;
    private float abilityRechargeDelay;
    public bool speedModified;
    public float playerNormalSpeed;
    public float playerSpeedMultiplier;
    public float playerDamageMultiplier;
    public float abilityRechargeMultiplier;
    public bool damaged;
    public bool swinging;
    public float damageTimer;
	private float boundsX1, boundsX2, boundsY1, boundsY2;

    private bool frozen;
	private float frozenCounter;

    public float lastStand;
    public float compositeWeaponry;

    public Text timeText;

    public float timePlaying;

    public SpriteRenderer hitboxSr;

    public GameObject reviveParticles;

    ParticleSystem healPs;
    ParticleSystem.EmissionModule healEm;

    public ParticleSystem tpStartPs;
    ParticleSystem.EmissionModule tpStartEm;

    public ParticleSystem tpEndPs;
    ParticleSystem.EmissionModule tpEndEm;

    ParticleSystem regenPs;
    ParticleSystem.EmissionModule regenEm;

    public GameObject abilityBarBottom;
    public CanvasGroup abilityBarBottomCanvas;
    public bool inZone;

    public GameObject death;

    public int continuesUsed;

    public string timeTakenText;

    //audio //copy me to any script that triggers a sound
    private AudioManager audioManager;
    //public string spawnSoundName;

    // Use this for initialization
    void Start () {
        weaponModules = new List<WeaponModule>();
		sr = GetComponent<SpriteRenderer> ();
		inventory = GameObject.Find ("PlayerInventory").GetComponent<PlayerInventory> ();
		maxWeapons = 2;
        maxAbilities = 1;
        playerNormalSpeed = 4.0f;
		speed = playerNormalSpeed;
		health = 5f;
        lives = 3f;
        shield = 0f;
		Invoke ("LateStart", 0.01f);
		invulnTimer = 0;
		invulnDelay = 1;
        canMove = true;
		canShoot = true;
        swinging = false;
        usingAbility = false;
        abilityMeter = 100.0f;
        abilityRechargeTimer = 0.0f;
        abilityRechargeDelay = 3.0f;
        abilityRechargeMultiplier = 1f;
        frenzied = false;
        screenWipe = (GameObject)(Resources.Load("ScreenWipe"));
        speedModified = false;
        playerDamageMultiplier = 1.0f;
        playerSpeedMultiplier = 1.0f;
		boundsX1 = -6.6f;
		boundsY1 = -4.4f;
		boundsX2 = 6.6f;
		boundsY2 = 4.4f;
		frozenCounter = 0;
        frozen = false;
        continuesUsed = 0;

        damageTimer = 30;

        timePlaying = 0;

        lastStand = 1;
        compositeWeaponry = 1.0f;

        GameObject.Find("Sword").GetComponent<SpriteRenderer>().enabled = false;
        //GameObject.Find("Shadows").GetComponent<SpriteRenderer>().enabled = false;

        healPs = GameObject.Find("HealEffect").GetComponent<ParticleSystem>();
        healEm = healPs.emission;
        healEm.enabled = false;

        tpStartPs = GameObject.Find("ShadowsStart").GetComponent<ParticleSystem>();
        tpStartEm = tpStartPs.emission;
        //tpStartEm.enabled = false;

        tpEndPs = GameObject.Find("ShadowsEnd").GetComponent<ParticleSystem>();
        tpEndEm = tpEndPs.emission;
        //tpEndEm.enabled = false;

        regenPs = GameObject.Find("RegeneratorEffect").GetComponent<ParticleSystem>();
        regenEm = regenPs.emission;
        regenEm.enabled = false;

        //caching //copy me to any script that triggers a sound
        audioManager = AudioManager.instance;
        if(audioManager == null) {
            Debug.LogError("No audio manager found in scene, whoops");
        }

        if (!Global.bossMedley && !Global.tutorial) {
            timeText.enabled = false;
        }
    }
	
	// Update is called once per frame
	void Update () {
		/*if(Input.GetKeyDown(KeyCode.Escape)){
			if(Time.timeScale != 0){
				Time.timeScale = 0;
			}else{
				Time.timeScale = 1;
			}
		}*/
        if (canMove) {
            Move();
        }
        if (canShoot) {
            Shoot();
        }
        RechargeAbilityMeter();
        DelayAbilityRecharge();
        //Freeze();

        ToggleAbilityZone();
		Death ();
        UpdateAbilityUI();
		invulnTimer -= Time.deltaTime;
        abilityRechargeTimer -= Time.deltaTime;
        damageTimer += Time.deltaTime;
        timePlaying += Time.deltaTime;
        if (Global.bossMedley) {
            UpdateTimePlaying();
        }
	}

    private void UpdateTimePlaying() {

        float minutes = Mathf.Floor(timePlaying / 60);
        float seconds = timePlaying - (minutes * 60);

        if (seconds < 10) {
            timeTakenText = "Time:   " + Mathf.Round(minutes) + " : 0" + (Mathf.Floor(seconds) * 100) / 100f;
        }
        else {
            timeTakenText = "Time:   " + Mathf.Round(minutes) + " : " + (Mathf.Floor(seconds) * 100) / 100f;
        }
        timeText.text = timeTakenText;

    }
		
	private void findDefaultWeapons(){
        GameObject[] weapons = GameObject.FindGameObjectsWithTag("Weapon");
		for(int i = 0; i < weapons.Length ; i++){
			for(int j = 0; j < weaponSlots.Length ; j++){
				if(weapons[i].transform.parent == weaponSlots[j].transform){
					weaponModules.Add (weapons [i].GetComponent<WeaponModule> ());
					weaponModules [weaponModules.Count-1].equipped = true;
					weaponModules [weaponModules.Count-1].sr.sortingOrder = 2;
					j = weaponSlots.Length;
                    //Ghetto weapon code
                    weapons[i].GetComponent<WeaponModule>().Dropped(false);
				}
			}

		}
	}

	private void Shoot(){
		if(InputKeys.isDown(InputKeys.SHOOT)){
			foreach(WeaponModule wep in weaponModules){
				wep.Shoot ();
			}
		}
	}

	private void Move(){
		if(Input.GetKeyDown(KeyCode.O)){
			health += 10;
		}
		//if (!frozen) {
		//	speedModified = false;
		//}
		if (!frozen) {
            if (InputKeys.isDown(InputKeys.SLOW))
            {
                playerSpeedMultiplier = 0.5f;
            }else if (!InputKeys.isDown(InputKeys.SLOW))
            {
                playerSpeedMultiplier = 1.0f;
            }
		}else{
			playerSpeedMultiplier = 0.3f;
		}
		if(frozenCounter >= 0){
			frozenCounter -= Time.deltaTime;
		}else{
			frozen = false;
		}
		if(InputKeys.isDown(InputKeys.RIGHT)){
			if(transform.position.x < boundsX2){
                transform.position = new Vector2 (transform.position.x + (speed * playerSpeedMultiplier * Time.deltaTime), transform.position.y);
			}
		}
		if(InputKeys.isDown(InputKeys.LEFT)){
			if(transform.position.x > boundsX1){
                transform.position = new Vector2 (transform.position.x - (speed * playerSpeedMultiplier * Time.deltaTime), transform.position.y);
			}
		}
		if(InputKeys.isDown(InputKeys.UP)){
			if(transform.position.y < boundsY2){
                transform.position = new Vector2 (transform.position.x, transform.position.y  + (speed * playerSpeedMultiplier * Time.deltaTime));
			}
		}
		if(InputKeys.isDown(InputKeys.DOWN)){
			if(transform.position.y > boundsY1){
                transform.position = new Vector2 (transform.position.x, transform.position.y  - (speed * playerSpeedMultiplier * Time.deltaTime));
			}
		}
	}

	public void SetBounds(int state){
		if(state == 1){
			boundsX1 = -6.6f;
			boundsY1 = -4.4f;
			boundsX2 = 6.6f;
			boundsY2 = 4.4f;
			return;
		}else if(state == 2){
			boundsX1 = -10.75f;
			boundsX2 = 9.25f;
			boundsY1 = -6.5f;
			boundsY2 = 6.5f;
		}else if(state == 3){
			boundsX1 = -17.4f;
			boundsX2 = 17.4f;
			boundsY1 = -9.7f;
			boundsY2 = 9.7f;
		}
	}

    public bool InAbilityZone() {
        if (transform.position.x < boundsX1 + 4.5f && transform.position.y < boundsY1 + 1.0f) {
            inZone = true;
            return true;
        }
        inZone = false;
        return false;
    }

    private void ToggleAbilityZone() {
        float vDistance = Vector2.Distance(new Vector2(transform.position.x, transform.position.y), new Vector2(boundsX1, boundsY1));
        if (vDistance < 5.0f && vDistance > 0) {
            abilityBarBottomCanvas.alpha = vDistance / 5.0f;
        }
    }

    private void RechargeAbilityMeter() {
        if (abilityMeter < 100.0f) {
            if (!usingAbility && abilityRechargeTimer <= 0) {
                abilityMeter += 10f * abilityRechargeMultiplier * Time.deltaTime;
            }
        }
    }

    private void DelayAbilityRecharge() {
        if (usingAbility) {
            abilityRechargeTimer = abilityRechargeDelay;
        }
    }

	private void SwapWeapon(Collider2D col){
		if(InputKeys.isPressed(InputKeys.WEP1)){
			SwapWeapon (0, col);
		}
		if(InputKeys.isPressed(InputKeys.WEP2)){
			SwapWeapon (1, col);
		}
	}

	private void SwapWeapon(int pos, Collider2D col){
		WeaponModule wep = col.gameObject.GetComponent<WeaponModule> ();
		if(weaponSlots[pos].transform.childCount != 0){
			WeaponModule unequip = weaponSlots [pos].transform.GetChild (0).GetComponent<WeaponModule> ();
			weaponModules.Remove (unequip);
			unequip.equipped = false;
			unequip.transform.position = transform.position;
			weaponSlots [pos].transform.GetChild (0).parent = null;
			unequip.transform.localScale = unequip.originalSize;
			unequip.transform.rotation = Quaternion.identity;
			unequip.sr.sortingOrder = 0;

            //Ghetto weapon code
            unequip.Dropped(true);

			//Destroy (weaponSlots [pos].transform.GetChild (0).gameObject);
		}
		if(!wep.equipped){
			wep.transform.localScale = wep.originalSize;
			wep.transform.parent = weaponSlots [pos].transform;
			wep.transform.position = new Vector2(
				weaponSlots [pos].transform.position.x,
				weaponSlots [pos].transform.position.y);
			wep.transform.localPosition = new Vector2 (
				wep.transform.localPosition.x + wep.displacement.x,
				wep.transform.localPosition.y + wep.displacement.y);
			wep.transform.localScale = wep.transform.lossyScale;
			wep.sr.sortingOrder = 2;
			weaponModules.Add (wep);
			wep.equipped = true;

            //Ghetto weapon code
            wep.Dropped(false);
		}
	}

	public void ReplaceWeapon(GameObject wep, int slot){
		GameObject wepobj = Instantiate (wep) as GameObject;
		WeaponModule wepmod = wepobj.GetComponent<WeaponModule> ();
		WeaponModule unequip = weaponSlots [slot - 1].transform.GetChild (0).GetComponent<WeaponModule> ();
		weaponModules.Remove (unequip);
		wepmod.OverrideStart ();
		Destroy (weaponSlots [slot - 1].transform.GetChild (0).gameObject);
		wepmod.transform.localScale = wepmod.originalSize;
		wepmod.transform.parent = weaponSlots [slot - 1].transform;
		wepmod.transform.position = new Vector2(
			weaponSlots [slot - 1].transform.position.x,
			weaponSlots [slot - 1].transform.position.y);
		wepmod.transform.localPosition = new Vector2 (
			wepmod.transform.localPosition.x + wepmod.displacement.x,
			wepmod.transform.localPosition.y + wepmod.displacement.y);
		wepmod.transform.localScale = wepmod.transform.lossyScale;
		wepmod.sr.sortingOrder = 2;
		weaponModules.Add (wepmod);
		wepmod.equipped = true;
	}


	// FOR TESTING PURPOSES
	private void OnTriggerEnter2D(Collider2D col){
        if (col.gameObject.tag == "Enemy" && invulnTimer <= 0 && !swinging) { //collision with enemy damage. Destroys enemy and damages player, same as a bullet.
            TakeDamage(1.0f);
            TriggerCustomEvent();
        }else if (col.gameObject.tag == "EnemyBullet" && invulnTimer <= 0){
			Destroy (col.gameObject);
            TakeDamage(1.0f);
        }
        else if (col.gameObject.tag == "Weapon" && !col.gameObject.GetComponent<WeaponModule>().equipped && !swinging){
			inventory.addWeapon (col.gameObject.GetComponent<WeaponModule> ().weaponId);
			Destroy (col.gameObject);
		}
	}

	private void OnTriggerStay2D(Collider2D collision)
	{
        if (collision.gameObject.tag == "IceBullet")
        {
            Freeze();
            //speedModified = true;
            frozen = true;
			frozenCounter = 0.1f;
            //Destroy(col.gameObject);
        }
        if (collision.gameObject.tag == "SuicideBomb")
        {
            TakeDamage(1.0f);
        }
	}
	/*private void OnTriggerExit2D(Collider2D collision)
	{
        if (collision.gameObject.tag == "IceBullet")
        {
            speedModified = false;
            playerSpeedMultiplier = 1f;
            //Destroy(col.gameObject);
        }
	}*/
	public void TakeDamage(float damage){
		if(invulnTimer <= 0){
            if(shield > 0) { //added this for shieldRegen passive ability. HMU if any issues. -J
                shield--;
                return;
            }
			invulnTimer = invulnDelay;
			health -= damage;
            health = Mathf.Floor(health + 0.15f);
			InvokeRepeating ("Invulnerability", 0, 0.1f);
            damaged = true;
            damageTimer = 0;
            TriggerCustomEvent();
            UpdateHealth();
		}
	}

    public void Freeze() {
        playerSpeedMultiplier = 0.3f;
    }

	//private void OnTriggerStay2D(Collider2D col){
	//	if(col.gameObject.tag == "Weapon"){
	//		SwapWeapon (col);
	//	}
	//}

	private void Death(){
		if(health <= 0){
            lives--;
            UpdateLives();
            if (lives > 0)
            {
                transform.position = new Vector3(0.0f, -2.0f, 0.0f);
                Instantiate(screenWipe, transform.position, Quaternion.Euler(0, 0, 0));
                invulnTimer = 5.0f;
            }
            health = 5.0f;
            UpdateHealth();
            if (lives <= 0) {
                foreach (Projectile o in Object.FindObjectsOfType<Projectile>()) {
                    Destroy(o);
                }
                CancelInvoke("Invulnerability");
                Instantiate(death, this.transform.position, Quaternion.identity);
                sr.color = new Color(1f, 1f, 1f, 0f);
                foreach(GameObject weapon in weaponSlots) {
                    weapon.GetComponentInChildren<SpriteRenderer>().enabled = false;
                }
                canShoot = false;
                canMove = false;
                hitboxSr.enabled = false;
                Invoke("DeathScene", 2f);
            }
            
		}
	}

    private void DeathScene(){
        SceneManager.LoadSceneAsync("Death", LoadSceneMode.Additive);
    }

    public void ResumeGame() {
        transform.position = new Vector3(0.0f, -2.0f, 0.0f);
        GameObject revive = Instantiate(reviveParticles, new Vector3(transform.position.x, transform.position.y + 9.9f, transform.position.z), Quaternion.identity);
        revive.transform.Rotate(new Vector3(90, 90, 0));
        foreach (GameObject weapon in weaponSlots)
        {
            weapon.GetComponentInChildren<SpriteRenderer>().enabled = true;
        }
        sr.color = new Color(1f, 1f, 1f, 1f);
        hitboxSr.enabled = true;
        continuesUsed++;
        Invoke("Revive", 3.6f);
        invulnTimer = 5f;
        UpdateHealth();
    }

    public void Revive() {
        canShoot = true;
        canMove = true;
        Instantiate(screenWipe, transform.position, Quaternion.Euler(0, 0, 0));
    }

	private void OnApplicationQuit(){
		foreach (Projectile o in Object.FindObjectsOfType<Projectile>()) {
			DestroyImmediate(o);
		}

	}

	private void LateStart(){
		findDefaultWeapons ();
        //audioManager.PlaySound("Respawn");
	}

    public void DamagePlayer(float dmg) { //public class to damage the player (for non-projectile damage).
        TakeDamage(dmg);
    }

	public void Invulnerability(){
		if(sr.color.a > 0f){
			sr.color = new Color (1f, 1f, 1f, 0f);
		}else{
			sr.color = new Color (1f, 1f, 1f, 1f);
		}
		if(invulnTimer <= 0){
			CancelInvoke ();
			sr.color = new Color (1f, 1f, 1f, 1f);
            damaged = false;
		}
	}

    //Will probably make the health bar thing universal, otherwise gotta make individual outlines for each ship
    //if they have different health
    public void UpdateHealth() {
        healthBar.fillAmount = health / 5;
        //livesText.text = "Lives: " + lives;
    }

    public void UpdateLives() {
        if (lives == 3) {
            lives1.color = new Color(1f, 1f, 1f, 1f);
            lives2.color = new Color(1f, 1f, 1f, 1f);
            lives3.color = new Color(1f, 1f, 1f, 1f);
        }
        else if (lives == 2) {
            lives1.color = new Color(1f, 1f, 1f, 0f);
            lives2.color = new Color(1f, 1f, 1f, 1f);
            lives3.color = new Color(1f, 1f, 1f, 1f);
        }
        else if (lives == 1) {
            lives1.color = new Color(1f, 1f, 1f, 0f);
            lives2.color = new Color(1f, 1f, 1f, 0f);
            lives3.color = new Color(1f, 1f, 1f, 1f);
        }
        else if (lives == 0) {
            lives1.color = new Color(1f, 1f, 1f, 0f);
            lives2.color = new Color(1f, 1f, 1f, 0f);
            lives3.color = new Color(1f, 1f, 1f, 0f);
        }
    }

    public void UpdateAbilityUI() {
        abilityBar.fillAmount = abilityMeter / 100;
    }

    public void TriggerCustomEvent() {
		
        if (Global.Difficulty == 1) {
            AnalyticsEvent.Custom("HealthLostRec", new Dictionary<string, object>
            {
                { "Stage", "S" + Global.Stage + "W" + StageProgressTracker.waveCount}
            });
        }
        else if (Global.Difficulty == 2)
        {
            AnalyticsEvent.Custom("HealthLostVet", new Dictionary<string, object>
            {
                { "Stage", "S" + Global.Stage + "W" + StageProgressTracker.waveCount}
            });
        } else if (Global.Difficulty == 3)
        {
            AnalyticsEvent.Custom("HealthLostBH", new Dictionary<string, object>
            {
                { "Stage", "S" + Global.Stage + "W" + StageProgressTracker.waveCount}
            });
        }
            

    }

}
