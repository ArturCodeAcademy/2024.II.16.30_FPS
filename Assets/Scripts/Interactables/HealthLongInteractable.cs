using System;
using UnityEngine;
using UnityEngine.Events;

public class HealthLongInteractable : MonoBehaviour, ILongInteractable
{
	[Header("Interactable")]
	[SerializeField, Min(0.1f)] private float _healthPerSecond = 1f;
	[SerializeField, Min(0.1f)] private float _beginHealthAmount = 10f;

	[field: Header("Interactable")]
	[field: SerializeField]
	public UnityEvent<float> ProgressChangedUnityEvent { get; private set; }
	public event Action<float> ProgressChanged;

	public float Progress => 1 - _currentHealthAmount / _beginHealthAmount;
	public bool Active => true;

	[SerializeField] private UnityEvent _healthEnded;

	private float _currentHealthAmount;
	private Health _playerHealth;

	private void Awake()
	{
		ProgressChangedUnityEvent ??= new UnityEvent<float>();
		_currentHealthAmount = _beginHealthAmount;
	}

	private void Start()
	{
		_playerHealth = Player.Instance.Health;
	}

	public void Interact()
	{
		if (_currentHealthAmount <= 0 || _playerHealth.Current >= _playerHealth.Max)
			return;

		float healAmount = _healthPerSecond * Time.deltaTime;
		healAmount = Mathf.Min(healAmount, _currentHealthAmount);
		_playerHealth.Heal(healAmount);
		_currentHealthAmount -= healAmount;
		ProgressChanged?.Invoke(Progress);
		ProgressChangedUnityEvent.Invoke(Progress);

		if (_currentHealthAmount <= 0)
			_healthEnded.Invoke();
	}

	public void StopInteraction()
	{

	}
}
