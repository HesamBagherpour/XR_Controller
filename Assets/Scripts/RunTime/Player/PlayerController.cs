using System.Collections.Generic;
using AS.Ekbatan_Showdown.Xr_Wrapper.Gun.Bolt;
using AS.Ekbatan_Showdown.Xr_Wrapper.Player.GrabState;
using AS.Ekbatan_Showdown.Xr_Wrapper.Player.GripState;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace AS.Ekbatan_Showdown.Xr_Wrapper.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] XRDirectInteractor rightInteractor;
        [SerializeField] XRDirectInteractor leftInteractor;

        List<IXRSelectInteractable> grabbedObjects = new List<IXRSelectInteractable>();

        BoltControl colidedObject;
        GameObject colidedHandGameObject;
        PlayerHand colidedHand;

        IPlayerGrab grabState;
        PlayerGrabIdle idle = new PlayerGrabIdle();
        PlayerGrabOneHand oneHandGrab = new PlayerGrabOneHand();
        PlayerGrabTwoHand twoHandGrab = new PlayerGrabTwoHand();

        IPlayerGrip gripState;
        internal PlayerGripTake gripTake = new PlayerGripTake();
        internal PlayerGripStay gripStay = new PlayerGripStay();
        internal PlayerGripRelease gripRelease = new PlayerGripRelease();

        void Start()
        {
            ChangeGrabState();
        }

        void init(IXRSelectInteractable _grabbedObject)
        {
            grabbedObjects.Add(_grabbedObject);
        }

        int GetNumberOfSelectingInteractor()
        { 
            int sum = 0;
            if(rightInteractor.hasSelection) sum ++;
            if(leftInteractor.hasSelection) sum ++;
            return sum;
        }

        void ChangeGrabState()
        {
            switch (GetNumberOfSelectingInteractor())
            {
                case 0:
                    grabState = idle;
                    break;
                case 1:
                    grabState = oneHandGrab;
                    break;
                case 2:
                    grabState = twoHandGrab;
                    break;
            }
            grabState.Init(this);
            grabState.Enter();
        }

        void SetColidedObject(BoltControl _colidedObject)
        {
            colidedObject = _colidedObject;
        }

        internal void ChangeGripState(IPlayerGrip state)
        {
            gripState = state;
            gripState.init(this,colidedObject);
        }

        internal void Take()
        {
            grabState.Take(colidedObject);
        }
        internal void Release()
        {
            grabState.Release(colidedObject);
        }

        internal void FirstSelectEntered(IXRSelectInteractable grabbedObject, PlayerHand playerHand)
        {
            init(grabbedObject);
            ChangeGrabState();
        }
        internal void SelectEntered()
        {
            ChangeGrabState();
        }
        internal void SelectExited(IXRSelectInteractable grabbedObject)
        {
            if(grabbedObjects.Contains(grabbedObject) && !grabbedObject.firstInteractorSelecting.hasSelection)
                grabbedObjects.Remove(grabbedObject);

            ChangeGrabState();
        }
        internal void HandPosition(float changedPosition)
        {
            //if(gripState != null)
        }

        internal void TriggerStay(Collider other, GameObject handObject, PlayerHand hand)
        {
            if(gripState != gripStay && colidedObject == null)
            {
                colidedHand = hand;
                colidedHandGameObject = handObject;
                SetColidedObject(other.transform.GetComponent<BoltControl>());
            }
            colidedObject.DeactiveSecondAttackColliderOnTriggerEnter();
        }
        internal void TriggerExit()
        {
            colidedObject.ActiveSecondAttackColliderOnTriggerExit();
            
            if(gripState == gripRelease && colidedObject != null)
                SetColidedObject(null);
        }
        internal PlayerHand GetColidedHand()
        {
            return colidedHand;
        }

        internal void HandActivation(bool value)
        {
            if(colidedHandGameObject != null)
                colidedHandGameObject.SetActive(value);
        }
    }
}