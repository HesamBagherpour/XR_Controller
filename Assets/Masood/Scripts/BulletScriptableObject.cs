using UnityEngine;

[CreateAssetMenu(fileName = "Bullet", menuName = "ScriptableObjects/Bullet", order = 1)]
public class BulletScriptableObject : ScriptableObject
{
    public GameObject BulletPrefab;
    public float Range;
    public float MaxBulletAmount;
    public float Damage;
    public GunType GunType;
}