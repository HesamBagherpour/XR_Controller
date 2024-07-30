using System;
using UnityEngine;

public enum ShootingMode
{
    safety,
    semi,
    burst,
    fullAuto,
}

public enum ModeType
{
    pistol = 1,
    rifle = 3,
}

public enum ChangeModeDirection
{
    up = 1,
    down = -1,
}

public class ShootingModeControl : MonoBehaviour
{
    [SerializeField] ModeType type;
    
    Animator animator;
    ShootingMode mode;
    int allowedModes;

    public Action<ShootingMode> OnShootingModeChange;
    
    void Start()
    {
        animator = GetComponent<Animator>();   
        mode = ShootingMode.semi;
        allowedModes = (int)type;
    }

    public void ChangeMode(ChangeModeDirection direction)
    {
        var modeId = (int)mode + (int)direction;

        if(modeId <= allowedModes && modeId >= 0)
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