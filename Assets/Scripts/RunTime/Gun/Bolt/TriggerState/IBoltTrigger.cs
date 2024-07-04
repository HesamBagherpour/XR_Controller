using AS.Ekbatan_Showdown.Xr_Wrapper.Gun.Bolt.GripState;
using AS.Ekbatan_Showdown.Xr_Wrapper.Gun.HandsOnGunControl;

namespace AS.Ekbatan_Showdown.Xr_Wrapper.Gun.Bolt.TriggerState
{
    interface IBoltState
    {
        virtual void ChangeGripState(GripBase gripBase) { }
        void OnTrigger(BoltControl boltControl, HandOnGunControl handsOnGun);
    }
}