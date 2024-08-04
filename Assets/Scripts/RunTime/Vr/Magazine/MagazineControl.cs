using UnityEngine;

public class MagazineControl : MonoBehaviour
{
   [SerializeField] private int bullets;
   [SerializeField] private GameObject colider;
   [SerializeField] private BulletScriptableObject bulletRefrence;

    //[Serializable]
    //public class BulletData
    //{
    //    public float Range;
    //    public float Damage;
    //}

    //[SerializeField] private float _bulletAmount;

    void Start()
    {

#if UnlimitedAmmo
        bullets = 9999999;
#endif

    }

    public void OnSelectEnter()
    {
        ColiderSetActive(false);
    }

    public void OnSelectExit()
    {
        ColiderSetActive(true);
    }

    void ColiderSetActive(bool value)
    {
        if(colider != null)
            colider.SetActive(value);
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
        Debug.Log("No  bullet in clip");
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