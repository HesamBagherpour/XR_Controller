using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

namespace AS.Ekbatan_Showdown.Xr_Wrapper.Player
{
    public class InteractorHandControl : MonoBehaviour
    {
        [SerializeField] PlayerHand hand;
        public PlayerHand Hand { get { return hand; } }
        
        [SerializeField] GameObject handGameObject;
        [SerializeField] PlayerController playerController;

        [Space]
        [SerializeField] InputActionReference selectInput;
        [SerializeField, Range(0, 1)] float pressureSensitivity = 0.5f;

        [Space]
        [SerializeField] InputActionReference handPosInput;

        XRDirectInteractor interactor;
        Vector3 handPosition;

        void Start()
        {
            interactor = GetComponent<XRDirectInteractor>();

            interactor.selectEntered.AddListener(OnSelectEntered);
            interactor.selectExited.AddListener(OnSelectExited);

            selectInput.action.started += TakeAction;
            selectInput.action.canceled += ReleaseAction;

            handPosInput.action.performed += HandPositionInput;
        }
        void OnDestroy()
        {
            interactor.selectEntered.RemoveListener(OnSelectEntered);
            interactor.selectExited.RemoveListener(OnSelectExited);

            selectInput.action.started -= TakeAction;
            selectInput.action.canceled -= ReleaseAction;

            handPosInput.action.performed -= HandPositionInput;
        }

        void OnSelectEntered(SelectEnterEventArgs args)
        {
            if(args.interactableObject.interactorsSelecting.Count == 1)
                playerController.FirstSelectEntered(args.interactableObject, hand);
            else
                playerController.SelectEntered();

            if(args.interactableObject.transform.tag == "Gun")
                SetHandActive(false);
        }
        void OnSelectExited(SelectExitEventArgs args)
        {
            playerController.SelectExited(args.interactableObject);
            SetHandActive(true);
        }

        void SetHandActive(bool value)
        {
            handGameObject.SetActive(value);
        }

        void TakeAction(InputAction.CallbackContext callback)
        {
            if(callback.ReadValue<float>() > pressureSensitivity)
            {
                playerController.Take();
                handPosition = Camera.main.transform.localPosition - transform.localPosition;
            }
        }

        void ReleaseAction(InputAction.CallbackContext callback)
        {
            if(callback.ReadValue<float>() < pressureSensitivity)
            {
                playerController.Release();
            }
        }

        void HandPositionInput(InputAction.CallbackContext callback)
        {
            //Debug.Log((handPosition -(Camera.main.transform.localPosition - transform.localPosition)).magnitude);
            playerController.HandPosition((handPosition -(Camera.main.transform.localPosition - transform.localPosition)).magnitude);
        }

        void OnTriggerStay(Collider other)
        {
            if(other.transform.tag == "Bolt")
                playerController.TriggerStay(other, handGameObject, hand);
        }

        void OnTriggerExit(Collider other)
        {
            if(other.transform.tag == "Bolt")
                playerController.TriggerExit();
        }
    }
}