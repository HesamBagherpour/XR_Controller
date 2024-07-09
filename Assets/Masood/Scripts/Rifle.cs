using UnityEngine;

public class Rifle : Gun
{
    [SerializeField] private float _durationBetweenShoot = .2f;
    [SerializeField] private bool _readyToShoot;
    private float _lastShootTime;

    public override void DoAction()
    {
        //Debug.Log("Rifle Do Action");
        if (!_readyToShoot)
            return;
        if (!_currentMagazine.HasBullet())
        {
            Debug.Log("Rifle magazine is empty");
            return;
        }
        Shoot();
        _readyToShoot = false;
        _lastShootTime = Time.time;
    }

    protected override void Initialize()
    {
        GunType = GunType.Rifle;
        _gunController.AddGunReactionsToTrigger(DoAction);
    }

    private void Update()
    {
        //if (Fire.IsPressed())
        //{
        //    DoAction();
        //}

        if (Time.time > _lastShootTime + _durationBetweenShoot)
            _readyToShoot = true;
    }
}

