using UnityEngine;

namespace CharacterCreator
{
    [CreateAssetMenu(fileName = "CharacterScriptableObject", menuName = "ScriptableObjects/CharacterCreator", order = 0)]
    public class BaseScriptableObject : ScriptableObject
    {
        [SerializeField] Sprite _icon;
        [SerializeField] Color _color;
        [SerializeField] Mesh _mesh;
        [SerializeField] BodyTypes _bodyType;

        public Sprite Icon
        {
            get { return _icon; }
            set { _icon = value; }
        }

        public Color Color
        {
            get { return _color; }
        }

        public Mesh Mesh
        {
            get { return _mesh; }
        }

        public BodyTypes BodyType
        {
            get { return _bodyType; }
        }
    }
}
