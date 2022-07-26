using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARFaceManager))]
public class MyFacePrefabChange : MonoBehaviour
{
	private ARFaceManager arfacemanager;
	/*[SerializeField]
	private ARSession arSession;*/
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

	/* private void OnEnable()
	 {
		 if(arfacemanager != null && arfacemanager.subsystem != null)
		 {
			 SetVisible((m_Face.trackingState == TrackingState.Tracking) && (ARSession.state > ARSessionState.Ready));
			 m_Face.updated += OnUpdated;
		 }
		 else
		 {
			 enabled = false;
		 }
	 }

	 private void OnDisable()
	 {
		 m_Face.updated -= OnUpdated;
		 SetVisible(false);
	 }

	 private void SetVisible(bool visible)
	 {
		 if (onePrefab != null && dropdown.value == 0)
		 {
			 onePrefab.SetActive(visible);
		 }
		 if (twoPrefab != null && dropdown.value == 1)
		 {
			 twoPrefab.SetActive(visible);
		 }
	 }


	 private void FacePrefabshange(Dropdown select)
	 {
		 CreateObjectNecessary();
		 SetVisible(false);
		 switch (select.value)
		 {
			 case 0:
				 arfacemanager.facePrefab = onePrefab;
				 onePrefab.SetActive(true);
				 break;
			 case 1:
				 arfacemanager.facePrefab = twoPrefab;
				 twoPrefab.SetActive(true);
				 break;
		 }
		 arSession.Reset();
	 }

	 private void CreateObjectNecessary()
	 {
		 if (onePrefab == null)
		 {
			 onePrefab = Instantiate(prefabs[0], prefabs[0].transform);
			 onePrefab.SetActive(false);
		 }
		 if (twoPrefab == null)
		 {
			 twoPrefab = Instantiate(prefabs[1], prefabs[1].transform);
			 twoPrefab.SetActive(false);
		 }
	 }

	 void OnUpdated(ARFaceUpdatedEventArgs eventArgs)
	 {
		 CreateObjectNecessary();
		 SetVisible((m_Face.trackingState == TrackingState.Tracking) && (ARSession.state > ARSessionState.Ready));
	 }*/
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
		//arSession.Reset();
	}

	private void Reset_UI()
	{
		dropdown.onValueChanged.RemoveAllListeners();
	}
}
