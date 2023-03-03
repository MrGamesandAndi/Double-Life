using General;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace BattleMinigame
{
    public class BattleManager : MonoBehaviour
    {
        [SerializeField] GameObject _player;
        [SerializeField] GameObject _enemy;
        [SerializeField] Button _attackBtn;
        [SerializeField] Button _healBtn;
        [SerializeField] ParticleSystem _healParticles;
        [SerializeField] GameObject _winPanel;
        [SerializeField] GameObject _losePanel;
        [SerializeField] GameObject _battlePanel;
        [SerializeField] List<Sprite> _availableFoodEnemies;

        [SerializeField] float _playerHealth = 100f;
        [SerializeField] float _enemyHealth = 100f;
        [SerializeField] float _playerDamagePerHit = 10f;
        [SerializeField] float _enemyDamagePerHit = 10f;
        [SerializeField] float _amountPlayerHeal = 5f;
        [SerializeField] float _amountEnemyHeal = 5f;

        bool _isPlayerTurn = true;

        private void Start()
        {
            InitializePlayer();
            InitializeEnemy();
        }

        private void InitializePlayer()
        {
            GameManager.Instance.currentLoadedDouble = PopulationManager.Instance.GetRandomDouble();
            _player.SetActive(true);
            _player.GetComponent<Animator>().Play("Battle_Idle");
        }

        private void InitializeEnemy()
        {
            int random = Random.Range(1, _availableFoodEnemies.Count);
            _enemy.GetComponent<SpriteRenderer>().sprite = _availableFoodEnemies[random];
            LeanTween.moveY(_enemy, 7f, 1f).setLoopPingPong().setEase(LeanTweenType.easeInCubic);
        }

        private void Attack(GameObject target, float damage)
        {
            if (target == _enemy)
            {
                _player.GetComponent<Animator>().Play("Attack");
                _enemyHealth -= damage;

                if (_enemyHealth <= 0f)
                {
                    _player.GetComponent<Animator>().Play("Level_Up");
                    _battlePanel.SetActive(false);
                    _winPanel.SetActive(true);
                    return;
                }
            }
            else
            {
                LeanTween.moveZ(_enemy, -5f, 0.1f).setOnComplete(new Action(ReturnToOrigin));
                _playerHealth -= damage;

                if (_playerHealth <= 0f)
                {
                    _player.GetComponent<Animator>().Play("Die");
                    _battlePanel.SetActive(false);
                    _losePanel.SetActive(true);
                    return;
                }
            }

            ChangeTurn();
        }

        private void ReturnToOrigin()
        {
            LeanTween.moveZ(_enemy, -3f, 0.1f);
        }

        private void Heal(GameObject target, float amount)
        {
            if (target == _enemy)
            {
                _enemyHealth += amount;

                if (_enemyHealth > 100f)
                {
                    _enemyHealth = 100f;
                }

                _healParticles.transform.position = _enemy.transform.position;
                _healParticles.Play();
            }
            else
            {
                _playerHealth += amount;

                if (_playerHealth > 100f)
                {
                    _playerHealth = 100f;
                }

                _healParticles.transform.position = _player.transform.position;
                _healParticles.Play();
            }

            ChangeTurn();
        }

        public void AttackButton()
        {
            Attack(_enemy, _playerDamagePerHit);
        }

        public void HealButton()
        {
            Heal(_player, _amountPlayerHeal);
        }

        private void ChangeTurn()
        {
            _isPlayerTurn = !_isPlayerTurn;
            _attackBtn.interactable = _isPlayerTurn;
            _healBtn.interactable = _isPlayerTurn;

            if (!_isPlayerTurn)
            {
                StartCoroutine(EnemyTurn());
            }
        }

        private IEnumerator EnemyTurn()
        {
            yield return new WaitForSeconds(5f);
            int random = Random.Range(1, 3);

            switch (random)
            {
                case 1:
                    Attack(_player, _enemyDamagePerHit);
                    break;
                case 2:
                    Heal(_enemy, _amountEnemyHeal);
                    break;
            }
        }
    }
}