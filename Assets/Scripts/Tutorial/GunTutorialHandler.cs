using ArioSoren.TutorialKit;
//using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GunTutorialHandler : HighlightBehavior
{
    private bool _isShow;
    //[SerializeField] private ShootingModeControl _shootingModeControl;
    [SerializeField] private GameObject _player;
    [SerializeField] private GunController _gunController;
    [SerializeField] private float _nearGunDistance;


    public UnityEvent OnGrabGun;
    public UnityEvent OnDropGun;
    public UnityEvent OnGetNearGun;
    public UnityEvent OnGetFarGun;
    public UnityEvent OnMagazineEnter;
    public UnityEvent OnMagazineEject;


    private bool IsGunReadyToShoot;
    private bool _nearGun = false;
    private float _gunDistance;
    private float alphaInc = 0.05f;
    private float CurrentValue;
    private bool _allowFadeMaterial;
    private Material HandsMaterial;


    public override void Hide()
    {
        //throw new System.NotImplementedException();
        _isShow = false;
    }

    public override void Show()
    {
        //throw new System.NotImplementedException();
        _isShow = true;
        //_shootingModeControl.OnShootingModeChange = (mode) => { _shootingMode = mode; };

    }

    // Start is called before the first frame update
    void Start()
    {

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
