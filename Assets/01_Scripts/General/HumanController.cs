using CharacterCreator;
using General;
using SaveSystem;
using UnityEngine;

public class HumanController : MonoBehaviour
{
    public static HumanController Instance { get; private set; }
    public CharacterData characterData;

    [Header("Body References")]
    [SerializeField] GameObject _headReference;
    [SerializeField] GameObject _hairReference;
    [SerializeField] GameObject _rEyeReference;
    [SerializeField] GameObject _lEyeReference;
    [SerializeField] GameObject _rEyebrowReference;
    [SerializeField] GameObject _lEyebrowReference;
    [SerializeField] GameObject _mouthReference;
    [SerializeField] GameObject _shirtReference;
    [SerializeField] GameObject _pantsReference;
    [SerializeField] GameObject _shoesReference;
    [SerializeField] GameObject _handsReference;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError($"Trying to create second HumanController on {gameObject.name}");
            Destroy(gameObject);
            return;
        }

        Instance = this;
        characterData = GameManager.Instance.currentLoadedDouble;
        LoadParts();
    }

    public void LoadParts()
    {
        if (characterData == null)
        {
            return;
        }

        SetColor(BodyTypes.Shirt, characterData.Color);
        SetColor(BodyTypes.Skintone, characterData.Skintone);

        SetMesh(_hairReference, BodyPartsCollection.Instance.ReturnHairMesh(GameManager.Instance.currentLoadedDouble.HairKey));
        SetColor(BodyTypes.Hair, GameManager.Instance.currentLoadedDouble.HairColor);

        SetSprite(_lEyeReference, BodyPartsCollection.Instance.ReturnEyeSprite(GameManager.Instance.currentLoadedDouble.EyeKey));
        SetSprite(_rEyeReference, BodyPartsCollection.Instance.ReturnEyeSprite(GameManager.Instance.currentLoadedDouble.EyeKey));
        SetColor(BodyTypes.Eye, GameManager.Instance.currentLoadedDouble.EyeColor);

        SetSprite(MouthReference, BodyPartsCollection.Instance.ReturnMouthSprite(GameManager.Instance.currentLoadedDouble.MouthKey));

        SetSprite(_lEyebrowReference, BodyPartsCollection.Instance.ReturnEyebrowSprite(GameManager.Instance.currentLoadedDouble.EyebrowKey));
        SetSprite(_rEyebrowReference, BodyPartsCollection.Instance.ReturnEyebrowSprite(GameManager.Instance.currentLoadedDouble.EyebrowKey));
        SetColor(BodyTypes.Eyebrow, GameManager.Instance.currentLoadedDouble.EyebrowColor);
    }

    public GameObject HeadReference
    {
        get
        {
            return _headReference;
        }
    }

    public GameObject HandsReference
    {
        get
        {
            return _handsReference;
        }
    }

    public GameObject HairReference
    {
        get
        {
            return _hairReference;
        }
    }

    public GameObject LEyebrowReference
    {
        get
        {
            return _lEyebrowReference;
        }
    }
    public GameObject REyebrowReference
    {
        get
        {
            return _rEyebrowReference;
        }
    }

    public GameObject LEyeReference
    {
        get
        {
            return _lEyeReference;
        }
    }
    public GameObject REyeReference
    {
        get
        {
            return _rEyeReference;
        }
    }
    public GameObject MouthReference
    {
        get
        {
            return _mouthReference;
        }
    }

    public string Name
    {
        get => characterData.Name;
        set => characterData.Name = value;
    }

    public string LastName
    {
        get => characterData.LastName;
        set => characterData.LastName = value;
    }

    public string Nickname
    {
        get => characterData.Nickname;
        set => characterData.Nickname = value;
    }

    public string RelationshipCode
    {
        get => characterData.RelationshipCode;
        set => characterData.RelationshipCode = value;
    }

    public Color Color
    {
        get => characterData.Color;
        set => characterData.Color = value;
    }

    public string ZodiacCode
    {
        get => characterData.ZodiacCode;
        set => characterData.ZodiacCode = value;
    }

    public string SexPreferenceCode
    {
        get => characterData.SexPreferenceCode;
        set => characterData.SexPreferenceCode = value;
    }

    public Color Skintone
    {
        get => characterData.Skintone;
        set => characterData.Skintone = value;
    }

    public string HairKey
    {
        get => characterData.HairKey;
        set => characterData.HairKey = value;
    }

    public Color HairColor
    {
        get => characterData.HairColor;
        set => characterData.HairColor = value;
    }

    public string EyebrowKey
    {
        get => characterData.EyebrowKey;
        set => characterData.EyebrowKey = value;
    }

    public Color EyebrowColor
    {
        get => characterData.EyebrowColor;
        set => characterData.EyebrowColor = value;
    }

    public string EyeKey
    {
        get => characterData.EyeKey;
        set => characterData.EyeKey = value;
    }

    public Color EyeColor
    {
        get => characterData.EyeColor;
        set => characterData.EyeColor = value;
    }
    public string MouthKey
    {
        get => characterData.MouthKey;
        set => characterData.MouthKey = value;
    }

    public int Gender
    {
        get => characterData.Gender;
        set => characterData.Gender = value;
    }

    public void SetBodyPart(BaseScriptableObject objectInfo)
    {
        switch (objectInfo.BodyType)
        {
            case BodyTypes.None:
                break;
            case BodyTypes.Skintone:
                SetColor(BodyTypes.Skintone, objectInfo.Color);
                SetColor(BodyTypes.Skintone, objectInfo.Color);
                Skintone = objectInfo.Color;
                break;
            case BodyTypes.Hair:
                SetMesh(_hairReference, objectInfo.Mesh);
                HairKey = objectInfo.name;
                break;
            case BodyTypes.Eyebrow:
                SetSprite(_lEyebrowReference, objectInfo.Icon);
                SetSprite(_rEyebrowReference, objectInfo.Icon);
                EyebrowKey = objectInfo.name;
                break;
            case BodyTypes.Eye:
                SetSprite(_lEyeReference, objectInfo.Icon);
                SetSprite(_rEyeReference, objectInfo.Icon);
                EyeKey = objectInfo.name;
                break;
            case BodyTypes.Mouth:
                SetSprite(_mouthReference, objectInfo.Icon);
                MouthKey = objectInfo.name;
                break;
            default:
                break;
        }
    }

    public void SetSprite(GameObject bodyPart, Sprite newSprite)
    {
        bodyPart.GetComponent<SpriteRenderer>().sprite = newSprite;
    }

    public void SetColor(BodyTypes bodyPart, Color newColor)
    {
        switch (bodyPart)
        {
            case BodyTypes.None:
                break;
            case BodyTypes.Skintone:
                _headReference.GetComponent<SkinnedMeshRenderer>().material.SetColor("_BaseColor", newColor);
                _handsReference.GetComponent<SkinnedMeshRenderer>().material.SetColor("_BaseColor", newColor);
                break;
            case BodyTypes.Hair:
                _hairReference.GetComponent<MeshRenderer>().material.SetColor("_BaseColor", newColor);
                HairColor = newColor;
                break;
            case BodyTypes.Eyebrow:
                _lEyebrowReference.GetComponent<SpriteRenderer>().color = newColor;
                _rEyebrowReference.GetComponent<SpriteRenderer>().color = newColor;
                EyebrowColor = newColor;
                break;
            case BodyTypes.Eye:
                _lEyeReference.GetComponent<SpriteRenderer>().material.SetColor("_PupilNew", newColor);
                _rEyeReference.GetComponent<SpriteRenderer>().material.SetColor("_PupilNew", newColor);
                EyeColor = newColor;
                break;
            case BodyTypes.Shirt:
                _shirtReference.GetComponent<SkinnedMeshRenderer>().material.SetColor("_BaseColor", newColor);
                Color = newColor;
                break;
            default:
                break;
        }
    }

    public void SetMaterial(GameObject bodypart, Material material)
    {
        bodypart.GetComponentInChildren<SpriteRenderer>().material = material;
    }

    public void SetMesh(GameObject bodyPart, Mesh newMesh)
    {
        bodyPart.GetComponent<MeshFilter>().mesh = newMesh;
    }

    public void LoadCharacterData(string fileName = "Base")
    {
        FileHandler.ReadFromJSON<CharacterData>(fileName, SaveType.Character_Data);
    }

    public void SaveCharacterData()
    {
        if (!FileHandler.CheckIfCharacterFileExists(Name + LastName))
        {
            PopulationManager.Instance.AddDouble(characterData);
            GenerateAI.Instance.AddIndividualAI(characterData);
        }

        AchievementManager.instance.AddAchievementProgress("Unlock_TownHall", 1);
        AchievementManager.instance.AddAchievementProgress("Unlock_Park", 1);
        AchievementManager.instance.AddAchievementProgress("Unlock_TV", 1);
    }

    public void PlayAnimation(string animationName)
    {
        GetComponent<Animator>().Play(animationName);
    }
}
