using General;
using Relationships;
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
        AddExtraRelationship();
        GetComponent<SceneLoader>().LoadScene();
    }

    private void AddExtraRelationship()
    {
        if (HumanController.Instance.RelationshipCode == "cc_rel_1")
        {
            if (!RelationshipSystem.Instance.CheckIfLoveInterestExists(1))
            {
                RelationshipSystem.Instance.AddNewRelationship(GameManager.Instance.currentLoadedDouble.Id, 1, 4, true);
            }
        }
    }
}
