using System;
using System.Collections;
using UnityEngine;

namespace AS.Ekbatan_Showdown.Xr_Wrapper.RunTime.Gun
{
    public class TriggerControl : MonoBehaviour
    {
        [SerializeField, Range(0f, 1)] float returnSpeed = 0.05f;
        Animator animator;
        public Action OnTrigger;
        private bool _actionKeepDoing = false;

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
                //First Check
                //if Magazine Entered
                //if Bolt Pulled

                //SHOOT START
                
                //OnTrigger?.Invoke();
                _actionKeepDoing = true;
                StartCoroutine(SimulateOnKey());
            }
        }

        public void OnActionCancle()
        {
            //SHOOT STOP
            StartCoroutine(ReturnTodefault());
            _actionKeepDoing = false;
        }

        IEnumerator ReturnTodefault()
        {
            var animationValue = animator.GetFloat("TriggerValue");
            while (animationValue > 0)
            {
                yield return new WaitForEndOfFrame();
                animationValue -= returnSpeed;
            }
            animator.SetFloat("TriggerValue", 0);
        }


     
        IEnumerator SimulateOnKey()
        {
            while(_actionKeepDoing )
            {
                yield return new WaitForEndOfFrame();
                OnTrigger?.Invoke();
            }
        }
    }
}