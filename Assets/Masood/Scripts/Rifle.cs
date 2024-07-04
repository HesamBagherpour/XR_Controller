using UnityEngine;

public class Rifle : Gun
{
    [SerializeField] private float _durationBetweenShoot = .2f;
    [SerializeField] private bool _readyToShoot;
    private float _lastShootTime;

    public override void DoAction()
    {
        if (!_readyToShoot)
            return;
        Shoot();
        _readyToShoot = false;
        _lastShootTime = Time.time;
    }

    protected override void Initialize()
    {
        GunType = GunType.Rifle;
    }

    private void Update()
    {
        //if (Fire.ReadValue<float>() > 0)
        //    DoAction();
        if (Fire.IsPressed())
        {
            //Debug.Log("IsPressed");
            DoAction();
        }

            if (Time.time > _lastShootTime + _durationBetweenShoot)
            _readyToShoot = true;
    }
}

