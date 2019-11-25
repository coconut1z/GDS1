using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentShield : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        CheckBullets();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "EnemyBullet")
        {
            Destroy(other.gameObject);
        }
    }

    private void CheckBullets()
    {
        GameObject[] g = GameObject.FindGameObjectsWithTag("EnemyBullet");
        for (int i = 0; i < g.Length; i++)
        {
            if (GetComponent<Collider2D>().bounds.Contains(g[i].transform.position))
            {
                Destroy(g[i]);
            }
        }
    }

}
