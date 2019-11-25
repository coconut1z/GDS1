using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AbilityDraggable : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    [TextArea] public string nameUI;
    [TextArea] public string extraUI;
    public GameObject abilityPrefab;
    public bool passive;
    public AbilityModule ability;
    public bool equipped;
    public int slot;
	private bool moused;
	private GameObject info;
	public GameObject inst;

    // Use this for initialization
    void Start()
    {
        if (!equipped)
        {
            slot = -1;
        }
        ability = abilityPrefab.GetComponent<AbilityModule>();
		moused = false;
		info = (GameObject)(Resources.Load ("InventoryPrefabs/AbilityInfoPanelHover"));
    }

    // Update is called once per frame
    void Update()
    {

    }

    public int getAbilityId()
    {
        return ability.ReturnId();
    }

    public AbilityModule getAbility()
    {
        return ability;
    }
	public void OnPointerEnter(PointerEventData eventData){
		if(equipped && !InputKeys.isDown(InputKeys.SHOOT)){
			inst = Instantiate (info) as GameObject;
			inst.transform.parent = transform.parent.parent.parent;
			inst.transform.localScale = new Vector2 (1, 1);
			inst.transform.position = new Vector2(transform.position.x + 50,transform.position.y - 75);
			inst.GetComponent<AbilityInfoPanelHover> ().HoverSet (nameUI, extraUI);


		}
		Debug.Log ("Mouse Enter");
	}

	public void OnPointerExit(PointerEventData eventData){
		if(inst != null){
			Destroy (inst);
		}
		Debug.Log ("Mouse Exit");
	}

}
