using Stats;
using UnityEngine;

namespace SmartInteractions
{
    public class FeedbackUIPanel : MonoBehaviour
    {
        [SerializeField] GameObject _statPanelPrefab;
        [SerializeField] Transform _statRoot;

        public AIStatPanel AddStat(AIStat linkedStat,float initialValue)
        {
            var newGO = Instantiate(_statPanelPrefab, _statRoot);
            newGO.name = $"Stat_{linkedStat.DisplayName}";
            var statPanelLogic = newGO.GetComponent<AIStatPanel>();
            statPanelLogic.Bind(linkedStat, initialValue);
            return statPanelLogic;
        }
    }
}
