using UnityEngine;

public class SetPosition : MonoBehaviour
{
    [SerializeField] private bool _local = false;

    [SerializeField] private Vector3 _start;
    [SerializeField] private Vector3 _end;

	public void SetProgress(float progress)
	{
		GetStartAndEnd(out Vector3 start, out Vector3 end);
		transform.position = Vector3.Lerp(start, end, progress);
	}

	private void GetStartAndEnd(out Vector3 start, out Vector3 end)
	{
		start = _local ? transform?.parent?.TransformPoint(_start) ?? _start : _start;
		end = _local ? transform?.parent?.TransformPoint(_end) ?? _end : _end;
	}

#if UNITY_EDITOR

	private void OnDrawGizmosSelected()
	{
		GetStartAndEnd(out Vector3 start, out Vector3 end);

		Gizmos.color = Color.white;
		Gizmos.DrawLine(start, end);
		Gizmos.color = Color.green;
		Gizmos.DrawSphere(start, 0.1f);
		Gizmos.color = Color.red;
		Gizmos.DrawSphere(end, 0.1f);
	}

#endif
}
