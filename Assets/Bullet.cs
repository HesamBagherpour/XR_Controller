using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] Rigidbody rigidbody;

    void Awake()
    {
        rigidbody = transform.GetChild(0).GetComponent<Rigidbody>();
    }

    public void OnDrop()
    {
        rigidbody.isKinematic = false;
        //Time.timeScale = 0.05f;
        rigidbody.AddForce(transform.GetChild(0).forward, ForceMode.VelocityChange);
    }
}