using AS.Ekbatan_Showdown.Xr_Wrapper.Gun.Bolt.TriggerState;
using AS.Ekbatan_Showdown.Xr_Wrapper.Gun.HandsOnGunControl;

namespace AS.Ekbatan_Showdown.Xr_Wrapper.Gun.Bolt.GripState
{
    public abstract class GripBase : BoltTriggerEnter
    {
        public abstract void OnEnter(BoltControl boltControl, HandOnGunControl handOnGun);
    }
}