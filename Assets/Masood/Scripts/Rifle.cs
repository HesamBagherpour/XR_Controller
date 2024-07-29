using UnityEngine;

public class Rifle : Gun
{
    [SerializeField] private float _durationBetweenShoot = .2f;
    [SerializeField] private bool _readyToShoot;
    private float _lastShootTime;

    public override void DoAction()
    {
        if (!_readyToShoot || _shootingMode == ShootingMode.safety)
            return;


        if (!_currentMagazine.HasBullet())
        {
            Debug.Log("Rifle magazine is empty");
            return;
        }
        Shoot();
        brustshotingCount++;
        _readyToShoot = false;
        _lastShootTime = Time.time;
    }

    protected override void Initialize()
    {
        GunType = GunType.Rifle;
        _gunController.AddGunReactionsToTrigger(DoAction);
        Fire.performed += Fire_performed;

    }


    private int brustshotingCount = 0;
    private void Update()
    {
        //if (Fire.IsPressed())
        //{
        //    DoAction();
        //}
        if (_shootingMode == ShootingMode.fullAuto)
            if (Time.time > _lastShootTime + _durationBetweenShoot)
                _readyToShoot = true;

        if (_shootingMode == ShootingMode.burst)
        {
            if (Time.time > _lastShootTime + _durationBetweenShoot && brustshotingCount<3)
            {
                _readyToShoot = true;
            }
            if (!Fire.IsPressed())
                brustshotingCount = 0;
        }
    }
    private void Fire_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (_shootingMode == ShootingMode.semi)
            DoAction();
    }
}

