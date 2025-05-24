using UnityEngine;

public class PersistentAudio : MonoBehaviour
{
    private static PersistentAudio instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Sahne değişse bile bu objeyi yok etme
        }
        else
        {
            Destroy(gameObject); // Aynı objeden başka varsa yok et
        }
    }
}
