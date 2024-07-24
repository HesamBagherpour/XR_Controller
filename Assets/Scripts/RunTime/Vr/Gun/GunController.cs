using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Transformers;

public class GunController : MonoBehaviour
{
    [SerializeField] HandsOnGunControl handOnGun;
    [SerializeField] TriggerControl triggerControlRight;
    [SerializeField] TriggerControl triggerControlLeft;

    [SerializeField] XRGrabInteractable xRGrabInteractable;
    [SerializeField] XRGeneralGrabTransformer grabTransformer;

    List<GameObject> firstAttachColliders = new List<GameObject>();
    [SerializeField] Collider secondAttachColider;
    [SerializeField] Collider boltColider;

    [SerializeField] Transform secondAttachPoint;

    IGunState gunState;
    Idle idle = new Idle();
    OneHandGrab oneHandGrab = new OneHandGrab();
    TwoHandGrab twoHandGrab = new TwoHandGrab();

    PlayerHand firstSelectingHand;

    void Start()
    {
        xRGrabInteractable.firstSelectEntered.AddListener(OnFirstSelectEntered);
        xRGrabInteractable.selectEntered.AddListener(OnSelectEntered);
        xRGrabInteractable.selectExited.AddListener(OnSelectExited);

        GetFirstAttachColiders();
        MoveToState(idle);           
    }

    public void AddGunReactionsToTrigger(Action shoot)
    {
        triggerControlLeft.OnTrigger = shoot;
        triggerControlRight.OnTrigger = shoot;
    }

    void OnDestroy()
    {
        xRGrabInteractable.firstSelectEntered.RemoveListener(OnFirstSelectEntered);
        xRGrabInteractable.selectEntered.RemoveListener(OnSelectEntered);
        xRGrabInteractable.selectExited.RemoveListener(OnSelectExited);
    }

    void GetFirstAttachColiders()
    {
        foreach (var colider in xRGrabInteractable.colliders)
            if(colider != secondAttachColider && colider != boltColider)
                firstAttachColliders.Add(colider.gameObject);
    }

    void MoveToState(IGunState state)
    {
        if(gunState != null)
            gunState.Exit();

        gunState = state;
        gunState.init(this, handOnGun);
        gunState.Enter();
    }

    List<IXRSelectInteractor> GetSelectingInteractors() { return xRGrabInteractable.interactorsSelecting; }
    IXRSelectInteractor GetFirstSelectingInteractor() { return xRGrabInteractable.firstInteractorSelecting; }
    XRInteractionManager GetInteractionManager() { return xRGrabInteractable.interactionManager; }

    void ChangeSelection()
    {
        switch (GetSelectingInteractors().Count)
        {
            case 0:
                MoveToState(idle);
                break;
            case 1:
                MoveToState(oneHandGrab);
                break;
            case 2:
                MoveToState(twoHandGrab);
                break;
        }
    }

    public bool IsInTwoHandGrab()
    {
        return gunState == twoHandGrab;
    }

    public void SetTwoHandRotationMode(XRGeneralGrabTransformer.TwoHandedRotationMode rotationMode)
    {
        grabTransformer.allowTwoHandedRotation = rotationMode;
    }

    public void SetSecondaryAttachTransform(Transform transform)
    {
        xRGrabInteractable.secondaryAttachTransform = transform;
    }
    public void SetDefaultSecondaryAttachTransform()
    {
        xRGrabInteractable.secondaryAttachTransform = secondAttachPoint;
    }

    public void TriggerStay(float value, PlayerHand hand)
    {
        if(firstSelectingHand == hand)
            gunState.TriggerStay(value, GetFirstActiveHand());
    }
    public void TriggerCancel(PlayerHand hand)
    {
        if(firstSelectingHand == hand)
            gunState.TriggerCancel(GetFirstActiveHand());
    }

    public void ChangeShootingMode(PlayerHand hand, ChangeModeDirection direction)
    {
        if(firstSelectingHand == hand)
            GetFirstActiveHand().ChangeShootMode(direction);
    }

    internal TriggerControl GetFirstActiveHand()
    {
        if(triggerControlRight.gameObject.activeSelf)
            return triggerControlRight;
        else if(triggerControlLeft.gameObject.activeSelf)
            return triggerControlLeft;
        else return null;
    }

    internal void FirstAttachColidersSetActive(bool value)
    {    
        foreach (var colider in firstAttachColliders)
            if (colider.activeSelf != value)
                colider.SetActive(value);
    }
    internal void SecondAttachColiderSetActive(bool value)
    {
        if (secondAttachColider.gameObject.activeSelf != value)
            secondAttachColider.gameObject.SetActive(value);
    }
    internal void BoltColiderSetActive(bool value)
    {
        if (boltColider.enabled != value)
            boltColider.enabled = value;
    }

    internal PlayerHand GetFirstSelectedHand()
    {
        return firstSelectingHand;
    }

    void OnFirstSelectEntered(SelectEnterEventArgs eventArgs)
    {
        var playerHandController = GetFirstSelectingInteractor().transform.GetComponent<PlayerHandController>();
        firstSelectingHand = playerHandController.Hand;
    }

    void OnSelectEntered(SelectEnterEventArgs eventArgs)
    {
        ChangeSelection();
    }

    void OnSelectExited(SelectExitEventArgs eventArgs)
    {
        CancelOnFirstSelectExited(eventArgs);
        ChangeSelection();
    }

    void CancelOnFirstSelectExited(SelectExitEventArgs args)
    {
        if(!GetFirstSelectingInteractor().hasSelection)
            GetInteractionManager().CancelInteractableSelection(args.interactableObject);
    }
}