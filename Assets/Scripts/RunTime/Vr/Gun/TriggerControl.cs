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
    bool readyToShoot = true;

    void Start()
    {
        handFingerAnimator = GetComponent<Animator>();
    }

    public void OnActionStay(float value)
    {
        handFingerAnimator.SetFloat("TriggerValue", value);
        
        if (value > 0.7)
        {
            if (readyToShoot)
            {
                OnTriggerStart?.Invoke();
                readyToShoot = false;
            }
        }
        else if(value < 0.4)
        {
            readyToShoot = true;
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