using UnityEngine;

public enum BeatType
{
    Normal,
    Hold,
    Double,
    Bomb
}

public class BeatObject : MonoBehaviour
{
    public float speed = 1f;
    public float beatTime;
    public Vector3 targetPosition = Vector3.zero;
    public BeatType type = BeatType.Normal;

    private bool isHit = false;
    private int hitCount = 0;

    void Update()
    {
        if (isHit) return;

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPosition) < 0.05f)
        {
            Debug.Log("Beat reached center but not hit: " + gameObject.name);
            Destroy(gameObject);
        }
    }

    public void MarkAsHit()
    {
        switch (type)
        {
            case BeatType.Normal:
                HandleNormal();
                break;

            case BeatType.Double:
                HandleDouble();
                break;

            case BeatType.Hold:
                HandleHold();
                break;

            case BeatType.Bomb:
                HandleBomb();
                break;
        }
    }

    void HandleNormal()
    {
        isHit = true;
        Debug.Log("Normal beat hit.");
        Destroy(gameObject);
    }

    void HandleDouble()
    {
        hitCount++;
        Debug.Log($"Double beat hit count: {hitCount}");
        if (hitCount >= 2)
        {
            isHit = true;
            Destroy(gameObject);
        }
    }

    void HandleHold()
    {
        isHit = true;
        Debug.Log("Hold beat hit — simulate long press");
        Destroy(gameObject); // Şimdilik tek dokunuşta, ileride süre tutabiliriz
    }

    void HandleBomb()
    {
        isHit = true;
        Debug.LogWarning("Bomb hit! Penalty triggered.");
        TouchManager.Instance.ResetCombo(); // Instance tanımlı değilse yorum satırına al
        Destroy(gameObject);
    }
}
