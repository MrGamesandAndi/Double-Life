using System;
using UnityEngine;

namespace Needs
{
    public class Need
    {
        public event EventHandler OnCoreUse;
        public event EventHandler OnNeedReset;

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

        public void UseNeed(float useAmount, float multiplier)
        {
            if (_ringAmount >= useAmount)
            {
                _ringAmount -= useAmount * Time.deltaTime * multiplier;
                //Debug.Log("coreAmount:" + _coreAmount + "; ringAmount" + _ringAmount);
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
                }
                else
                {
                    //Debug.Log("CANNOT USE! " + "coreAmount:" + _coreAmount + "; ringAmount" + _ringAmount);
                    OnNeedReset?.Invoke(this, EventArgs.Empty);
                    ResetNeed();
                }
            }
        }

        public void ResetNeed()
        {
            _ringAmount = _totalRingAmount;
            _coreAmount = MAX;
        }
    }
}
