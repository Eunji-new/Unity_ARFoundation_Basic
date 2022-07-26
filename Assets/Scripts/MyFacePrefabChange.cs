using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARFaceManager))]
public class MyFacePrefabChange : MonoBehaviour
{
	private ARFaceManager arfacemanager;
	[SerializeField]
	private ARSession arSession;
	[SerializeField]
	private Dropdown m_Dropdown;
	public Dropdown dropdown
    {
		get => m_Dropdown;
		set => m_Dropdown = value;
	}
	[SerializeField]
	private List<GameObject> m_ObjectsToPlace;
	public List<GameObject> objectsToPlace
	{
		get => m_ObjectsToPlace;
		set => m_ObjectsToPlace = value;
	}
	private ARFace m_Face;

	private GameObject onePrefab;
	private GameObject twoPrefab;

    private void Awake()
    {
		arfacemanager = GetComponent<ARFaceManager>();
		m_Face = GetComponent<ARFace>();
		arfacemanager.facePrefab = m_ObjectsToPlace[0];
	}
    void Start()
	{
		Set_UI();
	}

	private void Set_UI()
	{
		Reset_UI();
		dropdown.onValueChanged.AddListener(delegate {
			FacePrefabshange(dropdown);
		});
	}

	private void FacePrefabshange(Dropdown select)
	{
		arfacemanager.facePrefab = m_ObjectsToPlace[select.value];
		arSession.Reset();
	}

	private void Reset_UI()
	{
		dropdown.onValueChanged.RemoveAllListeners();
	}
}
