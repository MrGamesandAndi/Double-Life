using System.Collections;
using UnityEngine;

namespace ParkMinigame
{
    public class WormController : MonoBehaviour
    {
        [SerializeField] MinigameManager _minigameManager;
        [SerializeField] Transform _positions;
        [SerializeField] Transform _dead;
        [SerializeField] Transform _flower;
        [SerializeField] float _showTime = 0.5f;
        [SerializeField] int _currentPlacement = 0;

        public delegate void WormAction();
        public static event WormAction Move;
        public static event WormAction InFlower;

        public MinigameManager MinigameManager { get => _minigameManager; set => _minigameManager = value; }
        public int CurrentPlacement { get => _currentPlacement; set => _currentPlacement = value; }

        private void Start()
        {
            StartCoroutine(SpawnWorm());
        }

        private IEnumerator SpawnWorm()
        {
            foreach (Transform worm in _positions)
            {
                if (CurrentPlacement == _positions.childCount - 1)
                {
                    if (MinigameManager.InFlower(this))
                    {
                        StartCoroutine(WormInFlower());
                    }
                }
                else
                {
                    if (Move != null)
                    {
                        Move();
                    }

                    worm.gameObject.SetActive(true);
                    yield return new WaitForSeconds(_showTime);

                    while (MinigameManager.Pause)
                    {
                        yield return null;
                    }

                    worm.gameObject.SetActive(false);
                    CurrentPlacement++;
                }
            }
        }

        private IEnumerator WormInFlower()
        {
            if (InFlower != null)
            {
                InFlower();
            }

            _positions.GetChild(_positions.childCount - 1).gameObject.SetActive(true);
            MinigameManager.Pause = true;
            MinigameManager.Miss();
            yield return new WaitForSecondsRealtime(3.0f);
            MinigameManager.Pause = false;
            Destroy(_positions.parent.gameObject);
        }
    }
}
