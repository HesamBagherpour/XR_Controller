using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using RunTime.Shoot.Enums;
using UnityEngine;

public class GunStateNotificationController : MonoBehaviour
{
    [SerializeField] private GunStateNotificationView _view;
    [SerializeField] private List<GunStateNotificationData> _notificationData;
    

  public void UpdateGunStateNotification(CantShootState state)
  {        
        _view.SetNotificationText(_notificationData.Find(x=>x.state==state).NotificationText);
  }

}


[Serializable]
public class GunStateNotificationData
{
    public CantShootState state;
    public string NotificationText;
}
