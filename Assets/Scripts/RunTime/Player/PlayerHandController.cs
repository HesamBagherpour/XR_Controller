using AS.Ekbatan_Showdown.Xr_Wrapper.RunTime.Gun;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

namespace AS.Ekbatan_Showdown.Xr_Wrapper.RunTime.Player
{
    public class PlayerHandController : MonoBehaviour
    {
        [SerializeField] PlayerHand hand;
        public PlayerHand Hand { get { return hand; } }

        [SerializeField] GameObject handGameObject;
        [SerializeField] PlayerHandAnimation handAnimation;

        [Space]
        [SerializeField] InputActionReference selectInput;
        [SerializeField, Range(0, 1)] float pressureSensitivity = 0.5f;
        [SerializeField] InputActionReference gripInput;

        [Space]
        [SerializeField] InputActionReference handPosInput;

        XRDirectInteractor interactor;
        Vector3 OldHandPosition;
        float handPositionFloat;

        GunController gunController;
        BoltControl boltControl;

        void Start()
        {
            interactor = GetComponent<XRDirectInteractor>();

            interactor.selectEntered.AddListener(OnSelectEntered);
            interactor.selectExited.AddListener(OnSelectExited);

            selectInput.action.started += TakeAction;
            selectInput.action.canceled += ReleaseAction;
            gripInput.action.performed += triggerStay;
            gripInput.action.canceled += triggerCancel;

            handPosInput.action.performed += HandPositionInput;
        }
        void OnDestroy()
        {
            interactor.selectEntered.AddListener(OnSelectEntered);
            interactor.selectExited.AddListener(OnSelectExited);

            selectInput.action.started -= TakeAction;
            selectInput.action.canceled -= ReleaseAction;
            gripInput.action.performed -= triggerStay;
            gripInput.action.canceled -= triggerCancel;

            handPosInput.action.performed -= HandPositionInput;
        }

        void OnTriggerStay(Collider other)
        {
            if( ! InteractorHasSelection() && other.transform.tag == "Bolt")
                SetBoltScript(other.GetComponent<BoltControl>());
        }
        void OnTriggerExit(Collider other)
        {
            if( ! InteractorHasSelection() && other.transform.tag == "Bolt")
                SetBoltScript(null);
        }

        void OnSelectEntered(SelectEnterEventArgs eventArgs)
        {
            handGameObject.SetActive(false);

            if(SelectedInteractable().tag == "Gun")
                SetGunController(SelectedInteractable().GetComponent<GunController>());
        }
        void OnSelectExited(SelectExitEventArgs eventArgs)
        {
            handGameObject.SetActive(true);
            SetGunController(null);
        }

        void SetGunController(GunController _gunController)
        {
            gunController = _gunController;
        }
        GunController GetGunController()
        {
            return gunController;
        }

        bool InteractorHasSelection()
        {
            return interactor.hasSelection;
        }

        Transform SelectedInteractable()
        {
            return interactor.interactablesSelected[0].transform;
        }

        void SetBoltScript(BoltControl _boltControl)
        {
            boltControl = _boltControl;
        }

        void TakeAction(InputAction.CallbackContext callback)
        {
            if(boltControl != null && callback.ReadValue<float>() > pressureSensitivity)
            {
                OldHandPosition = transform.localPosition;
            }
        }

        void ReleaseAction(InputAction.CallbackContext callback)
        {
            if(boltControl != null && callback.ReadValue<float>() < pressureSensitivity)
            {
                boltControl.LeaveBolt();
                SetBoltScript(null);
            }
        }

        void triggerStay(InputAction.CallbackContext context)
        {
            if(GetGunController() != null)
            {
                var pressure = context.ReadValue<float>();
                GetGunController().TriggerStay(pressure, hand);
            }
        }
        void triggerCancel(InputAction.CallbackContext context)
        {
            if(GetGunController() != null)
            {
                GetGunController().TriggerCancel(hand);
            }
        }

        void HandPositionInput(InputAction.CallbackContext context)
        {
            if(boltControl != null && InteractorHasSelection())
            {
                var distance = transform.localPosition - OldHandPosition;

                int direction = 0;
                var angle = Quaternion.Angle(Quaternion.LookRotation(distance), SelectedInteractable().rotation);

                if (angle > 130)
                    direction = 1;
                else if (angle < 50)
                    direction = -1;

                handPositionFloat = distance.magnitude * direction * 6;
                OldHandPosition = transform.localPosition;
                boltControl.MoveBolt(handPositionFloat);
            }
        }
    }
}