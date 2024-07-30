using System;
using UnityEngine;

public class Clip : MonoBehaviour
{
   [SerializeField] private int bullets;
    //[Serializable]
    //public class BulletData
    //{
    //    public float Range;
    //    public float Damage;
    //}

    //[SerializeField] private float _bulletAmount;
    [SerializeField] private BulletScriptableObject bulletRefrence;
    void Start()
    {
        //bullets = 30;
#if UnlimitedAmmo
        bullets = 9999999;
#endif
    }

    public void DecreaseBullet()
    {
        if(bullets > 0)
            bullets --;
    }

    public int GetBulletsLeft()
    {
        return bullets;
    }

    public void SetBullet(int value)
    {
        bullets = value;
    }


    public BulletScriptableObject GetBullet()
    {
        if (bullets > 0)
        {
            bullets--;
            return bulletRefrence;
        }
        return null;
    }

    public bool HasBullet()
    {
        return bullets > 0;
    }

    public int GetBulletAmount()
    {
        return bullets;
    }
}