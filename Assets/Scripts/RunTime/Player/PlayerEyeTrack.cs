using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.UI;
using AS.Ekbatan_Showdown.Xr_Wrapper.RunTime.Player;

public class PlayerEyeTrack : MonoBehaviour
{
    [Header("Head Gaze Interactor")]
    [SerializeField] XRGazeInteractor gazeInteractor;

    [Header("Hand Controllers")]
    [SerializeField] PlayerHandController leftDirectController;
    [SerializeField] PlayerHandController rightDirectController;

    [Header("Ray Interactor")]
    [SerializeField] GameObject leftRayInteractor;
    [SerializeField] GameObject righRayInteractor;

    void Start()
    {
        gazeInteractor.uiHoverEntered.AddListener(OnUiHoverEntered);
        gazeInteractor.uiHoverExited.AddListener(OnUiHoverExited);
    }

    void OnDestroy()
    {
        gazeInteractor.uiHoverEntered.RemoveListener(OnUiHoverEntered);
        gazeInteractor.uiHoverExited.RemoveListener(OnUiHoverExited);
    }

    void OnUiHoverEntered(UIHoverEventArgs hover)
    {
        ControllerSetActive(rightDirectController, righRayInteractor);
        ControllerSetActive(leftDirectController, leftRayInteractor);
    }

    void OnUiHoverExited(UIHoverEventArgs hover)
    {
        if(IsAnyUiHovered() == false)
        {
            ControllerSetDeActive(righRayInteractor);
            ControllerSetDeActive(leftRayInteractor);
        }
    }

    bool IsAnyUiHovered()
    {
        return gazeInteractor.IsOverUIGameObject();
    }

    void ControllerSetActive(PlayerHandController handController, GameObject rayInteractor)
    {
        if (handController.InteractorHasSelection() == false)
            if(rayInteractor.activeSelf != true)
                rayInteractor.SetActive(true);
    }

    void ControllerSetDeActive(GameObject rayInteractor)
    {
        if(rayInteractor.activeSelf != false)
            rayInteractor.SetActive(false);
    }
}