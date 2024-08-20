﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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
        public UnityEvent OnStart;
        public UnityEvent OnFinished;


        private void Start()
        {
            StartCoroutine(DelayStart());
        }

        IEnumerator DelayStart()
        {
            yield return new WaitForSeconds(FelayOnStart);
            Init();
            OnStart?.Invoke();
        }

        public void NextStep()
        {
            HideStep(CurrentStep);
            if (CurrentStep+1>=tutorialSteps.Count)
            {
                OnFinished?.Invoke();
                return;
            }
            CurrentStep++;
            Debug.Log("TutorialSegment NextStep " + CurrentStep);
            tutorialSteps[CurrentStep].ShowStep();
            TutorialStateChanged?.Invoke(true, CurrentStep);
            StepStarted?.Invoke(CurrentStep);
        }
        public void GotoStep(int step)
        {
            Debug.Log("TutorialSegment GotoStep " + step);
            HideStep(step - 1);
            if (step >= tutorialSteps.Count)
            {
                OnFinished?.Invoke();
                return;
            }
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
            GotoStep(12);
        }


    }


}