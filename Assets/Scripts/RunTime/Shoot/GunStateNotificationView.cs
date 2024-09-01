using TMPro;
using UnityEngine;

public class GunStateNotificationView:MonoBehaviour
{
    [SerializeField] private TMP_Text NotificationText;
    public void SetNotificationText(string text)
    {
        NotificationText.text = text;
    }

}