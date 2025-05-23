using UnityEngine;

public class BeatObject : MonoBehaviour
{
    public float speed = 1f;
    public Vector3 targetPosition = Vector3.zero;
    private bool isHit = false;

    void Update()
    {
        if (isHit) return;

        // Hareket
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // Hedefe ulaştıysa ve vurulmadıysa log + destroy (geçici kapalı)
        if (Vector3.Distance(transform.position, targetPosition) < 0.05f)
        {
            Debug.Log("Beat reached center but not hit: " + gameObject.name);
            // Destroy(gameObject); // Test sürecinde kapalı
        }
    }

    public void MarkAsHit()
    {
        isHit = true;
        Debug.Log("Beat hit: " + gameObject.name);
        Destroy(gameObject);
    }
}
