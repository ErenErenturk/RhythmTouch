using UnityEngine;
using UnityEngine.UI;
using TMPro;  // Bunu ekle

public class AudioManager : MonoBehaviour
{
    public AudioSource musicSource;
    public Slider volumeSlider;
    public TMP_Text volumeValueText;  // Text yerine TMP_Text

    void Start()
    {
        float savedVolume = PlayerPrefs.GetFloat("Volume", 1f);
        int sliderValue = Mathf.RoundToInt(savedVolume * 100);
        volumeSlider.value = sliderValue;

        UpdateVolume(sliderValue);
        volumeSlider.onValueChanged.AddListener(UpdateVolume);
    }

    public void UpdateVolume(float sliderValue)
    {
        float volume = sliderValue / 100f;
        musicSource.volume = volume;

        if (volumeValueText != null)
            volumeValueText.text = ((int)sliderValue).ToString() + "%";

        PlayerPrefs.SetFloat("Volume", volume);
    }
}
