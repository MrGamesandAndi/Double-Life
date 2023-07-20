using UnityEngine;

namespace Buildings.TVTower
{
    public class LoadNews : MonoBehaviour
    {
        private void Start()
        {
            AchievementManager.instance.gameObject.GetComponent<AchievenmentListIngame>().OpenWindow();
        }
    }
}