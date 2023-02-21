using TMPro;
using UnityEngine;

namespace Minigames
{
    public class PointController : MonoBehaviour
    {
        [SerializeField] TextMeshPro _pointsText;

        public void SetPoint(int points)
        {
            _pointsText.SetText(points.ToString());
        }
    }
}
