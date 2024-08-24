using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateColor : MonoBehaviour
{
    public Renderer objectRenderer;
    public Color targetColor = Color.red;
    public float duration = 1f;
    public bool AutoStart = false;
    public string MaterialProperty = "_BaseColor";

    private Material material;
    private Color originalColor;
    private bool AlloAnimationLoop = false;

    private void Start()
    {
        if (AutoStart)
            StartColorAnimation();
    }

    public void StartColorAnimation()
    {
        if (objectRenderer != null)
        {
            material = objectRenderer.material;
            if (material.HasProperty(MaterialProperty))
            {
                originalColor = material.GetColor(MaterialProperty);
                AlloAnimationLoop = true;
                StartCoroutine(GlowLoop());
            }
        }
    }
    private void OnDisable()
    {
        AlloAnimationLoop = false;
    }

    public void StopColorAnimation()
    {
        AlloAnimationLoop = false;
    }

    IEnumerator GlowLoop()
    {
        while (AlloAnimationLoop)
        {
            yield return StartCoroutine(LerpColor(originalColor, targetColor, duration / 2));
            yield return StartCoroutine(LerpColor(targetColor, originalColor, duration / 2));
        }
    }

    IEnumerator LerpColor(Color startColor, Color endColor, float lerpDuration)
    {
        float time = 0;
        while (time < lerpDuration)
        {
            material.SetColor(MaterialProperty, Color.Lerp(startColor, endColor, time / lerpDuration));
            time += Time.deltaTime;
            yield return null;
        }
        material.SetColor(MaterialProperty, endColor);
    }
}
