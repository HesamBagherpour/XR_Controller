using System;
using System.Collections.Generic;
using RunTime.Shoot.Enums;
using UnityEngine;

namespace RunTime.Shoot
{
    public class MagazineStateNotificationController : MonoBehaviour
    {
        [SerializeField] private MagazineStateNotificationView _view;
        [SerializeField] private List<MagazineStateNotificationData> _notificationData;
        
        public void UpdateMagazineStateNotification(CantDropState state)
        {        
            _view.SetNotificationText(_notificationData.Find(x=>x.state==state).NotificationText);
        }
    }

    [Serializable]
    public class MagazineStateNotificationData
    {
        public CantDropState state;
        public string NotificationText;
    }
}