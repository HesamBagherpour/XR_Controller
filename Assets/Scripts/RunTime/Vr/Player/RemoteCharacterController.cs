using RootMotion.FinalIK;
using UnityEngine;

public class RemoteCharacterController : MonoBehaviour
{
    [Header("Origin")]
    [SerializeField] Transform head;
    [SerializeField] Transform leftHand;
    [SerializeField] Transform rightHand;

    [Header("Remote Character")]
    [SerializeField] GameObject character;

    HandsAnimation remoteHandAnimation;
    VRIK vRIK;

    void Awake()
    {
        if(character != null)
        {
            GetRemoteCharacterAnimatorController();
            SetRemoteCharacterVRIKReferences();
        }
    }

    void GetRemoteCharacterAnimatorController()
    {
        remoteHandAnimation = character.GetComponent<HandsAnimation>();

        leftHand.GetComponentInChildren<PlayerHandAnimation>().SetHandsAnimation(remoteHandAnimation);
        rightHand.GetComponentInChildren<PlayerHandAnimation>().SetHandsAnimation(remoteHandAnimation);
    }

    void SetRemoteCharacterVRIKReferences()
    {
        vRIK = character.GetComponent<VRIK>();
        
        vRIK.solver.spine.headTarget = head;
        vRIK.solver.leftArm.target = GetOffsetTransform(leftHand);
        vRIK.solver.rightArm.target = GetOffsetTransform(rightHand);
    }

    Transform GetOffsetTransform(Transform _transform)
    {
        Transform offset = null;

        foreach (Transform t in _transform)
        {
            if (t.tag == "HandOffset")
                offset = t;
        }

        return offset;
    }
}