using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArioSoren.TutorialKit
{
    public class TutorialSegment : MonoBehaviour
    {
        public float FelayOnStart = 2;
        public event Action<int> StepStarted;
        public event Action<int> StepPassed;
        public event Action<bool, int> TutorialStateChanged;
        public int CurrentStep = -1;

        [SerializeField] private List<TutorialStep> tutorialSteps;


        private void Start()
        {
            StartCoroutine(DelayStart());
        }

        IEnumerator DelayStart()
        {
            yield return new WaitForSeconds(FelayOnStart);
            Init();
        }

        //public void NextStep()
        //{
        //    HideStep(CurrentStep);
        //    CurrentStep++;
        //    tutorialSteps[CurrentStep].ShowStep();
        //    TutorialStateChanged?.Invoke(true, CurrentStep);
        //    StepStarted?.Invoke(CurrentStep);
        //}
        public void GotoStep(int step)
        {
            Debug.Log("TutorialSegment GotoStep " + step);
            HideStep(step - 1);
            CurrentStep = step;
            tutorialSteps[CurrentStep].ShowStep();
            TutorialStateChanged?.Invoke(true, CurrentStep);
            StepStarted?.Invoke(CurrentStep);
        }

        public void HideStep(int step)
        {
            if (step < 0)
                return;
            tutorialSteps[step].HideStep();
            StepPassed?.Invoke(step);
            TutorialStateChanged?.Invoke(false, step);
        }

        public void Init()
        {
            foreach (var step in tutorialSteps)
            {
                step.HideStep();
            }
            //NextStep();
            GotoStep(0);
        }


    }


}