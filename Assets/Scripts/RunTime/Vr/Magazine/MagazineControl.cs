using System;
using System.Collections;
using RunTime.Shoot;
using RunTime.Shoot.Enums;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.XR.Interaction.Toolkit;

public class MagazineControl : MonoBehaviour
{
   [SerializeField] private int bullets;
   [SerializeField] private GameObject colider;
   [SerializeField] public MagazineType magazineType;
   [SerializeField] private BulletScriptableObject bulletRefrence;
   [SerializeField] private Grabbable grabInteractable;
   [SerializeField] private MagazineStateNotificationController _magazineStateNotificationController;
   public bool allowDropMagazine;

    public Action OnMagazinePickup;
    public Action OnMagazinedrop;

    //[Serializable]
    //public class BulletData
    //{
    //    public float Range;
    //    public float Damage;
    //}

    //[SerializeField] private float _bulletAmount;

    void Start()
    {
        //grabInteractable.selectEntered.RemoveAllListeners();
        //grabInteractable.selectExited.RemoveAllListeners();
        grabInteractable.selectEntered.AddListener(SelectEntered);
        //if (OnMagazinePickup!=null)
        //grabInteractable.selectEntered.AddListener((_)=>OnMagazinePickup?.Invoke());
        grabInteractable.selectExited.AddListener(SelectExited);
        //if (OnMagazinedrop != null)
        //    grabInteractable.selectEntered.AddListener((_) => OnMagazinedrop?.Invoke());

#if UnlimitedAmmo
        bullets = 9999999;
#endif

    }

    private void OnDestroy()
    {
        //grabInteractable.selectEntered.RemoveListener(SelectEntered);
        grabInteractable.selectEntered.RemoveAllListeners();
        //grabInteractable.selectExited.RemoveListener(SelectExited);
        grabInteractable.selectExited.RemoveAllListeners();
    }

    void SelectEntered(SelectEnterEventArgs args)
    {
        SetGrabActive(true);
        if (OnMagazinePickup != null)
            OnMagazinePickup?.Invoke();
    }
    void SelectExited(SelectExitEventArgs args)
    {
        //CheckMagazineState();
        
        StartCoroutine(DelayToDeselect());
        if (OnMagazinedrop != null)
             OnMagazinedrop?.Invoke();
    }
    public void SetMagazineState(CantDropState state)
    {
        if (_magazineStateNotificationController == null)
            _magazineStateNotificationController = FindObjectOfType<MagazineStateNotificationController>();

        if (_magazineStateNotificationController == null)
            return;

        _magazineStateNotificationController.UpdateMagazineStateNotification(state);
    }
    public void CheckMagazineState()
    {
        SetMagazineState(CantDropState.Lock);
    }
    public void SetAllowMagazineDrop(bool allow)
    {
        allowDropMagazine = allow;
    }
    IEnumerator DelayToDeselect()
    {
        yield return new WaitForSeconds(0.1f);

        if(! grabInteractable.isSelected)
            SetGrabActive(false);
    }

    public void SetGrabActive(bool value)
    {
        grabInteractable.isSelectable = value;
    }

    public void AllowInteractOnBoltTriggered(bool value)
    {
        if(magazineType != MagazineType.pistolmag)
            grabInteractable.isBoltTriggred = !value;
    }

    public void AllowInteractOnGunStateChange(bool value)
    {
        if(magazineType != MagazineType.pistolmag)
        {
            Debug.Log("Magazine Control Is Gun Grabbed: " + value);
            grabInteractable.allowGrab = value;
        }
    }

    public void OnEnteredGun()
    {
        if(magazineType == MagazineType.pistolmag)
            ColiderSetActive(false);
    }

    public void OnExitedGun()
    {
        if(magazineType == MagazineType.pistolmag)
            ColiderSetActive(true);
    }

    void ColiderSetActive(bool value)
    {
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