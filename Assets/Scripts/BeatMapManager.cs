using System.Collections.Generic;
using UnityEngine;

public class BeatMapManager : MonoBehaviour
{
    public AudioSource musicSource;
    public Transform circle;
    public Transform targetZone;
    public string beatmapFile = "beatmap.json";
    public float rotationPerBeat = 30f;

    public List<float> activeBeats { get; private set; } = new List<float>();

    private List<float> beatTimes;
    private int currentBeatIndex = 0;
    private float totalRotation = 0f;

    void Start()
    {
        var beatData = BeatMapLoader.LoadBeatMap(beatmapFile);
        if (beatData == null)
        {
            Debug.LogError("Beat data couldn't be loaded.");
            return;
        }

        beatTimes = beatData.beats;
        activeBeats = beatData.beats;
        musicSource.Play();
    }

    void Update()
    {
        if (beatTimes == null || currentBeatIndex >= beatTimes.Count || !musicSource.isPlaying)
            return;

        float songTime = musicSource.time;

        if (songTime >= beatTimes[currentBeatIndex])
        {
            totalRotation += rotationPerBeat;

            // Mutlak rotasyon veriyoruz → kayma olmaz
            circle.localRotation = Quaternion.Euler(0f, 0f, -totalRotation);

            currentBeatIndex++;
        }
    }

    void LateUpdate()
    {
        if (targetZone != null)
        {
            targetZone.localPosition = new Vector3(0f, 0.5f, 0f); // zorla sabit tut
            targetZone.localRotation = Quaternion.identity;       // açısını sıfırla
        }
    }

}
