using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

namespace ArioSoren.TutorialKit
{
    public class TutorialSegment : MonoBehaviour
    {
        public float DelayOnStart = 2;
        //public event Action<int> StepStarted;
        //public event Action<int> StepPassed;
        //public event Action<bool, int> TutorialStateChanged;
        public int CurrentStep = -1;
        public UnityEvent OnStart;
        public UnityEvent OnFinished;

        [SerializeField] private List<TutorialStep> tutorialSteps;
        [SerializeField] private List<XRGrabInteractable> _grabables;
        [SerializeField] private float _delayBeforeNextStep;


        private void Start()
        {
            StartCoroutine(DelayStart());
            //_grabables=GetComponentsInChildren<XRGrabInteractable>().ToList();
            foreach (var item in _grabables)
                item.enabled = false;
        }

        IEnumerator DelayStart()
        {
            yield return new WaitForSeconds(DelayOnStart);
            Init();
            OnStart?.Invoke();
        }

        public void NextStep1()
        {
            HideStep(CurrentStep);
            if (CurrentStep + 1 >= tutorialSteps.Count)
            {
                OnFinished?.Invoke();
                return;
            }
            CurrentStep++;
            Debug.Log("TutorialSegment NextStep " + CurrentStep);
            tutorialSteps[CurrentStep].ShowStep();
            //TutorialStateChanged?.Invoke(true, CurrentStep);
            //StepStarted?.Invoke(CurrentStep);
        }
        public void GotoStep(int step)
        {
            if (step < 0)
                OnFinished?.Invoke();
            if (step - 1 != CurrentStep)
                return;

            Debug.Log("TutorialSegment GotoStep " + step);
            HideStep(step - 1);
            if (step >= tutorialSteps.Count)
            {
                OnFinished?.Invoke();
                return;
            }
            StartCoroutine(DelayStartStep(step));
        }
        IEnumerator DelayStartStep(int step)
        {
            yield return new WaitForSeconds(_delayBeforeNextStep);
            CurrentStep = step;
            tutorialSteps[CurrentStep].ShowStep();
        }

        public void HideStep(int step)
        {
            if (step < 0)
                return;
            tutorialSteps[step].HideStep();
            //StepPassed?.Invoke(step);
            //TutorialStateChanged?.Invoke(false, step);
        }

        public void Init()
        {
            foreach (var step in tutorialSteps)
            {
                step._tutorialSegment = this;
                step.HideStep();
            }
            //NextStep();
            GotoStep(0);
        }





        private List<TutorialStep> GetAllSteps()
        {
            TutorialStep[] allSteps = GetComponentsInChildren<TutorialStep>();
            return allSteps.ToList();
        }

        [ContextMenu("FilltutorialSteps")]
        public void FilltutorialSteps()
        {
            List<TutorialStep> allsteps = new List<TutorialStep>();
            tutorialSteps.Clear();
            allsteps.Clear();
            allsteps.AddRange(GetAllSteps());

            for (int i = 0; i < allsteps.Count; i++)
            {
                tutorialSteps.Add(allsteps.Find(x => x.Step == i));
            }
        }



        [ContextMenu("RearrangeStepIds")]
        public void RearrangeStepIds()
        {
            for (int i = 0; i < tutorialSteps.Count; i++)
            {
                tutorialSteps[i].Step = i;
                if (i + 1 != tutorialSteps.Count)
                    tutorialSteps[i].NextStep = i + 1;
                else
                    tutorialSteps[i].NextStep = -1;

            }
        }


    }


}