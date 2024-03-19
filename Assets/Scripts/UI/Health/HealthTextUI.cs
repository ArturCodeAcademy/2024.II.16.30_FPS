using TMPro;
using UnityEngine;

public class HealthTextUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private string _format = "{0:0.0} / {1:0.0}";

    [Space]
    [SerializeField] private Health _health;

    private void OnEnable()
    {
		_health.OnHealthChanged += OnHealthChanged;
		OnHealthChanged(_health.Current, _health.Max);
	}

    private void OnDisable()
    {
		_health.OnHealthChanged -= OnHealthChanged;
	}

    private void OnHealthChanged(float current, float max)
    {
		_text.text = string.Format(_format, current, max, current / max);
	}
}
