using System;
using System.Collections;
using UnityEngine;

public class TriggerControl : MonoBehaviour
{
    [SerializeField, Range(0f, 1)] float returnSpeed = 0.05f;
    [SerializeField] ShootingModeControl shootingMode;
    Animator handFingerAnimator;

    public Action OnTriggerStart;
    public Action OnTriggerEnd;

    void Start()
    {
        handFingerAnimator = GetComponent<Animator>();
    }

    public void OnActionStay(float value)
    {
        handFingerAnimator.SetFloat("TriggerValue", value);
        
        if (value > 0.6)
        {
            OnTriggerStart?.Invoke();
        }
    }

    public void OnActionCancle()
    {
        OnTriggerEnd?.Invoke();
        StartCoroutine(ReturnTodefault());
    }

    IEnumerator ReturnTodefault()
    {
        var animationValue = handFingerAnimator.GetFloat("TriggerValue");
        while (animationValue > 0)
        {
            yield return new WaitForEndOfFrame();
            animationValue -= returnSpeed;
        }
        handFingerAnimator.SetFloat("TriggerValue", 0);
    }

    public void ChangeShootMode(ChangeModeDirection direction)
    {
        shootingMode.ChangeMode(direction);
    }
}