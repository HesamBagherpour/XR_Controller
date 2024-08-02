using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public enum MagazineType
{
    ak47mag,
    mp5mag,
    pistolmag,
}

public class MagazineReceiver : MonoBehaviour
{
    [Header("Receiver")]
    [SerializeField] MagazineType magazineType;
    [SerializeField] SocketTagChecker xRSocketInteractor;
    [SerializeField] Animator animator;

    MagazineControl magazine;

    public event Action<Transform> OnMagazineSelectEnter;
    public event Action OnMagazineSelectExit;

    void Start()
    {
        xRSocketInteractor.selectEntered.AddListener(MagazineSelectEnter);
        xRSocketInteractor.selectExited.AddListener(MagazineSelectExit);
        xRSocketInteractor.hoverExited.AddListener(MagazineHoverExit);
        xRSocketInteractor.Tag = magazineType.ToString();
    }

    void OnDestroy()
    {
        xRSocketInteractor.selectEntered.RemoveListener(MagazineSelectEnter);
        xRSocketInteractor.selectExited.RemoveListener(MagazineSelectExit);
        xRSocketInteractor.hoverExited.RemoveListener(MagazineHoverExit);
    }

    void MagazineSelectEnter(SelectEnterEventArgs args)
    {
        magazine = args.interactableObject.transform.GetComponent<MagazineControl>();
        magazine.OnSelectEnter();
        OnMagazineSelectEnter?.Invoke(args.interactableObject.transform);
    }

    void MagazineSelectExit(SelectExitEventArgs args)
    {
        magazine.OnSelectExit();
        magazine = null;
        OnMagazineSelectExit?.Invoke();
    }

    void MagazineHoverExit(HoverExitEventArgs args)
    {
        xRSocketInteractor.allowSelect = true;
    }

    public void ForceRelease()
    {
        xRSocketInteractor.allowSelect = false;
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