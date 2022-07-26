using UnityEngine;
using UnityEngine.Animations;

public class NumberManager : MonoBehaviour
{
    [SerializeField]
    GameObject m_NumberObject;

    public void Enable3DNumber(bool enable)
    {
        m_NumberObject.SetActive (enable);
    }
}
