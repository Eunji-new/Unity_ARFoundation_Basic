using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class DistanceManager : MonoBehaviour
{
    [SerializeField]
    ImageTrackingObjectManager m_ImageTrackingObjectManager;
    [SerializeField]
    GameObject m_SumPrefab;

    GameObject m_SpawnedSumPrefab;
    GameObject m_OneObject;
    GameObject m_TwoObject;
    float m_Distance;
    bool m_SumActive;
    const float k_SumDistance = 0.3f;

    void Start()
    {
        m_SpawnedSumPrefab = Instantiate(m_SumPrefab, Vector3.zero, Quaternion.identity);
        m_SpawnedSumPrefab.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        m_OneObject = m_ImageTrackingObjectManager.spawnedOnePrefab;
        m_TwoObject = m_ImageTrackingObjectManager.spawnedTwoPrefab;
        //트래킹 중인 이미지가 2개 이상일 떄
        if(m_ImageTrackingObjectManager.NumberOfTrackedImages() > 1)
        {
            m_Distance = Vector3.Distance(m_OneObject.transform.position, m_TwoObject.transform.position);

            if(m_Distance <= k_SumDistance)
            {
                if(!m_SumActive)
                {
                    m_SpawnedSumPrefab.SetActive(true);
                    m_SumActive = true;
                }
                m_SpawnedSumPrefab.transform.position = (m_OneObject.transform.position + m_TwoObject.transform.position) / 2;
            }
            else
            {
                m_SpawnedSumPrefab.SetActive(false);
                m_SumActive = false;
            }
        }
        else
        {
            m_SpawnedSumPrefab.SetActive(false);
            m_SumActive = false;
        }

    }
}
