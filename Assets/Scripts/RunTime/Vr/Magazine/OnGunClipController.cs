using UnityEngine;

public class OnGunClipController : MonoBehaviour
{
    [SerializeField] BoltControl boltControl;
    [SerializeField] Clip clipOnGun;

    [Header("Clip")]
    [SerializeField] GameObject clipPrefab;
    [SerializeField] GameObject clipObjectOnGun;

    [Header("Receiver Animator")]
    [SerializeField] Animator receiverAnimator;

    bool isthereAnyClipInGun = false;

    void OnTriggerStay(Collider other)
    {
        if(other.transform.tag == "Clip" && isthereAnyClipInGun == false)
        {
            ChangeMagazineState(true);
            clipObjectOnGun.SetActive(true);
            receiverAnimator.CrossFade("NewMp5Recieve", 1);
            //clipOnGun.SetBullet(other.GetComponent<Clip>().GetBulletsLeft());
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