using AS.Ekbatan_Showdown.Xr_Wrapper.Gun.Bolt.GripState;
using AS.Ekbatan_Showdown.Xr_Wrapper.Gun.Bolt.TriggerState;
using AS.Ekbatan_Showdown.Xr_Wrapper.Gun.HandsOnGunControl;
using AS.Ekbatan_Showdown.Xr_Wrapper.Player;
using UnityEngine;

namespace AS.Ekbatan_Showdown.Xr_Wrapper.Gun.Bolt
{
    public class BoltControl : MonoBehaviour
    {
        [SerializeField] GunController gunController;
        [SerializeField] HandOnGunControl handsOnGunControl;

        PlayerHand colidedHand;

        IBoltState boltState;
        BoltTriggerIdle triggerIdle = new BoltTriggerIdle();
        BoltTriggerEnter triggerEnter = new BoltTriggerEnter();
        BoltTriggerExit triggerExit = new BoltTriggerExit();

        void Start()
        {
            MoveToIdle();
        }

        void MoveTriggerToState(IBoltState state)
        {
            boltState = state;
            boltState.OnTrigger(this, handsOnGunControl);
        }

        internal void TriggerEnter(PlayerHand _hand)
        {
            colidedHand = _hand;
            MoveTriggerToState(triggerEnter);
        }
        internal void TriggerExit(PlayerHand _hand)
        {
            colidedHand = _hand;
            MoveTriggerToState(triggerExit);
        }

        internal void OnGripStart()
        {
            boltState.ChangeGripState(new GripStart());
        }
        internal void OnGripRelease()
        {
            boltState.ChangeGripState(new GripRelease());
        }

        internal void MoveToIdle()
        {
            MoveTriggerToState(triggerIdle);
        }
        internal PlayerHand GetColidedHand()
        {
            return colidedHand;
        }

        internal void SetHandActive(PlayerHand _hand)
        {
            handsOnGunControl.ActivateBoltHand(_hand);
        }
        internal void SetHandDeactive(PlayerHand _hand)
        {
            handsOnGunControl.DeactivateBoltHand(_hand);
        }

        internal void DeactiveSecondAttackColliderOnTriggerEnter()
        {
            gunController.SecondAttachColiderSetActive(false);
        }
        internal void ActiveSecondAttackColliderOnTriggerExit()
        {
            gunController.SecondAttachColiderSetActive(true);
        }
    }
}