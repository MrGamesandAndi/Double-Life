using General;
using Localisation;
using Population;
using SaveSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FriendshipDisplayer : MonoBehaviour
{
    [SerializeField] Sprite _spaLogo;
    [SerializeField] Sprite _engLogo;

    [SerializeField] Image _leftLogo;
    [SerializeField] Image _rightLogo;

    [SerializeField] GameObject _containerPrefab;

    [SerializeField] GameObject _leftFriendsContainer;
    [SerializeField] GameObject _rightFriendsContainer;

    Vector3 offScreenLeftPosition;
    Vector3 offScreenRightPosition;
    Vector3 centerPosition;
    RectTransform rt;

    void Awake()
    {
        rt = GetComponent<RectTransform>();

        centerPosition = new Vector3(0, 0, 0);
        offScreenRightPosition = new Vector3(Screen.width, 0, 0);

        rt.localPosition = offScreenRightPosition;
    }

    private void Start()
    {
        if (SaveManager.Instance.PlayerData.language == Language.Spanish)
        {
            _leftLogo.sprite = _spaLogo;
            _rightLogo.sprite = _spaLogo;
        }
        else
        {
            _leftLogo.sprite = _engLogo;
            _rightLogo.sprite = _engLogo;
        }

        foreach (var friend in GameManager.Instance.currentLoadedDouble.Relationships)
        {
            string name = $"{PopulationManager.Instance.ReturnDouble(friend.targetId).Name} {PopulationManager.Instance.ReturnDouble(friend.targetId).LastName}";
            GameObject gameObject = Instantiate(_containerPrefab);
            gameObject.GetComponentInChildren<TextMeshProUGUI>().text = name;
            gameObject.transform.parent = _leftFriendsContainer.transform;
        }
    }

    public void Show()
    {
        LeanTween.cancel(gameObject);
        rt.localPosition = offScreenRightPosition;
        LeanTween.move(rt, centerPosition, 0.5f).setEase(LeanTweenType.easeInOutExpo);
    }

    public void Hide()
    {
        LeanTween.cancel(gameObject);
        LeanTween.move(rt, offScreenRightPosition, 0.5f).setEase(LeanTweenType.easeInOutExpo);
    }
}
