using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public void OnDrop()
    {
        transform.GetChild(0).AddComponent<Rigidbody>();
    }
}