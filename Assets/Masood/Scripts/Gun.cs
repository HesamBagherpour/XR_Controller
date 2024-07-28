using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum GunType
{
    None, Pistol, Rifle
}

public abstract class Gun : MonoBehaviour
{
    public PlayerInputActions PlayerControls;
    public LayerMask ValidLayers;
    public GunType GunType;
    public Action<bool> onShoot;
    public BulletScriptableObject CurrentBullet;//bullet  in gun

    protected InputAction Fire;

    [SerializeField] private GameObject _shooptStartPosition;
    [SerializeField] protected Magazine _currentMagazine;
    [SerializeField] protected GunController _gunController;
    [SerializeField] protected ShootingMode _shootingMode;

    [SerializeField] private ShootingModeControl _shootingModeControl;



    protected abstract void Initialize();
    public abstract void DoAction();

    private void Awake()
    {
        PlayerControls = new PlayerInputActions();
    }
    protected void Start()
    {
        Initialize();
        ImpactHandler.Initialize();
        _shootingModeControl.OnShootingModeChange = (mode) => { _shootingMode = mode; };
    }

    private void OnEnable()
    {
        Fire = PlayerControls.Player.Fire;
        Fire.Enable();

    }

    private void OnDisable()
    {
        Fire.Disable();
    }

    protected void Shoot()
    {
        CurrentBullet = _currentMagazine.GetBullet();
        if (CurrentBullet == null)
        {
            onShoot?.Invoke(false);
            return;
        }

        RaycastHit hit;
        if (Physics.Raycast(_shooptStartPosition.transform.position, //+ UnityEngine.Random.onUnitSphere * 0.1f,
             transform.forward, out hit, CurrentBullet.MaxDistance,
             ValidLayers, QueryTriggerInteraction.Ignore))
        {//OnRaycastHit(hit, currentBullet.Damage,currentBullet.PhysicForceOnHit);

            var hitData = new HitData()
            {
                collide = hit.collider.gameObject,
                DamageAmount = CurrentBullet.Damage,
                HitForce = CurrentBullet.PhysicForceOnHit,
                HitPoint = hit.point,
                normal = hit.normal,
                DistanceFactor = GetDistanceFactor(_shooptStartPosition.transform.position, hit.collider.gameObject.transform.position)
            };
            OnRaycastHit(hitData);
         }
        onShoot?.Invoke(true);
    }

    private float GetDistanceFactor(Vector3 startPoint, Vector3 endPoint)
    {
        Vector3.Distance(startPoint, endPoint);

        return 0;           
    }

    public int GetBulletCountInMagazine()
    {
        _currentMagazine.cou
    }

    //protected virtual void OnRaycastHit(RaycastHit hit, float damage)
    //{
    //    Debug.Log(hit.collider.gameObject.name);
    //    var damageable = hit.collider.GetComponent<Idamageable>();
    //    if (damageable != null)
    //        damageable.ReceiveDamage(hit, damage);
    //} 
    protected virtual void OnRaycastHit(HitData data)
    {
        Debug.Log(data.collide.name);
        var damageable = data.collide.GetComponent<Idamageable>();
        if (damageable != null)
            damageable.ReceiveDamage(data);
    }


    public class HitData
    {
        public Vector3 normal;
        public float DamageAmount;
        //public Vector3 HitPosition;
        public Vector3 HitPoint;
        public float HitForce;
        public float DistanceFactor;
        public GameObject collide;
    }
}