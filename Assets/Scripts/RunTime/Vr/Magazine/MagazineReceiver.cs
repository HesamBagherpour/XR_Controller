using System;
using System.Collections;
using Unity.VisualScripting;
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
    [SerializeField] float corssFadeTime = 0.5f;

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

    void Init(Transform _magazine)
    {
        magazine = _magazine.GetComponent<MagazineControl>();
    }

    void MagazineSelectEnter(SelectEnterEventArgs args)
    {
        Init(args.interactableObject.transform);
        magazine.OnSelectEnter();
        OnMagazineSelectEnter?.Invoke(args.interactableObject.transform);

        if(magazineType == MagazineType.pistolmag)
            PlayAnimation("magreceive");
    }

    void MagazineSelectExit(SelectExitEventArgs args)
    {
        magazine.OnSelectExit();
        magazine = null;
        OnMagazineSelectExit?.Invoke();
    }

    void MagazineHoverExit(HoverExitEventArgs args)
    {
        AllowSelect(true);
    }

    public void ForceRelease()
    {
        if (magazineType == MagazineType.pistolmag)
            PlayAnimation("magrelease");
        else
            AllowSelect(false);
    }

    void AnimationEvent()
    {
        AllowSelect(false);
        StartCoroutine(AllowSelectCouroutine());
    }

    IEnumerator AllowSelectCouroutine()
    {
        yield return new WaitForSeconds(0.7f);
        AllowSelect(true);
    }

    void AllowSelect(bool value)
    {
        xRSocketInteractor.allowSelect = value;
    }

    void PlayAnimation(string animation)
    {
        animator.CrossFade(animation, corssFadeTime);
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