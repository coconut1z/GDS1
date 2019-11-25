using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Linq;


public class EquipmentDragger : MonoBehaviour {

	public const string DRAGGABLE_TAG = "UIDraggable";
	public const string DRAGGABLE_PANEL = "UIPanel";
	public InventoryPanel ip;

	private bool dragging = false;
	private Vector2 originalPosition;
	private Transform moduleToDrag;
	private Image moduleToDragImage;
	List<RaycastResult> hitObjects = new List<RaycastResult>();

	// Use this for initialization
	void Start () {
		ip = GetComponent<InventoryPanel> ();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0)){
			moduleToDrag = GetDraggableTransformUnderMouse ();
			if(moduleToDrag != null){
				dragging = true;
				moduleToDrag.SetAsLastSibling ();
				//originalPosition = moduleToDrag.position;
				originalPosition = moduleToDrag.localPosition;
				moduleToDragImage = moduleToDrag.GetComponent<Image> ();
				moduleToDragImage.raycastTarget = false;
			}
		}

		if(dragging){
			moduleToDrag.position = Input.mousePosition;
		}

		if(Input.GetMouseButtonUp(0)){
			if(moduleToDrag != null){
				Transform moduleToReplace = GetDraggableTransformUnderMouse ();
				if(moduleToReplace != null){
					//moduleToDrag.position = moduleToReplace.position;
					//moduleToReplace.position = originalPosition;
					//Vector2 dragPos = moduleToDrag.localPosition;
					Vector2 replacePos = moduleToReplace.localPosition;
					//moduleToDrag.localPosition = moduleToReplace.InverseTransformPoint(moduleToReplace.position);
					//moduleToReplace.localPosition = moduleToDrag.InverseTransformPoint(originalPosition);

					WeaponDraggable Drag = moduleToDrag.gameObject.GetComponent<WeaponDraggable> ();
					WeaponDraggable Replace = moduleToReplace.gameObject.GetComponent<WeaponDraggable> ();
					if(Replace.equipped && !Drag.equipped){
						Drag.equipped = true;
						Replace.equipped = false;
						Drag.slot = Replace.slot;
						Replace.slot = -1;
						Transform panel = moduleToDrag.parent;
						moduleToDrag.parent = moduleToReplace.parent;
						moduleToReplace.parent = panel;
						ip.RemoveEquipped (Replace.getWeaponId());
						ip.AddEquipped (Drag.getWeaponId());
						ip.RemoveWeapon (Drag.getWeaponId());
						ip.AddWeapon (Replace.getWeaponId());
					}else if(!Replace.equipped && Drag.equipped){
						Drag.equipped = false;
						Replace.equipped = true;
						Replace.slot = Drag.slot;
						Drag.slot = -1;
						Transform panel = moduleToDrag.parent;
						moduleToDrag.parent = moduleToReplace.parent;
						moduleToReplace.parent = panel;
						ip.RemoveEquipped (Drag.getWeaponId());
						ip.AddEquipped (Replace.getWeaponId());
						ip.RemoveWeapon (Replace.getWeaponId());
						ip.AddWeapon (Drag.getWeaponId());
					}else if(Replace.equipped && Drag.equipped){
						int dragSlot = Drag.slot;
						Drag.slot = Replace.slot;
						Replace.slot = dragSlot;
					}else if(!Replace.equipped && !Drag.equipped){
						Transform panel = moduleToDrag.parent;
						moduleToDrag.parent = moduleToReplace.parent;
						moduleToReplace.parent = panel;
					}
					moduleToDrag.localPosition = replacePos;
					moduleToReplace.localPosition = originalPosition;
				}else{
					moduleToDrag.localPosition = originalPosition;
				}

				moduleToDragImage.raycastTarget = true;
				moduleToDrag = null;

			}
			dragging = false;
		}

	}

	private GameObject GetModuleUnderMouse(){
		var pointer = new PointerEventData (EventSystem.current);

		pointer.position = Input.mousePosition;
		EventSystem.current.RaycastAll (pointer, hitObjects);

		if(hitObjects.Count <= 0){
			return null;
		}
		return hitObjects.First ().gameObject;
	}

	private Transform GetDraggableTransformUnderMouse(){
		GameObject clickedModule = GetModuleUnderMouse ();

		if (clickedModule != null && clickedModule.tag == DRAGGABLE_TAG){
			return clickedModule.transform;
		}else if(clickedModule != null && clickedModule.tag == DRAGGABLE_PANEL){
			return clickedModule.transform.GetChild (4).transform;
		}
		return null;
	}
}
