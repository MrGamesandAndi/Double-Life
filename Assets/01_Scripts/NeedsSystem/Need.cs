using System;
using TraitSystem;
using UnityEngine;

namespace Needs
{
    public class Need
    {
        public event EventHandler OnCoreUse;

        const float MAX = 100f;
        float _ringAmount;
        float _totalRingAmount;
        float _coreAmount;

        public Need()
        {
            _totalRingAmount = 50f;
            _ringAmount = _totalRingAmount;
            _coreAmount = MAX;
        }

        public bool TryUseNeed(float useAmount, float multiplier)
        {
            if (_ringAmount >= useAmount)
            {
                _ringAmount -= useAmount * Time.deltaTime * multiplier;
                //Debug.Log("coreAmount:" + _coreAmount + "; ringAmount" + _ringAmount);
                return true;
            }
            else
            {
                useAmount -= _ringAmount;

                if (_coreAmount >= useAmount)
                {
                    OnCoreUse?.Invoke(this, EventArgs.Empty);
                    _ringAmount = 0f;
                    _coreAmount -= useAmount * Time.deltaTime * multiplier;
                    //Debug.Log("coreAmount:" + _coreAmount + "; ringAmount" + _ringAmount);
                    return true;
                }
                else
                {
                    //Debug.Log("CANNOT USE! " + "coreAmount:" + _coreAmount + "; ringAmount" + _ringAmount);
                    ResetNeed();
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

        public void ResetNeed()
        {
            _ringAmount = _totalRingAmount;
            _coreAmount = MAX;
        }
    }
}
