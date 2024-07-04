using AS.Ekbatan_Showdown.Xr_Wrapper.Gun.Bolt;
using UnityEngine;

namespace AS.Ekbatan_Showdown.Xr_Wrapper.Player.GripState
{
    public class PlayerGripTake : IPlayerGrip
    {
        PlayerController playerController;

        public void init(PlayerController _playerController, BoltControl colidedObject)
        {
            playerController = _playerController;
        }

        public void Enter()
        {
            //Set Hand Off
            MoveToStay();
        }

        void MoveToStay()
        {
            playerController.ChangeGripState(playerController.gripStay);
        }
    }
}