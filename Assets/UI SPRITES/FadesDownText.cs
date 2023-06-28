using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadesDownText : MonoBehaviour
{
    public float fadeInDuration = 1.0f;     // Duration of the fade-in animation
    public float descendDuration = 1.0f;    // Duration of the  animation
    public float targetYPosition = 400.3f;  // Target Y position of the text

    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private float initialY;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        initialY = rectTransform.localPosition.y;

        rectTransform.localPosition = new Vector3(rectTransform.localPosition.x, initialY + 200f, rectTransform.localPosition.z);
        canvasGroup.alpha = 0f;

        StartCoroutine(FadeInAndDescend());
    }

    private System.Collections.IEnumerator FadeInAndDescend()
    {
        yield return new WaitForSeconds(0.5f); // Delay before starting the animation

        float startTime = Time.time;

        while (canvasGroup.alpha < 1f)
        {
            float elapsedTime = Time.time - startTime;
            float normalizedTime = Mathf.Clamp01(elapsedTime / fadeInDuration);

            rectTransform.localPosition = new Vector3(rectTransform.localPosition.x, Mathf.Lerp(initialY + 200f, targetYPosition, normalizedTime), rectTransform.localPosition.z);
            canvasGroup.alpha = normalizedTime;

            yield return null;
        }

        yield return new WaitForSeconds(0.5f); // Delay after the fade-in animation

        startTime = Time.time;

        while (rectTransform.localPosition.y > targetYPosition)
        {
            float elapsedTime = Time.time - startTime;
            float normalizedTime = Mathf.Clamp01(elapsedTime / descendDuration);

            rectTransform.localPosition = new Vector3(rectTransform.localPosition.x, Mathf.Lerp(initialY, targetYPosition, normalizedTime), rectTransform.localPosition.z);

            yield return null;
        }
    }

}
