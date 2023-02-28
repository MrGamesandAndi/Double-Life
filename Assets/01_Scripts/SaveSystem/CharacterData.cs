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
        [SerializeField] string _name = "";
        [SerializeField] string _lastName = "";
        [SerializeField] string _nickname = "";
        [SerializeField] string _relationshipCode = "cc_rel_6";
        [SerializeField] Color _color = Color.red;
        [SerializeField] string _zodiacCode = "cc_zodiac_01";
        [SerializeField] string _sexPreferenceCode = "cc_sex_03";
        [SerializeField] int _gender = 0;

        [SerializeField] Color _skintone = new Color(245, 210, 157, 255);
        [SerializeField] string _hairKey = "Hair1";
        [SerializeField] Color _hairColor = Color.black;
        [SerializeField] string _eyebrowKey = "Brow1";
        [SerializeField] Color _eyebrowColor = Color.black;
        [SerializeField] string _eyeKey = "Eye1";
        [SerializeField] Color _eyeColor = new Color(101, 50, 24, 255);
        [SerializeField] string _mouthKey = "Mouth4";

        [SerializeField] List<int> _traits = new List<int>(4);
        [SerializeField] List<GridObject.SaveObject> _purchasedFurniture = new List<GridObject.SaveObject>();
        [SerializeField] NeedsManager _needsManager;
        [SerializeField] DoubleState _currentState;

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
    }
}