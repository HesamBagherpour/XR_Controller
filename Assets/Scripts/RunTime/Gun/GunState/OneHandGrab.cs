using AS.Ekbatan_Showdown.Xr_Wrapper.Gun.HandsOnGunControl;
using AS.Ekbatan_Showdown.Xr_Wrapper.Player;
using UnityEngine;

namespace AS.Ekbatan_Showdown.Xr_Wrapper.Gun.GunState
{
    public class OneHandGrab : IGunState
    {
        GunController gunController;
        HandOnGunControl handOnGunControl;

        public void init(GunController _gunController, HandOnGunControl _handOnGun)
        {
            gunController = _gunController;
            handOnGunControl = _handOnGun;
        }

        public void Enter()
        {
            gunController.FirstAttachColidersSetActive(false);
            gunController.SecondAttachColiderSetActive(true);
            handOnGunControl.SetToSingleGrab(gunController.GetFirstSelectedHand());
        }

        void IGunState.TriggerEnter(Collider other)
        {
            var properHand = GetProperHand(gunController.GetFirstSelectedHand());
            handOnGunControl.ActivateBoltHand(properHand);
        }

        void IGunState.TriggerExit(Collider other)
        {
            var properHand = GetProperHand(gunController.GetFirstSelectedHand());
            handOnGunControl.DeactivateBoltHand(properHand);
        }

        PlayerHand GetProperHand(PlayerHand hand)
        {
            if(hand == PlayerHand.Left)
                return PlayerHand.Right;
            else
                return PlayerHand.Left;
        }
    }
}