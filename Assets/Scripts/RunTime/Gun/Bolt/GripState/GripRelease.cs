using AS.Ekbatan_Showdown.Xr_Wrapper.Gun.HandsOnGunControl;

namespace AS.Ekbatan_Showdown.Xr_Wrapper.Gun.Bolt.GripState
{
    public class GripRelease : GripBase
    {
        public override void OnEnter(BoltControl boltControl, HandOnGunControl handOnGun)
        {
            handOnGun.DeactivateBoltHand(boltControl.GetColidedHand());
        }
    }
}