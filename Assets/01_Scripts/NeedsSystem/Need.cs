using System;
using UnityEngine;

namespace Needs
{
    public class Need
    {
        const float MAX = 100f;
        float _ringAmount;
        float _totalRingAmount;
        float _coreAmount;
        float _regenSpeed = 20f;
        float _lastUseTime;

        public Need()
        {
            _totalRingAmount = 40f;
            _ringAmount = _totalRingAmount;
            _coreAmount = MAX;
        }

        public bool TryUseNeed(float useAmount)
        {
            if (_ringAmount >= useAmount)
            {
                _ringAmount -= useAmount;
                _lastUseTime = Time.time;
                Debug.Log("coreAmount:" + _coreAmount + "; ringAmount" + _ringAmount);
                return true;
            }
            else
            {
                useAmount -= _ringAmount;

                if (_coreAmount >= useAmount)
                {
                    _ringAmount = 0f;
                    _coreAmount -= useAmount;
                    _lastUseTime = Time.time;
                    Debug.Log("coreAmount:" + _coreAmount + "; ringAmount" + _ringAmount);
                    return true;
                }
                else
                {
                    Debug.Log("CANNOT USE! " + "coreAmount:" + _coreAmount + "; ringAmount" + _ringAmount);
                    return false;
                }
            }
        }

        public float GetRingNormalizedValue()
        {
            return _ringAmount / MAX;
        }

        public float GetTotalRingNormalizedValue()
        {
            return _totalRingAmount / MAX;
        }

        public float GetCoreNormalizedValue()
        {
            return _coreAmount / MAX;
        }

        public void Update()
        {
            if (CanRegen())
            {
                _ringAmount += _regenSpeed * Math.Max(GetCoreNormalizedValue(), 0.1f) * Time.deltaTime;
                _ringAmount = Mathf.Clamp(_ringAmount, 0f, _totalRingAmount);
            }
        }

        private bool CanRegen()
        {
            return Time.time >= _lastUseTime;
        }
    }
}
