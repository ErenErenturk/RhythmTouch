using UnityEngine;
using UnityEngine.UI;

public class SettingsButtonHandler : MonoBehaviour
{
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private Toggle muteToggle;

    private void Start()
    {
        bool isMuted = PlayerPrefs.GetInt("Muted", 0) == 1;
        AudioListener.pause = isMuted;

        if (muteToggle != null)
        {
            muteToggle.isOn = isMuted;
            muteToggle.onValueChanged.AddListener(OnToggleSound);
        }
    }

    public void OnSettingsClicked()
    {
        if (settingsPanel != null)
            settingsPanel.SetActive(!settingsPanel.activeSelf);
    }

    private void OnToggleSound(bool isMuted)
    {
        AudioListener.pause = isMuted;
        PlayerPrefs.SetInt("Muted", isMuted ? 1 : 0);
        PlayerPrefs.Save();
        Debug.Log("Sound is now " + (isMuted ? "muted" : "unmuted"));
    }
}
