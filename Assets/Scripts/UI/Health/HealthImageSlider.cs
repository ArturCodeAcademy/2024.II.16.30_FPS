using UnityEngine;
using UnityEngine.UI;

public class HealthImageSlider : MonoBehaviour
{
	[SerializeField] private Image _fill;
	[SerializeField] private bool _useGradient = true;
	[SerializeField] private Gradient _gradient;

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
		_fill.fillAmount = current / max;

		if (_useGradient)
		{
			_fill.color = _gradient.Evaluate(_fill.fillAmount);
		}
	}
}
