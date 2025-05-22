using UnityEngine;
using UnityEngine.UI;

public class ColorChanger : MonoBehaviour
{
    public Image targetImage;
    public float changeInterval = 2f;  // Kaç saniyede bir renk geçişi olsun

    private Color currentColor;
    private Color nextColor;
    private float timer = 0f;

    void Start()
    {
        if (targetImage == null) return;

        currentColor = GetRandomHSVColor();
        nextColor = GetRandomHSVColor();
    }

    void Update()
    {
        if (targetImage == null) return;

        timer += Time.deltaTime;
        float t = Mathf.Clamp01(timer / changeInterval); // 0-1 arası zamanla ilerler

        // Yumuşak geçiş
        Color lerped = Color.Lerp(currentColor, nextColor, t);
        targetImage.color = lerped;

        if (t >= 1f)
        {
            // Bir sonraki geçişe hazırlan
            currentColor = nextColor;
            nextColor = GetRandomHSVColor();
            timer = 0f;
        }
    }

    private Color GetRandomHSVColor()
    {
        float hue = Random.Range(0f, 1f);
        return Color.HSVToRGB(hue, 1f, 1f); // Tam doygun ve parlak renk
    }
}
