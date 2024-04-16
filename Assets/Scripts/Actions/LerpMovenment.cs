using UnityEngine;
using UnityEngine.Events;

public class LerpMovenment : MonoBehaviour
{
	public UnityEvent StartMovement;
	public UnityEvent EndMovement;

	public Vector3 FromPosition { get; private set; }
	public Vector3 ToPosition { get; private set; }

	[SerializeField] private Vector3[] _positions;
    [SerializeField, Min(0.1f)] private float _movementDuration = 1.0f;
	[SerializeField] private bool _loopMovement = true;
	[SerializeField] private bool _localPosition = true;
	[SerializeField] private bool _startOnAwake = true;

    private int _currentPositionIndex = 0;
    private bool _forward = true;

	private float _elapsedTime = 0.0f;

	private void Awake()
	{
		StartMovement ??= new UnityEvent();
		EndMovement ??= new UnityEvent();
	}

	private void Start()
    {
		if (_positions.Length < 2)
        {
			Debug.LogError("LerpMovenment: At least two positions are required.");
			return;
		}

		FromPosition = _positions[_currentPositionIndex];
		_elapsedTime = _movementDuration;

		if (_startOnAwake)
			MoveToNextPosition();
	}

	private void Update()
	{
		if (_elapsedTime >= _movementDuration)
			return;

		_elapsedTime += Time.deltaTime;

		Vector3 position = Vector3.Lerp(FromPosition, ToPosition, _elapsedTime / _movementDuration);
		if (_localPosition)
			transform.localPosition = position;
		else
			transform.position = position;

		if (_elapsedTime >= _movementDuration)
			EndMovement?.Invoke();
	}

	public void MoveToNextPosition()
	{
		_currentPositionIndex += _forward ? 1 : -1;

		if (_currentPositionIndex >= _positions.Length)
		{
			if (_loopMovement)
				_currentPositionIndex = 0;
			else
			{
				_currentPositionIndex = _positions.Length - 2;
				_forward = false;
			}
		}
		else if (_currentPositionIndex < 0)
		{
			_currentPositionIndex = 1;
			_forward = true;
		}

		FromPosition = _localPosition? transform.localPosition : transform.position;
		ToPosition = _positions[_currentPositionIndex];
		_elapsedTime = 0.0f;

		StartMovement?.Invoke();
	}

	public void MoveToSelectedPosition(int posIndex)
	{
		FromPosition = _localPosition ? transform.localPosition : transform.position;
		ToPosition = _positions[posIndex];
		_elapsedTime = 0.0f;
		_currentPositionIndex = posIndex;

		StartMovement?.Invoke();
	}

#if UNITY_EDITOR

	private void OnDrawGizmosSelected()
	{
		if (_positions is null or { Length: < 2 })
			return;

		Gizmos.color = Color.green;

		const float RADIUS = 0.1f;

		for (int i = 0; i < _positions.Length; i++)
		{
			var from = _localPosition ? transform.parent?.TransformPoint(_positions[i]) ?? _positions[i] : _positions[i];
			var to = _localPosition ? transform.parent?.TransformPoint(_positions[(i + 1) % _positions.Length]) ?? _positions[(i + 1) % _positions.Length] : _positions[(i + 1) % _positions.Length];
			Gizmos.DrawSphere(from, RADIUS);

			if (i < _positions.Length - 1 || _loopMovement)
				Gizmos.DrawLine(from, to);
		}
	}

#endif
}
