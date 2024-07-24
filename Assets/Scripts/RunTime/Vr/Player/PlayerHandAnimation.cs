using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerHandAnimation : MonoBehaviour
{
    [SerializeField] InputActionReference selectAction;
    [SerializeField] InputActionReference activateAction;

    [SerializeField] Transform controller;

    Animator animator;
    GameObject handGameObject;
    bool isActive = true;

    /*void OnEnable()
    {
        selectAction.action.performed += Gripping;
        selectAction.action.canceled += GripRelease;

        activateAction.action.performed += Pinching;
        activateAction.action.canceled += PinchRelease;
    }*/

    void Awake()
    {
        var handIndex = Enumerable.Range(0, controller.childCount).Where(x => controller.GetChild(x).tag == "Hand").First();
        handGameObject = controller.GetChild(handIndex).gameObject;
        animator = handGameObject.GetComponent<Animator>();

        //handGameObject = transform.GetChild(0).gameObject;
        //animator= handGameObject.GetComponent<Animator>();

        selectAction.action.performed += OnGripping;
        selectAction.action.canceled += OnGripRelease;

        activateAction.action.performed += OnPinching;
        activateAction.action.canceled += OnPinchRelease;
    }

    void OnGripping(InputAction.CallbackContext obj)
    {
        if (isActive == true)
            animator.SetFloat("Grip", obj.ReadValue<float>());
    }

    void OnGripRelease(InputAction.CallbackContext obj)
    {
        GripRelease();
    }

    void OnPinching(InputAction.CallbackContext obj)
    {
        if(isActive == true)
            animator.SetFloat("Pinch", obj.ReadValue<float>());
    }

    void OnPinchRelease(InputAction.CallbackContext obj)
    {
        PinchRelease();
    }

    void GripRelease()
    {
        animator.SetFloat("Grip", 0f);
    }
    void PinchRelease()
    {
        animator.SetFloat("Pinch", 0f);
    }

    public void Active()
    {
        isActive = true;
    }
    public void Deactive ()
    {
        isActive = false;
        GripRelease();
        PinchRelease();
    }
}