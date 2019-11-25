using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Linq;

public class AbilityDragger : MonoBehaviour
{

    public const string DRAGGABLE_TAG = "UIDraggableA";
	public const string DRAGGABLE_TAGP = "UIDraggableP";
	public const string DRAGGABLE_PANEL = "UIPanel";
    public AbilityInventoryPanel ip;

    private bool dragging = false;
    private Vector2 originalPosition;
    private Transform moduleToDrag;
    private Image moduleToDragImage;
    List<RaycastResult> hitObjects = new List<RaycastResult>();
	public bool tutDrag;

    private List<Text> controlsText;
    private List<Text> abilityNameText;

    // Use this for initialization
    void Start()
    {
		ip = GetComponent<AbilityInventoryPanel>();
        controlsText = ip.controlsTexts;
        abilityNameText = ip.abilityNameTexts;
		tutDrag = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            moduleToDrag = GetDraggableTransformUnderMouse();
            if (moduleToDrag != null)
            {
                dragging = true;
                moduleToDrag.SetAsLastSibling();
                //originalPosition = moduleToDrag.position;
                originalPosition = moduleToDrag.localPosition;
                moduleToDragImage = moduleToDrag.GetComponent<Image>();
                moduleToDragImage.raycastTarget = false;
            }
        }

        if (dragging)
        {
            moduleToDrag.position = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (moduleToDrag != null)
            {
                Transform moduleToReplace = GetDraggableTransformUnderMouse();
				if (moduleToReplace != null && moduleToReplace.tag == moduleToDrag.tag)
                {
                    //moduleToDrag.position = moduleToReplace.position;
                    //moduleToReplace.position = originalPosition;
                    //Vector2 dragPos = moduleToDrag.localPosition;
                    Vector2 replacePos = moduleToReplace.localPosition;
                    //moduleToDrag.localPosition = moduleToReplace.InverseTransformPoint(moduleToReplace.position);
                    //moduleToReplace.localPosition = moduleToDrag.InverseTransformPoint(originalPosition);

                    AbilityDraggable Drag = moduleToDrag.gameObject.GetComponent<AbilityDraggable>();
                    AbilityDraggable Replace = moduleToReplace.gameObject.GetComponent<AbilityDraggable>();
                    if (Replace.equipped && !Drag.equipped)
                    {
						tutDrag = true;
                        Drag.equipped = true;
                        Replace.equipped = false;
                        Drag.slot = Replace.slot;
                        Replace.slot = -1;
                        Transform panel = moduleToDrag.parent;
                        moduleToDrag.parent = moduleToReplace.parent;
                        moduleToReplace.parent = panel;
                        ip.RemoveEquipped(Replace.getAbilityId());
                        ip.AddEquipped(Drag.getAbilityId());
                        ip.RemoveAbility(Drag.getAbilityId());
                        ip.AddAbility(Replace.getAbilityId());

                        abilityNameText[Drag.slot - 1].text = Drag.nameUI;

                        if (Drag.passive) {
                            controlsText[Drag.slot - 1].text = "Passive";
                        }
                        else {
                            if (Drag.slot - 1 == 0) {
                                controlsText[Drag.slot - 1].text = "Right Click";
                            }
                            else if (Drag.slot - 1 == 1)
                            {
                                controlsText[Drag.slot - 1].text = "Q";
                            }
                            else if (Drag.slot - 1 == 2)
                            {
                                controlsText[Drag.slot - 1].text = "E";
                            }
                            else if (Drag.slot - 1 == 3)
                            {
                                controlsText[Drag.slot - 1].text = "R";
                            }
                        }

                    }
                    else if (!Replace.equipped && Drag.equipped)
                    {
                        Drag.equipped = false;
                        Replace.equipped = true;
                        Replace.slot = Drag.slot;
                        Drag.slot = -1;
                        Transform panel = moduleToDrag.parent;
                        moduleToDrag.parent = moduleToReplace.parent;
                        moduleToReplace.parent = panel;
                        ip.RemoveEquipped(Drag.getAbilityId());
                        ip.AddEquipped(Replace.getAbilityId());
                        ip.RemoveAbility(Replace.getAbilityId());
                        ip.AddAbility(Drag.getAbilityId());

                        abilityNameText[Replace.slot - 1].text = Replace.nameUI;

                        if (Replace.passive)
                        {
                            controlsText[Replace.slot - 1].text = "Passive";
                        }
                        else
                        {
                            if (Replace.slot - 1 == 0)
                            {
                                controlsText[Replace.slot - 1].text = "Right Click";
                            }
                            else if (Replace.slot - 1 == 1)
                            {
                                controlsText[Replace.slot - 1].text = "Q";
                            }
                            else if (Replace.slot - 1 == 2)
                            {
                                controlsText[Replace.slot - 1].text = "E";
                            }
                            else if (Replace.slot - 1 == 3)
                            {
                                controlsText[Replace.slot - 1].text = "R";
                            }
                        }

                    }
                    else if (Replace.equipped && Drag.equipped)
                    {
                        int dragSlot = Drag.slot;
                        Drag.slot = Replace.slot;
                        Replace.slot = dragSlot;

                        abilityNameText[Drag.slot - 1].text = Drag.nameUI;
                        abilityNameText[Replace.slot - 1].text = Replace.nameUI;

                        if (Drag.passive)
                        {
                            controlsText[Drag.slot - 1].text = "Passive";
                        }
                        else
                        {
                            if (Drag.slot - 1 == 0)
                            {
                                controlsText[Drag.slot - 1].text = "Right Click";
                            }
                            else if (Drag.slot - 1 == 1)
                            {
                                controlsText[Drag.slot - 1].text = "Q";
                            }
                            else if (Drag.slot - 1 == 2)
                            {
                                controlsText[Drag.slot - 1].text = "E";
                            }
                            else if (Drag.slot - 1 == 3)
                            {
                                controlsText[Drag.slot - 1].text = "R";
                            }
                        }

                        if (Replace.passive)
                        {
                            controlsText[Replace.slot - 1].text = "Passive";
                        }
                        else
                        {
                            if (Replace.slot - 1 == 0)
                            {
                                controlsText[Replace.slot - 1].text = "Right Click";
                            }
                            else if (Replace.slot - 1 == 1)
                            {
                                controlsText[Replace.slot - 1].text = "Q";
                            }
                            else if (Replace.slot - 1 == 2)
                            {
                                controlsText[Replace.slot - 1].text = "E";
                            }
                            else if (Replace.slot - 1 == 3)
                            {
                                controlsText[Replace.slot - 1].text = "R";
                            }
                        }

                    }
                    else if (!Replace.equipped && !Drag.equipped)
                    {
                        Transform panel = moduleToDrag.parent;
                        moduleToDrag.parent = moduleToReplace.parent;
                        moduleToReplace.parent = panel;
                    }
                    moduleToDrag.localPosition = replacePos;
                    moduleToReplace.localPosition = originalPosition;
                }
                else
                {
                    moduleToDrag.localPosition = originalPosition;
                }

                moduleToDragImage.raycastTarget = true;
                moduleToDrag = null;

            }
            dragging = false;
        }

    }

    private GameObject GetModuleUnderMouse()
    {
        var pointer = new PointerEventData(EventSystem.current);

        pointer.position = Input.mousePosition;
        EventSystem.current.RaycastAll(pointer, hitObjects);

        if (hitObjects.Count <= 0)
        {
            return null;
        }
        return hitObjects.First().gameObject;
    }

    private Transform GetDraggableTransformUnderMouse()
    {
        GameObject clickedModule = GetModuleUnderMouse();

		if (clickedModule != null && (clickedModule.tag == DRAGGABLE_TAG || clickedModule.tag == DRAGGABLE_TAGP))
        {
            return clickedModule.transform;
		}else if(clickedModule != null && clickedModule.tag == DRAGGABLE_PANEL){
			return clickedModule.transform.GetChild (3).transform;
		}
        return null;
    }
}
