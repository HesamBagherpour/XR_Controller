using AS.Ekbatan_Showdown.Xr_Wrapper.Gun.Bolt.GripState;
using AS.Ekbatan_Showdown.Xr_Wrapper.Gun.HandsOnGunControl;

namespace AS.Ekbatan_Showdown.Xr_Wrapper.Gun.Bolt.TriggerState
{
    public class BoltTriggerEnter : IBoltState
    {
        GripBase gripBase;

        void IBoltState.ChangeGripState(GripBase gripState) 
        {
            gripBase = gripState;
        }

        public void OnTrigger(BoltControl boltControl, HandOnGunControl handOnGun)
        {
            if(gripBase != null)
                gripBase.OnEnter(boltControl, handOnGun);
        }
    }
}