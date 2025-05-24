using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToMenu : MonoBehaviour
{
    // Bu fonksiyon, buton tarafından çağrılır
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
