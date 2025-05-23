using UnityEngine;

public class BeatObject : MonoBehaviour
{
    public float speed = 1f;
    public Vector3 targetPosition = Vector3.zero;
    private bool isHit = false;

    void Update()
    {
        if (isHit) return;

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // Hedefe ulaştı ama tıklanmadıysa yok et
        if (Vector3.Distance(transform.position, targetPosition) < 0.05f)
        {
            Destroy(gameObject);
        }
    }

    public void MarkAsHit()
    {
        isHit = true;
        Destroy(gameObject);
    }
}
