using System;
using UnityEngine;

namespace Needs
{
    public class Need
    {
        public event EventHandler OnCoreUse;
        public event EventHandler OnNeedReset;

        const float MAX = 1000f;
        float _ringAmount;
        float _totalRingAmount;
        float _coreAmount;

        public float RingAmount { get => _ringAmount; set => _ringAmount = value; }
        public float CoreAmount { get => _coreAmount; set => _coreAmount = value; }

        public Need()
        {
            _totalRingAmount = 1000f;
            _ringAmount = _totalRingAmount;
            _coreAmount = 500f;
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

        public void ResetNeed()
        {
            _ringAmount = _totalRingAmount;
            _coreAmount = 500f;
        }

        public void ManualActivation()
        {
            _ringAmount = 1f;
            _coreAmount = 500f;
        }
    }
}
