using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class MagazineControl : MonoBehaviour
{
   [SerializeField] private int bullets;
   [SerializeField] private GameObject colider;
   [SerializeField] private BulletScriptableObject bulletRefrence;
   [SerializeField] private Grabbable grabInteractable;

    //[Serializable]
    //public class BulletData
    //{
    //    public float Range;
    //    public float Damage;
    //}

    //[SerializeField] private float _bulletAmount;

    void Start()
    {
        grabInteractable.selectEntered.AddListener(SelectEntered);
        grabInteractable.selectExited.AddListener(SelectExited);

#if UnlimitedAmmo
        bullets = 9999999;
#endif

    }

    private void OnDestroy()
    {
        grabInteractable.selectEntered.RemoveListener(SelectEntered);
        grabInteractable.selectExited.AddListener(SelectExited);
    }

    void SelectEntered(SelectEnterEventArgs args)
    {
        grabInteractable.isActive = true;
    }
    void SelectExited(SelectExitEventArgs args)
    {
        StartCoroutine(DelayToDeselect());
    }

    IEnumerator DelayToDeselect()
    {
        yield return new WaitForSeconds(0.1f);

        if(! grabInteractable.isSelected)
            grabInteractable.isActive = false;
    }

    public void OnEnteredGun()
    {
        ColiderSetActive(false);
    }

    public void OnExitedGun()
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