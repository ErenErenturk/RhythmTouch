using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Networking;

[System.Serializable]
public class BeatEntry
{
    public float time;
    public string type;
}

[System.Serializable]
public class BeatMapWrapper
{
    public List<BeatEntry> beats;
}

public class BeatMapLoader : MonoBehaviour
{
    public static IEnumerator LoadBeatMap(string filename, System.Action<List<BeatEntry>> onComplete)
    {
        string path = Path.Combine(Application.streamingAssetsPath, filename);
        string json = null;

#if UNITY_ANDROID && !UNITY_EDITOR
        UnityWebRequest request = UnityWebRequest.Get(path);
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Failed to load beatmap: " + request.error);
            onComplete(null);
            yield break;
        }

        json = request.downloadHandler.text;
#else
        if (!File.Exists(path))
        {
            Debug.LogError("Beatmap file not found: " + path);
            onComplete(null);
            yield break;
        }

        json = File.ReadAllText(path);
#endif

        if (string.IsNullOrEmpty(json))
        {
            Debug.LogError("Beatmap JSON is empty or invalid.");
            onComplete(null);
            yield break;
        }

        // BeatEntry listesi doğrudan parse edilemiyor → bir wrapper içine sarıyoruz
        string wrappedJson = "{\"beats\":" + json + "}";
        BeatMapWrapper data = JsonUtility.FromJson<BeatMapWrapper>(wrappedJson);
        onComplete(data.beats);
    }
}
