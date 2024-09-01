using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;



public class GunStateNotificationController : MonoBehaviour
{
    [SerializeField] private GunStateNotificationView _view;
    [SerializeField] private List<GunStateNotificationData> _notificationData;

    void Start()
    {
            }

    public void UpdateGunStateNotification(CantShootState state)
    {
        _view.SetNotificationText(_notificationData.Find(x=>x.state==state).NotificationText);
    }


}

public enum CantShootState { None, Mode,NoMagazine,NoBullet,Forbiden}

[Serializable]
public class GunStateNotificationData
{
    public CantShootState state;
    public string NotificationText;
}
