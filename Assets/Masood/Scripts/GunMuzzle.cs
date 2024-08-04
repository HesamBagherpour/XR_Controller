using System.Collections;
using UnityEngine;

public class GunMuzzle : MonoBehaviour
{
    [SerializeField] private GameObject _muzzleObject;
    [SerializeField] private float _destroyTime;

    private Gun _gun;

    private void Awake()
    {
        _gun = GetComponent<Gun>();
    }

    private void OnEnable()
    {
        _gun.onShoot += onShootHandle;
    }

    private void OnDisable()
    {
        _gun.onShoot -= onShootHandle;

    }

    private void onShootHandle(bool onShoot)
    {
        if (!onShoot)
            return;

        _muzzleObject.SetActive(true);
        StartCoroutine(DisableMuzzle());
    }


    private IEnumerator DisableMuzzle()
    {
        yield return new WaitForSeconds(_destroyTime);
        _muzzleObject.SetActive(false);
    }
}
