using System;
using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Transformers;

public class BoltControl : MonoBehaviour
{
    [SerializeField] HandsOnGunControl handOnGun;
    [SerializeField] GunController gunController;
    [SerializeField] Transform boltAttachPoint;
    [SerializeField] Animator animator;
    [SerializeField, Range(0, 1)] float returnSpeed = 0.1f;

    public Action<bool> OnBoltPull;
    public Action OnReadyToPull;

    void OnTriggerStay(Collider other)
    {
        if (other.transform.tag == "interactor")
        {
            var interactor = other.GetComponent<XRDirectInteractor>();
            if(interactor.hasSelection == false)
            {
                gunController.SetTwoHandRotationMode(XRGeneralGrabTransformer.TwoHandedRotationMode.FirstHandOnly);
                gunController.SetSecondaryAttachTransform(boltAttachPoint);
                handOnGun.SetSecondHandToBolt();
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(! gunController.IsInTwoHandGrab())
        {
            gunController.SetTwoHandRotationMode(XRGeneralGrabTransformer.TwoHandedRotationMode.FirstHandDirectedTowardsSecondHand);
            gunController.SetDefaultSecondaryAttachTransform();
            handOnGun.SetSecondHandToNormal();
        }
    }

    public void MoveBolt(float _value)
    {
        var value = GetAnimatorValue();

        if(value + _value >= -0.1f && value + _value <= 1.05)
        {
            value += _value;
            SetAnimatorValue(value);
        }

        if(value >= 0.9)
        {
            Pull(true);
        }

        if(value <= 0.3f)
        {
            OnReadyToPull?.Invoke();
        }
    }

    public void Pull(bool value)
    {
        OnBoltPull?.Invoke(value);
    }

    public void LeaveBolt()
    {
        StartCoroutine(ReturnToDefault());
    }

    IEnumerator ReturnToDefault()
    {
        var value = GetAnimatorValue();
        value = value > 1 ? 1 : value;
        while(value > 0)
        {
            value -= returnSpeed;
            SetAnimatorValue(value);
            yield return new WaitForEndOfFrame();
        }
        SetAnimatorValue(0);
    }

    float GetAnimatorValue()
    {
        return animator.GetFloat("Blend");
    }
    void SetAnimatorValue(float value)
    {
        animator.SetFloat("Blend", value);
    }
}