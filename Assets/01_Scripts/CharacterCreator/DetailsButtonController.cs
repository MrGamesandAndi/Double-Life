using UnityEngine;

namespace CharacterCreator
{
    public class DetailsButtonController : MonoBehaviour
    {
        private string _id;
        public BaseScriptableObject objectInfo;
        private void Start()
        {
            _id = gameObject.name;
            CharacterCreatorEvents.current.onDetailsButtonPressed += onButtonPressed;
        }

        private void onButtonPressed(string id)
        {
            if (id.Equals(this._id))
            {
                HumanController.Instance.SetBodyPart(objectInfo);
            }
        }

        public void ButtonPressed()
        {
            CharacterCreatorEvents.current.DetailsButtonPressed(_id);
        }
    }
}