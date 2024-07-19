using UnityEngine;
using static Gun;

public interface Idamageable
{
    //void ReceiveDamage(RaycastHit hit,float damageAmount);
    void ReceiveDamage(HitData data);
}