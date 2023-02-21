using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text.RegularExpressions;
using System.IO;
using UnityEditor;
using System.Linq;

namespace Localisation
{
    public class CSVLoader
    {
        [SerializeField] TextAsset csvFile; //Reference of the file that contains the text.
        [SerializeField] char _lineSeparator = '\n'; //Character that will separate the lines.
        [SerializeField] char _surround = '"'; //Character used to define an entry.
        [SerializeField] string[] _fieldSeparator = { "\",\"" }; //Separator used for defining each value.

        public void LoadCSV()
        {
            csvFile = Resources.Load<TextAsset>("localisation");
        }

        public Dictionary<string, string> GetDictionaryValues(string attributeId)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            string[] lines = csvFile.text.Split(_lineSeparator);
            int attributeIndex = -1;
            string[] headers = lines[0].Split(_fieldSeparator, StringSplitOptions.None);

            for (int i = 0; i < headers.Length; i++)
            {
                if (headers[i].Contains(attributeId))
                {
                    attributeIndex = i;
                    break;
                }
            }

            Regex CSVParser = new Regex(",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");

            for (int i = 1; i < lines.Length; i++)
            {
                string line = lines[i];
                string[] fields = CSVParser.Split(line);

                for (int j = 0; j < fields.Length; j++)
                {
                    fields[j] = fields[j].TrimStart(' ', _surround);
                    fields[j] = fields[j].Replace("/", "");
                    fields[j] = fields[j].Replace(_surround.ToString(), "");
                }

                if (fields.Length > attributeIndex)
                {
                    var key = fields[0];

                    if (dictionary.ContainsKey(key))
                    {
                        continue;
                    }

                    var value = fields[attributeIndex];
                    dictionary.Add(key, value);
                }
            }

            return dictionary;
        }

#if UNITY_EDITOR
        public void Add(string key, string value)
        {
            string appended = string.Format("\n\"{0}\",\"{1}\",\"\"", key, value);
            File.AppendAllText("Assets/Resources/localisation.csv", appended);
            AssetDatabase.Refresh();
        }

        public void Remove(string key)
        {
            string[] lines = csvFile.text.Split(_lineSeparator);
            string[] keys = new string[lines.Length];

            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];
                keys[i] = line.Split(_fieldSeparator, StringSplitOptions.None)[0];
            }

            int index = -1;

            for (int i = 0; i < keys.Length; i++)
            {
                if (keys[i].Contains(key))
                {
                    index = i;
                    break;
                }
            }

            if (index > -1)
            {
                string[] newLines;
                newLines = lines.Where(w => w != lines[index]).ToArray();
                string replaced = string.Join(_lineSeparator.ToString(), newLines);
                File.WriteAllText("Assets/Resources/localisation.csv", replaced);
            }
        }

        public void Edit(string key, string value)
        {
            Remove(key);
            Add(key, value);
        }
#endif
    }
}