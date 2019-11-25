using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityInfoPanelHover : MonoBehaviour
{

    private Text nameT, extra;
    private AbilityDraggable abi;

    // Use this for initialization
    void Start()
    {
        nameT = transform.GetChild(0).GetComponent<Text>();
        extra = transform.GetChild(1).GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        /*abi = transform.GetChild(2).GetComponent<AbilityDraggable>();
        if (abi != null)
        {
            nameT.text = abi.nameUI;
            extra.text = abi.extraUI;
			if(abi.passive){
				abi.gameObject.tag = "UIDraggableP";
			}
        }
        */
    }

	public void HoverSet(string name, string ex){
		nameT = transform.GetChild(0).GetComponent<Text>();
		extra = transform.GetChild(1).GetComponent<Text>();
		nameT.text = name;
		extra.text = ex;
	}
}
