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
    protected InputAction Fire;

    [SerializeField] private GameObject _shooptStartPosition;
    [SerializeField] private Magazine _currentMagazine;

    protected abstract void Initialize();
    public abstract void DoAction();


    private void Awake()
    {
        PlayerControls = new PlayerInputActions();
    }
    protected void Start()
    {
        Initialize();
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
            return;
        }

        RaycastHit hit;
        if (Physics.Raycast(_shooptStartPosition.transform.position,
             transform.forward, out hit, currentBullet.Range,
             ValidLayers, QueryTriggerInteraction.Ignore))
            OnRaycastHit(hit, currentBullet.Damage);
    }

    private void OnRaycastHit(RaycastHit hit, float damage)
    {
        Debug.Log(hit.collider.gameObject.name);
        var damageable = hit.collider.GetComponent<Idamageable>();
        if (damageable != null)
            damageable.ReceiveDamage(damage);
    }
}




