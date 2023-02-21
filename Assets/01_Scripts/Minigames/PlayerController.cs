using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Minigames
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] List<GameObject> _playerPlacement = new List<GameObject>();
        [SerializeField] List<GameObject> _gasClouds;
        [SerializeField] float _sprayLenght = 1f;

        int _currentPosition = 0;

        public List<GameObject> GasClouds { get => _gasClouds; set => _gasClouds = value; }
        public int CurrentPosition { get => _currentPosition; set => _currentPosition = value; }

        public delegate void PlayerAction();
        public static event PlayerAction Shoot;

        private void Start()
        {
            foreach (var position in _playerPlacement)
            {
                position.gameObject.SetActive(false);
            }

            _playerPlacement[CurrentPosition].gameObject.SetActive(true);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                CheckLeftPress();
            }

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                CheckRightPress();
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                CheckSprayPress();
            }
        }

        private void CheckLeftPress()
        {
            if (CurrentPosition > 0)
            {
                _playerPlacement[CurrentPosition].gameObject.SetActive(false);
                CurrentPosition--;
                _playerPlacement[CurrentPosition].gameObject.SetActive(true);
            }
        }

        private void CheckRightPress()
        {
            if (CurrentPosition < _playerPlacement.Count - 1)
            {
                _playerPlacement[CurrentPosition].gameObject.SetActive(false);
                CurrentPosition++;
                _playerPlacement[CurrentPosition].gameObject.SetActive(true);
            }
        }

        private void CheckSprayPress()
        {
            for (int i = 0; i < GasClouds.Count; i++)
            {
                if (i == CurrentPosition)
                {
                    StartCoroutine(ShootGas(i));
                }
            }
        }

        private IEnumerator ShootGas(int id)
        {
            Shoot();
            StartCoroutine(MoveHands(id));
            GasClouds[id].gameObject.SetActive(true);
            yield return new WaitForSecondsRealtime(_sprayLenght);
            GasClouds[id].gameObject.SetActive(false);
        }

        private IEnumerator MoveHands(int id)
        {
            _playerPlacement[id].transform.GetChild(1).GetChild(1).gameObject.SetActive(true);
            _playerPlacement[id].transform.GetChild(1).GetChild(2).gameObject.SetActive(false);
            yield return new WaitForSecondsRealtime(0.5f);
            _playerPlacement[id].transform.GetChild(1).GetChild(1).gameObject.SetActive(false);
            _playerPlacement[id].transform.GetChild(1).GetChild(2).gameObject.SetActive(true);
        }
    }
}
