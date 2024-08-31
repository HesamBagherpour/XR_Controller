using System.Collections;
using UnityEngine;
using UnityEngine.Animations;

public class ActiveXrOrigin : MonoBehaviour
{
    [SerializeField] RotationConstraint HeadRotation;
    [SerializeField] RotationConstraint LeftHandRotation;
    [SerializeField] RotationConstraint RightHandRotation;
    [SerializeField] PositionConstraint HeadPosition;
    [SerializeField] PositionConstraint LeftHandPosition;
    [SerializeField] PositionConstraint RightHandPosition;

    void Start()
    {
        StartCoroutine(ActiveCouroutine());
    }

    IEnumerator ActiveCouroutine()
    {
        yield return new WaitForSeconds(2);

        HeadRotation.constraintActive = true;
        LeftHandRotation.constraintActive = true;
        RightHandRotation.constraintActive = true;

        HeadPosition.constraintActive = true;
        LeftHandPosition.constraintActive = true;
        RightHandPosition.constraintActive = true;
    }
}