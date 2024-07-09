using System.Collections;
using UnityEngine;

namespace AS.Ekbatan_Showdown.Xr_Wrapper.RunTime.Gun
{
    public class TriggerControl : MonoBehaviour
    {
        Animator animator;

        void Start()
        {
            animator = GetComponent<Animator>();
        }

        public void OnActionStay(float value)
        {
            Debug.Log("OnActionStay");
            animator.SetFloat("TriggerValue", value);

            if (value > 0.6)
            {
                //SHOOT START
            }
        }

        public void OnActionCancle()
        {
            Debug.Log("OnActionCancle");
            //SHOOT STOP
            StartCoroutine(ReturnTodefault());
        }

        IEnumerator ReturnTodefault()
        {
            var animationValue = animator.GetFloat("TriggerValue");
            while (animationValue > 0)
            {
                yield return new WaitForEndOfFrame();
                animationValue -= 0.02f;
            }
            animator.SetFloat("TriggerValue", 0);
        }
    }
}