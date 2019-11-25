using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityInfoPanel : MonoBehaviour
{

	private Text nameT, extra, typeT;
    private AbilityDraggable abi;

    // Use this for initialization
    void Start()
    {
        nameT = transform.GetChild(0).GetComponent<Text>();
        extra = transform.GetChild(1).GetComponent<Text>();
		typeT = transform.GetChild (2).GetComponent<Text> ();
    }

    // Update is called once per frame
    void Update()
    {
        abi = transform.GetChild(3).GetComponent<AbilityDraggable>();
        if (abi != null)
        {
            nameT.text = abi.nameUI;
            extra.text = abi.extraUI;
			if (abi.passive) {
				typeT.text = "PASSIVE";
                typeT.color = new Color(1, 0.5f, 1, 1);
			}else{
				typeT.text = "ACTIVE";
                typeT.color = new Color(0, 1, 0, 1);
				GetComponent<Image>().color = new Color (136f / 255f, 169f / 255f, 143f / 255f);
			} 
			if(abi.passive){
				abi.gameObject.tag = "UIDraggableP";
			}
        }
    }

	public void HoverSet(string name, string ex){
		nameT.text = name;
		extra.text = ex;
	}
}
