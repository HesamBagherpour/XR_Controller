using ArioSoren.TutorialKit;
//using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;

public class TutorialEventHandler : HighlightBehavior
{
    private bool _isShow=false;
    //[SerializeField] private ShootingModeControl _shootingModeControl;
    [SerializeField] private GameObject _player;
    [SerializeField] private GunController _gunController;
    [SerializeField] private float _nearGunDistance;
    [SerializeField] private Gun Gun;
    [SerializeField] private MagazineControl _magazineControl;


    public UnityEvent OnStart;
    public UnityEvent OnGrabGun;
    public UnityEvent OnDropGun;
    public UnityEvent OnGetNearGun;
    public UnityEvent OnGetFarGun;
    public UnityEvent OnMagazinePickup;
    public UnityEvent OnMagazineDrop;
    public UnityEvent OnMagazineEnter;
    public UnityEvent OnMagazineEject;
    public UnityEvent OnStartMovement;
    public UnityEvent OnStartrotate;
    public UnityEvent Onbolt;
    public UnityEvent OnShootingModeChange;
    public UnityEvent OnShoot;
    public UnityEvent OnEnd;


    private bool IsGunReadyToShoot;
    private bool _nearGun = false;
    private float _gunDistance;
    private float alphaInc = 0.05f;
    private float CurrentValue;
    private bool _allowFadeMaterial;
    private Material HandsMaterial;
    private playerMovementData _playerMovementData;
    private playerRotationData _playerRotationData;


    public override void Hide()
    {
        //throw new System.NotImplementedException();
        _isShow = false;
        if (OnMagazineEnter.GetPersistentEventCount() > 0)
            Gun.OnMagazineEneterd -= magazineEneterd;

        if (OnMagazineEject.GetPersistentEventCount() > 0)
            Gun.OnMagazineEjected -= magazineEjected;

        if (Onbolt.GetPersistentEventCount() > 0)
            Gun.Onbolted -= bolted;

        if (OnShootingModeChange.GetPersistentEventCount() > 0)
        {
            Debug.Log("waiting for TutorialEventHandler OnShootingModeChange");
            Gun.OnShootingModeChanged -= ShootingmodeChanged;
        }

        if (OnShoot.GetPersistentEventCount() > 0)
            Gun.onShoot -= Shooted;

        if (_magazineControl != null)
        {
            _magazineControl.OnMagazinePickup -= MagazinePickedUp;
            _magazineControl.OnMagazinedrop -= MagazineDropped;
            //Debug.Log("Magazine Pickedup");
        }
        OnEnd?.Invoke();
    }

    public override void Show()
    {
        //throw new System.NotImplementedException();
        _isShow = true;
        //_shootingModeControl.OnShootingModeChange = (mode) => { _shootingMode = mode; };
        if (OnMagazineEnter.GetPersistentEventCount() > 0)
            Gun.OnMagazineEneterd += magazineEneterd;

        if (OnMagazineEject.GetPersistentEventCount() > 0)
            Gun.OnMagazineEjected += magazineEjected;

        if (Onbolt.GetPersistentEventCount() > 0)
            Gun.Onbolted += bolted;

        if (OnShootingModeChange.GetPersistentEventCount() > 0)
        {
            Debug.Log("waiting for TutorialEventHandler OnShootingModeChange");
            Gun.OnShootingModeChanged += ShootingmodeChanged;
        }

        if (OnShoot.GetPersistentEventCount() > 0)
            Gun.onShoot += Shooted;

        OnStart?.Invoke();

        if (_magazineControl != null)
        {
            _magazineControl.OnMagazinePickup += MagazinePickedUp;
            _magazineControl.OnMagazinedrop += MagazineDropped;
            //Debug.Log("Magazine Pickedup");
        }

    }


    private void magazineEneterd()
    {
        Debug.Log("magazineEneterd");
        OnMagazineEnter.Invoke();
    }  
    private void magazineEjected()
    {
        Debug.Log("magazineEjected");
        OnMagazineEject.Invoke();
    }

    private void bolted()
    {
        Debug.Log("bolted");
        Onbolt.Invoke();
    }

    private void ShootingmodeChanged(ShootingMode mode)
    {
        Debug.Log("ShootingmodeChanged");
        OnShootingModeChange.Invoke();
    }

    private void Shooted(bool shoot)
    {
        Debug.Log("Shooted");
        OnShoot.Invoke();
    }

    private void MagazinePickedUp()
    {
        Debug.Log("MagazinePickedUp");
        OnMagazinePickup?.Invoke();
    } 
    
    private void MagazineDropped()
    {
        Debug.Log("MagazineDropped");
        OnMagazineDrop?.Invoke();
    }
    // Start is called before the first frame update
    void Start()
    {
        _playerMovementData = new playerMovementData()
        {
            InitialPosition = _player.transform.position,
            deltaDistance = 0.3f,
            IsEventCalled = false
        };
        _playerRotationData = new playerRotationData()
        {
            InitialRotation = _player.transform.rotation,
            deltaDistance = 20,
            IsEventCalled = false
        };

    }

    // Update is called once per frame
    void Update()
    {
        if (!_isShow)
            return;

        if (_gunController != null)
        {
            if (IsGunReadyToShoot != _gunController.IsGunReadyToShoot())
            {
                IsGunReadyToShoot = _gunController.IsGunReadyToShoot();
                if (IsGunReadyToShoot)
                    OnGrabGun.Invoke();
                else
                    OnDropGun.Invoke();
            }

            _gunDistance = Vector3.Distance(_player.transform.position, _gunController.gameObject.transform.position);
            if (_nearGun != (_gunDistance < _nearGunDistance))
            {
                _nearGun = (_gunDistance < _nearGunDistance);
                if (_nearGun)
                    OnGetNearGun.Invoke();
                else
                    OnGetFarGun.Invoke();
            }
        }


        if (_allowFadeMaterial && HandsMaterial != null)
            FadeMaterialAnimation(HandsMaterial);

        if ((OnStartMovement.GetPersistentEventCount() > 0) && (!_playerMovementData.IsEventCalled))
        {
            float distance = Vector3.Distance(_player.transform.position,
                _playerMovementData.InitialPosition);

//            Debug.Log("distance=" + distance);
            if (distance > _playerMovementData.deltaDistance)
            {
                OnStartMovement?.Invoke();
                _playerMovementData.IsEventCalled = true;
            }
        }   
        
        if ((OnStartrotate.GetPersistentEventCount() > 0) && (!_playerRotationData.IsEventCalled))
        {
            float distance = Quaternion.Angle(_player.transform.rotation,
                _playerRotationData.InitialRotation);
            //Debug.Log("distance rotation=" + distance);
            if (distance > _playerRotationData.deltaDistance)
            {
                OnStartrotate?.Invoke();
                _playerRotationData.IsEventCalled = true;
            }
        }
    }



    public void StartFadeMaterial(Material material)
    {
        _allowFadeMaterial = true;
        HandsMaterial = material;
    }

    public void StopFadeMaterial(Material material)
    {
        _allowFadeMaterial = false;
        HandsMaterial = null;
    }

    public void FadeMaterialAnimation(Material mat)
    {
        Color baseColor = mat.GetColor("_BaseColor");
        CurrentValue += alphaInc;
        baseColor.a = CurrentValue;

        mat.SetColor("_BaseColor", baseColor);

        if (CurrentValue > 1)
            alphaInc *= -1;
        if (CurrentValue < 0)
            alphaInc *= -1;
    }


}

public struct playerMovementData
{
    public Vector3 InitialPosition;
    public float deltaDistance;
    public bool IsEventCalled;
}
public struct playerRotationData
{
    public quaternion InitialRotation;
    public float deltaDistance;
    public bool IsEventCalled;
}
