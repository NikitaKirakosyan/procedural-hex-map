using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
public class ColorToggle : MonoBehaviour
{
    [SerializeField] private Toggle _toggle;
    [SerializeField] private Image _background;
    [SerializeField] private Image _checkmark;

    private bool _isToggleInitialized;
    private int _index;
    private Action<int> _onSelected;
    private Color _color;


    public void Setup(int index, Action<int> onSelected, Color color, bool? isOn = null, ToggleGroup toggleGroup = null)
    {
        _index = index;
        _onSelected = onSelected;
        
        if(!_isToggleInitialized)
        {
            _isToggleInitialized = true;
            _toggle.onValueChanged.AddListener(OnToggleValueChanged);
        }

        if(isOn.HasValue)
        {
            _toggle.isOn = isOn.Value;
        }

        _toggle.group = toggleGroup;

        if(_color != color)
        {
            _color = color;
            _background.color = _color;
            _checkmark.color = _color;
        }
    }


    private void OnToggleValueChanged(bool value)
    {
        _onSelected?.Invoke(_index);
    }
}
