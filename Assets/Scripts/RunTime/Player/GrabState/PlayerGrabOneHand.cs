using AS.Ekbatan_Showdown.Xr_Wrapper.Gun.Bolt;
using UnityEngine;

namespace AS.Ekbatan_Showdown.Xr_Wrapper.Player.GrabState
{
    public class PlayerGrabOneHand : IPlayerGrab
    {
        PlayerController playerController;

        public void Init(PlayerController _playerController)
        {
            playerController = _playerController;
        }

        public void Enter()
        {

        }

        public void Take(BoltControl colidedObject)
        {
            if(colidedObject != null)
            {
                playerController.ChangeGripState(playerController.gripTake);
                playerController.HandActivation(false);
                colidedObject.SetHandActive(playerController.GetColidedHand());
            }
        }

        public void Release(BoltControl colidedObject)
        {
            if(colidedObject != null)
            {
                playerController.ChangeGripState(playerController.gripRelease);
                playerController.HandActivation(true);
                colidedObject.SetHandDeactive(playerController.GetColidedHand());
            }
        }
    }
}