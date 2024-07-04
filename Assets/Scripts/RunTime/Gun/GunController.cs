using System.Collections.Generic;
using AS.Ekbatan_Showdown.Xr_Wrapper.Gun.GunState;
using AS.Ekbatan_Showdown.Xr_Wrapper.Gun.HandsOnGunControl;
using AS.Ekbatan_Showdown.Xr_Wrapper.Player;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace AS.Ekbatan_Showdown.Xr_Wrapper.Gun
{
    public class GunController : MonoBehaviour
    {
        [SerializeField] XRGrabInteractable xRGrabInteractable;
        [SerializeField] GameObject SecondAttachColider;
        List<GameObject> firstAttachColliders = new List<GameObject>();

        PlayerHand firstSelectingHand;
        HandOnGunControl handOnGun;

        IGunState gunState;
        Idle idle = new Idle();
        OneHandGrab oneHandGrab = new OneHandGrab();
        TwoHandGrab twoHandGrab = new TwoHandGrab();

        void Start()
        {
            xRGrabInteractable.firstSelectEntered.AddListener(OnFirstSelectEntered);
            xRGrabInteractable.selectEntered.AddListener(OnSelectEntered);
            xRGrabInteractable.selectExited.AddListener(OnSelectExited);

            GetFirstAttachColiders();
            handOnGun = GetComponent<HandOnGunControl>();

            MoveToState(idle);
        }
        void OnDestroy()
        {
            xRGrabInteractable.firstSelectEntered.RemoveListener(OnFirstSelectEntered);
            xRGrabInteractable.selectEntered.RemoveListener(OnSelectEntered);
            xRGrabInteractable.selectExited.RemoveListener(OnSelectExited);
        }

        void GetFirstAttachColiders()
        {
            foreach (var colider in xRGrabInteractable.colliders)
                if(colider != SecondAttachColider)
                    firstAttachColliders.Add(colider.gameObject);
        }

        void MoveToState(IGunState state)
        {
            gunState = state;
            gunState.init(this, handOnGun);
            gunState.Enter();
        }

        List<IXRSelectInteractor> GetSelectingInteractors() { return xRGrabInteractable.interactorsSelecting; }
        IXRSelectInteractor GetFirstSelectingInteractor() { return xRGrabInteractable.firstInteractorSelecting; }
        XRInteractionManager GetInteractionManager() { return xRGrabInteractable.interactionManager; }

        void ChangeSelection()
        {
            switch (GetSelectingInteractors().Count)
            {
                case 0:
                    MoveToState(idle);
                    break;
                case 1:
                    MoveToState(oneHandGrab);
                    break;
                case 2:
                    MoveToState(twoHandGrab);
                    break;
            }
        }

        void OnFirstSelectEntered(SelectEnterEventArgs args)
        {
            var playerHandControl = GetFirstSelectingInteractor().transform.GetComponent<InteractorHandControl>();
            firstSelectingHand = playerHandControl.Hand;
        }

        //cancels all interactors selection if first interactor exit
        void CancelOnFirstSelectExited(SelectExitEventArgs args)
        {
            if(!GetFirstSelectingInteractor().hasSelection)
                GetInteractionManager().CancelInteractableSelection(args.interactableObject);
        }

        void OnSelectEntered(SelectEnterEventArgs args)
        {
            ChangeSelection();
        }
        void OnSelectExited(SelectExitEventArgs args)
        {
            CancelOnFirstSelectExited(args);
            ChangeSelection();
        }

        internal void FirstAttachColidersSetActive(bool value)
        {    
            foreach (var colider in firstAttachColliders)
                if (colider.activeSelf != value)
                    colider.SetActive(value);
        }
        internal void SecondAttachColiderSetActive(bool value)
        {
            if (SecondAttachColider.activeSelf != value)
                SecondAttachColider.SetActive(value);
        }

        internal void TriggerEnter(Collider other)
        {
            gunState.TriggerEnter(other);
        }
        internal void TriggerExit(Collider other)
        {
            gunState.TriggerExit(other);
        }

        internal PlayerHand GetFirstSelectedHand()
        {
            return firstSelectingHand;
        }
    }
}