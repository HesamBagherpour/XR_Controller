namespace AS.Ekbatan_Showdown.Xr_Wrapper.RunTime.Gun.GunStates
{
    interface IGunState 
    {
        public void init(GunController _gunController, HandsOnGunControl _handOnGun);
        public void Enter();

        public virtual void TriggerStay(float value, TriggerControl triggerControl) { }
        public virtual void TriggerCancel(TriggerControl triggerControl) { }
        public virtual void Exit() { }
    }
}