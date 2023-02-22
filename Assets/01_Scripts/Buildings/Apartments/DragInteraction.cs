using AudioSystem;
using General;
using LevelingSystem;
using SaveSystem;
using UnityEngine;

public class DragInteraction : MonoBehaviour
{
    public GameObject _snapPoint;

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
            int randomAmount = 0;
            randomAmount = Random.Range(50, 101);
            var name = GameManager.Instance.currentLoadedDouble.Name + GameManager.Instance.currentLoadedDouble.LastName;

            for (int i = 0; i < GenerateAI.Instance.transform.childCount; i++)
            {
                var ai = GenerateAI.Instance.transform.GetChild(i);

                if (ai.gameObject.name == name)
                {
                    //ai.GetComponent<>
                    RoomManager.Instance.humanModel.GetComponent<XPTracker>().AddXP(50);
                }
            }

            GameManager.Instance.GainFunds(randomAmount);
            AudioManager.Instance.PlaySfx(_foodSound);
            gameObject.transform.position = _snapPointPosition;
            alreadySticked = true;
            Instantiate(_foodParticles, _snapPoint.transform, false);
            Destroy(gameObject);
            RoomManager.Instance.ShowTabs();
            RoomManager.Instance.UpdateMoneyText();
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
