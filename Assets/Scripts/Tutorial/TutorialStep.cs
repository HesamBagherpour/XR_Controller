using System.Collections.Generic;
//using DG.Tweening;
using UnityEngine;

namespace ArioSoren.TutorialKit
{
    public class TutorialStep : MonoBehaviour
    {
        public int Step;
        [SerializeField] private List<GameObject> _highlightObjects;
        [SerializeField] private AudioSource _audioSource;

        public HighlightBehavior Behaviour;

        public AudioClip AudioClip;


        
        public void ShowStep()
        {
            if (Behaviour != null)
                Behaviour.Show();
            foreach (GameObject go in _highlightObjects)
            {
                go.SetActive(true);
            }

            if (AudioClip != null)
                _audioSource.PlayOneShot(AudioClip);
        }

        private void OpenDialogue()
        {
            //var seq = DOTween.Sequence();
            //seq.Append(_dialogueFrame.DOScale(1.3f, 0.3f).From(0).SetEase(Ease.OutBack, 1.2f).SetDelay(0.1f));
        }

        public void HideStep()
        {
            if (Behaviour != null)
                Behaviour.Hide();
            //if (_dialogueFrame != null)
            //{
            //    CloseDialogue();
            //}

            foreach (GameObject go in _highlightObjects)
            {
                go.SetActive(false);
            }
        }

        //private void CloseDialogue()
        //{
        //    //var seq = DOTween.Sequence();
        //    //seq.Append(_dialogueFrame.DOScale(0, 0.03f).From(1));

        //}
    }
}