using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class FadeUI : MonoBehaviour
{
    private Image image;

    private void OnValidate()
    {
        Assert.AreEqual(1, GetComponentsInChildren<Image>().Length);
    }

    private void Awake()
    {
        image = GetComponentInChildren<Image>();
        Alpha = 0.0f;
    }

    private float Alpha
    {
        set => image.color = new Color(0.0f, 0.0f, 0.0f, value);
    }

    public IEnumerator FadeIn()
    {
        return Fade(start: 0.0f, end: 1.0f, duration: 1.0f);
    }

    public IEnumerator FadeOut()
    {
        return Fade(start: 1.0f, end: 0.0f, duration: 1.0f);
    }

    private IEnumerator Fade(float start, float end, float duration)
    {
        float elapsed = 0.0f;
        Alpha = start;
        while (elapsed < duration)
        {
            Alpha = Mathf.Lerp(start, end, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        Alpha = end;
    }
}
