using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropWeaponOnDestroy : MonoBehaviour {

	public int stage;
	public GameObject[] stage1;
	public GameObject[] stage2;
	public GameObject[] stage3;
	public GameObject[] stage4;
    public GameObject[] stage5;
    public GameObject[] stage6;
	public GameObject wepDrop;

    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void LateUpdate(){
		transform.rotation = Quaternion.identity;
	}

	void OnDestroy(){
		if(transform.position.x > -7.1 && transform.position.x < 7.1 &&
			transform.position.y > -4.8 && transform.position.y < 6){
			if(stage == 1){
				//GameObject g = Instantiate (stage1 [Random.Range (1, stage1.Length)]) as GameObject;
				//g.transform.position = transform.position;
				GameObject g = Instantiate(wepDrop) as GameObject;
				g.transform.position = transform.position;
				g.GetComponent<WepDrop> ().wepID = 101 + Random.Range (1, stage1.Length);
			}else if(stage == 2){
				GameObject g = Instantiate(wepDrop) as GameObject;
				g.transform.position = transform.position;
				g.GetComponent<WepDrop> ().wepID = 200 + Random.Range (1, stage2.Length);
			}else if(stage == 3){
				GameObject g = Instantiate(wepDrop) as GameObject;
				g.transform.position = transform.position;
				g.GetComponent<WepDrop> ().wepID = 300 + Random.Range (1, stage3.Length);;
			}else if(stage == 4){
				GameObject g = Instantiate(wepDrop) as GameObject;
				g.transform.position = transform.position;
				g.GetComponent<WepDrop> ().wepID = 400 + Random.Range (1, stage4.Length);
			}
            else if (stage == 5) {
				GameObject g = Instantiate(wepDrop) as GameObject;
				g.transform.position = transform.position;
				g.GetComponent<WepDrop> ().wepID = 500 + Random.Range (1, stage5.Length);
            }
            else if (stage == 6) {
				GameObject g = Instantiate(wepDrop) as GameObject;
				g.transform.position = transform.position;
				g.GetComponent<WepDrop> ().wepID = 600 + Random.Range (1, stage6.Length);
            }
        }
	}
}
