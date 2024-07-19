﻿using UnityEngine;
using static Gun;

public class PhysicsTarget : TargetBoard, Idamageable
{
    private Rigidbody _rigidbody;
    //[SerializeField] private float _force;

    //public override void ReceiveDamage(RaycastHit hit, float damageAmount)
    //{
    //    base.ReceiveDamage(hit, damageAmount);
    //    if (_rigidbody == null)
    //    {
    //        _rigidbody = GetComponent<Rigidbody>();
    //    }
    //    _rigidbody.AddForceAtPosition(_force * hit.normal*-1, hit.transform.position);
    //}   
    public override void ReceiveDamage(HitData data)
    {
        base.ReceiveDamage(data);
        if (_rigidbody == null)
        {
            _rigidbody = GetComponent<Rigidbody>();
        }
        _rigidbody.AddForceAtPosition(data.HitForce * data.normal*-1, data.collide.transform.position);
    }
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        if (_rigidbody == null)
        {
            _rigidbody = GetComponent<Rigidbody>();
        }
    }

}