using System.Runtime.CompilerServices;
using AS.Ekbatan_Showdown.Xr_Wrapper.RunTime.Gun;
using UnityEngine;
using UnityEngine.InputSystem;

namespace AS.Ekbatan_Showdown.Xr_Wrapper.RunTime.Magazine
{
    public class OnGunClipController : MonoBehaviour
    {
        [SerializeField] BoltControl boltControl;
        [SerializeField] GameObject clipPrefab;
        [SerializeField] GameObject clipObjectOnGun;
        [SerializeField] Clip clipOnGun;
        bool isthereAnyClipInGun = false;

        void OnTriggerStay(Collider other)
        {
            if(other.transform.tag == "Clip" && isthereAnyClipInGun == false)
            {
                ChangeMagazineState(true);
                clipObjectOnGun.SetActive(true);
                clipOnGun.SetBullet(other.GetComponent<Clip>().GetBulletsLeft());
                other.GetComponent<ClipController>().destroy();

                //Adds Magazine To Gun
            }
        }

        public void TakeMagazine()
        {
            if(isthereAnyClipInGun == true)
            {
                ChangeMagazineState(false);
                clipObjectOnGun.SetActive(false);
                Instantiate(clipPrefab, clipOnGun.transform);
                boltControl.Pull(false);

                //Take Magazine From Gun
            }
        }

        void ChangeMagazineState(bool value)
        {
            isthereAnyClipInGun = value;
        }

        public bool IsthereAnyClipInGun()
        {
            return isthereAnyClipInGun;
        }
    }
}