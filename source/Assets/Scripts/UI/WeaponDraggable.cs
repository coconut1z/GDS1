using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WeaponDraggable : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

	[TextArea] public string nameUI;
	public float damageUI;
	public float firerateUI;
	[TextArea] public string extraUI;
	public GameObject weaponPrefab;
	public WeaponModule weapon;
	public bool equipped;
	public int slot;
	public int stage;
	private GameObject info;
	public GameObject inst;

	// Use this for initialization
	void Start () {
		if(!equipped){
			slot = -1;
		}
		weapon = weaponPrefab.GetComponent<WeaponModule> ();
		info = (GameObject)(Resources.Load ("InventoryPrefabs/InfoPanelHover"));
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public int getWeaponId(){
		return weapon.ReturnId (); 
	}

	public WeaponModule getWeapon(){
		return weapon;
	}

	public void OnPointerEnter(PointerEventData eventData){
		if(equipped && !InputKeys.isDown(InputKeys.SHOOT)){
			inst = Instantiate (info) as GameObject;
			inst.transform.parent = transform.parent.parent.parent;
			inst.transform.localScale = new Vector2 (1, 1);
			inst.transform.position = new Vector2(transform.position.x + 50,transform.position.y - 75);
			inst.GetComponent<InfoPanelScriptHover> ().HoverSet (nameUI, (int)damageUI, (int)firerateUI, GetComponent<WeaponDraggable>());


		}
        AudioManager.instance.PlaySound("HoverBeep");
		Debug.Log ("Mouse Enter");
	}

	public void OnPointerExit(PointerEventData eventData){
		if(inst != null){
			Destroy (inst);
		}
		Debug.Log ("Mouse Exit");
	}
}
