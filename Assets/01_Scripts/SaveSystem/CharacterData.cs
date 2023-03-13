using General;
using GridSystem;
using Needs;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace SaveSystem
{
    [Serializable]
    public class CharacterData
    {
        [SerializeField] int _id;
        [SerializeField] string _name;
        [SerializeField] string _lastName;
        [SerializeField] string _nickname;
        [SerializeField] string _relationshipCode;
        [SerializeField] Color _color;
        [SerializeField] string _zodiacCode;
        [SerializeField] string _sexPreferenceCode;
        [SerializeField] int _gender;
        [SerializeField] int _level;
        [SerializeField] int _experience;

        [SerializeField] Color _skintone;
        [SerializeField] string _hairKey;
        [SerializeField] Color _hairColor;
        [SerializeField] string _eyebrowKey;
        [SerializeField] Color _eyebrowColor;
        [SerializeField] string _eyeKey;
        [SerializeField] Color _eyeColor;
        [SerializeField] string _mouthKey;

        [SerializeField] List<int> _traits;
        [SerializeField] List<GridObject.SaveObject> _purchasedFurniture;
        [SerializeField] NeedsManager _needsManager;
        [SerializeField] DoubleState _currentState;

        public CharacterData()
        {
            _id = 100;
            _name = "";
            _lastName = "";
            _nickname = "";
            _relationshipCode = "cc_rel_6";
            _color = Color.red;
            _zodiacCode = "cc_zodiac_01";
            _sexPreferenceCode = "cc_sex_03";
            _gender = 0;
            _level = 0;
            _experience = 0;

            _skintone = new Color(245f, 210f, 157f, 255f);
             _hairKey = "Hair1";
            _hairColor = Color.black;
             _eyebrowKey = "Brow1";
            _eyebrowColor = Color.black;
             _eyeKey = "Eye1";
             _eyeColor = new Color(101f, 50f, 24f, 255f);
             _mouthKey = "Mouth4";

            _traits = new List<int>(4);
        }

        public string Name { get => _name; set => _name = value; }
        public string LastName { get => _lastName; set => _lastName = value; }
        public string Nickname { get => _nickname; set => _nickname = value; }
        public string RelationshipCode { get => _relationshipCode; set => _relationshipCode = value; }
        public Color Color { get => _color; set => _color = value; }
        public string ZodiacCode { get => _zodiacCode; set => _zodiacCode = value; }
        public string SexPreferenceCode { get => _sexPreferenceCode; set => _sexPreferenceCode = value; }
        public Color Skintone { get => _skintone; set => _skintone = value; }
        public string HairKey { get => _hairKey; set => _hairKey = value; }
        public Color HairColor { get => _hairColor; set => _hairColor = value; }
        public string EyebrowKey { get => _eyebrowKey; set => _eyebrowKey = value; }
        public Color EyebrowColor { get => _eyebrowColor; set => _eyebrowColor = value; }
        public string EyeKey { get => _eyeKey; set => _eyeKey = value; }
        public Color EyeColor { get => _eyeColor; set => _eyeColor = value; }
        public string MouthKey { get => _mouthKey; set => _mouthKey = value; }
        public List<int> Traits { get => _traits; set => _traits = value; }
        public int Gender { get => _gender; set => _gender = value; }
        public List<GridObject.SaveObject> PurchasedFurniture { get => _purchasedFurniture; set => _purchasedFurniture = value; }
        public NeedsManager NeedsManager { get => _needsManager; set => _needsManager = value; }
        public DoubleState CurrentState { get => _currentState; set => _currentState = value; }
        public int Level { get => _level; set => _level = value; }
        public int Experience { get => _experience; set => _experience = value; }
        public int Id { get => _id; set => _id = value; }
    }
}