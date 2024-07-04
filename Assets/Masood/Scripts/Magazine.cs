using System;
using UnityEngine;

public class Magazine : MonoBehaviour
{
    [Serializable]
    public class BulletData
    {
        public float Range;
        public float Damage;
    }

    [SerializeField] private float _bulletAmount;
    [SerializeField] private BulletScriptableObject bulletRefrence;


    private void Start()
    {
        _bulletAmount = bulletRefrence.MaxBulletAmount;
    }

    public BulletScriptableObject GetBullet()
    {
        if (_bulletAmount > 0)
        {
            _bulletAmount--;
            return bulletRefrence;
        }
        return null;
    }
}
