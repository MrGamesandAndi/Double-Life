using AudioSystem;
using LevelingSystem;
using UnityEngine;

public class DragInteraction : MonoBehaviour
{
    public GameObject _snapPoint;
    public int expReward;

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
            ExperienceManager.Instance.LevelSystem.AddExperience(expReward);
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
