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


    protected InputAction Fire;

    [SerializeField] private GameObject _shooptStartPosition;
    [SerializeField] protected Magazine _currentMagazine;

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
        var currentBullet = _currentMagazine.GetBullet();
        if (currentBullet == null)
        {
            onShoot?.Invoke(false);
            return;
        }

        RaycastHit hit;
        if (Physics.Raycast(_shooptStartPosition.transform.position + UnityEngine.Random.onUnitSphere * 0.1f,
             transform.forward, out hit, currentBullet.Range,
             ValidLayers, QueryTriggerInteraction.Ignore))
            OnRaycastHit(hit, currentBullet.Damage);
        onShoot?.Invoke(true);
    }

    protected virtual void OnRaycastHit(RaycastHit hit, float damage)
    {
        Debug.Log(hit.collider.gameObject.name);
        var damageable = hit.collider.GetComponent<Idamageable>();
        if (damageable != null)
            damageable.ReceiveDamage(hit, damage);
    }



}




