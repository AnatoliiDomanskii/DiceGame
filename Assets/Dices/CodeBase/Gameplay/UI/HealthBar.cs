using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Unit _unit;
    private Slider _slider;
    private TextMeshProUGUI _textMeshPro;

    public Unit Unit { get { return _unit; } }

    private void Awake()
    {
        _slider = GetComponent<Slider>();
        _textMeshPro = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void UpdateValue()
    {
        _slider.value = _unit.Health;
        _textMeshPro.text = $"{_unit.Health}+{_unit.Armor}";
    }

    public void SetUnit(Unit unit)
    {
        _unit = unit;

        _slider.maxValue = _unit.MaxHealth;

        _unit.OnHealthChenged += UpdateValue;

        UpdateValue();
    }
}
