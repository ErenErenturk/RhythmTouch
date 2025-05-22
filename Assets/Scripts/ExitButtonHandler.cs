using UnityEngine;

public class ExitButtonHandler : MonoBehaviour
{
    public void OnExitClicked()
    {
        Application.Quit();
        Debug.Log("Game closed."); // Sadece editörde görünür
    }
}
