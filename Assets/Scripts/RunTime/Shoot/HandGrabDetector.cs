using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class HandGrabDetector : MonoBehaviour
{
    private XRGrabInteractable grabInteractable;
    private XRSimpleInteractable simpleInteractable;
    [SerializeField] private GameObject leftHand;
    [SerializeField] private GameObject RightHand;

    void Awake()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.AddListener(OnGrab);
            grabInteractable.selectExited.AddListener(OnRelease);

        }

        simpleInteractable = GetComponent<XRSimpleInteractable>();
        if (simpleInteractable != null)
        {
            simpleInteractable.selectEntered.AddListener(OnGrab);
            simpleInteractable.selectExited.AddListener(OnRelease);
        }

    }

    void OnDestroy()
    {
        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.RemoveListener(OnGrab);
            grabInteractable.selectExited.RemoveListener(OnRelease);
        }

        if (simpleInteractable != null)
        {
            simpleInteractable.selectEntered.RemoveListener(OnGrab);
            simpleInteractable.selectExited.RemoveListener(OnRelease);
        }
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        XRBaseInteractor interactor = args.interactor;

        if (interactor.name.Contains("Left"))
            leftHand.SetActive(true);
        else if (interactor.name.Contains("Right"))
            RightHand.SetActive(true);
    }

    private void OnRelease(SelectExitEventArgs args)
    {
        XRBaseInteractor interactor = args.interactor;
        
        if (interactor.name.Contains("Left"))
            leftHand.SetActive(false);
        else if (interactor.name.Contains("Right"))
            RightHand.SetActive(false);
    }
}
