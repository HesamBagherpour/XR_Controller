using UnityEngine.XR.Interaction.Toolkit;

public class Grabbable : XRGrabInteractable
{
    public bool isHoverable = true;
    public bool isActive = false;

    public override bool IsHoverableBy(IXRHoverInteractor interactor)
    {
        if(interactor.transform.tag == "interactor")
            isHoverable = true;

        return base.IsHoverableBy(interactor) && isActive;
    }

    public override bool IsSelectableBy(IXRSelectInteractor interactor)
    {
        if(interactor.transform.tag == "interactor")
            isActive = true;

        return base.IsSelectableBy(interactor) && isActive;
    }
}