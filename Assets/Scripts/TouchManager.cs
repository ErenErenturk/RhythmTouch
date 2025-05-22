using UnityEngine;

public class TouchManager : MonoBehaviour
{
    public Transform targetZone; // TargetZone objesini buraya sürükle
    public float hitAngleThreshold = 15f; // ±15 derece

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Mobilde dokunma / PC'de tıklama
        {
            float angle = NormalizeAngle(targetZone.eulerAngles.z);

            if (angle < hitAngleThreshold || angle > (360 - hitAngleThreshold))
            {
                Debug.Log("Başarılı!");
            }
            else
            {
                Debug.Log("Kaçırdın!");
            }
        }
    }

    float NormalizeAngle(float angle)
    {
        return (angle % 360 + 360) % 360;
    }
}
