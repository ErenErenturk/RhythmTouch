using UnityEngine;
using TMPro;
using System.Collections;

public class TouchManager : MonoBehaviour
{
    public Transform targetZone;
    public float hitAngleThreshold = 15f;
    public TextMeshProUGUI feedbackText;
    public TextMeshProUGUI scoreText;
    private int score = 0;
    private Coroutine feedbackCoroutine;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            float angle = NormalizeAngle(targetZone.eulerAngles.z);

            if (angle < hitAngleThreshold || angle > (360 - hitAngleThreshold))
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
