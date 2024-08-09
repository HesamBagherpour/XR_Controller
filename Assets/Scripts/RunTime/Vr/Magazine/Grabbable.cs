using UnityEngine.XR.Interaction.Toolkit;

public class Grabbable : XRGrabInteractable
{
    public bool isHoverable = true;
    public bool isSelectable = false;

    public bool isBoltTriggred = false;
    bool bolt = false;

    public bool allowGrab = true;
    bool _allowGrab = true;

    public override bool IsHoverableBy(IXRHoverInteractor interactor)
    {
        if(interactor.transform.tag == "interactor")
            isHoverable = true;

        return base.IsHoverableBy(interactor) && isSelectable && ! bolt && _allowGrab;
    }

    public override bool IsSelectableBy(IXRSelectInteractor interactor)
    {
        if(interactor.transform.tag == "interactor")
        {
            _allowGrab = allowGrab;
            bolt = isBoltTriggred;
            isSelectable = true;
        }

        if(interactor.transform.tag != "interactor")
        {
            bolt = false;
            _allowGrab = true;
        }

        return base.IsSelectableBy(interactor) && isSelectable && ! bolt && _allowGrab;
    }
}