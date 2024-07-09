using UnityEngine;
using UnityEngine.InputSystem;

namespace AS.Ekbatan_Showdown.Xr_Wrapper.RunTime.Player
{
    [RequireComponent(typeof(Animator))]
    public class PlayerHandAnimation : MonoBehaviour
    {
        [SerializeField] InputActionReference selectAction;
        [SerializeField] InputActionReference activateAction;
        Animator animator;

        void OnEnable()
        {
            selectAction.action.performed += Gripping;
            selectAction.action.canceled += GripRelease;

            activateAction.action.performed += Pinching;
            activateAction.action.canceled += PinchRelease;
        }

        void Awake() => animator = GetComponent<Animator>();

        void Gripping(InputAction.CallbackContext obj) => animator.SetFloat("Grip", obj.ReadValue<float>());

        public void GripRelease(InputAction.CallbackContext obj) => animator.SetFloat("Grip", 0f);

        void Pinching(InputAction.CallbackContext obj) => animator.SetFloat("Pinch", obj.ReadValue<float>());

        public void PinchRelease(InputAction.CallbackContext obj) => animator.SetFloat("Pinch", 0f);
    }
}