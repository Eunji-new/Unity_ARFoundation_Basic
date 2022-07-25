using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

public class CheckRuntimeDepth : MonoBehaviour
{
    [SerializeField]
    AROcclusionManager m_OcclusionManager;

    [SerializeField]
    Text m_DepthAvailabilityInfo;

    private void Update()
    {
        Debug.Assert(m_OcclusionManager != null, "no occlusion Manager");
        Debug.Assert(m_DepthAvailabilityInfo != null, "no text box");
        m_DepthAvailabilityInfo.enabled = ((m_OcclusionManager.descriptor?.supportsHumanSegmentationStencilImage == false))
                                                                && (m_OcclusionManager.descriptor?.supportsHumanSegmentationDepthImage == false)
                                                                && (m_OcclusionManager.descriptor?.supportsEnvironmentDepthImage == false);
    }
}
