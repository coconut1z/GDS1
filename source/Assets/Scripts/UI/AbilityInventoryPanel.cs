using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityInventoryPanel : MonoBehaviour
{

    public Transform equippedTransform;
    private Vector2[] epos;
    private List<int> abilities;
    private List<int> equippedAbilities;
    private PlayerInventory playerInventory;
    private GameObject infoPanel;
    private Transform unequippedPanel;

    public List<Text> controlsTexts;
    public List<Text> abilityNameTexts;

    // Use this for initialization
    void Start()
    {
        abilities = new List<int>();
        equippedAbilities = new List<int>();
        epos = new Vector2[4];
        playerInventory = GameObject.Find("PlayerInventory").GetComponent<PlayerInventory>();
        epos[0] = new Vector2(-600, 210);
        epos[1] = new Vector2(-450, 210);
        epos[2] = new Vector2(-600, -30);
        epos[3] = new Vector2(-450, -30);
        infoPanel = (GameObject)(Resources.Load("InventoryPrefabs/AbilityInfoPanel"));

        Invoke("LateStart", 0.1f);
    }

    // Update is called once per frame
    void Update()
    {
		for(int i = 0; i < abilities.Count; i++){
			//print (abilities[i]);
		}
    }


    public void PopulateList(){
        for (int i = 0; i < transform.childCount; i++){
            if (!transform.GetChild(i).name.Equals("AbilityOKButton") &&
                !transform.GetChild(i).name.Equals("EquippedAbilities") &&
                !transform.GetChild(i).name.Equals("AbilitiesText") &&
				!transform.GetChild(i).name.Equals("ScrollAbility") &&
				!transform.GetChild(i).name.Equals("OKButton") &&
				!transform.GetChild(i).name.Equals("EquippedWeapons") &&
				!transform.GetChild(i).name.Equals("WeaponsText") &&
				!transform.GetChild(i).name.Equals("ScrollWep") && 
				!transform.GetChild(i).name.Equals("AbilityInventoryButton") &&
				!transform.GetChild(i).name.Equals("AbilityWeaponButton")){
                Destroy(transform.GetChild(i).gameObject);
            }
            if (transform.GetChild(i).name.Equals("ScrollAbility")){
                unequippedPanel = transform.GetChild(i).GetChild(0).GetChild(0);
                for (int j = 0; j < unequippedPanel.childCount; j++){
                    Destroy(unequippedPanel.GetChild(j).gameObject);
                }
            }
        }

        abilities.Clear();
        for (int i = 0; i < playerInventory.abilitiesInventory.Count; i++)
        {
            abilities.Add(playerInventory.abilitiesInventory[i]);
        }

		float stepY = 260;
        float stepX = 0;
        int row = 1;
        unequippedPanel.GetComponent<RectTransform>().offsetMin = new Vector2(
            unequippedPanel.GetComponent<RectTransform>().offsetMin.x, 0);
        unequippedPanel.GetComponent<RectTransform>().offsetMax = new Vector2(
            unequippedPanel.GetComponent<RectTransform>().offsetMax.x, 1);
        unequippedPanel.GetComponent<RectTransform>().anchorMin = new Vector2(0, 0);
        unequippedPanel.GetComponent<RectTransform>().anchorMax = new Vector2(0, 1);
        unequippedPanel.GetComponent<RectTransform>().pivot = new Vector2(0, 1);
        for (int i = 0; i < abilities.Count; i++)
        {
            GameObject g = Instantiate(infoPanel) as GameObject;
            GameObject abi = Instantiate(GetAbilityUI(abilities[i])) as GameObject;
            abi.transform.parent = g.transform;
            abi.transform.localScale = new Vector3(0.6f, 0.6f, 1);
            g.transform.parent = transform;
            g.transform.localPosition = new Vector2(stepX * 325 + -180, stepY);
            g.transform.localScale = Vector3.one;
            abi.GetComponent<RectTransform>().anchorMax = new Vector2(0, 1);
            abi.GetComponent<RectTransform>().anchorMin = new Vector2(0, 1);
            g.transform.parent = unequippedPanel;
            stepX++;
            if ((i + 1) % 3 == 0)
            {
                stepY -= 225;
                stepX = 0;
                row++;
                if (row > 3)
                {
                    //stepY = 112.5f;
                    Vector2 size = unequippedPanel.GetComponent<RectTransform>().sizeDelta;
                    unequippedPanel.GetComponent<RectTransform>().offsetMin = new Vector2(
                        unequippedPanel.GetComponent<RectTransform>().offsetMin.x,
                        unequippedPanel.GetComponent<RectTransform>().offsetMin.y - 225);
                    //unequippedPanel.localPosition = new Vector3 (unequippedPanel.localPosition.x, 
                    //  unequippedPanel.localPosition.y + 225f/2f, 0);
                    //stepY -= 225f/2f;
                }
            }
            abi.GetComponent<RectTransform>().localPosition = new Vector3(-80, 45, 0);
        }



        /*
        for(int i = 0; i < weapons.Count; i++){
            GameObject g = Instantiate (weapons [i]) as GameObject;
            if(g.GetComponent<WeaponDraggable>().stage == 1){
                g.transform.parent = transform;
                g.transform.localPosition = new Vector2 (step + -180, 300);
                step += 125;
                g.transform.localScale = Vector3.one;
            }else{
                //Destroy (g);
            }
        }
        step = 0;
        for(int i = 0; i < weapons.Count; i++){
            GameObject g = Instantiate (weapons [i]) as GameObject;
            if(g.GetComponent<WeaponDraggable>().stage == 2){
                g.transform.parent = transform;
                g.transform.localPosition = new Vector2 (step + -180, 100);
                step += 125;
                g.transform.localScale = Vector3.one;
            }else{
                //Destroy (g);
            }
        }   
        */
    }

    private GameObject GetAbilityUI(int abilityId)
    {
        switch (abilityId)
        {
            case Global.TELEPORT:
                return (GameObject)(Resources.Load("InventoryPrefabs/Ability/Teleport"));
            case Global.PHASE:
                return (GameObject)(Resources.Load("InventoryPrefabs/Ability/Phase"));
            case Global.SHIELD:
                return (GameObject)(Resources.Load("InventoryPrefabs/Ability/Shield"));
            case Global.STASISBUBBLE:
                return (GameObject)(Resources.Load("InventoryPrefabs/Ability/StasisBubble"));
            case Global.SWORD:
                return (GameObject)(Resources.Load("InventoryPrefabs/Ability/SwordSwing"));
            case Global.SPEEDBOOST:
                return (GameObject)(Resources.Load("InventoryPrefabs/Ability/SpeedBoost"));
            case Global.DRONE:
                return (GameObject)(Resources.Load("InventoryPrefabs/Ability/Drone"));
            case Global.COMPOSITEWEAPONRY:
                return (GameObject)(Resources.Load("InventoryPrefabs/Ability/CompositeWeaponry"));
            case Global.APREGENBOOST:
                return (GameObject)(Resources.Load("InventoryPrefabs/Ability/APRegen"));
            case Global.HEAL:
                return (GameObject)(Resources.Load("InventoryPrefabs/Ability/Heal"));
            case Global.LASTSTAND:
                return (GameObject)(Resources.Load("InventoryPrefabs/Ability/LastStand"));
            case Global.EMP:
                return (GameObject)(Resources.Load("InventoryPrefabs/Ability/EMP"));
            case Global.BFOLASER:
                return (GameObject)(Resources.Load("InventoryPrefabs/Ability/BFOLaser"));
            case Global.BULLETRING:
                return (GameObject)(Resources.Load("InventoryPrefabs/Ability/BulletRing"));
            case Global.ROCKETRING:
                return (GameObject)(Resources.Load("InventoryPrefabs/Ability/RocketRing"));
            case Global.TIMESLOW:
                return (GameObject)(Resources.Load("InventoryPrefabs/Ability/TimeSlow"));
            case Global.REGENERATOR:
                return (GameObject)(Resources.Load("InventoryPrefabs/Ability/Regenerator"));
            case Global.SWORDRING:
                return (GameObject)(Resources.Load("InventoryPrefabs/Ability/SwordRing"));
            case Global.SHIELDPASSIVE:
                return (GameObject)(Resources.Load("InventoryPrefabs/Ability/ShieldPassive"));
            case Global.DRONEMKII:
                return (GameObject)(Resources.Load("InventoryPrefabs/Ability/DroneMkII"));
            case Global.COMPANIONDRONEI:
                return (GameObject)(Resources.Load("InventoryPrefabs/Ability/CompanionDroneI"));
            case Global.COMPANIONDRONEII:
                return (GameObject)(Resources.Load("InventoryPrefabs/Ability/CompanionDroneII"));
            default:
                return (GameObject)(Resources.Load("InventoryPrefabs/Ability/SpeedBoost"));
        }
    }

    public void ReassignEquipped()
    {
        
    }

    public void ReassignInventory()
    {
		playerInventory.ReassignAbilityInventory(abilities);
    }

    private void LateStart()
    {
        PopulateList();
        for (int i = 0; i < equippedAbilities.Count; i++)
        {
            GameObject g = Instantiate(GetAbilityUI(equippedAbilities[i])) as GameObject;
            g.transform.position = epos[i];
        }
    }

    public void RemoveEquipped(int i)
    {
        equippedAbilities.Remove(i);
    }

    public void AddEquipped(int i)
    {
        equippedAbilities.Add(i);
    } 

    public void RemoveAbility(int i)
    {
        abilities.Remove(i);
    }

    public void AddAbility(int i)
    {
        abilities.Add(i);
    }

}
