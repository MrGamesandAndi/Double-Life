using SaveSystem;
using System.Collections.Generic;
using UnityEngine;

namespace General
{
    public class PopulationManager : MonoBehaviour
    {
        public static PopulationManager Instance { get; private set; } = null;
        public List<CharacterData> DoublesList { get => _doublesList; set => _doublesList = value; }

        [SerializeField] List<CharacterData> _doublesList;
        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }


        private void Start()
        {
            LoadAllDoubles();
        }

        private void LoadAllDoubles()
        {
            DoublesList = FileHandler.ReturnAllFilesInFolder<CharacterData>(SaveType.Character_Data);
        }
    }
}