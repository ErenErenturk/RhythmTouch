using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class BeatData
{
    public float bpm;
    public List<float> beats;
}

public class BeatMapLoader : MonoBehaviour
{
    public static BeatData LoadBeatMap(string filename)
    {
        string path = Path.Combine(Application.streamingAssetsPath, filename);
        if (!File.Exists(path))
        {
            Debug.LogError("Beatmap file not found: " + path);
            return null;
        }

        string json = File.ReadAllText(path);
        return JsonUtility.FromJson<BeatData>(json);
    }
}
