using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathingScript : MonoBehaviour {

	public PathStraight[] node;
	public Enemy[] enemies;
	public float initialSpeedMultiplier;
	public float initialDelay;
	private List<EnemyNode> enemyNodes;

	// Use this for initialization
	void Start () {
		enemyNodes = new List<EnemyNode> ();
		foreach(Enemy e in enemies){
			enemyNodes.Add (new EnemyNode (e));
		}
		Invoke ("LateStart", 0.01f);
	}
	
	// Update is called once per frame
	void Update () {
		for(int i = 0 ; i < enemyNodes.Count; i++){
			if (enemyNodes[i].enemy == null){
				enemyNodes.RemoveAt(i);
				i--;
			}
		}
		foreach(EnemyNode en in enemyNodes){
            if (en.delayTimer <= 0) {
				en.enemy.canMove = true;
				if(node.Length != en.nodePos){
					FaceTowards (node [en.nodePos].transform, en.enemy);
					if(Vector2.Distance(en.enemy.transform.position, node[en.nodePos].transform.position) <= 0.15f){
						en.enemy.speed = en.enemy.originalSpeed * node [en.nodePos].speedMultiplier;
						en.delayTimer = node [en.nodePos].delay;
						en.nodePos++;
					}
				}
			}else{
				en.enemy.canMove = false;
				en.delayTimer -= Time.deltaTime;
			}

		}

	}

	private void FaceTowards(Transform target, Enemy self){
		float Rad = Mathf.Atan2 (
			target.position.y - self.transform.position.y,
			target.position.x - self.transform.position.x
		);
		float RadToDeg = (180 / Mathf.PI) * Rad - 90;
		self.transform.rotation = Quaternion.Euler (0, 0, RadToDeg);
	}


	private class EnemyNode{

		public Enemy enemy;
		public int nodePos;
		public float delayTimer;

		public EnemyNode(Enemy enemy){
			this.enemy = enemy;
			nodePos = 0;
			delayTimer = 0;
		}

	}

	private void LateStart(){
		foreach(EnemyNode en in enemyNodes){
			en.enemy.speed = en.enemy.originalSpeed * initialSpeedMultiplier;
			en.delayTimer = initialDelay;
		}
	}

}
