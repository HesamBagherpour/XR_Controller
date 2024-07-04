using AS.Ekbatan_Showdown.Xr_Wrapper.Gun.HandsOnGunControl;
using UnityEngine;

namespace AS.Ekbatan_Showdown.Xr_Wrapper.Gun.GunState
{
    interface IGunState 
    {
        public void init(GunController _gunController, HandOnGunControl _handOnGun);
        public void Enter();
        
        public virtual void TriggerEnter(Collider other) { }
        public virtual void TriggerExit(Collider other) { }
    }
}