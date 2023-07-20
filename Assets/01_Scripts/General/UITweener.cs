using UnityEngine;

namespace General
{
    public class UITweener : MonoBehaviour
    {
        [SerializeField] GameObject _objectToAnimate;
        [SerializeField] UIAnimationTypes _animationType;
        [SerializeField] LeanTweenType _easeType;
        [SerializeField] float _duration;
        [SerializeField] float _delay;
        [SerializeField] bool _loop;
        [SerializeField] bool _pingPong;
        [SerializeField] bool _startPositionOffset;
        [SerializeField] Vector3 _from;
        [SerializeField] Vector3 _to;
        [SerializeField] bool _showOnEnable;
        [SerializeField] bool _workOnDisable;

        private LTDescr _tweenObject;

        private void OnEnable()
        {
            if (_showOnEnable)
            {
                Show();
            }
        }

        public void Show()
        {
            HandleTween();
        }

        public void HandleTween()
        {
            if (_objectToAnimate == null)
            {
                _objectToAnimate = gameObject;
            }

            switch (_animationType)
            {
                case UIAnimationTypes.Move:
                    MoveAbsolute();
                    break;
                case UIAnimationTypes.Scale:
                    Scale();
                    break;
                case UIAnimationTypes.ScaleX:
                    Scale();
                    break;
                case UIAnimationTypes.ScaleY:
                    Scale();
                    break;
                case UIAnimationTypes.Fade:
                    Fade();
                    break;
                default:
                    break;
            }

            _tweenObject.setDelay(_delay);
            _tweenObject.setEase(_easeType);

            if (_loop)
            {
                _tweenObject.loopCount = int.MaxValue;
            }

            if (_pingPong)
            {
                _tweenObject.setLoopPingPong();
            }
        }

        public void Fade()
        {
            if (gameObject.GetComponent<CanvasGroup>() == null)
            {
                gameObject.AddComponent<CanvasGroup>();
            }

            if (_startPositionOffset)
            {
                _objectToAnimate.GetComponent<CanvasGroup>().alpha = _from.x;
            }

            _tweenObject = LeanTween.alphaCanvas(_objectToAnimate.GetComponent<CanvasGroup>(), _to.x, _duration);
        }

        public void MoveAbsolute()
        {
            _objectToAnimate.GetComponent<RectTransform>().anchoredPosition = _from;
            _tweenObject = LeanTween.move(_objectToAnimate.GetComponent<RectTransform>(), _to, _duration);
        }

        public void Scale()
        {
            if (_startPositionOffset)
            {
                _objectToAnimate.GetComponent<RectTransform>().localScale = _from;
            }

            _tweenObject = LeanTween.scale(_objectToAnimate, _to, _duration);
        }

        private void SwapDirection()
        {
            var temp = _from;
            _from = _to;
            _to = temp;
        }

        public void Disable()
        {
            SwapDirection();
            HandleTween();
            _tweenObject.setOnComplete(() =>
            {
                SwapDirection();
                gameObject.SetActive(false);
            });
        }
    }
}
