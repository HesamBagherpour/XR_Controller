using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Grabbable : XRGrabInteractable
{
    public bool isHoverable = true;
    public bool isSelectable = false;
    public bool isBoltTriggred = false;
    bool bolt = false;

    public override bool IsHoverableBy(IXRHoverInteractor interactor)
    {
        if(interactor.transform.tag == "interactor")
            isHoverable = true;

        return base.IsHoverableBy(interactor) && isSelectable && ! bolt;
    }

    public override bool IsSelectableBy(IXRSelectInteractor interactor)
    {
        if(interactor.transform.tag == "interactor")
        {
            bolt = isBoltTriggred;
            isSelectable = true;
        }

        if(interactor.transform.tag != "interactor")
            bolt = false;

        return base.IsSelectableBy(interactor) && isSelectable && ! bolt;
    }
}