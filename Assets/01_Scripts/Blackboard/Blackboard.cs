using Stats;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Blackboards
{
    public class Blackboard
    {
        Dictionary<BlackboardKey, int> _intValues = new Dictionary<BlackboardKey, int>();
        Dictionary<BlackboardKey, float> _floatValues = new Dictionary<BlackboardKey, float>();
        Dictionary<BlackboardKey, bool> _boolValues = new Dictionary<BlackboardKey, bool>();
        Dictionary<BlackboardKey, string> _stringValues = new Dictionary<BlackboardKey, string>();
        Dictionary<BlackboardKey, Vector3> _vector3Values = new Dictionary<BlackboardKey, Vector3>();
        Dictionary<BlackboardKey, GameObject> _gameObjectValues = new Dictionary<BlackboardKey, GameObject>();
        Dictionary<BlackboardKey, object> _genericValues = new Dictionary<BlackboardKey, object>();
        Dictionary<AIStat, float> _AiStatValues = new Dictionary<AIStat, float>();

        public bool TryGet(BlackboardKey key, out int value, int defaultValue = 0)
        {
            return TryGet(_intValues, key, out value, defaultValue);
        }

        public int GetInt(BlackboardKey key)
        {
            return Get(_intValues, key);
        }

        public void Set(BlackboardKey key, int value)
        {
            _intValues[key] = value;
        }

        public bool TryGet(BlackboardKey key, out float value, float defaultValue = 0f)
        {
            return TryGet(_floatValues, key, out value, defaultValue);
        }

        public float GetFloat(BlackboardKey key)
        {
            return Get(_floatValues, key);
        }

        public void Set(BlackboardKey key, float value)
        {
            _floatValues[key] = value;
        }

        public bool TryGet(BlackboardKey key, out bool value, bool defaultValue = false)
        {
            return TryGet(_boolValues, key, out value, defaultValue);
        }

        public bool GetBool(BlackboardKey key)
        {
            return Get(_boolValues, key);
        }

        public void Set(BlackboardKey key, bool value)
        {
            _boolValues[key] = value;
        }

        public bool TryGet(BlackboardKey key, out string value, string defaultValue = "")
        {
            return TryGet(_stringValues, key, out value, defaultValue);
        }

        public string GetString(BlackboardKey key)
        {
            return Get(_stringValues, key);
        }

        public void Set(BlackboardKey key, string value)
        {
            _stringValues[key] = value;
        }

        public bool TryGet(BlackboardKey key, out Vector3 value, Vector3 defaultValue)
        {
            return TryGet(_vector3Values, key, out value, defaultValue);
        }

        public Vector3 GetVector3(BlackboardKey key)
        {
            return Get(_vector3Values, key);
        }

        public void Set(BlackboardKey key, Vector3 value)
        {
            _vector3Values[key] = value;
        }

        public bool TryGet(BlackboardKey key, out GameObject value, GameObject defaultValue = null)
        {
            return TryGet(_gameObjectValues, key, out value, defaultValue);
        }

        public GameObject GetGameObject(BlackboardKey key)
        {
            return Get(_gameObjectValues, key);
        }

        public void Set(BlackboardKey key, GameObject value)
        {
            _gameObjectValues[key] = value;
        }

        public bool TryGetGeneric<T>(BlackboardKey key, out T value, T defaultValue)
        {
            if (_genericValues.ContainsKey(key))
            {
                value = (T)_genericValues[key];
                return true;
            }

            value = defaultValue;
            return false;
        }

        public T GetGeneric<T>(BlackboardKey key)
        {
            if (!_genericValues.ContainsKey(key))
            {
                throw new System.ArgumentException($"Could not find value for {key} in GenericValues");
            }

            return (T)_genericValues[key];
        }

        public void SetGeneric<T>(BlackboardKey key, T value)
        {
            _genericValues[key] = value;
        }

        public bool TryGetStat(AIStat linkedStat, out float value, int defaultValue = 0)
        {
            if (_AiStatValues.ContainsKey(linkedStat))
            {
                value = _AiStatValues[linkedStat];
                return true;
            }

            value = defaultValue;
            return false;
        }

        public float GetStat(AIStat linkedStat)
        {
            if (!_AiStatValues.ContainsKey(linkedStat))
            {
                throw new ArgumentException($"Could not find value for {linkedStat.DisplayName} in AIStats");
            }

            return _AiStatValues[linkedStat];
        }

        public void SetStat(AIStat linkedStat, float value)
        {
            _AiStatValues[linkedStat] = value;
        }

        private T Get<T>(Dictionary<BlackboardKey, T> keySet, BlackboardKey key)
        {
            if (!keySet.ContainsKey(key))
            {
                throw new ArgumentException($"Could not find value for {key} in {typeof(T).Name} Values");
            }

            return keySet[key];
        }

        private bool TryGet<T>(Dictionary<BlackboardKey, T> keySet, BlackboardKey key, out T value, T defaultValue = default)
        {
            if (keySet.ContainsKey(key))
            {
                value = keySet[key];
                return true;
            }

            value = default;
            return false;
        }
    }
}