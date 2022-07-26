using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawn : MonoBehaviour
{
    public GameObject bullet;
    public Transform firePos;
    public float shootTime;
    private float spawnTime = 0f;

    void Update()
    {
        spawnTime += Time.deltaTime;

        if (Input.touchCount > 0 && spawnTime > shootTime)
        {
            Touch touch = Input.GetTouch(0);
            if(touch.phase == TouchPhase.Began)
            {
                GameObject _bullet = Instantiate(bullet);
                _bullet.transform.position = firePos.transform.position;
                _bullet.transform.rotation = firePos.transform.rotation;
                spawnTime = 0f;
            }
            
        }
    }
}
