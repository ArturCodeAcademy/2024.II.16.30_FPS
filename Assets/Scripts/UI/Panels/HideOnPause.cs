using UnityEngine;

public class HideOnPause : MonoBehaviour
{
	[SerializeField] private GameObject[] _objectsToHide;

	private void OnEnable()
	{
		PauseSystem.Paused += Hide;
		PauseSystem.Unpaused += Show;
	}

	private void OnDisable()
	{
		PauseSystem.Paused -= Hide;
		PauseSystem.Unpaused -= Show;
	}

	private void Hide()
	{
		foreach (var obj in _objectsToHide)
			obj.SetActive(false);
	}

	private void Show()
	{
		foreach (var obj in _objectsToHide)
			obj.SetActive(true);
	}
}
