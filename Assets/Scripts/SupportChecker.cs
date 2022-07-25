using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
public class SupportChecker : MonoBehaviour
{

    [SerializeField]
    ARSession m_Session;

    [SerializeField]
    Text m_LogText;

    [SerializeField]
    Button m_InstallButton;

    private void OnEnable()
    {
        StartCoroutine(CheckSupport());
    }

    void Log(string message)
    {
        m_LogText.text += $"{message}\n";
    }

    void SetInstallButtonActive(bool active)
    {
        if (m_InstallButton != null)
            m_InstallButton.gameObject.SetActive(active);
    }

    public void OnInstallButtonPressed()
    {
        StartCoroutine(Install());
    }

    IEnumerator Install()
    {
        SetInstallButtonActive(false);
        if (ARSession.state == ARSessionState.NeedsInstall)
        {
            Log("Attempting install...");
            yield return ARSession.Install();
            if (ARSession.state == ARSessionState.NeedsInstall)
            {
                Log("The software update failed, or you declined the update");
                SetInstallButtonActive(true);
            }
            else if (ARSession.state == ARSessionState.Ready)
            {
                Log("Success! Starting AR session ,,,");
                m_Session.enabled = true;
            }
        }
        else
        {
            Log("����!: ARSession does not require install");
        }
    }

    IEnumerator CheckSupport()
    {
        SetInstallButtonActive(false);
        Log("AR Support�� üũ�ϴ���...");

        yield return ARSession.CheckAvailability();

        if (ARSession.state == ARSessionState.NeedsInstall)
        {
            Log("Your device supports AR");
            Log("Requires a software update");
            Log("Attemptinf install...");
            yield return ARSession.Install();
        }

        if (ARSession.state == ARSessionState.Ready)
        {
            Log("Your device support AR!");
            Log(" Starting AR Session...");

            m_Session.enabled = true;

        }
        else
        {
            switch (ARSession.state)
            {
                case ARSessionState.Unsupported:
                    Log("Your device dose not support AR");
                    break;
                case ARSessionState.NeedsInstall:
                    Log("sw ������Ʈ ����, �Ǵ� �ٿ�׷��̵尡 �ʿ��մϴ�.");
                    break;
            }
            Log("\n[Start non-AR experienve instead]");
        }
    }
}
