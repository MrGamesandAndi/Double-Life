using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace SaveSystem
{
    public static class FileHandler
    {
        public static void SaveToJSON<T>(List<T> toSave, string fileName, SaveType saveType = 0)
        {
            string content = JsonHelper.ToJson<T>(toSave.ToArray());
            WriteFile(GetPath(fileName, saveType), content);
        }

        public static void SaveToJSON<T>(T toSave, string fileName, SaveType saveType = 0)
        {
            string content = JsonUtility.ToJson(toSave);
            WriteFile(GetPath(fileName, saveType), content);
        }

        public static List<T> ReadListFromJSON<T>(string fileName, SaveType saveType = 0)
        {
            string content = ReadFile(GetPath(fileName, saveType));

            if (string.IsNullOrEmpty(content) || content == "{}")
            {
                return new List<T>();
            }

            List<T> res = JsonHelper.FromJson<T>(content).ToList();
            return res;
        }

        public static T ReadFromJSON<T>(string fileName, SaveType saveType = 0)
        {
            string content = ReadFile(GetPath(fileName, saveType));

            if (string.IsNullOrEmpty(content) || content == "{}")
            {
                return default;
            }

            T res = JsonUtility.FromJson<T>(content);
            return res;
        }

        private static string GetPath(string fileName, SaveType saveType)
        {
            string path = "";

            switch (saveType)
            {
                case SaveType.Player_Data:
                    path = Path.Combine(Application.persistentDataPath, "PlayerData");
                    break;
                case SaveType.Character_Data:
                    path = Path.Combine(Application.persistentDataPath, "CharacterData");
                    break;
                default:
                    path = Path.Combine(Application.persistentDataPath, "PlayerData");
                    break;
            }

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            return Path.Combine(path, fileName);
        }

        private static void WriteFile(string path, string content)
        {
            FileStream fileStream = new FileStream(path, FileMode.Create);
            using (StreamWriter writer = new StreamWriter(fileStream))
            {
                writer.Write(content);
            }
        }

        private static string ReadFile(string path)
        {
            if (File.Exists(path))
            {
                using(StreamReader reader=new StreamReader(path))
                {
                    string content = reader.ReadToEnd();
                    return content;
                }
            }

            return "";
        }

        public static List<T> ReturnAllFilesInFolder<T>(SaveType saveType)
        {
            List<T> files = new List<T>(); 
            string path = GetPath("", saveType);

            if (Directory.Exists(path))
            {
                 DirectoryInfo d = new DirectoryInfo(path);

                foreach (var file in d.GetFiles())
                {
                    files.Add(JsonUtility.FromJson<T>(ReadFile(GetPath(file.Name, saveType))));
                }
            }

            return files;
        }

        public static bool CheckIfCharacterFileExists(string fileName)
        {
            string path = GetPath("", SaveType.Character_Data);

            if (Directory.Exists(path))
            {
                DirectoryInfo d = new DirectoryInfo(path);

                foreach (var file in d.GetFiles())
                {
                    if (file.Name == fileName)
                    {
                        return true;
                    }
                }
            }

            return false;
        }


        public static void DeleteFileFromFolder(string filename, SaveType saveType)
        {
           string path = GetPath(filename, saveType);
           File.Delete(Path.Combine(path));
        }
    }
}
