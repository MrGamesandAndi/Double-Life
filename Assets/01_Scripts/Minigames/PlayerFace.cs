using General;
using UnityEngine;

namespace ParkMinigame
{
    public class PlayerFace : MonoBehaviour
    {
        [SerializeField] SpriteRenderer _skintone;
        [SerializeField] SpriteRenderer _lEye;
        [SerializeField] SpriteRenderer _REye;
        [SerializeField] SpriteRenderer _mouth;

        private void Start()
        {
            _skintone.color = PlayerController.Instance.RandomDouble.Skintone;
            _lEye.color= PlayerController.Instance.RandomDouble.EyeColor;
            _REye.color = PlayerController.Instance.RandomDouble.EyeColor;
            _mouth.sprite = BodyPartsCollection.Instance.ReturnMouthSprite(PlayerController.Instance.RandomDouble.MouthKey);

        }
    }
}

