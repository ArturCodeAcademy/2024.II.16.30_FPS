using UnityEngine;

public class PausePanel : MonoBehaviour
{
	private void Start()
	{
		if (!PauseSystem.IsPaused)
			gameObject.SetActive(false);
	}

	public void Resume()
	{
		PauseSystem.Unpause();
	}
}
