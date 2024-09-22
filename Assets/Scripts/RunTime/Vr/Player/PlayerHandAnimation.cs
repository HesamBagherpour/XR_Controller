using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerHandAnimation : MonoBehaviour
{
    [SerializeField] InputActionReference selectAction;
    [SerializeField] InputActionReference activateAction;
    [SerializeField] Transform controller;
    [SerializeField] PlayerHandController playerHand;

    HandsAnimation handsAnimation;
    Animator handDeformAnimator;
    Animator directInteractorAnimator;
    GameObject handGameObject;
    bool isActive = true;
    bool Isis = false;

    void Awake()
    {
        var handIndex = Enumerable.Range(0, controller.childCount).Where(x => controller.GetChild(x).tag == "Hand").First();
        handGameObject = controller.GetChild(handIndex).gameObject;

        handDeformAnimator = handGameObject.GetComponent<Animator>();
        directInteractorAnimator = GetComponent<Animator>();

        //handGameObject = transform.GetChild(0).gameObject;
        //animator= handGameObject.GetComponent<Animator>();

        selectAction.action.performed += OnGripping;
        selectAction.action.canceled += OnGripRelease;

        activateAction.action.performed += OnPinching;
        activateAction.action.canceled += OnPinchRelease;
    }

    public void SetHandsAnimation(HandsAnimation _handsAnimation)
    {
        handsAnimation = _handsAnimation;
    }

    void OnGripping(InputAction.CallbackContext obj)
    {
        var value = obj.ReadValue<float>();
        Grip(value);
        RemoteCharacterGrip(value);
    }

    void Grip(float value)
    {
        if(isActive == true)
        handDeformAnimator.SetFloat("Grip", value);
    }
    void RemoteCharacterGrip(float value)
    {
        if(handsAnimation != null)
            handsAnimation.Grab(playerHand.Hand, value);
    }

    void OnGripRelease(InputAction.CallbackContext obj)
    {
        GripRelease();
        RemoteCharacterGripRelease();
    }

    void OnPinching(InputAction.CallbackContext obj)
    {
        if(handsAnimation != null)
            handsAnimation.Pinch(playerHand.Hand, obj.ReadValue<float>());

        if(isActive == true)
            handDeformAnimator.SetFloat("Pinch", obj.ReadValue<float>());
    }

    void OnPinchRelease(InputAction.CallbackContext obj)
    {
        PinchRelease();
        RemoteCharacterPinchRelease();
    }

    void GripRelease()
    {
        handDeformAnimator.SetFloat("Grip", 0f);
    }
    void PinchRelease()
    {
        handDeformAnimator.SetFloat("Pinch", 0f);
    }

    void RemoteCharacterGripRelease()
    {
        if(handsAnimation != null)
            handsAnimation.Grab(playerHand.Hand, 0f);
    }
    void RemoteCharacterPinchRelease()
    {
        if(handsAnimation != null)
            handsAnimation.Pinch(playerHand.Hand, 0f);
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

    public void PlayRecoil()
    {
        if(Isis)
        {
            directInteractorAnimator.CrossFade("Recoil2",0.1f);
            Isis = false;
        }
        else
        {
            directInteractorAnimator.CrossFade("Recoil1",0.1f);
            Isis = true;
        }
    }
}