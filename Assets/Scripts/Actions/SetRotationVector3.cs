using UnityEngine;

public class SetRotationVector3 : MonoBehaviour
{
    [SerializeField] private bool _local = false;

    [SerializeField] private Vector3 _start;
    [SerializeField] private Vector3 _end;

    public void SetProgress(float progress)
    {
        Quaternion rotation = Quaternion.Euler(Vector3.Lerp(_start, _end, progress));
		if (_local)
        {
			transform.localRotation = rotation;
		}
		else
        {
			transform.rotation = rotation;
		}
	}
}
