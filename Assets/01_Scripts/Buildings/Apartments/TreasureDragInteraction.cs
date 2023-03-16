using General;
using Needs;
using SceneManagement;
using ShopSystem;
using UnityEngine;

namespace Apartments
{
    public class TreasureDragInteraction : MonoBehaviour
    {
        public GameObject _snapPoint;
        public Scenes _sceneToLoad;
        public int id;

        Vector3 _snapPointPosition;
        Vector3 _screenPoint;
        bool _isCorrectPlace = false;
        bool alreadySticked = false;
        Rigidbody _gameObjectRB;

        [SerializeField] GameObject _foodParticles;
        [SerializeField] AudioClip _foodSound;

        private void Start()
        {
            _snapPointPosition = _snapPoint.transform.position;
            _gameObjectRB = GetComponent<Rigidbody>();
        }

        private void OnMouseDown()
        {
            _screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
            _gameObjectRB.isKinematic = false;
        }

        private void OnMouseDrag()
        {
            if (!alreadySticked)
            {
                Vector3 currentScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, _screenPoint.z);
                Vector3 currentPosition = Camera.main.ScreenToWorldPoint(currentScreenPoint);
                transform.position = currentPosition;
            }
        }

        private void OnMouseUp()
        {
            if (_isCorrectPlace)
            {
                gameObject.transform.position = _snapPointPosition;
                alreadySticked = true;

                if (id == 1 || id == 9)
                {
                    ScenesManager.Instance.LoadScene(_sceneToLoad, Scenes.Apartment_Room);
                }
                else
                {
                    Destroy(gameObject);

                    if (GameManager.Instance.currentLoadedDouble.CurrentState == DoubleState.Sick && id == 6)
                    {
                        RoomManager.Instance.DialogueRunner.StartDialogue("Thanks");
                        Treasure gainedTreasure = BodyPartsCollection.Instance.ReturnRandomTreasure(TreasureRarity.Rare);
                        GameManager.Instance.GainTreasure(gainedTreasure.id, 1);
                        PopulationManager.Instance.GetAIByID(GameManager.Instance.currentLoadedDouble.Id).NeedCompleted(NeedType.Sickness);
                    }

                    if (GameManager.Instance.currentLoadedDouble.CurrentState == DoubleState.Angry && id == 7)
                    {
                        RoomManager.Instance.DialogueRunner.StartDialogue("Thanks");
                        Treasure gainedTreasure = BodyPartsCollection.Instance.ReturnRandomTreasure(TreasureRarity.Rare);
                        GameManager.Instance.GainTreasure(gainedTreasure.id, 1);
                        PopulationManager.Instance.GetAIByID(GameManager.Instance.currentLoadedDouble.Id).NeedCompleted(NeedType.HaveFight);
                    }
                }
            }
        }

        private void OnTriggerEnter(Collider collision)
        {
            if (collision.gameObject.name == "Snap Point")
            {
                _isCorrectPlace = true;
            }
        }

        private void OnTriggerExit(Collider collision)
        {
            if (collision.gameObject.name == "Snap Point")
            {
                _isCorrectPlace = false;
            }
        }
    }
}