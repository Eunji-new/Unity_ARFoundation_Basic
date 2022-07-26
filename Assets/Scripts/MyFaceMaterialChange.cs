using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

[RequireComponent(typeof(ARFaceManager))]
public class MyFaceMaterialChange : MonoBehaviour
{
    private ARFaceManager m_arFaceManager;
    [SerializeField]
    private Dropdown m_dropdown;

    [SerializeField]
    Material[] m_materials;

    void Start()
    {
        m_arFaceManager = GetComponent<ARFaceManager>();
        m_arFaceManager.facePrefab.GetComponent<MeshRenderer>().material = m_materials[0];
    }

    private void Set_UI()
    {
        Reset_UI();

        m_dropdown.onValueChanged.AddListener(delegate{
            FaceMaterialChange(m_dropdown);
        });
    }

    private void Reset_UI()
    {
        m_dropdown.onValueChanged.RemoveAllListeners();
    }

    private void FaceMaterialChange(Dropdown select)
    {
        foreach (ARFace face in m_arFaceManager.trackables)
        {
            face.gameObject.GetComponent<MeshRenderer>().material = m_materials[select.value];
        }
        m_arFaceManager.facePrefab.GetComponent<MeshRenderer>().material = m_materials[select.value];
    }
}
