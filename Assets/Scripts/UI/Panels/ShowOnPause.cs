using UnityEngine;

public class ShowOnPause : MonoBehaviour
{
	[SerializeField] private GameObject[] _objectsToShow;

	private void OnEnable()
	{
		PauseSystem.Paused += Show;
		PauseSystem.Unpaused += Hide;
	}

	private void OnDisable()
	{
		PauseSystem.Paused -= Show;
		PauseSystem.Unpaused -= Hide;
	}

	private void Hide()
	{
		foreach (var obj in _objectsToShow)
			obj.SetActive(false);
	}

	private void Show()
	{
		foreach (var obj in _objectsToShow)
			obj.SetActive(true);
	}
}