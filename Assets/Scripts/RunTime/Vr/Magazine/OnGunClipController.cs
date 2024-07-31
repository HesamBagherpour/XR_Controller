using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public enum MagazineType
{
    ak47mag,
    mp5mag,
}

public class OnGunClipController : MonoBehaviour
{
    [SerializeField] BoltControl boltControl;
    [SerializeField] Clip clipOnGun;

    [Header("Receiver")]
    [SerializeField] MagazineType magazineType;
    [SerializeField] SocketTagChecker xRSocketInteractor;
    [SerializeField] Animator animator;
    [SerializeField] Collider _colider;

    bool isthereAnyClipInGun = false;

    public event Action<Transform> OnMagazineSelectEnter;
    public event Action OnMagazineSelectExit;

    void Start()
    {
        xRSocketInteractor.selectEntered.AddListener(MagazineSelectEnter);
        xRSocketInteractor.selectExited.AddListener(MagazineSelectExit);
        xRSocketInteractor.Tag = magazineType.ToString();
    }

    void MagazineSelectEnter(SelectEnterEventArgs args)
    {
        OnMagazineSelectEnter?.Invoke(args.interactableObject.transform);
        Debug.Log(args.interactableObject.transform.tag);
    }

    void MagazineSelectExit(SelectExitEventArgs args)
    {
        OnMagazineSelectExit?.Invoke();
    }

    void OnGunUngrabbed()
    {
        ColiderSetActive(false);
    }

    void OnGunGrabbed()
    {
        ColiderSetActive(true);
    }

    void ColiderSetActive(bool value)
    {
        _colider.enabled = value;
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

    void ChangeMagazineState(bool value)
    {
        isthereAnyClipInGun = value;
    }

    public bool IsthereAnyClipInGun()
    {
        return isthereAnyClipInGun;
    }
}