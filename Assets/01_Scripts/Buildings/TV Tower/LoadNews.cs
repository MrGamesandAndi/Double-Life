using UnityEngine;

public class LoadNews : MonoBehaviour
{
    private void Start()
    {
        AchievementManager.instance.gameObject.GetComponent<AchievenmentListIngame>().OpenWindow();
    }
}
