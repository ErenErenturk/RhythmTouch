using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class TouchManager : MonoBehaviour
{
    public Transform targetZone;
    public float hitAngleThreshold = 15f;
    public float timeThreshold = 0.2f;
    public AudioSource musicSource;
    public BeatMapManager beatMapManager;

    public TextMeshProUGUI feedbackText;
    public TextMeshProUGUI scoreText;

    private int score = 0;
    private Coroutine feedbackCoroutine;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            float angle = NormalizeAngle(targetZone.eulerAngles.z);
            float songTime = musicSource.time;

            bool isAngleValid = angle < hitAngleThreshold || angle > (360 - hitAngleThreshold);

            float closestDiff = float.MaxValue;
            foreach (float beat in beatMapManager.activeBeats)
            {
                float diff = Mathf.Abs(beat - songTime);
                if (diff < closestDiff)
                    closestDiff = diff;
            }

            bool isTimingValid = closestDiff <= timeThreshold;

            if (isAngleValid && isTimingValid)
            {
                score++;
                UpdateScoreUI();
                ShowFeedback("Great!", Color.green);
            }
            else
            {
                ShowFeedback("Miss!", Color.red);
            }
        }
    }

    void UpdateScoreUI()
    {
        scoreText.text = "Score: " + score.ToString();
    }

    float NormalizeAngle(float angle)
    {
        return (angle % 360 + 360) % 360;
    }

    void ShowFeedback(string message, Color color)
    {
        if (feedbackCoroutine != null)
            StopCoroutine(feedbackCoroutine);

        feedbackCoroutine = StartCoroutine(FeedbackRoutine(message, color));
    }

    IEnumerator FeedbackRoutine(string message, Color color)
    {
        feedbackText.text = message;
        feedbackText.color = color;
        feedbackText.alpha = 1;

        yield return new WaitForSeconds(1f);

        float fadeTime = 0.5f;
        while (feedbackText.alpha > 0)
        {
            feedbackText.alpha -= Time.deltaTime / fadeTime;
            yield return null;
        }

        feedbackText.text = "";
    }
}
