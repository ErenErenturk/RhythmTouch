using UnityEngine;
using System.Collections.Generic;

public class BeatMapManager : MonoBehaviour
{
    public AudioSource musicSource;
    public string beatmapFile = "beatmap.json";
    public GameObject beatPrefab;

    public float spawnRadius = 3f;
    public float beatSpeed = 1.5f;

    private List<float> beatTimes;
    private int currentBeatIndex = 0;

    void Start()
    {
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
            SpawnBeat();
            currentBeatIndex++;
        }
    }

    void SpawnBeat()
    {
        float angle = Random.Range(0f, 360f) * Mathf.Deg2Rad;
        Vector3 spawnPos = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0f) * spawnRadius;

        GameObject beatObj = Instantiate(beatPrefab, spawnPos, Quaternion.identity);
        BeatObject bo = beatObj.GetComponent<BeatObject>();
        bo.targetPosition = Vector3.zero;
        bo.speed = beatSpeed;
    }
}
