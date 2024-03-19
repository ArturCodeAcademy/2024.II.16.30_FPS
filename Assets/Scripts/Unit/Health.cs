using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    public bool IsDead => Current <= 0;
    public float Percent => Current / Max;

    [field: SerializeField] public float Current { get; private set; }
    [field: SerializeField, Min(1)] public float Max { get; private set; }

    public event Action<float, float> OnHealthChanged;
    public event Action OnDeath;

    public float Heal(float amount)
    {
        if (amount <= 0)
			return 0;

		if (IsDead)
			return 0;

		float res = Mathf.Min(Max - Current, amount);
        Current += res;
        OnHealthChanged?.Invoke(Current, Max);
        return res;
	}

    public float Damage(float amount)
    {
        if (amount <= 0)
            return 0;

        if (IsDead)
			return 0;

        float res = Mathf.Min(Current, amount);
        Current -= res;
        OnHealthChanged?.Invoke(Current, Max);

        if (IsDead)
			OnDeath?.Invoke();

        return res;
    }

#if UNITY_EDITOR

    private void OnValidate()
    {
		if (Current > Max)
			Current = Max;
	}

#endif
}
