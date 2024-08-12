using System.Collections.Generic;
using ArioSoren.TutorialKit;
using UnityEngine;

public class AnimatedArrow : HighlightBehavior
{
    [SerializeField] private List<GameObject> arrowObject;
    public override void Show()
    {
        foreach (var o in arrowObject)
        {
            o.SetActive(true);
        }
         
    }

    public override void Hide()
    {
        foreach (var o in arrowObject)
        {
            o.SetActive(false);
        }
    }



}