using General;
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
        [SerializeField] int _zodiacCode;
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
        [SerializeField] DoubleState _currentState;
        [SerializeField] List<Vector3> _purchasedFurniture;
        [SerializeField] List<RelationshipData> _relationships;

        public CharacterData()
        {
        }

        public CharacterData(string name, string lastName, string nickname, string relationshipCode, Color color, int zodiacCode, string sexPreferenceCode, Color skintone, string hairKey, Color hairColor, string eyebrowKey, Color eyebrowColor, string eyeKey, Color eyeColor, string mouthKey, List<int> traits, int gender, DoubleState currentState, int level, int experience, int id, List<Vector3> purchasedFurniture, List<RelationshipData> relationships)
        {
            Name = name;
            LastName = lastName;
            Nickname = nickname;
            RelationshipCode = relationshipCode;
            Color = color;
            ZodiacCode = zodiacCode;
            SexPreferenceCode = sexPreferenceCode;
            Skintone = skintone;
            HairKey = hairKey;
            HairColor = hairColor;
            EyebrowKey = eyebrowKey;
            EyebrowColor = eyebrowColor;
            EyeKey = eyeKey;
            EyeColor = eyeColor;
            MouthKey = mouthKey;
            Traits = traits;
            Gender = gender;
            CurrentState = currentState;
            Level = level;
            Experience = experience;
            Id = id;
            PurchasedFurniture = purchasedFurniture;
            Relationships = relationships;
        }

        public string Name { get => _name; set => _name = value; }
        public string LastName { get => _lastName; set => _lastName = value; }
        public string Nickname { get => _nickname; set => _nickname = value; }
        public string RelationshipCode { get => _relationshipCode; set => _relationshipCode = value; }
        public Color Color { get => _color; set => _color = value; }
        public int ZodiacCode { get => _zodiacCode; set => _zodiacCode = value; }
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
        public DoubleState CurrentState { get => _currentState; set => _currentState = value; }
        public int Level { get => _level; set => _level = value; }
        public int Experience { get => _experience; set => _experience = value; }
        public int Id { get => _id; set => _id = value; }
        public List<Vector3> PurchasedFurniture { get => _purchasedFurniture; set => _purchasedFurniture = value; }
        public List<RelationshipData> Relationships { get => _relationships; set => _relationships = value; }

        [Serializable]
        public struct RelationshipData
        {
            public int targetId;
            public int relationshipLevel;
            public bool isLove;

            public RelationshipData(int targetId, int relationshipLevel, bool isLove)
            {
                this.targetId = targetId;
                this.relationshipLevel = relationshipLevel;
                this.isLove = isLove;
            }
        }
    }
}