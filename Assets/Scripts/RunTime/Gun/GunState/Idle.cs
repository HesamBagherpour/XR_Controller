using AS.Ekbatan_Showdown.Xr_Wrapper.Gun.HandsOnGunControl;

namespace AS.Ekbatan_Showdown.Xr_Wrapper.Gun.GunState
{
    public class Idle : IGunState
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
            gunController.FirstAttachColidersSetActive(true);
            gunController.SecondAttachColiderSetActive(true);
            handOnGunControl.SetToNoGrab();
        }
    }
}