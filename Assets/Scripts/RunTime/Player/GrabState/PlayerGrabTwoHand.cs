namespace AS.Ekbatan_Showdown.Xr_Wrapper.Player.GrabState
{
    public class PlayerGrabTwoHand : IPlayerGrab
    {
        PlayerController playerController;

        public void Init(PlayerController _playerController)
        {
            playerController = _playerController;
        }

        public void Enter()
        {

        }
    }
}