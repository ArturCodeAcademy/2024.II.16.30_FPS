using UnityEngine;
using UnityEngine.Events;

public class DefaultInteractable : MonoBehaviour, IInteractable
{
	public UnityEvent Interacted;

	[field: SerializeField] public bool Active { get; private set; } = true;

	private void Awake()
	{
		Interacted ??= new UnityEvent();
	}

	public void Interact()
	{
		if (!Active)
			return;

		Interacted?.Invoke();
	}

	public void SetActive(bool active)
	{
		Active = active;
	}
}
