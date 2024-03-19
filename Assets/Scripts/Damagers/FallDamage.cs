using UnityEngine;

public class FallDamage : MonoBehaviour
{
	[SerializeField, Min(0)] private float _damagePerVelocity = 10f;
	[SerializeField, Min(0)] private float _minVelocity = 10f;

	private CharacterControllerGravity _gravity;
	private Health _health;

	private void Awake()
	{
		_gravity = GetComponent<CharacterControllerGravity>();
		_health = GetComponent<Health>();
	
	}

	private void OnEnable()
	{
		_gravity.Land += OnLand;
	}

	private void OnDisable()
	{
		_gravity.Land -= OnLand;
	}

	private void OnLand(object sender, LandEventArgs args)
	{
		float velocity = Mathf.Abs(args.Velocity);
		if (velocity < _minVelocity)
			return;

		float damageValue = (velocity - _minVelocity) * _damagePerVelocity;
		_health.Damage(damageValue);
	}
}