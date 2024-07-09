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
            var value = animator.GetFloat("Blend");
            value += _value;
            animator.SetFloat("Blend", value);
        }

        public void LeaveBolt()
        {
            StartCoroutine(ReturnToDefault());
        }

        IEnumerator ReturnToDefault()
        {
            var value = animator.GetFloat("Blend");
            while(value > 0)
            {
                value -= 0.1f;
                animator.SetFloat("Blend", value);
                yield return new WaitForEndOfFrame();
            }
            animator.SetFloat("Blend", 0);
        }
    }
}