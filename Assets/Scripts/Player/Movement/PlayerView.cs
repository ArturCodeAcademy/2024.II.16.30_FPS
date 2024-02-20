using UnityEngine;

public class PlayerView : MonoBehaviour
{
    [SerializeField] private float _sensitivity = 10f;
    [SerializeField] private bool _invertX = false;
    [SerializeField] private bool _invertY = false;

    [Space(3)]
    [SerializeField] private Transform _player;

    private float _xRotation = 0f;

    private const float MAX_X_ROTATION = 90f;
    private const float MIN_X_ROTATION = -90f;

	private void Update()
	{
		Vector2 viewDelta = new (Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        _player.Rotate(Vector3.up * viewDelta.x * _sensitivity * (_invertX ? -1 : 1));

        _xRotation -= viewDelta.y * _sensitivity * (_invertY ? -1 : 1);
        _xRotation = Mathf.Clamp(_xRotation, MIN_X_ROTATION, MAX_X_ROTATION);
        transform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
	}
}
