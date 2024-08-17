using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;

public class InputTest : MonoBehaviour
{
    public void RightTrigger(InputAction.CallbackContext context)
    {
        Debug.Log("RightTrigger : " + context.ReadValue<float>());
    }

    public void RightGrip(InputAction.CallbackContext context)
    {
        Debug.Log("RightGrip : " + context.ReadValue<float>());
    }

    public void RightPrimaryButton(InputAction.CallbackContext context)
    {
        Debug.Log("RightPrimaryButton");
    }

    public void RightSecondaryButton(InputAction.CallbackContext context)
    {
        Debug.Log("RightSecondaryButton ");
    }

    public void RightJoystick(InputAction.CallbackContext context)
    {
        Debug.Log("RightTrigger : " + context.ReadValue<Vector2>());
    }

    public void RightControllerPosition(InputAction.CallbackContext context)
    {
        Debug.Log("RightControllerPosition : " + context.ReadValue<Vector3>());
    }

    public void RightControllerRotation(InputAction.CallbackContext context)
    {
        Debug.Log("RightControllerRotation : " + context.ReadValue<Quaternion>());
    }

    public void LeftTrigger(InputAction.CallbackContext context)
    {
        Debug.Log("LeftTrigger : " + context.ReadValue<float>());
    }

    public void LeftGrip(InputAction.CallbackContext context)
    {
        Debug.Log("LeftGrip : " + context.ReadValue<float>());
    }

    public void LeftPrimaryButton(InputAction.CallbackContext context)
    {
        Debug.Log("LeftPrimaryButton");
    }

    public void LeftSecondaryButton(InputAction.CallbackContext context)
    {
        Debug.Log("LeftSecondaryButton ");
    }

    public void LeftJoystick(InputAction.CallbackContext context)
    {
        Debug.Log("LeftJoystick : " + context.ReadValue<Vector2>());
    }

    public void LeftControllerPosition(InputAction.CallbackContext context)
    {
        Debug.Log("LeftControllerPosition : " + context.ReadValue<Vector3>());
    }

    public void LeftControllerRotation(InputAction.CallbackContext context)
    {
        Debug.Log("LeftControllerRotation : " + context.ReadValue<Quaternion>());
    }

    public void MenuButton(InputAction.CallbackContext context) 
    {
        Debug.Log("MenuButton");
    }

    public void MetaButton(InputAction.CallbackContext context)
    {
        Debug.Log("MetaButton");
    }
}
