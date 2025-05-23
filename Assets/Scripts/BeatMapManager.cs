using UnityEngine;
using System.Collections.Generic;

public class BeatMapManager : MonoBehaviour
{
    public AudioSource musicSource;
    public string beatmapFile = "beatmap.json";
    public float spawnRadius = 3f;
    public float beatSpeed = 1.5f;
    public List<BeatEntry> beatEntries { get; private set; }

    private int currentBeatIndex = 0;

    void Start()
    {
        StartCoroutine(BeatMapLoader.LoadBeatMap(beatmapFile, OnBeatMapLoaded));
    }

    void OnBeatMapLoaded(List<BeatEntry> entries)
    {
        if (entries == null || entries.Count == 0)
        {
            Debug.LogError("Beat entries couldn't be loaded.");
            return;
        }

        beatEntries = entries;
        musicSource.Play();
    }

    void Update()
    {
        if (beatEntries == null || currentBeatIndex >= beatEntries.Count || !musicSource.isPlaying)
            return;

        float songTime = musicSource.time;
        if (songTime >= beatEntries[currentBeatIndex].time)
        {
            SpawnBeat(beatEntries[currentBeatIndex]);
            currentBeatIndex++;
        }
    }

    void SpawnBeat(BeatEntry entry)
    {
        float angle = Random.Range(0f, 360f) * Mathf.Deg2Rad;
        Vector3 spawnPos = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0f) * spawnRadius;

        string prefabName = $"Beat_{entry.type}";
        GameObject prefab = Resources.Load<GameObject>($"Beats/{prefabName}");

        if (prefab == null)
        {
            Debug.LogError($"Prefab for beat type '{entry.type}' not found.");
            return;
        }

        GameObject beatObj = Instantiate(prefab, spawnPos, Quaternion.identity);
        beatObj.transform.localScale = new Vector3(1f, 1f, 1f);
        beatObj.name = $"Spawned_{entry.type}";

        BeatObject bo = beatObj.GetComponent<BeatObject>();
        if (bo != null)
        {
            bo.targetPosition = Vector3.zero;
            bo.speed = beatSpeed;
            bo.beatTime = entry.time;
        }
    }
}
