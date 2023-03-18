using General;
using UnityEngine;

public class CheckIfDataIsOK : MonoBehaviour
{
    public void CheckInfo()
    {
        if (string.IsNullOrEmpty(HumanController.Instance.name))
        {
            return;
        }

        if (string.IsNullOrEmpty(HumanController.Instance.LastName))
        {
            return;
        }

        foreach (var item in HumanController.Instance.characterData.Traits)
        {
            if(item == 0)
            {
                return;
            }
        }

        HumanController.Instance.SaveCharacterData();
        GetComponent<SceneLoader>().LoadScene();
    }
}
