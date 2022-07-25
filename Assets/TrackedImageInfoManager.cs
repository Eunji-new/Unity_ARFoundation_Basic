using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

//필수 조건
[RequireComponent(typeof(ARTrackedImageManager)] //이게 없으면 이 스크립트 사용불가입니다.
public class TrackedImageInfoManager : MonoBehaviour
{
    [SerializeField]
    Camera m_WorldSpaceCanvasCamera;

    [SerializeField]
    Texture2D m_DefaultTexture;
    ARTrackedImageManager m_TrackedImageManager;

    private void Awake()
    {
        m_TrackedImageManager = GetComponent<ARTrackedImageManager > ();
    }

    private void OnEnable()
    {
        m_TrackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
    }

    private void OnDisable()
    {
        m_TrackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged;

    }

    void UpdateInfo(ARTrackedImage trackedImage)
    {
        var canvas = trackedImage.GetComponentInChildren<Canvas>();
        canvas.worldCamera = m_WorldSpaceCanvasCamera;

        var text = canvas.GetComponentInChildren<Text>();
        text.text = string.Format(
            "{0}\ntackingState: {1}\nGUID:  {2}\nReference size : {3} cm\nDetect size : {4} cm",
            trackedImage.referenceImage.name,
            trackedImage.trackingState,
            trackedImage.referenceImage.guid,
            trackedImage.referenceImage.size * 100f,
            trackedImage.size * 100f
            );

        var planeParentGo = trackedImage.transform.GetChild(0).gameObject;
        var planeGo = planeParentGo.transform.GetChild(0).gameObject;

        if(trackedImage.trackingState != TrackingState.None)
        {
            planeGo.SetActive(true);
            trackedImage.transform.localScale = new Vector3(trackedImage.size.x, 1f, trackedImage.size.y);
            var material = planeGo.GetComponentInChildren<MeshRenderer>().material;
            material.mainTexture = (trackedImage.referenceImage.texture == null) ? m_DefaultTexture : trackedImage.referenceImage.texture;
        }
        else
        {
            planeGo.SetActive(false);
        }
    }
    void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach(var trackedImage in eventArgs.added)
        {
            trackedImage.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            UpdateInfo(trackedImage);
        }

        foreach(var trackedImage in eventArgs.updated)
        {
            UpdateInfo(trackedImage);
        }
    }
}
