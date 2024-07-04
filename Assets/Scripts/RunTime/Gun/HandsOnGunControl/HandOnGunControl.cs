using AS.Ekbatan_Showdown.Xr_Wrapper.Player;
using UnityEngine;

namespace AS.Ekbatan_Showdown.Xr_Wrapper.Gun.HandsOnGunControl
{
    public class HandOnGunControl : MonoBehaviour
    {
        [SerializeField] GameObject firstHand_Left;
        [SerializeField] GameObject firstHand_Right;
        [SerializeField] GameObject secondHand_Left;
        [SerializeField] GameObject secondHand_Right;
        [SerializeField] GameObject boltHand_Left;
        [SerializeField] GameObject boltHand_Right;

        GameObject[] firstHands = new GameObject[2];
        GameObject[] secondHands = new GameObject[2];
        GameObject[] boltHands = new GameObject[2];

        void Awake()
        {
            firstHands[(int)PlayerHand.Left] = firstHand_Left;
            firstHands[(int)PlayerHand.Right] = firstHand_Right;
            secondHands[(int)PlayerHand.Left] = secondHand_Left;
            secondHands[(int)PlayerHand.Right] = secondHand_Right;
            boltHands[(int)PlayerHand.Left] = boltHand_Left;
            boltHands[(int)PlayerHand.Right] = boltHand_Right;
        }

        public void SetToNoGrab()
        {
            SetDeactive(firstHands);
            SetDeactive(secondHands);
            SetDeactive(boltHands);
        }

        public void SetToSingleGrab(PlayerHand grabHand)
        {
            SetActive(firstHands, grabHand);
            SetDeactive(secondHands);
        }

        public void SetToDoubleGrab(PlayerHand firstEnteredHand)
        {
            PlayerHand secondEnteredHand;
            if(firstEnteredHand == PlayerHand.Left)
                secondEnteredHand = PlayerHand.Right;
            else
                secondEnteredHand = PlayerHand.Left;

            SetActive(firstHands, firstEnteredHand);
            SetActive(secondHands, secondEnteredHand);
        }

        public void ActivateBoltHand(PlayerHand playerHand)
        {
            boltHands[(int)playerHand].SetActive(true);
        }

        public void DeactivateBoltHand(PlayerHand playerHand)
        {
            boltHands[(int)playerHand].SetActive(false);
        }

        void SetActive(GameObject[] hands, PlayerHand grabHand)
        {
            foreach (var hand in hands)
                if(hand == hands[(int)grabHand])
                    hand.SetActive(true);
                else
                    hand.SetActive(false);
        }

        void SetDeactive(GameObject[] hands)
        {
            foreach (var hand in hands)
                if(hand.activeSelf != false)
                    hand.SetActive(false);
        }
    }
}