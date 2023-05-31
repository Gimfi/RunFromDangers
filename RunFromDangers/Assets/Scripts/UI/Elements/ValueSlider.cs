using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Elements
{
    public class ValueSlider : MonoBehaviour
    {
        public event Action<int> OnValueChanged;
        public int Value => _value;

        [SerializeField]
        private string _textMain;
        [SerializeField]
        private Vector2 _minMaxValues;
        [SerializeField]
        private Slider _slider;
        [SerializeField]
        private TMP_Text _text;

        private int _value;

        public void Initialize(int startValue)
        {
            _slider.minValue = _minMaxValues.x;
            _slider.maxValue = _minMaxValues.y;

            _slider.value = startValue;
        }

        private void Update()
        {
            int oldValue = _value;
            _value = (int)_slider.value;
            _text.text = $"{_textMain} {_value}";

            if (oldValue != _value)
            {
                OnValueChanged?.Invoke(_value);
            }
        }
    }
}