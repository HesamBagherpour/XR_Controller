using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Animator))]
public class HandAnimation : MonoBehaviour
{
    [SerializeField] private InputActionReference selectAction;
    [SerializeField] private InputActionReference activateAction;
    private Animator animator;

    private void OnEnable()
    {
        selectAction.action.performed += Gripping;
        selectAction.action.canceled += GripRelease;

        activateAction.action.performed += Pinching;
        activateAction.action.canceled += PinchRelease;
    }

    void Awake() => animator = GetComponent<Animator>();

    void Gripping(InputAction.CallbackContext obj) => animator.SetFloat("Grip", obj.ReadValue<float>());

    void GripRelease(InputAction.CallbackContext obj) => animator.SetFloat("Grip", 0f);

    void Pinching(InputAction.CallbackContext obj) => animator.SetFloat("Pinch", obj.ReadValue<float>());

    void PinchRelease(InputAction.CallbackContext obj) => animator.SetFloat("Pinch", 0f);

}
