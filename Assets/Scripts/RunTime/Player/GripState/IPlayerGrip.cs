using AS.Ekbatan_Showdown.Xr_Wrapper.Gun.Bolt;
using UnityEngine;

namespace AS.Ekbatan_Showdown.Xr_Wrapper.Player.GripState
{
    interface IPlayerGrip
    {
        void init(PlayerController playerController, BoltControl colidedObject);
        void Enter();
    }
}