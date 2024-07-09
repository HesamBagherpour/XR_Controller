using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Transformers;

namespace AS.Ekbatan_Showdown.Xr_Wrapper.RunTime.Gun
{
    public class BoltControl : MonoBehaviour
    {
        [SerializeField] HandsOnGunControl handOnGun;
        [SerializeField] GunController gunController;
        [SerializeField] Transform boltAttachPoint;
        [SerializeField] Animator animator;
        [SerializeField, Range(0, 1)] float returnSpeed = 0.1f;

        void OnTriggerStay(Collider other)
        {
            gunController.SetTwoHandRotationMode(XRGeneralGrabTransformer.TwoHandedRotationMode.FirstHandOnly);
            gunController.SetSecondaryAttachTransform(boltAttachPoint);
            handOnGun.SetSecondHandToBolt();
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
            value += _value;
            SetAnimatorValue(value);

            if(value >= 0.9)
            {
                Pull(true);
            }
        }

        public void Pull(bool value)
        {
            //Bolt Pulled;
        }

        public void LeaveBolt()
        {
            StartCoroutine(ReturnToDefault());
        }

        IEnumerator ReturnToDefault()
        {
            var value = GetAnimatorValue();
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
}