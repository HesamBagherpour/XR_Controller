using UnityEngine;

public interface Idamageable
{
    void ReceiveDamage(RaycastHit hit,float damageAmount);
}