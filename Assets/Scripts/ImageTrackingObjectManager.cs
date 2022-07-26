using System;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ImageTrackingObjectManager : MonoBehaviour
{
    [SerializeField]
    ARTrackedImageManager m_ImageManager;
    [SerializeField]
    XRReferenceImageLibrary m_ImageLibrary;

    [SerializeField]
    GameObject m_OnePrefab;
    [SerializeField]
    GameObject m_TwoPrefab;

    GameObject m_SpawnedOnePrefab;
    public GameObject spawnedOnePrefab
    {
        get => m_SpawnedOnePrefab;
        set => m_SpawnedOnePrefab = value;
    }

    GameObject m_SpawnedTwoPrefab;
    public GameObject spawnedTwoPrefab
    {
        get => m_SpawnedTwoPrefab;
        set => m_SpawnedTwoPrefab = value;
    }

    int m_NumberOfTrackedImages;

    NumberManager m_OneNumberManager;
    NumberManager m_TwoNumberManager;

    static Guid s_FirstImageGUID;
    static Guid s_SecondImageGUID;

    private void OnEnable()
    {
        s_FirstImageGUID = m_ImageLibrary[0].guid;
        s_SecondImageGUID = m_ImageLibrary[1].guid;

        m_ImageManager.trackedImagesChanged += ImageManagerOnTrackedImagesChanged;
    }

    private void OnDisable()
    {
        m_ImageManager.trackedImagesChanged -= ImageManagerOnTrackedImagesChanged;
    }

    private void ImageManagerOnTrackedImagesChanged(ARTrackedImagesChangedEventArgs obj)
    {
        foreach (ARTrackedImage image in obj.added)
        {
            if (image.referenceImage.guid == s_FirstImageGUID)
            {
                m_SpawnedOnePrefab = Instantiate(m_OnePrefab, image.transform.position, image.transform.rotation);
                m_OneNumberManager = m_SpawnedOnePrefab.GetComponent<NumberManager>();
            }
            else if (image.referenceImage.guid == s_SecondImageGUID)
            {
                m_SpawnedTwoPrefab = Instantiate(m_TwoPrefab, image.transform.position, image.transform.rotation);
                m_TwoNumberManager = m_SpawnedTwoPrefab.GetComponent<NumberManager>();
            }
        }

        foreach(ARTrackedImage image in obj.updated)
        {
            if(image.trackingState == TrackingState.Tracking)
            {
                if (image.referenceImage.guid == s_FirstImageGUID)
                {
                    m_OneNumberManager.Enable3DNumber(true);
                    m_SpawnedOnePrefab.transform.SetPositionAndRotation(image.transform.position, image.transform.rotation);
                }
                else if(image.referenceImage.guid == s_SecondImageGUID)
                {
                    m_TwoNumberManager.Enable3DNumber(true);
                    m_SpawnedTwoPrefab.transform.SetPositionAndRotation(image.transform.position, image.transform.rotation);   
                }
            }
        }

        foreach (ARTrackedImage image in obj.removed)
        {
            if(image.referenceImage.guid == s_FirstImageGUID)
            {
                Destroy(m_SpawnedOnePrefab);
            }
            else if(image.referenceImage.guid == s_SecondImageGUID)
            {
                Destroy(m_SpawnedTwoPrefab);
            }
        }
    }

    public int NumberOfTrackedImages()
    {
        m_NumberOfTrackedImages = 0;
        foreach(ARTrackedImage image in m_ImageManager.trackables)
        {
            if (image.trackingState == TrackingState.Tracking)
                m_NumberOfTrackedImages++;
        }
        return m_NumberOfTrackedImages;
    }
}
