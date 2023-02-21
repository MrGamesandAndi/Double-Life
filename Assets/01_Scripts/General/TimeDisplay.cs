using System;
using TMPro;
using UnityEngine;

namespace General
{
    public class TimeDisplay : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI _hourText;

        private void Update()
        {
            _hourText.text = DateTime.Now.ToString("hh:mm tt");
        }
    }
}