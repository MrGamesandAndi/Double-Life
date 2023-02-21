using Stats;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SmartInteractions
{
    public class AIStatPanel : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI _statName;
        [SerializeField] Slider _statValue;

        AIStat _linkedStat;

        public void Bind(AIStat stat, float initialValue)
        {
            _linkedStat = stat;
            _statName.text = _linkedStat.DisplayName;
            _statValue.SetValueWithoutNotify(initialValue);
        }

        public void OnStatChanged(float newValue)
        {
            _statValue.SetValueWithoutNotify(newValue);
        }
    }
}
