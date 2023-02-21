using UnityEngine;
using UnityEngine.UI;

namespace General
{
    public class ScrollingBackground : MonoBehaviour
    {
        RawImage _background;
        [SerializeField] float _xScrollSpeed;

        private void Start()
        {
            _background = GetComponent<RawImage>();
        }

        private void Update()
        {
            _background.uvRect = new Rect(_background.uvRect.position + new Vector2(_xScrollSpeed, 0) * Time.deltaTime, _background.uvRect.size);
        }
    }
}