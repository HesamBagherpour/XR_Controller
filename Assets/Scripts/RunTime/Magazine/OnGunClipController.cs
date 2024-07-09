using UnityEngine;
using UnityEngine.InputSystem;

namespace AS.Ekbatan_Showdown.Xr_Wrapper.RunTime.Magazine
{
    public class OnGunClipController : MonoBehaviour
    {
        [SerializeField] InputActionReference SelectInputAction;
        [SerializeField] GameObject clipPrefab;
        [SerializeField] GameObject clipObjectOnGun;
        [SerializeField] Clip clipOnGun;
        bool isthereAnyClipInGun = false;

        void Start()
        {
            SelectInputAction.action.started += OnReleaseClip;
        }

        void OnDestroy()
        {
            SelectInputAction.action.started -= OnReleaseClip;
        }

        void OnReleaseClip(InputAction.CallbackContext callback)
        {
            isthereAnyClipInGun = false;
            clipObjectOnGun.SetActive(false);
        }

        void OnTriggerStay(Collider other)
        {
            if(other.transform.tag == "Clip")
                if(isthereAnyClipInGun == false)
                {
                    isthereAnyClipInGun = true;
                    clipObjectOnGun.SetActive(true);
                    clipOnGun.SetBullet(other.GetComponent<Clip>().GetBulletsLeft());
                    other.GetComponent<ClipController>().destroy();
                }
        }

        public void ReleaseClip()
        {
            Instantiate(clipPrefab, clipOnGun.transform);
        }
    }
}