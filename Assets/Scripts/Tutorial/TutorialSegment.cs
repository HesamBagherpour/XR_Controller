using System;
using System.Collections.Generic;
using UnityEngine;

namespace ArioSoren.TutorialKit
{
    public class TutorialSegment : MonoBehaviour
    {
        public event Action<int> StepStarted;
        public event Action<int> StepPassed;
        public event Action<bool, int> TutorialStateChanged;

        [SerializeField] private List<TutorialStep> tutorialSteps;
        //public int lastStartedStep;
        //public int lastFinishedStep;
        public int CurrentStep = -1;


        private void Start()
        {
            Init();
        }
        public void NextStep()
        {
            HideStep(CurrentStep);
            CurrentStep++;
            tutorialSteps[CurrentStep].ShowStep();
            TutorialStateChanged?.Invoke(true, CurrentStep);
            StepStarted?.Invoke(CurrentStep);
        }
        public void GotoStep(int step)
        {
            HideStep(step-1);
            CurrentStep= step;
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

        //public void SetStep(int lastStartedStep, int lastFinishedStep)
        //{
        //    //this.lastStartedStep = lastStartedStep;
        //    //this.lastFinishedStep = lastFinishedStep;
        //}

        //protected virtual void OnStepPassed(int step)
        //{
        //    //lastFinishedStep = step;
        //    StepPassed?.Invoke(step);

        //    TutorialStateChanged?.Invoke(false, step);

        //}

        //protected virtual void OnStepStarted(int step)
        //{
        //    //lastStartedStep = step;

        //}

        public void Init()
        {
            foreach (var step in tutorialSteps)
            {
                step.HideStep();
            }
            //if (lastFinishedStep < lastStartedStep)
            //{
                //var step = FindLastStartableStep(lastStartedStep);
                NextStep();
            //}
        }

        //public int FindLastStartableStep(int lastStep)
        //{
        //    while (true)
        //    {
        //        if (lastStep == 1) return 1;

        //        if (tutorialSteps.Find(s => s.Step == lastStep).Startable)
        //        {
        //            return lastStep;
        //        }

        //        lastStep -= 1;
        //    }
        //}
    }

    public enum HighlightType
    {
        CirculateAroundObject,
        AnimatedArrow,
        AnimatedHand,
    }

}