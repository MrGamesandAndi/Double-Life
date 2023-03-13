using TMPro;
using UnityEngine;

namespace ParkMinigame
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
