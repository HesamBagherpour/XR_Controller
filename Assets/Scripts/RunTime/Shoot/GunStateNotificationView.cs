using System.Collections;
using TMPro;
using UnityEngine;

public class GunStateNotificationView:MonoBehaviour
{
    [SerializeField] private TMP_Text NotificationText;
    [SerializeField] private float HideDelay;
    [SerializeField] private float MaxHideDelay=8;
    public void SetNotificationText(string text)
    {
        NotificationText.text = text;
        HideDelay = MaxHideDelay;
        gameObject.SetActive(true);
    }

    private void Update()
    {
        HideDelay-=Time.deltaTime;
        if (HideDelay < 0 )
        {
            gameObject.SetActive(false);
        }
    }

}