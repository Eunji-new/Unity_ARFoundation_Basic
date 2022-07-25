using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PlaceSpawnObject : MonoBehaviour
{

    private ARRaycastManager raycastManager;
    private GameObject spwanObject;

    private void Awake()
    {
        raycastManager = GetComponent<ARRaycastManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            List<ARRaycastHit> hits = new List<ARRaycastHit>();

            if(raycastManager.Raycast(touch.position, hits, TrackableType.PlaneWithinPolygon))
            {
                Pose hitpose = hits[0].pose;
                if (!spwanObject)
                {
                    spwanObject = Instantiate(raycastManager.raycastPrefab, hitpose.position + Vector3.up * 0.1f, hitpose.rotation);
                }
                else
                {
                    spwanObject.transform.position = hitpose.position + (Vector3.up * 0.1f);
                    spwanObject.transform.rotation = hitpose.rotation;
                }
            }
        }
    }
}
