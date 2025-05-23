using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Networking;

[System.Serializable]
public class BeatData
{
    public float bpm;
    public List<float> beats;
}

public class BeatMapLoader : MonoBehaviour
{
    public static IEnumerator LoadBeatMap(string filename, System.Action<BeatData> onComplete)
    {
        string path = Path.Combine(Application.streamingAssetsPath, filename);

#if UNITY_ANDROID && !UNITY_EDITOR
        UnityWebRequest request = UnityWebRequest.Get(path);
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Failed to load beatmap: " + request.error);
            onComplete(null);
            yield break;
        }

        string json = request.downloadHandler.text;
#else
        if (!File.Exists(path))
        {
            Debug.LogError("Beatmap file not found: " + path);
            onComplete(null);
            yield break;
        }

        string json = File.ReadAllText(path);
#endif

        if (string.IsNullOrEmpty(json))
        {
            Debug.LogError("Beatmap JSON is empty or invalid.");
            onComplete(null);
            yield break;
        }

        BeatData data = JsonUtility.FromJson<BeatData>(json);
        onComplete(data);
    }
}
