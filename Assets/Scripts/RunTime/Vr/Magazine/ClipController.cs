using UnityEngine;

public class ClipController : MonoBehaviour
{
    public void destroy()
    {
        Destroy(transform.parent.gameObject);
    }
}