using AS.Ekbatan_Showdown.Xr_Wrapper.Gun.Bolt;

namespace AS.Ekbatan_Showdown.Xr_Wrapper.Player.GrabState
{
    interface IPlayerGrab
    {
        public void Init(PlayerController playerController);
        public void Enter();
        public virtual void Take(BoltControl colidedObject) { }
        public virtual void Release(BoltControl colidedObject) { }
    }
}