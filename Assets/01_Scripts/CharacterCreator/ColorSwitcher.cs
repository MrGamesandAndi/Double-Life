using UnityEngine;
using UnityEngine.UI;

namespace CharacterCreator
{
    public class ColorSwitcher : MonoBehaviour
    {
        [SerializeField] Color _color;
        [SerializeField] BodyTypes _bodyPartToColor;
        [SerializeField] HSVPicker.ColorPicker picker;

        private void Start()
        {
            GetComponent<Image>().color = _color;
        }

        public void SetColor()
        {
            HumanController.Instance.SetColor(_bodyPartToColor, _color);
        }

        public void SetColorWithColorPicker()
        {
            picker.onValueChanged.AddListener(color =>
            {
                HumanController.Instance.SetColor(_bodyPartToColor, color);
            });
        }
    }
}
