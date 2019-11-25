using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectileSetSpeed : MonoBehaviour {

	public float speed;
	public string name;
	// Use this for initialization
	void Start () {
		//if(name.Equals("DynamicTimeDelete")){
		//	for(int i = 0; i < transform.childCount; i++){
		//		transform.GetChild (i).GetComponent<DynamicTimeDelete> ().setSpeed = speed;
		//	}
		//}else{
		//	for(int i = 0; i < transform.childCount; i++){
		//		transform.GetChild (i).GetComponent<EnemyRedBulletDynamicNoDelete> ().setSpeed = speed;
		//	}
		//}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
