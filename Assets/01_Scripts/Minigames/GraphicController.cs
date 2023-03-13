using System.Collections;
using UnityEngine;

namespace ParkMinigame
{
    public class GraphicController : MonoBehaviour
    {
        [SerializeField] Transform _missGraphics;
        [SerializeField] Transform _deadFlowerGraphics;
        [SerializeField] Transform _aliveFlowerGraphics;

        float _animationDelay = 0.5f;

        public void Miss(bool active)
        {
            _missGraphics.gameObject.SetActive(active);
        }

        public IEnumerator FlowerDeath()
        {
            for (int i = 0; i < _deadFlowerGraphics.childCount; i++)
            {
                _aliveFlowerGraphics.gameObject.SetActive(false);
                _deadFlowerGraphics.gameObject.SetActive(true);
                yield return new WaitForSecondsRealtime(_animationDelay);
                _aliveFlowerGraphics.gameObject.SetActive(true);
                _deadFlowerGraphics.gameObject.SetActive(false);
            }
        }
    }
}
