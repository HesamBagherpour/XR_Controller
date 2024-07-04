using UnityEngine;

public class Pistol : Gun
{
    //[SerializeField] private float _durationBetweenShoot = .2f;
    [SerializeField] private bool _readyToShoot;
    //private float _lastShootTime;

    public override void DoAction()
    {
        //_lastShootTime = Time.time;
        //if (!_readyToShoot)
        //    return;
        //Shoot();
        //_readyToShoot = false;
    }

    protected override void Initialize()
    {
        GunType = GunType.Pistol;
        Fire.performed += Fire_performed;//getkeydown
    }

    private void Fire_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        //Debug.Log("Performed");
        Shoot();
    }

    //private void Update()
    //{
    //    //if (Input.GetKey(KeyCode.A))
    //    if (Fire.ReadValue<float>()>0)
    //    DoAction();


    //    //if (Fire2.IsPressed())//getkey
    //    //    Debug.Log("IsPressed");

    //    if (Fire2.WasPressedThisFrame())
    //        Debug.Log("WasPressedThisFrame");
    //    //riflevalue = Rifle.ReadValue<float>();
    //    if (Time.time > _lastShootTime + _durationBetweenShoot)
    //        _readyToShoot = true;
    //}
}

