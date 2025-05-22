using UnityEngine;

public class BackButtonHandler : MonoBehaviour
{
    public GameObject settingsPanel;

    public void OnBackButton()
    {
        settingsPanel.SetActive(false);
    }
}
