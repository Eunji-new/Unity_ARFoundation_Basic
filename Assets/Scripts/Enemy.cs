using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private EnemySpawn enemySpawn;
    private void Awake()
    {
        enemySpawn = GameObject.Find("EnemySpawn").GetComponent<EnemySpawn>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bullet")
        {
            enemySpawn.EnemyDie();
            Destroy(gameObject);
        }
    }
}
