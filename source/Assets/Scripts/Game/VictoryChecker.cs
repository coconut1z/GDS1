using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class VictoryChecker : MonoBehaviour
{
	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
			
	}

    public void BossDead() {
        Invoke("Victory", 10f);
    }

    public void Victory() {
        SceneManager.LoadSceneAsync("Victory", LoadSceneMode.Additive);
    }
}
