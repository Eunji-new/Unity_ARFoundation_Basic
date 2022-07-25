using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLauncher : MonoBehaviour
{
    [SerializeField]
    Rigidbody m_ProjectilePrefab;

    [SerializeField]
    float m_InitialSpeed = 25;

    private void Update()
    {
        if (m_ProjectilePrefab == null)
            return;
        if (Input.touchCount == 0)
            return;
        var touch = Input.touches[0];
        if(touch.phase == TouchPhase.Began)
        {
            var ray = GetComponent<Camera>().ScreenPointToRay(touch.position);
            var projectile = Instantiate(m_ProjectilePrefab, ray.origin, Quaternion.identity);
            var rigidbody = projectile.GetComponent<Rigidbody>();
            rigidbody.velocity = ray.direction * m_InitialSpeed;
        }
    }
}
