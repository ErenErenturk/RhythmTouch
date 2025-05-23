using UnityEngine;
using TMPro;
using System.Collections;

public class TouchManager : MonoBehaviour
{
    public float hitRadius = 0.5f;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI feedbackText;

    private int score = 0;
    private Coroutine feedbackCoroutine;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 clickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            clickPos.z = 0f;

            Collider2D[] hits = Physics2D.OverlapCircleAll(clickPos, hitRadius);

            bool hitSuccess = false;

            foreach (var hit in hits)
            {
                BeatObject beat = hit.GetComponent<BeatObject>();
                if (beat != null)
                {
                    beat.MarkAsHit();
                    score++;
                    UpdateScoreUI();
                    ShowFeedback("Great!", new Color(0f, 1f, 1f), new Color(0f, 0.3f, 0.5f)); // Cyan + koyu mavi
                    hitSuccess = true;
                    break;
                }
            }

            if (!hitSuccess)
            {
                score = Mathf.Max(0, score - 1);
                UpdateScoreUI();
                ShowFeedback("Miss!", Color.red, new Color(0.3f, 0f, 0f)); // Kırmızı + koyu bordo
            }
        }
    }

    void UpdateScoreUI()
    {
        scoreText.text = "Score: " + score;
    }

    void ShowFeedback(string message, Color faceColor, Color underlayColor)
    {
        if (feedbackCoroutine != null)
            StopCoroutine(feedbackCoroutine);

        feedbackCoroutine = StartCoroutine(FeedbackRoutine(message, faceColor, underlayColor));
    }

    IEnumerator FeedbackRoutine(string message, Color faceColor, Color underlayColor)
    {
        feedbackText.text = message;
        feedbackText.faceColor = faceColor;
        feedbackText.fontMaterial.SetColor("_UnderlayColor", underlayColor);
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
