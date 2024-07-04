using UnityEngine;

namespace AS.Ekbatan_Showdown.Xr_Wrapper
{
    public class Clip : MonoBehaviour
    {
        int bullets;

        void Start()
        {
            bullets = 30;
        }

        public void DecreaseBullet()
        {
            if(bullets > 0)
                bullets --;
        }

        public int GetBulletsLeft()
        {
            return bullets;
        }

        public void SetBullet(int value)
        {
            bullets = value;
        }
    }
}