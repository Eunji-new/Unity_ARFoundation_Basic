using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
	public GameObject enemy;
	public int maxEnemyCount;
	private int xPos;
	private int yPos;
	private int zPos;
	private int curEnemyCount = 0;
	private float spawnTime = 0f;

	void Update()
	{
		spawnTime += Time.deltaTime;
		if (curEnemyCount < maxEnemyCount && spawnTime > 2.0f)
		{
			zPos = Random.Range(5, 10);
			xPos = Random.Range(-10, 20);
			yPos = Random.Range(-5, 5);
			Instantiate(enemy, new Vector3(xPos, yPos, zPos), Quaternion.Euler(new Vector3(0, 180, 0)));
			curEnemyCount++;
			spawnTime = 0;
		}
	}

	public void EnemyDie()
	{
		curEnemyCount--;
	}
}
