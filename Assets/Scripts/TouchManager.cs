using UnityEngine;
using TMPro;
using System.Collections;

public class TouchManager : MonoBehaviour
{
    public static TouchManager Instance { get; private set; } // Singleton erişimi

    public float hitRadius = 0.5f;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI feedbackText;
    public TextMeshProUGUI comboText;
    public BeatMapManager beatMapManager;
    public AudioSource musicSource;
    public HitTimingConfig timingConfig;

    private int score = 0;
    private int perfectCombo = 0;
    private Coroutine feedbackCoroutine;
    private Coroutine comboCoroutine;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }


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
                    float songTime = musicSource.time;
                    float diff = GetClosestBeatDifference(songTime);

                    if (diff <= timingConfig.perfectThreshold)
                    {
                        score += 5;
                        perfectCombo++;
                        ShowFeedback("Perfect!", new Color(1f, 0.95f, 0.4f), new Color(0.4f, 0.3f, 0f));
                        if (perfectCombo >= 3)
                            ShowComboText("Perfect x" + perfectCombo + "!");
                    }
                    else if (diff <= timingConfig.greatThreshold)
                    {
                        score += 3;
                        perfectCombo = 0;
                        ShowFeedback("Great!", new Color(0f, 1f, 1f), new Color(0f, 0.3f, 0.5f));
                    }
                    else if (diff <= timingConfig.goodThreshold)
                    {
                        score += 1;
                        perfectCombo = 0;
                        ShowFeedback("Good", new Color(0.4f, 1f, 0.4f), new Color(0f, 0.3f, 0f));
                    }
                    else
                    {
                        score = Mathf.Max(0, score - 1);
                        perfectCombo = 0;
                        ShowFeedback("Miss!", Color.red, new Color(0.3f, 0f, 0f));
                    }

                    UpdateScoreUI();
                    hitSuccess = true;
                    break;
                }
            }

            if (!hitSuccess)
            {
                score = Mathf.Max(0, score - 1);
                perfectCombo = 0;
                UpdateScoreUI();
                ShowFeedback("Miss!", Color.red, new Color(0.3f, 0f, 0f));
            }
        }
    }

    float GetClosestBeatDifference(float songTime)
    {
        float closest = float.MaxValue;
        foreach (var beat in beatMapManager.beatEntries)
        {
            float diff = Mathf.Abs(beat.time - songTime);
            if (diff < closest)
                closest = diff;
        }
        return closest;
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
        feedbackText.transform.localScale = Vector3.zero;

        float punchTime = 0.15f;
        float elapsed = 0f;
        while (elapsed < punchTime)
        {
            float t = elapsed / punchTime;
            float scale = Mathf.SmoothStep(0f, 1.2f, t);
            feedbackText.transform.localScale = Vector3.one * scale;
            elapsed += Time.deltaTime;
            yield return null;
        }

        feedbackText.transform.localScale = Vector3.one;

        yield return new WaitForSeconds(0.4f);

        float fadeTime = 0.5f;
        while (feedbackText.alpha > 0)
        {
            feedbackText.alpha -= Time.deltaTime / fadeTime;
            yield return null;
        }

        feedbackText.text = "";
        feedbackText.transform.localScale = Vector3.one;
    }

    void ShowComboText(string message)
    {
        if (comboCoroutine != null)
            StopCoroutine(comboCoroutine);

        comboCoroutine = StartCoroutine(ComboRoutine(message));
    }

    IEnumerator ComboRoutine(string message)
    {
        comboText.text = message;
        comboText.alpha = 1;
        comboText.transform.localScale = Vector3.zero;

        float scaleTime = 0.2f;
        float elapsed = 0f;
        while (elapsed < scaleTime)
        {
            float t = elapsed / scaleTime;
            float scale = Mathf.SmoothStep(0f, 1.5f, t);
            comboText.transform.localScale = Vector3.one * scale;
            elapsed += Time.deltaTime;
            yield return null;
        }

        comboText.transform.localScale = Vector3.one;

        yield return new WaitForSeconds(1f);

        float fadeTime = 0.5f;
        while (comboText.alpha > 0)
        {
            comboText.alpha -= Time.deltaTime / fadeTime;
            yield return null;
        }

        comboText.text = "";
        comboText.transform.localScale = Vector3.one;
    }
    public void ResetCombo()
    {
        perfectCombo = 0;
        ShowFeedback("Combo Broken!", Color.gray, Color.black);
        UpdateScoreUI();
    }

}
