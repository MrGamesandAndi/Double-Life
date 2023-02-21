using System;
using System.Collections.Generic;
using UnityEngine;

namespace LevelingSystem
{
    [CreateAssetMenu(menuName = "RPG/XP Table", fileName = "XPTranslationTable")]
    public class XPTranslationTable : BaseXPTranslation
    {
        [SerializeField] List<XPTranslationTableEntry> _table;

        public override bool AddXP(int amount)
        {
            if (AtLevelCap)
            {
                return false;
            }

            CurrentXP += amount;

            for (int i = _table.Count - 1; i >= 0; i--)
            {
                var entry = _table[i];

                if (CurrentXP >= entry.xpRequiered)
                {
                    if (entry.level != CurrentLevel)
                    {
                        CurrentLevel = entry.level;
                        AtLevelCap = _table[_table.Count - 1].level == CurrentLevel;
                        LevelledUp = true;
                        return true;
                    }

                    break;
                }
            }

            return false;
        }

        public override void SetLevel(int level)
        {
            CurrentXP = 0;
            CurrentLevel = 1;
            AtLevelCap = false;

            foreach (var entry in _table)
            {
                if (entry.level == level)
                {
                    AddXP(entry.xpRequiered);
                    return;
                }
            }

            throw new ArgumentOutOfRangeException($"Could not find any entry for level {level}");
        }

        protected override int GetXPRequieredForNextLevel()
        {
            if (AtLevelCap)
            {
                return int.MaxValue;
            }

            for (int i = 0; i < _table.Count; i++)
            {
                var entry = _table[i];

                if (entry.level == CurrentLevel)
                {
                    return _table[i + 1].xpRequiered - CurrentXP;
                }
            }

            throw new ArgumentOutOfRangeException($"Could not find any entry for level {CurrentLevel}");
        }

        public override int GetTotalXPForNextLevel()
        {
            if (AtLevelCap)
            {
                return int.MaxValue;
            }

            for (int i = 0; i < _table.Count; i++)
            {
                var entry = _table[i];

                if (entry.level == CurrentLevel)
                {
                    return _table[i + 1].xpRequiered;
                }
            }

            throw new ArgumentOutOfRangeException($"Could not find any entry for level {CurrentLevel}");
        }
    }
}
