using System.Collections;
using UnityEngine;
using UnityEngine.VFX;

public class SwordVFXFadeFix : MonoBehaviour
{
    [Header("VFX")]
    public VisualEffect swordVFX;

    public string alphaClipParam = "Alfa Clipping";
    public string colorParam = "Color";

    [Header("Fade Settings")]
    public float fadeDuration = 1.0f;

    [Header("Values")]
    public float enabledAlphaClip = 0.05f;
    public float disabledAlphaClip = 3.0f;

    [Header("Color Settings")]
    [ColorUsage(true, true)]
    public Color enabledColor = new Color(3f, 1.5f, 0.2f, 1f); // HDR

    [ColorUsage(true, true)]
    public Color disabledColor = new Color(0f, 0f, 0f, 0f);

    private bool isOn = true;
    private bool isBusy = false;

    private void Start()
    {
        if (swordVFX == null)
            swordVFX = GetComponent<VisualEffect>();

        swordVFX.SetFloat(alphaClipParam, enabledAlphaClip);
        swordVFX.SetVector4(colorParam, enabledColor);
        swordVFX.Play();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isBusy)
        {
            if (isOn)
                StartCoroutine(FadeOut());
            else
                StartCoroutine(FadeIn());
        }
    }

    private IEnumerator FadeOut()
    {
        isBusy = true;

        float startAlphaClip = swordVFX.GetFloat(alphaClipParam);
        Vector4 startColor = swordVFX.GetVector4(colorParam);

        float time = 0f;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            float t = time / fadeDuration;

            float alphaClip = Mathf.Lerp(startAlphaClip, disabledAlphaClip, t);
            Vector4 color = Vector4.Lerp(startColor, (Vector4)disabledColor, t);

            swordVFX.SetFloat(alphaClipParam, alphaClip);
            swordVFX.SetVector4(colorParam, color);

            yield return null;
        }

        swordVFX.SetFloat(alphaClipParam, disabledAlphaClip);
        swordVFX.SetVector4(colorParam, disabledColor);

        swordVFX.Stop();

        isOn = false;
        isBusy = false;
    }

    private IEnumerator FadeIn()
    {
        isBusy = true;

        swordVFX.Play();

        float startAlphaClip = swordVFX.GetFloat(alphaClipParam);
        Vector4 startColor = swordVFX.GetVector4(colorParam);

        float time = 0f;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            float t = time / fadeDuration;

            float alphaClip = Mathf.Lerp(startAlphaClip, enabledAlphaClip, t);
            Vector4 color = Vector4.Lerp(startColor, (Vector4)enabledColor, t);

            swordVFX.SetFloat(alphaClipParam, alphaClip);
            swordVFX.SetVector4(colorParam, color);

            yield return null;
        }

        swordVFX.SetFloat(alphaClipParam, enabledAlphaClip);
        swordVFX.SetVector4(colorParam, enabledColor);

        isOn = true;
        isBusy = false;
    }
}