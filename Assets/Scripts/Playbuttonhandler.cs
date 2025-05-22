using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButtonHandler : MonoBehaviour
{
    public void OnPlayClicked()
    {
        SceneManager.LoadScene("Game"); // Oyun sahnenin ismi bu olmalı
    }
}
