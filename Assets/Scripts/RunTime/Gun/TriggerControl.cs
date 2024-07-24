using System;
using System.Collections;
using UnityEngine;

namespace AS.Ekbatan_Showdown.Xr_Wrapper.RunTime.Gun
{
    public class TriggerControl : MonoBehaviour
    {
    [SerializeField, Range(0f, 1)] float returnSpeed = 0.05f;
    [SerializeField] ShootingModeControl shootingMode;
    Animator handFingerAnimator;

    public Action OnTrigger;

    void Start()
    {
        handFingerAnimator = GetComponent<Animator>();
    }

    public void OnActionStay(float value)
    {
        handFingerAnimator.SetFloat("TriggerValue", value);

        if (value > 0.6)
        {
            OnTrigger?.Invoke();
        }
    }

    public void OnActionCancle()
    {
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
}