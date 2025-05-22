using System.Collections.Generic;
using UnityEngine;

public class BeatMapManager : MonoBehaviour
{
    public AudioSource musicSource;
    public Transform targetZone;
    public string beatmapFile = "beatmap.json";
    public float radius = 2f;

    private List<float> beatTimes;
    private int currentBeatIndex = 0;

    void Start()
    {
        // JSON'dan beat verisini yükle
        var beatData = BeatMapLoader.LoadBeatMap(beatmapFile);
        if (beatData == null)
        {
            Debug.LogError("Beat data couldn't be loaded.");
            return;
        }

        beatTimes = beatData.beats;
        musicSource.Play();
    }

    void Update()
    {
        if (beatTimes == null || currentBeatIndex >= beatTimes.Count || !musicSource.isPlaying)
            return;

        float songTime = musicSource.time;

        if (songTime >= beatTimes[currentBeatIndex])
        {
            float angle = Random.Range(0f, 360f);
            float radians = angle * Mathf.Deg2Rad;

            Vector3 newPosition = new Vector3(Mathf.Cos(radians), Mathf.Sin(radians), 0) * radius;
            targetZone.localPosition = newPosition;

            currentBeatIndex++;
        }
    }
}
