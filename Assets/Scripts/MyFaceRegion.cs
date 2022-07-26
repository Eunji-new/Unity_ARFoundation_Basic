using Unity.Collections;
using UnityEngine;
using UnityEngine.XR.ARCore;
using UnityEngine.XR.ARFoundation;

public class MyFaceRegion : MonoBehaviour
{
    public GameObject nosePrefab;
    public GameObject headLeftPrefab;
    public GameObject headRightPrefab;
    private ARFaceManager faceManager;
    private ARSessionOrigin sessionOrigin;
    private ARCoreFaceSubsystem subsystem;
    private GameObject noseObj;
    private GameObject headleftObj;
    private GameObject headrightObj;

    NativeArray<ARCoreFaceRegionData> faceRegions;
    void Start()
    {
        faceManager = GetComponent<ARFaceManager>();
        sessionOrigin = GetComponent<ARSessionOrigin>();
        subsystem = (ARCoreFaceSubsystem)faceManager.subsystem;
        faceManager.facesChanged += OnFaceChanged;
    }

    void OnFaceChanged(ARFacesChangedEventArgs args)
    {
        if (args.updated.Count > 0)
        {
            foreach (ARFace face in faceManager.trackables)
            {
                subsystem.GetRegionPoses(face.trackableId, Unity.Collections.Allocator.Persistent, ref faceRegions);
                foreach (ARCoreFaceRegionData faceRegion in faceRegions)
                {
                    ARCoreFaceRegion regionType = faceRegion.region;

                    if (regionType == ARCoreFaceRegion.NoseTip)
                    {
                        if (!noseObj)
                        {
                            noseObj = Instantiate(nosePrefab, sessionOrigin.trackablesParent);
                        }
                        noseObj.transform.localPosition = faceRegion.pose.position;
                        noseObj.transform.localRotation = faceRegion.pose.rotation;
                        noseObj.SetActive(true);
                    }
                    else if (regionType == ARCoreFaceRegion.ForeheadLeft)
                    {
                        if (!headleftObj)
                        {
                            headleftObj = Instantiate(headLeftPrefab, sessionOrigin.trackablesParent);
                        }
                        headleftObj.transform.localPosition = faceRegion.pose.position;
                        headleftObj.transform.localRotation = faceRegion.pose.rotation;
                        headleftObj.SetActive(true);
                    }
                    else if (regionType == ARCoreFaceRegion.ForeheadRight)
                    {
                        if (!headrightObj)
                        {
                            headrightObj = Instantiate(headRightPrefab, sessionOrigin.trackablesParent);
                        }
                        headrightObj.transform.localPosition = faceRegion.pose.position;
                        headrightObj.transform.localRotation = faceRegion.pose.rotation;
                        headrightObj.SetActive(true);
                    }
                }
            }
        }
        if (args.removed.Count > 0)
        {
            noseObj.SetActive(false);
            headleftObj.SetActive(false);
            headrightObj.SetActive(false);
        }
    }
}