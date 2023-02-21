using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Minigames
{
    public class MinigameManager : MonoBehaviour
    {
        [SerializeField] PointController _pointController;
        [SerializeField] GraphicController _graphicController;
        [SerializeField] GameObject _descriptionPanel;

        [SerializeField] GameObject _wormsLeft;
        [SerializeField] GameObject _wormsRight;
        [SerializeField] GameObject _wormsInPlay;
        [SerializeField] PlayerController _player;
        [SerializeField] Transform _livesTransform;
        [SerializeField] Transform _deadGraphic;
        [SerializeField] float _spawnTime = 1.5f;
        [SerializeField] int _lives;
        [SerializeField] bool _pause = false;

        delegate void SpawnWorms();

        List<GameObject> _worms;
        bool _gameIsRunning;
        int _points;

        public bool Pause { get => _pause; set => _pause = value; }
        public GraphicController GraphicController { get => _graphicController; set => _graphicController = value; }

        private void Start()
        {
            //GameStart();
            StartCoroutine(NewGame());
        }

        private IEnumerator NewGame()
        {
            if (_gameIsRunning)
            {
                StopAllCoroutines();

                foreach (Transform child in _wormsInPlay.transform)
                {
                    Destroy(child.gameObject);
                }
            }

            if (_worms != null)
            {
                _worms.Clear();
            }

            _worms = new List<GameObject>
            {
                _wormsLeft,
                _wormsRight
            };

            _gameIsRunning = true;
            Pause = false;
            _points = 0;
            _lives = 3;
            GraphicController.Miss(false);
            ShowLivesLeft();
            UpdatePoints();
            yield return new WaitForSeconds(0.5f);
        }

        private void StopGame()
        {
            _gameIsRunning = false;

            foreach (Transform child in _wormsInPlay.transform)
            {
                Destroy(child.gameObject);
            }

            _descriptionPanel.SetActive(true);
        }

        public void GameStart()
        {
            StartCoroutine(NewGame());
            StartCoroutine(SpawnRandomWorms());
        }

        private IEnumerator SpawnRandomWorms()
        {
            while (_gameIsRunning)
            {
                while (Pause)
                {
                    yield return null;
                }

                GameObject newWorm = Instantiate(_worms[Random.Range(0, _worms.Count)]);
                newWorm.GetComponentInChildren<WormController>().MinigameManager = this;
                newWorm.transform.SetParent(_wormsInPlay.transform, true);
                yield return new WaitForSeconds(_spawnTime);
            }
        }

        public bool InFlower(WormController worm)
        {
            foreach (var t in _player.GasClouds)
            {
                if (t.activeSelf)
                {
                    OnePoint();
                    return false;
                }
            }

            return true;
        }

        private void OnePoint()
        {
            _points++;
            UpdatePoints();
        }

        private void UpdatePoints()
        {
            _pointController.SetPoint(_points);
        }

        public void Miss()
        {
            GraphicController.Miss(true);
            _lives--;
            StartCoroutine(_graphicController.FlowerDeath());
            ShowLivesLeft();

            if (_lives == 0)
            {
                StopGame();
            }
        }

        private void ShowLivesLeft()
        {
            switch (_lives)
            {
                case 3:
                    {
                        _livesTransform.GetChild(0).gameObject.SetActive(false);
                        _livesTransform.GetChild(1).gameObject.SetActive(false);
                        _livesTransform.GetChild(2).gameObject.SetActive(false);
                        break;
                    }
                case 2:
                    {
                        _livesTransform.GetChild(0).gameObject.SetActive(true);
                        break;
                    }
                case 1:
                    {
                        _livesTransform.GetChild(1).gameObject.SetActive(true);
                        break;
                    }
                case 0:
                    {
                        _livesTransform.GetChild(2).gameObject.SetActive(true);
                        break;
                    }
                default:
                    break;
            }
        }
    }
}
