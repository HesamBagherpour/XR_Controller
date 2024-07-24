using System;
using UnityEngine;

namespace AS.Ekbatan_Showdown.Xr_Wrapper.RunTime.Gun
{
    public enum ShootingMode
    {
        safety,
        semi,
        burst,
        fullAuto,
    }

    public enum ChangeModeDirection
    {
        up = 1,
        down = -1,
    }

    public class ShootingModeControl : MonoBehaviour
    {
        Animator animator;
        ShootingMode mode;

        public Action<ShootingMode> OnShootingModeChange;
    
        void Start()
        {
            animator = GetComponent<Animator>();   
            mode = ShootingMode.semi; 
        }

        public void ChangeMode(ChangeModeDirection direction)
        {
            var modeId = (int)mode + (int)direction;

            if(modeId <= 3 && modeId >= 0)
            {
                mode = (ShootingMode)modeId;
                PlayChangingModeAnimation(mode.ToString());
                OnShootingModeChange?.Invoke(mode);
            }
        }

        void PlayChangingModeAnimation(string animation)
        {
            animator.CrossFade(animation, 1f);
        }

        public ShootingMode GetMode()
        {
            return mode;
        }
    }
}