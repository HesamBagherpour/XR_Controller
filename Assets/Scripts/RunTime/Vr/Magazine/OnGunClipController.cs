using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public enum MagazineType
{
    ak47mag,
    mp5mag,
    pistolmag,
}

public class OnGunClipController : MonoBehaviour
{
    [Header("Receiver")]
    [SerializeField] MagazineType magazineType;
    [SerializeField] SocketTagChecker xRSocketInteractor;
    [SerializeField] Animator animator;

    Clip magazine;

    public event Action<Transform> OnMagazineSelectEnter;
    public event Action OnMagazineSelectExit;

    void Start()
    {
        xRSocketInteractor.selectEntered.AddListener(MagazineSelectEnter);
        xRSocketInteractor.selectExited.AddListener(MagazineSelectExit);
        xRSocketInteractor.Tag = magazineType.ToString();
    }

    void OnDestroy()
    {
        xRSocketInteractor.selectEntered.RemoveListener(MagazineSelectEnter);
        xRSocketInteractor.selectExited.RemoveListener(MagazineSelectExit);
    }

    void MagazineSelectEnter(SelectEnterEventArgs args)
    {
        magazine = args.interactableObject.transform.GetComponent<Clip>();
        magazine.OnSelectEnter();
        OnMagazineSelectEnter?.Invoke(args.interactableObject.transform);
    }

    void MagazineSelectExit(SelectExitEventArgs args)
    {
        magazine.OnSelectExit();
        OnMagazineSelectExit?.Invoke();
    }

    /*void OnTriggerStay(Collider other)
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
    }*/

    /*public void TakeMagazine()
    {
        if(isthereAnyClipInGun == true)
        {
            ChangeMagazineState(false);
            clipObjectOnGun.SetActive(false);
            Instantiate(clipPrefab, clipOnGun.transform);
            boltControl.Pull(false);

            //Take Magazine From Gun
        }
    }*/
}