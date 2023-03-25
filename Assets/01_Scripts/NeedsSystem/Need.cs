using System;
using UnityEngine;

namespace Needs
{
    public class Need
    {
        public event EventHandler OnCoreUse;
        public event EventHandler OnNeedReset;

        const float MAX = 3f;
        float _ringAmount;
        float _totalRingAmount;
        float _coreAmount;

        public Need()
        {
            _totalRingAmount = 3f;
            _ringAmount = _totalRingAmount;
            _coreAmount = MAX;
        }

        public void UseNeed(float useAmount, float multiplier)
        {
            if (_ringAmount >= useAmount)
            {
                _ringAmount -= useAmount * Time.deltaTime * multiplier;
            }
            else
            {
                useAmount -= _ringAmount;

                if (_coreAmount >= useAmount)
                {
                    OnCoreUse?.Invoke(this, EventArgs.Empty);
                    _ringAmount = 0f;
                    _coreAmount -= useAmount * Time.deltaTime * multiplier;
                }
                else
                {
                    OnNeedReset?.Invoke(this, EventArgs.Empty);
                    ResetNeed();
                }
            }
        }

        public void SetNeed()
        {
            _ringAmount = 0;
        }

        public void ResetNeed()
        {
            _ringAmount = _totalRingAmount;
            _coreAmount = MAX;
        }
    }
}
