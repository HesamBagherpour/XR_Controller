using UnityEngine.XR.Interaction.Toolkit;

public class Grabbable : XRGrabInteractable
{
    public bool isActive = false;

    public override bool IsHoverableBy(IXRHoverInteractor interactor)
    {
        return base.IsHoverableBy(interactor);
    }

    public override bool IsSelectableBy(IXRSelectInteractor interactor)
    {
        if(interactor.transform.tag == "interactor")
            isActive = true;

        return base.IsSelectableBy(interactor) && isActive;
    }
}